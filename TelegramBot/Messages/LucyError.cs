namespace TelegramBot.Messages
{
	internal class LucyError : ILucyAnswer
	{
		private readonly string _answer;

		public LucyError() : this(ErrorMessageType.CommonError)
		{
		}

		public LucyError(ErrorMessageType errorMessage)
		{
			_answer = LucyMessage.GetError(errorMessage);
		}

		public string GetAnswer()
		{
			return _answer;
		}
	}
}