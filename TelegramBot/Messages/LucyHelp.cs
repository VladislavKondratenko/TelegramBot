using System.Linq;
using TelegramBot.Exchange;

namespace TelegramBot.Messages
{
	internal class LucyHelp : ILucyAnswer
	{
		public string GetAnswer()
		{
			var currencies = GetCurrencies();

			return LucyMessage.GetHelp(currencies);
		}

		private static string GetCurrencies()
		{
			return Exchanger.GetInstance().ListCurrencies
							.Where(e => e.SaleRate != 0 && e.PurchaseRate != 0)
							.Select(c => c.Currency)
							.Aggregate((s1, s2) => $"{s1}, /{s2}")
							.Insert(0, "/");
		}
	}
}