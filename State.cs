using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chourbot_vacuum
{
    class State
    {
        // La grille à l'état correspondant
        // public DataGridView grid_state = new DataGridView();

        // La position de l'agent / Position initiale de l'agent
        public (int, int) agent_position = (0, 0);

        // Les positions de chacunes des poussières dans une liste
        public List<(int, int)> dust_position = new List<(int, int)>();

        // Les positions de chacuns des bijoux dans une liste
        public List<(int, int)> jewelry_position = new List<(int, int)>();

        public (int, int) get_position()
        {
            return agent_position;
        }
        public void set_agent_position(int x, int y)
        {
            agent_position.Item1 = x;
            agent_position.Item2 = y;
        }
    }
}
