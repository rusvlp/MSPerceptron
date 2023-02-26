namespace MSPerceptorn2.Students;

public class Student
{
    public double[] parameters;
    public List<double> outputs;

    public Student(double[] parameters, List<double> outputs)
    {
        this.parameters = parameters;
        this.outputs = outputs;
    }
}