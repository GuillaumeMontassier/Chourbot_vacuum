﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chourbot_vacuum
{
    class Problem
    {
        // état initial
/*        public State initial_State;*/

        // Actions possibles
        public List<String> actions = new List<String>(new string[] { "up", "down", "left", "right" });

        // Fonction de Succession successorFN

        // Test de l'objectif, ici grille sans poussière

        // Le coût du chemin

/*        public Problem(State a_state)
        {
            initial_State = new State(a_state);
        }*/
    }
}
