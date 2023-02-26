using System;
using System.Text.Json;
using System.Xml.Serialization;

namespace MSPerceptorn2
{
	public delegate double MyRandom(double from, double to);
	[Serializable]
	public class Perceptron
	{
		public Perceptron()
		{
			
		}
		public List<List<Neuron>> neurons = new List<List<Neuron>>();

		public Perceptron(Perceptron p)
		{
			
		}

		public void SetInputs(List<double> inputs)
		{
			if (inputs.Count != neurons[0].Count)
			{
				throw new ArgumentOutOfRangeException();
			}

			for (int i = 0; i < inputs.Count; i++)
			{
				neurons[0][i].input = inputs[i];
			}
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

		public void Load(string path)
		{
			string text = File.ReadAllText(path);
				
			string[] strings = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			List<double> weights = new List<double>();
			foreach (string s in strings)
			{
				weights.Add(double.Parse(s));
			}

			for (int i = 0, gc = 0; i < this.neurons.Count(); i++)
			{
				for (int j = 0; j < this.neurons[i].Count(); j++)
				{
					foreach (Neuron n in this.neurons[i][j].previousNeurons.Keys)
					{
						this.neurons[i][j].previousNeurons[n] = weights[gc];
						gc++;
					}
				}
			}
		}
		
		public void Save(string path)
		{
		
			/*XmlSerializer xml = new XmlSerializer(typeof(Perceptron));

			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
			{
				xml.Serialize(fs, this);
			} */
			string toWrite = "";
			foreach (List<Neuron> ln in neurons)
			{
				foreach (Neuron n in ln)
				{
					foreach (Neuron d in n.previousNeurons.Keys)
					{
						toWrite += n.previousNeurons[d] + " ";
						
					}
				}
			}
			Console.WriteLine(toWrite);
			File.WriteAllText(path, toWrite);
			
		}
		
		/*public async void SaveWeights(string path)
		{
			List<double> weights = new List<double>();
			foreach (List<Neuron> ln in neurons)
			{
				foreach (Neuron n in ln)
				{
					foreach (Neuron weight in n.previousNeurons.Keys)
				}
			}
		}*/
	} 
}

