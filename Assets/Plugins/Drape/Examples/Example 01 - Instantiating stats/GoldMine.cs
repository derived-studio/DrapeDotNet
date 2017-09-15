using UnityEngine;
using UnityEngine.UI;

namespace Drape.Eamples.Example1
{
    public class GoldMine : MonoBehaviour
    {
		public Text goldLabel;
		public Text outputLabel;
		public Text capacityLabel;

		private int startingValue = 0;
        private int startingOutput = 50;
        private int startingCapacity = 1000;

        private Resource _gold;
        private Stat _goldCapacity;
        private Stat _goldOutput;

        private Modifier _goldOutputModifier;
        private Modifier _goldCapacityModifier;

        void Start()
        {
            // ----------------------
            // Initializing registry
            // ----------------------
            // Registry instance is a dependecy required by stat constructor methods.
            // It stores references to all created stats. You can have one or more registries in the game.
            // In single player game you most likely need one registry.
            // In multiplayer game or rpg with evolving NPCs you would need registry per character.
            Registry registry = new Registry();

            // --------------------------
            // Initializing simple stats
            // --------------------------
            // We are creating gold capacity and gold output stats and store references for later.

            // First Let's create gold capacity data.
            // Stats are created using stat data object, so let's create one.
            // Stat data constructor requires stat code, name and starting value.
            StatData goldCapacityData = new StatData("gold-capacity", "Gold capacity", startingCapacity);
            // Now we pass stat data into stat constructor
            _goldCapacity = new Stat(goldCapacityData, registry);
            // Once created stat object has ToJSON() method useful for logging or serialization.
            Debug.Log("[stat] " + _goldCapacity.ToJSON());

            // Next, let's create gold output stat
            // In most cases stat data reference is not needed so we can create object directly from the constructor
            _goldOutput = new Stat(new StatData("Gold output", startingOutput), registry);
            // Lets debug log it, this time using registry to access it
            Debug.Log("[stat] " + registry.Get<Stat>("gold-output").ToJSON());

            // ----------------------
            // Initializing resources
            // ----------------------
            // Resource type stat exposes method such us Dispose(ammount), Restore(ammount) and Update(deltaTime).
            // It is design to facitlitate managing resource-like stats.

            // Let's create gold resource stat that depends on above capacity and output.
            _gold = new Resource(new ResourceData("Gold", startingValue, _goldCapacity.Code, _goldOutput.Code), registry);
            Debug.Log("[resource] " + _gold.ToJSON());


            // ----------------------
            // Initializing modifiers
            // ----------------------
            // Modifier is another type of stat. It is used for dunamic modification of stat values.
            // Modifiers can have both, positive and negative vaues.

            // Lets create faster mining modifier that will improve total output value by 50%.
            ModifierData outputMod = new ModifierData("Faster mining", _goldOutput.Code, 0, 0, 0, 0.5f);
            _goldOutputModifier = new Modifier(outputMod, registry);
            Debug.Log("[modifier] " + _goldOutputModifier.ToJSON());

            // Lets create btoken storage modfier that will reduce mine capacity by 500 units.
            ModifierData capacityMod = new ModifierData("Brokene storage", _goldCapacity.Code, -500, 0, 0, 0);
            _goldCapacityModifier = new Modifier(capacityMod, registry);
            Debug.Log("[modifier] " + _goldCapacityModifier.ToJSON());
        }

        void Update()
        {
            // ----------------------
            // Updating resources
            // ----------------------
            // Stat.Update() method is a tickable method taking delta time as a parameter.
            // It will dynamically recalculate resource value based on its output.
            // If output stat has applied modifiers those will be included in calculcation.
            _gold.Update(Time.deltaTime);
            // lets output those values into text fields
            goldLabel.text = _gold.Value.ToString("0.000");
            outputLabel.text = _goldOutput.Value.ToString("0.000");
            capacityLabel.text = _goldCapacity.Value.ToString("0.000");
        }


        // ----------------------
        // Applying modifiers
        // ----------------------

        /// <summary>
        /// Toggles capacity modifier on capacity stat
        /// </summary>
        public void ToggleCapacity()
        {
            if (_goldCapacity.ModifierCount == 0) {
                // We can add modifier to a stat with AddModifier method
                Debug.Log("ToggleCapacity() Adding capacity modifier");
                _goldCapacity.AddModifier(_goldCapacityModifier);
            } else {
                // We can remove all modifiers with ClearMods() method
                Debug.Log("ToggleCapacity() Removing modifiers");
                _goldCapacity.ClearMods();
            }
        }

        /// <summary>
        /// Toggles output modifier on output stat
        /// </summary>
        public void ToggleOutput()
        {
            if (_goldOutput.ModifierCount == 0) {
                Debug.Log("ToggleOutput() Adding output modifier");
                _goldOutput.AddModifier(_goldOutputModifier);
            } else {
                Debug.Log("ToggleOutput() Removing modifiers");
                _goldOutput.ClearMods();
            }
        }


        /// <summary>
        /// Empties mine, resetting gold stat value to 0
        /// </summary>
        public void EmptyuMine()
        {
            Debug.Log("Emptying mine");
            _gold.DisposeAll();
        }
    }

}