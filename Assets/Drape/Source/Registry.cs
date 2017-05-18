using System.Collections;
using System.Collections.Generic;

namespace Drape { 
    public class Registry
    {
        HashSet<Interfaces.IStat> stats;
        HashSet<Modifier> modifier;
        // skills
        // abilities

        public Registry(ILoader loader)
        {

        }


        public static Modifier GetModifier()
        {
            return null;
        }
    }
}