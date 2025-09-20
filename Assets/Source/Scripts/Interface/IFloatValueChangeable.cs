namespace Interface
{
    public interface IFloatValueChangeable : IValueChangeable<float>
    {
        float MaxValue { get; }
    }
}