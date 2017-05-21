namespace Drape
{
    [System.Serializable]
    public class MultistatData : BaseStatData
    {
        public string[] stats;
        public MultistatData(string code, string name, int value, string[] stats = null) : base(code, name, value)
        {
            if (stats == null) {
                stats = new string[0];
            }
            this.stats = stats;
        }
    }
}