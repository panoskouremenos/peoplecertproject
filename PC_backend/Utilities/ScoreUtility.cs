namespace PC_backend.Utilities
{
	//Only use for this is to calculate the user's score in a test %
	public static class ScoreUtility
	{
		public static int ConvertToPercentage(int score, int maxScore)
		{
			return (int)(((double)score / maxScore) * 100);
		}
	}
}
