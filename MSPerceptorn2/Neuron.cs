using System;
namespace MSPerceptorn2
{
	public enum NeuronType
	{
		NEURON_INPUT,
		NEURON_LAYER,
		NEURON_OUTPUT
	}

	public delegate double Function(double a);

	public class Neuron
	{
        public Dictionary<Neuron, double> previousNeurons = new Dictionary<Neuron, double>();
		public double output;
		public NeuronType type;
		public double? input;
		public Function activateFunction;


		public Neuron(double? input, Function function)
		{
			this.activateFunction = function;

			if (input == null)
			{
				this.type = NeuronType.NEURON_LAYER;
			} else
			{
				this.input = input;
				this.type = NeuronType.NEURON_INPUT;
			}
		}

		public void Calculate()
		{
			double res = 0;

			if (this.type == NeuronType.NEURON_INPUT)
			{
				res = input ?? default(double);
			} else
			{
			
				foreach (Neuron n in previousNeurons.Keys)
				{
					res += previousNeurons[n] * n.output;
                    //Console.WriteLine(previousNeurons[n] + " : " + n.output);
                }
				
			}
			this.output = activateFunction(res);
			
		}
	}
}

