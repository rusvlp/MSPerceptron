using System;
namespace MSPerceptorn2
{
	public delegate double MyRandom(double from, double to);
	public class Perceptron
	{
		public List<List<Neuron>> neurons = new List<List<Neuron>>();

		public Perceptron(Perceptron p)
		{
			
		}
		public Perceptron(int[] numberOfNeurons, double[] input, Function func, MyRandom rand)
		{
			for (int i = 0; i<numberOfNeurons.Length; i++)
			{
				//Console.WriteLine(numberOfNeurons[i]);
				neurons.Add(new List<Neuron>());
				for (int j = 0; j < numberOfNeurons[i]; j++)
				{
					if (i == 0)
					{
						neurons[i].Add(new Neuron(input[j], func));
					
                    } else
					{
						neurons[i].Add(new Neuron(null, func));
					}
						
				}
                fillRandom(rand);
            }
		}

		public void fillRandom(MyRandom rand)
		{
			for (int i = 0; i<this.neurons.Count-1; i++)
			{
				for (int j = 0; j < this.neurons[i].Count; j++)
				{
					
					for (int k = 0; k < this.neurons[i+1].Count; k++)
					{
						neurons[i + 1][k].previousNeurons[neurons[i][j]] = rand(0, 1);
						
					}
				}
			}
		}


		public List<double> Execute()
		{
			List<double> res = new List<double>();
			for (int i = 0; i<this.neurons.Count; i++)
			{
				for (int j = 0; j < this.neurons[i].Count; j++)
				{
					neurons[i][j].Calculate();
					if (i == this.neurons.Count - 1)
					{
						res.Add(neurons[i][j].Output);
					}
				}
			}
			return res;
		}

		public List<double> GetResults()
		{
			List<double> toRet = new List<double>();
			for (int i = 0; i < neurons[^1].Count; i++)
			{
				toRet.Add(neurons[^1][i].Output);
			}

			return toRet;
		}
	}
}

