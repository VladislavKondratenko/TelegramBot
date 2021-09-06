namespace TelegramBot.Exchange
{
	public record CurrencyInfo(
		string Date,
		string Bank,
		string BaseCurrency,
		string Currency,
		double SaleRate,
		double PurchaseRate);
}