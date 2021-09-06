using System;
using System.Linq;
using TelegramBot.Exchange;
using Xunit;

namespace TelegramBot.Tests
{
	public class ExchangerTests
	{
		private readonly DateTime _date;

		public ExchangerTests()
		{
			_date = DateTime.Parse("08/29/2021");
		}

		[Fact]
		public void CurrencyInfo_DateUSD_True()
		{
			const string currency = "USD";
			var exchanger = Exchanger.GetInstance(_date);

			var expected = new CurrencyInfo(
				"29.08.2021",
				"PB",
				"UAH",
				currency,
				27.1500000,
				26.7500000);

			var actual = exchanger.ListCurrencies.First(c => c.Currency == currency);

			Assert.Equal(expected, actual);
		}
	}
}