namespace TelegramBot.Exchange.JsonParsing
{
	internal record ExchangeRate(
		string baseCurrency,
		string currency,
		double saleRateNB,
		double purchaseRateNB,
		double saleRate,
		double purchaseRate);
}