using System;
using TelegramBot.Exchange;

namespace TelegramBot.Messages
{
	internal sealed class LucyCurrency : LucyDateCurrencyBase
	{
		public LucyCurrency(string message)
		{
			SetAnswer(message ?? throw new ArgumentNullException(nameof(message)));
		}

		private void SetAnswer(string message)
		{
			var currency = message.Replace("/", string.Empty).ToUpper();
			Exchanger = Exchanger.GetInstance();

			SetAnswerExchangeInfo(currency);
		}
	}
}