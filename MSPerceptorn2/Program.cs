using System;
namespace MSPerceptorn2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Perceptron perceptron = new Perceptron(new int[] {2, 3, 4, 5}, new double[] {1, 4}, x => x*2, (x, y) => GetRandomNumber(0, 1));
			List<double> res = perceptron.Execute();
			foreach (double d in res)
			{
				Console.WriteLine(d);
			}
		}

		public static double GetRandomNumber(double minimum, double maximum)
		{
			Random random = new Random();
			return random.NextDouble() * (maximum - minimum) + minimum;
		}

	}
}

