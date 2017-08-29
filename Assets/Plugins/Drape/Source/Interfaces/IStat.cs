namespace Drape.Interfaces
{
    public interface IStat
    {
        string ToString();
        string Code { get; }
        string Name { get; }
        float Value { get; }
    }
}