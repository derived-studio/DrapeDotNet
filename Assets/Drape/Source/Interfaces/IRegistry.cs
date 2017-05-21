namespace Drape.Interfaces
{
    public interface IRegistry
    {
        bool Add<T>(T stat) where T : IStat;
        T[] Get<T>() where T : IStat;
        T Get<T>(string code) where T : IStat;
        IStat Get(string code);
    }
}