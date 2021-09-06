namespace TelegramBot.Messages
{
	internal class LucyStart : ILucyAnswer
	{
		public string GetAnswer()
		{
			return LucyMessage.GetStart();
		}
	}
}