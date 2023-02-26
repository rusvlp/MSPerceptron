namespace MSPerceptorn2;

public class DatasetElement
{
    public byte[] data;
    public List<double> expectedOutput;
    
    public DatasetElement(byte[] data, List<double> eout)
    {
        this.data = data;
        this.expectedOutput = eout;
    }
}

