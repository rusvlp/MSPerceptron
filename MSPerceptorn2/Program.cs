using System;
using System.Net.Mime;
using MSPerceptorn2.Students;

namespace MSPerceptorn2
{
	public class Program
	{

		public static int[] numberOfNeurons = new int[] { 5, 10, 3 };
		public static void Main(string[] args)
		{

			Students();
			//	p.Save("C:\\Users\\Vlad\\1.txt");
		}

		public static void Students()
		{
			int choose = -1;
			while (choose != 0)
			{
				Console.WriteLine("Welcome to Perceptron!");
				Console.WriteLine("1 - Teach perceptron");
				Console.WriteLine("2 - Specify a path to saved weights");
				Console.WriteLine("0 - Exit");
				choose = int.Parse(Console.ReadLine());

				if (choose == 1)
				{
					TeachStudents();
				}

				if (choose == 2)
				{
					LoadStudents();
					
				}
				else
				{
					Console.WriteLine("Please enter a correct number");
				}

			}
			

		}

		public static void LoadStudents()
		{
			Console.WriteLine("Please enter path to saved weights");
			string path = Console.ReadLine();
			
			
			Console.WriteLine("Please enter input values");
			double[] inputs = new double[numberOfNeurons[0]];
			for (int i = 0; i < inputs.Count(); i++)
			{
				inputs[i] = double.Parse(Console.ReadLine());
			}
			
			Perceptron p = new Perceptron(numberOfNeurons, inputs, d => activationFunction(d), (x, y) => GetRandomNumber(x, y));
			p.Load(path);
			
			p.Execute();
			string result = String.Join(" ", p.GetResults());
			Console.WriteLine(result);
			
		}
		
		public static void TeachStudents()
		{
			
			List<Student> students = new List<Student>();
			students.Add(new Student(new double[]{1, 8, 0.99, 0.99, 1}, new List<double>(){0.99, 0.01, 0.01}));
			students.Add(new Student(new double[]{5, 7, 0.75, 0.83, 1}, new List<double>(){0.6, 0.2, 0.1}));
			students.Add(new Student(new double[]{3, 8, 0.85, 0.93, 1}, new List<double>(){0.8, 0.1, 0.05}));
			students.Add(new Student(new double[]{10, 5, 0.5, 0.43, 0}, new List<double>(){0.3, 0.5, 0.2}));
			students.Add(new Student(new double[]{15, 8, 0.1, 0.5, 0}, new List<double>(){0.5, 0.5, 0.1}));
			students.Add(new Student(new double[]{7, 3, 0.4, 0.6, 1}, new List<double>(){0.3, 0.6, 0.2}));
			students.Add(new Student(new double[]{0, 8, 0.99, 0.83, 0}, new List<double>(){0.7, 0.2, 0.1}));
			students.Add(new Student(new double[]{2, 7, 0.4, 0.3, 0}, new List<double>(){0.6, 0.3, 0.15}));
			
			
			//	PerceptronTeacher.Shuffle(students);
			Perceptron p = new Perceptron(numberOfNeurons, students[0].parameters, x => activationFunction(x),
				(from, to) => GetRandomNumber(from, to));
			BackPropagation bp = new BackPropagation(students[0].outputs, p, x => DerAF(x));
			for (int j = 0; j < 100; j++)
			{
				foreach (Student s in students)
				{
					bp.target = s.outputs;
					p.SetInputs(new List<double>(s.parameters));
					p.Execute();
					for (int i = 0; i < 100; i++)
					{
						
						bp.Execute();
					}
				}
			}

			Console.WriteLine("Please enter input values");
			List<double> inputs = new List<double>();
			for (int i = 0; i < p.neurons[0].Count; i++)
			{
				inputs.Add(double.Parse(Console.ReadLine()));
			}
			
			p.SetInputs(inputs);
			p.Execute();
			Console.WriteLine("Would you like to save weights? [y/n]");
			string choose = Console.ReadLine();
			
			string result = String.Join(" ", p.GetResults());
			Console.WriteLine(result);
			
			if (choose == "y")
			{
				Console.WriteLine("Please specify a path");
				string path = Console.ReadLine();
				p.Save("path" );
			}

			
		}
		
		public static void Numbers2()
		{
			Perceptron p = new Perceptron(new[] { 64, 64, 10 }, new double [] { 1, 2 }, x => activationFunction(x),
				(x, y) => GetRandomNumber(x, y));
			
		}
		
		public static void numbers()
		{
			List<DatasetElement> delements = new List<DatasetElement>();
			
			//filling dataset
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					delements.Add(new DatasetElement(File.ReadAllBytes("C:\\Users\\Vlad\\Desktop\\MSPerceptron\\mydataset\\" + i + "\\" + j +".bmp"), fillExpected(i, 10)));
				}
			}
			
			//converting byte into double

			double[] doubles = new double[delements[0].data.Length];
			for (int i = 0; i < doubles.Length; i++)
			{
				doubles[i] = (double)delements[0].data[i];
					
			}
			
			// creating Perceptron
			Perceptron p = new Perceptron(new[] { delements[0].data.Length, 100, 10 }, doubles,
				x => activationFunction(x), (x, y) => GetRandomNumber(x, y));
			PerceptronTeacher pt = new PerceptronTeacher(p, delements, x => DerAF(x));
			pt.Teach(3000);
		}
		
		private static List<double> fillExpected(int number, int max)
		{
			List<double> toRet = new List<double>();
			for (int i = 0; i < max; i++)
			{
				if (i == number)
				{
					toRet.Add(0.99d);
				}
				else
				{
					toRet.Add(0.01d);
				}

				
			}
			return toRet;
		}
		
		private static void ExecuteProgram()
		{
			/*Console.WriteLine("Welcome to image recognition neural network app. \nPlease locate dataset folder: ");
			string datasetFolder = Console.ReadLine() ?? "none";
			Console.WriteLine("Please enter number of variations of each number");
			int numberOfVars = Int32.Parse(Console.ReadLine() ?? "none");
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < numberOfVars; j++)
				{
				}
			} */
			List<List<byte[]>> dataset = new List<List<byte[]>>();
			for (int i = 0; i < 10; i++)
			{
				dataset.Add(new List<byte[]>());
				for (int j = 0; j < 10; j++)
				{
					dataset[i].Add(File.ReadAllBytes("C:\\Users\\Vlad\\Desktop\\MSPerceptron\\dataset\\" + i + "_" +j + ".bmp"));
				}
			}
			
		}

		public static void Test()
		{
			Perceptron perceptron = new Perceptron(new int[] {3, 2, 3}, new double[] {1, 2 ,3}, x => activationFunction(x), (x, y) => GetRandomNumber(0, 1));
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

			perceptron.neurons[0][0].input = 3d;
			perceptron.neurons[0][1].input = 32d;
			perceptron.neurons[0][2].input = 11d;

			bp.target = new List<double>() { 0.1, 0.3, 0.99 };
			for (int i = 0; i < 300; i++)
			{
				bp.Execute();
			}
		}
		
		public static double GetRandomNumber(double minimum, double maximum)
		{
			/*Random random = new Random();
			return random.NextDouble() * (maximum - minimum) + minimum; */
			return 0.5d;
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

