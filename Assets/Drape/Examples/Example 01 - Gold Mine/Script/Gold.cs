using Drape;

public class Gold : Resource
{
    public Gold(float value, GoldCapacity capacity, GoldOutput output) : base("Gold", value , capacity, output)
    {
    }

    public class GoldCapacity : Resource.Capacity
    {
        public GoldCapacity(int baseValue) : base("GoldCapacity", baseValue)
        {
        }
    }

    public class GoldOutput : Resource.Output
    {
        public GoldOutput(int baseValue) : base("GoldOutput", baseValue)
        {
        }
    }
}