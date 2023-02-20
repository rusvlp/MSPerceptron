using System;
using System.Net.Mime;

namespace MSPerceptorn2
{
	public class Program
	{
		public static void Main(string[] args)
		{

			Console.WriteLine("Welcome to image recognition neural network app. \nPlease locate dataset folder: ");
			string datasetFolder = Console.ReadLine() ?? "none";
			Console.WriteLine("Please enter number of variations of each number");
			int numberOfVars = Int32.Parse(Console.ReadLine() ?? "none");
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < numberOfVars; j++)
				{
					MediaTypeNames.Image img;
				}
			}

		}

		public static void Test()
		{
			Perceptron perceptron = new Perceptron(new int[] {2, 2, 3}, new double[] {0.5, 0.10, 0.3333}, x => activationFunction(x), (x, y) => GetRandomNumber(0, 1));
			List<double> res = perceptron.Execute();
			foreach (double d in res)
			{
				Console.WriteLine(d);
			}

			BackPropagation bp = new BackPropagation(new List<double>(){0.5, 0.99, 0.01}, perceptron, x => DerAF(x));
			for (int i = 0; i < 300; i++)
			{
				bp.Execute();
			}
		}
		
		public static double GetRandomNumber(double minimum, double maximum)
		{
			Random random = new Random();
			return random.NextDouble() * (maximum - minimum) + minimum;
		}

		public static double activationFunction(double x)
		{
			return 2 / (1 + Math.Pow(Math.E, -2 * x)) - 1;
		}

		public static double DerAF(double x)
		{
			return 4 * Math.Pow(Math.E, -2 * x) / ((1 + Math.Pow(Math.E, -2 * x)) * (1 + Math.Pow(Math.E, -2 * x)));
		}

		
	}
}

