using System.Collections.Generic;

namespace TelegramBot.Exchange.JsonParsing
{
	internal record PrivatExchange(
		string date,
		string bank,
		int baseCurrency,
		string baseCurrencyLit,
		List<ExchangeRate> exchangeRate);
}