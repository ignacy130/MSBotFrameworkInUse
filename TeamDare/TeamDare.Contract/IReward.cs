namespace TeamDare.Contract
{
    public interface IReward : IEntity
    {
        string Title { get; set; }
        int Value { get; set; }

    }
}