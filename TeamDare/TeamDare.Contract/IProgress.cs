namespace TeamDare.Contract
{
    public interface IProgress : IEntity
    {
        bool IsCompleted { get; set; }
        int PercentageCompleted { get; set; }
    }
}