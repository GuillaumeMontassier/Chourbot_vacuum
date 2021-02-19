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

        public Case agent_case;

        public (int, int) agent_position = (0, 0);

        public State()
        {

        }

        public State(Case a_case)
        {
            agent_case = new Case(a_case.cell);
        }

        public State(State a_state)
        {
            agent_case = new Case(a_state.agent_case.cell);

            agent_position = a_state.agent_position;
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
