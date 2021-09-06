using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace TelegramBot.Exchange.JsonParsing
{
	public class JsonParser
	{
		private readonly PrivatExchange _privat;
		private List<CurrencyInfo> _listCurrencies;

		public IEnumerable<CurrencyInfo> ListCurrencies => _listCurrencies;

		public JsonParser(string json)
		{
			if (json is null)
				throw new ArgumentNullException(nameof(json));

			_privat = JsonSerializer.Deserialize<PrivatExchange>(json);
			SetListCurrencies();
		}

		private void SetListCurrencies()
		{
			_listCurrencies = _privat.exchangeRate
									.Select(CreateCurrencyInfo)
									.ToList();
		}

		private CurrencyInfo CreateCurrencyInfo(ExchangeRate exchangeInfo)
		{
			return new CurrencyInfo(
				_privat.date,
				_privat.bank,
				_privat.baseCurrencyLit,
				exchangeInfo.currency,
				exchangeInfo.saleRate,
				exchangeInfo.purchaseRate);
		}
	}
}