using System;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using TelegramBot;

var bot = new TelegramBotClient(Configuration.BotToken);
var me = await bot.GetMeAsync();

bot.StartReceiving(new DefaultUpdateHandler(Handlers.HandleUpdateAsync, Handlers.HandleError));

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();