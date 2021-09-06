using System.Linq;
using TelegramBot.Exchange;
using TelegramBot.Messages;
using Xunit;

namespace TelegramBot.Tests
{
	public class LucyTests
	{
		[Fact]
		public void GetAnswer_Start_MessageOnStart()
		{
			var expected = LucyMessage.GetStart();

			var actual = new Lucy("/start").Answer;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetAnswer_Help_MessageOnHelp()
		{
			var currencies = Exchanger.GetInstance().ListCurrencies
									.Where(c => c.SaleRate != 0 && c.PurchaseRate != 0)
									.Select(c => c.Currency)
									.Aggregate((s1, s2) => $"{s1}, /{s2}")
									.Insert(0, "/");

			var expected = LucyMessage.GetHelp(currencies);
			var actual = new Lucy("/help").Answer;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetAnswer_Date29082021_MessageOnDate()
		{
			var currencyInfo = new CurrencyInfo(
				"29.08.2021",
				"PB",
				"UAH",
				"USD",
				27.1500000,
				26.7500000);

			var expected = LucyMessage.GetExchange(currencyInfo);
			var actual = new Lucy("29.08.2021-USD").Answer;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetAnswer_InvalidDate_ErrorMessageOnDate()
		{
			var expected = LucyMessage.GetError(ErrorMessageType.InvalidDate);
			var actual = new Lucy("30.02.2021-USD").Answer;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetAnswer_AAA_UnknownCurrency()
		{
			var expected = LucyMessage.GetError(ErrorMessageType.UnknownCurrency);
			var actual = new Lucy("AAA").Answer;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetAnswer_AB_CommonError()
		{
			var expected = LucyMessage.GetError(ErrorMessageType.CommonError);
			var actual = new Lucy("AB").Answer;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetAnswer_ABCD_CommonError()
		{
			var expected = LucyMessage.GetError(ErrorMessageType.CommonError);
			var actual = new Lucy("ABCD").Answer;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetAnswer_GEL_NotSupportedPB()
		{
			var expected = LucyMessage.GetError(ErrorMessageType.NotSupportedCurrency);
			var actual = new Lucy("GEL").Answer;

			Assert.Equal(expected, actual);
		}
	}
}