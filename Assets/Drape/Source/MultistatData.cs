namespace Drape
{
    [System.Serializable]
    public class MultistatData : BaseStatData
    {
        public string[] stats;
        public MultistatData(string code, string name, int value, string[] stats = null) : base(code, name, value)
        {
            this.stats = stats;
        }
    }
}   