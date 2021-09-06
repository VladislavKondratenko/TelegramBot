using System.Linq;
using TelegramBot.Exchange;

namespace TelegramBot.Messages
{
	internal abstract class LucyDateCurrencyBase : ILucyAnswer
	{
		private string _answer;

		protected Exchanger Exchanger { get; set; }

		public string GetAnswer()
		{
			return _answer;
		}

		protected void SetAnswerExchangeInfo(string currency)
		{
			if (IsCurrencyInvalid(currency))
				return;

			var currencyInfo = GetCurrencyInfo(currency);

			_answer = LucyMessage.GetExchange(currencyInfo);
		}

		protected void SetErrorAnswer(ErrorMessageType errorMessage)
		{
			_answer = new LucyError(errorMessage).GetAnswer();
		}

		private CurrencyInfo GetCurrencyInfo(string currency)
		{
			return Exchanger.ListCurrencies
							.First(c => c.Currency == currency);
		}

		private bool IsCurrencyInvalid(string currency)
		{
			if (ContainsThere(currency) is not true)
			{
				SetErrorAnswer(ErrorMessageType.UnknownCurrency);

				return true;
			}

			if (IsThereDataAbout(currency))
				return false;

			SetErrorAnswer(ErrorMessageType.NotSupportedCurrency);

			return true;
		}

		private bool IsThereDataAbout(string currency)
		{
			var currencyInfo = Exchanger.ListCurrencies
										.First(c => c.Currency == currency);

			return currencyInfo.SaleRate != 0 && currencyInfo.PurchaseRate != 0;
		}

		private bool ContainsThere(string currency)
		{
			return Exchanger.ListCurrencies
							.Select(c => c.Currency)
							.Contains(currency);
		}
	}
}