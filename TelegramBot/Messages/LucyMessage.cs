using System;
using TelegramBot.Exchange;

namespace TelegramBot.Messages
{
	internal static class LucyMessage
	{
		public static string GetStart()
		{
			return @"Hi there.
					You can receive exchange for date.
					Try to type ""dd.MM.yyyy-USD"".
					Also you can type only ""/USD""
					if you want to get exchange for today.
					USD is like example, you can choose
					anyone currency => /help";
		}

		public static string GetHelp(string currencies)
		{
			return $"You can choose anyone currency:\n{currencies}";
		}

		public static string GetError(ErrorMessageType errorType)
		{
			var errorMessage = errorType switch
			{
				ErrorMessageType.InvalidDate => "The date is not valid.",
				ErrorMessageType.UnknownCurrency => "This currency is unknown.",
				ErrorMessageType.NotSupportedCurrency => "This currency is not supported in PB",
				ErrorMessageType.CommonError => "Your query is not valid.",
				_ => throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null)
			};

			return $"{errorMessage}\n" +
					@"Try, for example ""31.09.2021-PLN"" or just ""PLN""
					Click /help and choose an available currency.";
		}

		public static string GetExchange(CurrencyInfo currencyInfo)
		{
			return $"Date: {currencyInfo.Date}\n" +
					$"Exchange: {currencyInfo.Currency} - {currencyInfo.BaseCurrency}\n" +
					$"Sale: {currencyInfo.SaleRate}\n" +
					$"Purchase: {currencyInfo.PurchaseRate}";
		}
	}
}