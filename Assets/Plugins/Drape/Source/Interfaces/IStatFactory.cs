namespace Drape.Interfaces
{
    public interface IStatFactory<TStat, TData>
        where TStat : IStat
        where TData : BaseStatData
    {
        TStat Create(TData data);
    }
}