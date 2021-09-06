using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBot.Exchange.JsonParsing;

namespace TelegramBot.Exchange
{
	public class Exchanger
	{
		private static readonly ConcurrentDictionary<DateTime, Exchanger> _library = new();
		private JsonParser _jsonParser;

		public IEnumerable<CurrencyInfo> ListCurrencies => _jsonParser.ListCurrencies;

		private Exchanger(DateTime date)
		{
			SetJsonParserAsync(date).Wait();
		}

		public static Exchanger GetInstance()
		{
			return GetInstance(DateTime.Now);
		}

		public static Exchanger GetInstance(DateTime dateTime)
		{
			var date = CorrectDate(dateTime);

			if (_library.TryGetValue(date, out var exchanger))
				return exchanger;

			exchanger = new Exchanger(date);
			_library.TryAdd(date, exchanger);

			return exchanger;
		}

		private static DateTime CorrectDate(DateTime date)
		{
			var noon = DateTime.Today.AddHours(12);

			if (date > DateTime.Today && date < noon)
				return DateTime.Today.AddDays(-1);

			return date >= noon ? DateTime.Today : date;
		}

		private static async Task<string> GetJsonAsync(DateTime date)
		{
			using (var http = new HttpClient())
				return await http.GetStringAsync(GetUri(date));
		}

		private static string GetUri(DateTime dateTime)
		{
			var date = dateTime.ToString(Configuration.DateFormat);

			return $"{Configuration.UriWithoutDate}{date}";
		}

		private async Task SetJsonParserAsync(DateTime date)
		{
			_jsonParser = new JsonParser(await GetJsonAsync(date));
		}
	}
}