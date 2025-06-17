namespace Interface
{
    public interface IExecutableState
    {
        bool IsExecuted { get; }

        void SetExecuted(bool isExecuted);
    }
}