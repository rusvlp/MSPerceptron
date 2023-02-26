
namespace MSPerceptorn2;

public class PerceptronTeacher
{
    public Perceptron Perceptron;
  //public BackPropagation BackPropagation;
    public List<DatasetElement> DatasetElements;
    public BackPropagation.DerActivationFunction daf;
    private static Random rng = new Random();
    private BackPropagation bp = new BackPropagation();
    
    
    public PerceptronTeacher(Perceptron p, List<DatasetElement> de, BackPropagation.DerActivationFunction daf)
    {
        this.Perceptron = p;
        this.daf = daf;
        this.DatasetElements = de;
        this.bp.perceptron = Perceptron;
        this.bp.daf = daf;
    }

    public void Teach(int numberOfIterations)
    {
        for (int i = 0; i < numberOfIterations; i++)
        {
            // Shuffle(DatasetElements);
            foreach (DatasetElement de in DatasetElements)
            {
                for (int j = 0; j < this.Perceptron.neurons[0].Count; j++)
                {
                    this.Perceptron.neurons[0][j].input = (double)de.data[j];
                    //       Console.WriteLine((double)de.data[j]);
                }

                //     Console.WriteLine("__________________________________________________");
                bp.target = de.expectedOutput;

                Console.WriteLine(this.Perceptron.neurons[1][0]
                    .previousNeurons[this.Perceptron.neurons[1][0].previousNeurons.Keys.ElementAt(0)]);
                this.Perceptron.Execute();
                //   Console.WriteLine("Iteration: " + i);
                bp.Execute();

            }
        }
    }

    public void NewTeach()
    {
        
    }
    
    public static void Shuffle<T>(IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }
}