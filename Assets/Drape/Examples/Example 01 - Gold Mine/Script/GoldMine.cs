using UnityEngine;
using UnityEngine.UI;
using Drape;
using System.Collections.Generic;

public class GoldMine : MonoBehaviour
{
    Resource _gold;
     
    private int startingValue = 0;
    private int startingOutput = 50;
    private int startingCapacity = 1000;

    public Text goldLabel;
    public Text outputLabel;
    public Text capacityLabel;

    Stat _goldCapacity;
    Stat _goldOutput;
    bool _goldCapacityLimit = false;
    bool _goldOutputBoost = false;

    public ModifierData outputMod = new ModifierData("gold-output-mod", "Faster mining", "gold-capacity", 0, 0, 0, 0.2f);
    public ModifierData capacityMod = new ModifierData("gold-cap-mod", "Brokene storage", "gold-output", -500, 0, 0, 0);
    private Modifier _goldOutputModifier;
    private Modifier _goldCapacityModifier;

    void Start()
    {
        _goldCapacity = new Stat("Gold capacity", startingCapacity);

        Dictionary<Drape.Interfaces.IStat, float> goldDeps = new Dictionary<Drape.Interfaces.IStat, float>();
        goldDeps.Add(_goldCapacity, 0.5f);
        _goldOutput = new Stat("Gold output", startingOutput, goldDeps);

        _gold = new Resource("Gold", startingValue, _goldCapacity, _goldOutput);

        // list serialized gold
        Debug.Log(_gold.ToJSON());
        Debug.Log(_goldOutput.ToJSON());

        Debug.Log("---------");
        Registry registry = new Registry();
        TextAsset bindata = Resources.Load("stats") as TextAsset;

        Stat[] stats = Stat.FromJSONArray<StatData>(bindata.text);
        Debug.Log(stats[1].ToJSON());

        Debug.Log("---------");
        Modifier mod = new Modifier("Faster mining", _goldOutput, 0, 1, 0, 1.2f);
        Debug.Log(mod.ToJSON());

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
            _goldCapacityModifier = CreateModifier(capacityMod, _goldCapacity);
            _goldCapacity.AddModifier(_goldCapacityModifier);
        } else {
            _goldCapacity.ClearMods();
        }
    }
    
    public void ToggleOutput()
    {
        _goldOutputBoost = !_goldOutputBoost;
        if (_goldOutputBoost) {
            _goldOutputModifier = CreateModifier(outputMod, _goldOutput);
            _goldOutput.AddModifier(_goldOutputModifier);
        } else {
            _goldOutput.ClearMods();
        }
    }

    public void EmptyuMine()
    {
        _gold.Dispose();
    }

    private Modifier CreateModifier(ModifierData props, Stat stat)
    {
        return new Modifier(props.name, stat, props.rawFlat, props.rawFactor, props.finalFlat, props.finalFactor);
    }
}
