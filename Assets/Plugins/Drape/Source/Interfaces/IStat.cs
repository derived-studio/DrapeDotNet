namespace Drape.Interfaces
{
    public interface IStat
    {
        string ToJSON();
        string Code { get; }
        string Name { get; }
        float Value { get; }
    }
}