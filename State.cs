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

        public Case agent_case;

        // La position de l'agent / Position initiale de l'agent
        public (int, int) agent_position = (0, 0);

        // Les positions de chacunes des poussières dans une liste
        public List<(int, int)> dust_position = new List<(int, int)>();

        // Les positions de chacuns des bijoux dans une liste
        public List<(int, int)> jewelry_position = new List<(int, int)>();

        // 
        /*public bool marked = false;*/


        public State()
        {

        }

        public State(Case a_case)
        {
            agent_case = new Case(a_case.cell);
        }

        // recopie de l'objet
        public State(State a_state)
        {
            /*agent_case = new Case(a_state.agent_position.Item1, a_state.agent_position.Item1);*/
            agent_case = new Case(a_state.agent_case.cell);

            agent_position = a_state.agent_position;
/*            agent_position.Item1 = a_state.agent_position.Item1;
            agent_position.Item2 = a_state.agent_position.Item2;*/

            foreach ((int,int) dust in a_state.dust_position)
            {
                dust_position.Add(dust);
            }

            foreach ((int, int) jewlery in a_state.jewelry_position)
            {
                jewelry_position.Add(jewlery);
            }
        }


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
