using System;
namespace MSPerceptorn2
{
	public enum NeuronType
	{
		NeuronInput,
		NeuronLayer,
		NeuronOutput
	}

	public delegate double Function(double a);

	public class Neuron
	{
        public Dictionary<Neuron, double> previousNeurons = new Dictionary<Neuron, double>();
		public double Output;
		public NeuronType type;
		public double? input;
		public Function activateFunction;

		public Dictionary<Neuron, double> newPreviousNeurons = new Dictionary<Neuron, double>();
		public Dictionary<Neuron, double> dedout = new Dictionary<Neuron, double>();
		public Dictionary<Neuron, double> doutdnet = new Dictionary<Neuron, double>();

		public Neuron(double? input, Function function)
		{
			this.activateFunction = function;

			if (input == null)
			{
				this.type = NeuronType.NeuronLayer;
			} else
			{
				this.input = input;
				this.type = NeuronType.NeuronInput;
			}
		}

		public void Calculate()
		{
			double res = 0;

			if (this.type == NeuronType.NeuronInput)
			{
				res = input ?? default(double);
			} else
			{
			
				foreach (Neuron n in previousNeurons.Keys)
				{
					res += previousNeurons[n] * n.Output;
                    //Console.WriteLine(previousNeurons[n] + " : " + n.output);
                }
				
			}
			this.Output = activateFunction(res);
			
		}

		public void ApplyNewWeights()
		{
			this.previousNeurons = newPreviousNeurons;
			this.newPreviousNeurons = new Dictionary<Neuron, double>();
		}
	}
}

