using System;
using System.Globalization;
using System.Linq;
using TelegramBot.Exchange;

namespace TelegramBot.Messages
{
	internal sealed class LucyDate : LucyDateCurrencyBase
	{
		public LucyDate(string message)
		{
			SetAnswer(message ?? throw new ArgumentNullException(nameof(message)));
		}

		private static void SplitMessage(string message, out string dateString, out string currency)
		{
			var split = message.ToUpper().Split('-');
			dateString = split.First();
			currency = split.Last();
		}

		private void SetAnswer(string message)
		{
			SplitMessage(message, out var date, out var currency);

			if (IsDateValid(date))
				SetAnswerExchangeInfo(currency);
			else
				SetErrorAnswer(ErrorMessageType.InvalidDate);
		}

		private bool IsDateValid(string date)
		{
			var validationDate = DateTime.TryParse(date, Configuration.CultureInfo, DateTimeStyles.None,
				out var dateTime);

			Exchanger = Exchanger.GetInstance(dateTime);

			return validationDate;
		}
	}
}