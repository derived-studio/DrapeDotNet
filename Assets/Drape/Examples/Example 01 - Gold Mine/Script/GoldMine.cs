using UnityEngine;
using UnityEngine.UI;
using Drape;
using Drape.TinyJson;

public class GoldMine : MonoBehaviour
{
    Resource _gold;

    private int startingValue = 0;
    private int startingOutput = 50;
    private int startingCapacity = 1000;

    public Text goldLabel;
    public Text outputLabel;
    public Text capacityLabel;

    // We are using local ModifierProps to expose Modifier.ModifierProps properties in inspector
    // and use them to construct concreete Modifier.ModifierProps object    
    public ModifierProps outputModifier = new ModifierProps(0, 0, 0, 1);
    public ModifierProps capacityModifier = new ModifierProps(-500, 0, 0, 0);

    Stat _goldCapacity;
    Stat _goldOutput;
    bool _goldCapacityLimit = false;
    bool _goldOutputBoost = false;
    Modifier _goldOutputModifier;
    Modifier _goldCapacityModifier;

    void Start()
    {
        _goldCapacity = new Stat("Gold capacity", startingCapacity);
        _goldOutput = new Stat("Gold output", startingOutput);
        _gold = new Resource("Gold", startingValue, _goldCapacity, _goldOutput);

        // list serialized gold
        Debug.Log(_gold.ToJson());
    }

    void Update()
    {
        _gold.Update(Time.deltaTime);
        goldLabel.text = _gold.Value.ToString("0.000");
        outputLabel.text = _goldOutput.Value.ToString("0.000");
        capacityLabel.text = _goldCapacity.Value.ToString("0.000");
    }

    public void ToggleCapacity()
    {
        _goldCapacityLimit = !_goldCapacityLimit;
        if (_goldCapacityLimit) {
            _goldCapacityModifier = CreateModifier("Gold capacity modifier", _goldCapacity, capacityModifier);
            _goldCapacity.AddMod(_goldCapacityModifier);
        } else {
            _goldCapacity.ClearMods();
        }
    }
    
    public void ToggleOutput()
    {
        _goldOutputBoost = !_goldOutputBoost;
        if (_goldOutputBoost) {
            _goldOutputModifier = CreateModifier("Gold output boost", _goldOutput, outputModifier);
            _goldOutput.AddMod(_goldOutputModifier);
        } else {
            _goldOutput.ClearMods();
        }
    }

    public void EmptyuMine()
    {
        _gold.Dispose();
    }


    private Modifier CreateModifier(string name, Stat stat, ModifierProps props)
    {
        return new Modifier(name, stat, new Modifier.ModifierProps(props.rawFlat, props.rawFactor, props.finalFlat, props.finalFactor));
    }

    [System.Serializable]
    public struct ModifierProps
    {
        public int rawFlat;
        public float rawFactor;
        public int finalFlat;
        public float finalFactor;

        public ModifierProps(int rawFlat, float rawFactor, int finalFlat, float finalFactor)
        {
            this.rawFlat = rawFlat;
            this.rawFactor = rawFactor;
            this.finalFlat = finalFlat;
            this.finalFactor = finalFactor;
        }
    }
}
