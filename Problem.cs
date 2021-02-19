using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chourbot_vacuum
{
    class Problem
    {
        // Position initial de l'aspirateur
        public int x = 0;
        public int y = 0;

        // Actions possibles
        public List<String> actions = new List<String>(new string[] { "up", "down", "left", "right" });
    }
}
