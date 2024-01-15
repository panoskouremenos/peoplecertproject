namespace PC_backend.Utilities
{
	public static class ScoreUtility
	{
		public static int ConvertToPercentage(int score, int maxScore)
		{
			return (int)(((double)score / maxScore) * 100);
		}
	}
}
