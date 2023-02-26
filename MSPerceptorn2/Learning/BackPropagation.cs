namespace MSPerceptorn2;

public class BackPropagation
{
    public List<double> target;
    public Perceptron perceptron;
    public double etotal { get; set; }
    public DerActivationFunction daf;
    public double teachSpeed = 3;
    public delegate double DerActivationFunction(double x);

    public BackPropagation()
    {
        
    }
    
    public BackPropagation(List<double> target, Perceptron p, DerActivationFunction daf)
    {
        this.daf = daf;
        this.target = target;
        this.perceptron = p;
    }

    public void CalculateETotal()
    {
        double error = 0;
        for (int i = 0; i < target.Count; i++)
        {
            error += 0.5d * Math.Pow((target[i] - perceptron.neurons[perceptron.neurons.Count - 1][i].Output), 2);
          
        }
        this.etotal = error;
    }

    public void Execute()
    {
        CalculateETotal();
        Console.WriteLine("Total Error iz: " + this.etotal);
        for (int i = perceptron.neurons.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < perceptron.neurons[i].Count; j++)
            {
                if (i == perceptron.neurons.Count - 1)
                {
                    CalculateErrorPerOutput(perceptron.neurons[i][j], target[j]);
                    //string newWeights = string.Join(" ",CalculateErrorPerOutput(perceptron.neurons[i][j], target[j]));
                    //Console.WriteLine(newWeights);
                }
                else
                {

                    CalculateErrorPerLayer(perceptron.neurons[i][j]);
                    //string newWeights = string.Join(" ", CalculateErrorPerLayer(perceptron.neurons[i][j]));
                    //Console.WriteLine(newWeights);
                }
            }
        }

        for (int i = perceptron.neurons.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < perceptron.neurons[i].Count; j++)
            {
                perceptron.neurons[i][j].ApplyNewWeights();
            }
        }

        perceptron.Execute();
       // string results = "Results is: " + string.Join(", ", perceptron.GetResults());
       // Console.WriteLine(results);

    }

    public List<double> CalculateErrorPerLayer(Neuron n)
    {
        List<double> newWeights = new List<double>();
        double step1 = 0;
        foreach (Neuron i in n.dedout.Keys)
        {
            double dednet = n.dedout[i] * n.doutdnet[i];
            double dnetdout = i.previousNeurons[n];
            double dedout = dednet * dnetdout;
            step1 += dedout;
        } 
        double net = n.previousNeurons.Keys.Sum(i => i.input * n.previousNeurons[i]) ?? default(double);
        double step2 = this.daf(net);
        foreach (Neuron i in n.previousNeurons.Keys)
        {
            i.dedout[n] = step1;
            i.doutdnet[n] = step2;
            newWeights.Add(n.previousNeurons[i] - teachSpeed * (i.Output * step1 * step2));
            n.newPreviousNeurons[i] = n.previousNeurons[i] - (i.Output * step1 * step2);
        }
        return newWeights;
    }
    
    public List<double> CalculateErrorPerOutput(Neuron n, double toCompare)
    {
        List<double> newWeights = new List<double>();
        double step1 = 2d * 0.5d * (toCompare - n.Output) * -1;
        double net = n.previousNeurons.Keys.Sum(i => i.input * n.previousNeurons[i]) ?? default(double);
        double step2 = this.daf(net);
        foreach (Neuron i in n.previousNeurons.Keys)
        {
            i.dedout[n] = step1;
            i.doutdnet[n] = step2;
            newWeights.Add(n.previousNeurons[i] - teachSpeed * (i.Output * step1 * step2));
            n.newPreviousNeurons[i] = n.previousNeurons[i] - (i.Output * step1 * step2);
        }

        return newWeights;

    }
}