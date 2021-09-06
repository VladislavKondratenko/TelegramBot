using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Messages;

namespace TelegramBot
{
	public static class Handlers
	{
		public static Task HandleError(ITelegramBotClient botClient, Exception exception,
			CancellationToken cancellationToken)
		{
			var errorMessage = exception switch
			{
				ApiRequestException apiRequestException =>
					$"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
				_ => exception.ToString()
			};

			Console.WriteLine(errorMessage);

			return Task.CompletedTask;
		}

		public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
			CancellationToken cancellationToken)
		{
			var handler = update.Type switch
			{
				UpdateType.Message => BotOnMessageReceived(botClient, update.Message),
				UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage),
				_ => UnknownUpdateHandler(update)
			};

			try
			{
				await handler;
			}
			catch (Exception exception)
			{
				await HandleError(botClient, exception, cancellationToken);
			}
		}

		private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
		{
			Console.WriteLine($"Receive message type: {message.Type}");

			if (message.Type is not MessageType.Text)
				return;

			var response = await Task.Run(()=> new Lucy(message.Text));

			var sentMessage = await botClient.SendTextMessageAsync(message.Chat.Id, response.Answer);

			Console.WriteLine($"The message was sent with id: {sentMessage.MessageId}");
		}

		private static Task UnknownUpdateHandler(Update update)
		{
			Console.WriteLine($"Unknown update type: {update.Type}");

			return Task.CompletedTask;
		}
	}
}