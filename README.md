
# Drape - Data-driven game stat modeling framework

## How it works

### Stat types

* Stat (simple stat)
* Modfier
* Resource
* Multistat

### Stat and modifiers

```
stat components
---------------
B - base value
R - raw modifier
F - final modifier

modifier types
---------------
f - flat
m - multiplier

raw value formula
-----------------
R = (B + Rf) * Rm
Rf = Rf(a) + Rf(l) + Rf(g) + … + Rf(x)
Rm = Rm(a) + Rm(l) + Rm(g) + … + Rm(y)

final value formula
-------------------
F = (R + Ff) * Fm
Ff = Ff(a) + Ff(l) + Ff(g) + …. + Ff(x)
Fm = 1 + Fm(a) + Ff(l) + Ff(g) + … + Ff(x)
```

<!-- 
Opt 2
Fm = (1 + Fm(a)) * (1 + Ff(l)) * (1 * Ff(g)) * … * (1 + Ff(x))
-->

## Serialization

### Json
JSON serialization supported with [SaladLab fork](https://github.com/SaladLab/Json.Net.Unity3D) of [Newtonsoft Json.NET](https://www.newtonsoft.com/json). You can get latest DLL file from [here](https://github.com/SaladLab/Json.Net.Unity3D/releases).

Private setter serialization supported with custom contract resolver method as described in [Daniel's blog](https://danielwertheim.se/json-net-private-setters/).

### Yaml
Yaml serialization supported with [YamlDotNet](https://github.com/aaubry/YamlDotNet). Get latest release dll from [here](https://ci.appveyor.com/project/aaubry/yamldotnet/build/4.2.2-pre0425/artifacts).

## Known issues

 - `BaseStatData.ToJSON()` method only serializes public properties and doesn't allow to serialize memebers.
 - Is `BaseStatData` needed? Can `Stat` be serialized directly?
 - Applying global modfier to multiple stats of same type not supported. Eg. improved melee damage should apply to all melee damage type stats: sword damage, axe damage, etc.
 - What is the reason of using  final stat modfier (F)? Is it needed at all?  