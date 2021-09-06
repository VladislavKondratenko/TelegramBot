using System.Globalization;
using System.Text.RegularExpressions;

namespace TelegramBot
{
	internal static class Configuration
	{
		public const string BotToken = "1973682413:AAHEdIJKCNGPPKid_Q402ojc86QWRojBK0A";
		public static CultureInfo CultureInfo => new("ru-ru");
		public static Regex CurrencyPattern => new(@"^/?([\w]{3}$)");
		public static Regex DatePattern => new(@"^([0-3]?[0-9]).([0-3]?[0-9]).((?:[0-9]{2})?[0-9]{2})-(\w{3})$");
		public static Regex HelpPattern => new(@"/help");

		public static Regex StartPattern => new(@"/start");
	}
}