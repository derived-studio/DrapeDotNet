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


    public ModifierData outputMod;
    private Modifier _goldOutputModifier;

    public ModifierData capacityMod;
    private Modifier _goldCapacityModifier;
    

    void Start()
    {
        Registry registry = new Registry();

        Debug.Log("-------- init capacity");
        _goldCapacity = new Stat(new StatData("Gold capacity", startingCapacity), registry);
        Debug.Log("ref> " + _goldCapacity.ToJSON());
        Debug.Log("reg>: " + registry.Get<Stat>(_goldCapacity.Code).ToJSON());

        Debug.Log("-------- init output");
        _goldOutput = new Stat(new StatData("Gold output", startingOutput, new Dictionary<string, float>() {
            //{ _goldCapacity.Code, 0.5f }
        }), registry);
        Debug.Log(_goldOutput.ToJSON());

        Debug.Log("-------- init output");
        _gold = new Resource(new ResourceData("Gold", startingValue, _goldCapacity.Code, _goldOutput.Code), registry);
        Debug.Log(_gold.ToJSON());


        Debug.Log("--------- init modifiers");
        outputMod = new ModifierData("Faster mining", _goldOutput.Code, 0, 0, 0, 0.2f);
        _goldOutputModifier = new Modifier(outputMod, registry);
        Debug.Log(_goldOutputModifier.ToJSON());

        capacityMod = new ModifierData("Brokene storage", _goldCapacity.Code, -500, 0, 0, 0);
        _goldCapacityModifier = new Modifier(capacityMod, registry);
        Debug.Log(_goldCapacityModifier.ToJSON());


        /*
        Debug.Log("---------");
        TextAsset bindata = Resources.Load("stats") as TextAsset;

        Stat[] stats = Stat.FromJSONArray<StatData>(bindata.text);
        Debug.Log(stats[1].ToJSON());
        */
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
            _goldCapacity.AddModifier(_goldCapacityModifier);
        } else {
            _goldCapacity.ClearMods();
        }
    }
    
    public void ToggleOutput()
    {
        _goldOutputBoost = !_goldOutputBoost;
        if (_goldOutputBoost) {
            _goldOutput.AddModifier(_goldOutputModifier);
        } else {
            _goldOutput.ClearMods();
        }
    }

    public void EmptyuMine()
    {
        _gold.Dispose();
    }
}
