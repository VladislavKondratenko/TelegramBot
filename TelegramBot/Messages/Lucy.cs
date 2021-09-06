using System;

namespace TelegramBot.Messages
{
	public class Lucy
	{
		private readonly ILucyAnswer _lucy;

		public string Answer => _lucy.GetAnswer();

		public Lucy(string message)
		{
			_lucy = ChooseLucy(message ?? throw new ArgumentNullException(nameof(message)));
		}

		private static ILucyAnswer ChooseLucy(string message)
		{
			if (Configuration.StartPattern.IsMatch(message))
				return new LucyStart();

			if (Configuration.HelpPattern.IsMatch(message))
				return new LucyHelp();

			if (Configuration.DatePattern.IsMatch(message))
				return new LucyDate(message);

			if (Configuration.CurrencyPattern.IsMatch(message))
				return new LucyCurrency(message);

			return new LucyError();
		}
	}
}