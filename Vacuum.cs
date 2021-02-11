using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chourbot_vacuum
{
    class Vacuum
    {

        // Liste des capteurs
        List<Sensor> sensors = new List<Sensor>();

        Case vacuum_case;
        int jewelry_picked_up = 0;

        // Problem
        Problem problem = new Problem();

        public Vacuum(Case new_vacuum_case)
        {
            vacuum_case = new_vacuum_case;
            vacuum_case.set_is_vacuum(true);
        }

        public Case get_vacuum_case()
        {
            return vacuum_case;
        }


        public void move(Case new_case)
        {
            vacuum_case.set_is_vacuum(false);
            vacuum_case = new_case;
            new_case.set_is_vacuum(true);
        }

        // Aspire les objets de la case
        public void clean_case()
        {
            vacuum_case.clean_jewelry();
            vacuum_case.clean_dust();
        }
        // Ramasser le bijou de la case
        public void pick_up_jewelry()
        {
            vacuum_case.clean_jewelry();
            jewelry_picked_up++;
        }

        // Deuxième fonction move qui renvoie la nouvelle position de l'agent
        public (int,int) move_agent((int,int) vacuum_position ,String new_action)
        {
            int new_x = 0;
            int new_y = 0;

            if (new_action == "haut")
            {
                // new_y = get_vacuum_case().y - 1;
                new_y = vacuum_position.Item2 - 1;
            }
            else if (new_action == "bas")
            {
                // new_y = get_vacuum_case().y + 1;
                new_y = vacuum_position.Item1 + 1;
            }
            else if (new_action == "gauche")
            {
                // new_x = get_vacuum_case().x - 1;
                new_x = vacuum_position.Item1 - 1;
            }
            else if (new_action == "droite")
            {
                // new_x = get_vacuum_case().x + 1;
                new_x = vacuum_position.Item1 + 1;
            }
            return (new_x, new_y);
        }

        // Algorithme d'exploration

        // Renvoie une liste d'état associé avec une action
        private List<(String, State)> successor(State actual_state)
        {
            List<(String, State)> set_actions_states = new List<(String, State)>();
            // Listes d'états (new_states) après exécution de l'actions du même index
            List<String> new_actions = new List<String>();

            foreach (String action in problem.actions)
            {
                if ((actual_state.agent_position.Item2 <= 0) && (action == "haut"))
                {
                    continue;
                }
                else if ((actual_state.agent_position.Item2 >= 5) && (action == "bas"))
                {
                    continue;
                }
                else if ((actual_state.agent_position.Item1 <= 0) && (action == "gauche"))
                {
                    continue;
                }
                else if ((actual_state.agent_position.Item1 >= 5) && (action == "droite"))
                {
                    continue;
                }
                State new_state = new State();
                // new_state.grid_state = actual_state.grid_state;
                new_state.dust_position = new List<(int, int)>(actual_state.dust_position);
                new_state.jewelry_position = new List<(int, int)>(actual_state.jewelry_position);

                // move agent (vacuum)
                new_state.agent_position = move_agent(actual_state.agent_position,action);

                set_actions_states.Add((action, new_state));
            }
            return set_actions_states;
        }


        // Fonction d'expansion de l'arbre
        public List<Node> Expand(Node node, Problem probem)
        {
            List<Node> successors = new List<Node>();
            foreach ((String, State) action_result in successor(node.State))
            {
                Node s = new Node();
                s.Parent_Node = node;
                s.Action = action_result.Item1;
                s.State = action_result.Item2;
                s.Path_Cost = node.Path_Cost + 1;
                successors.Add(s);
            }
            return successors;
        }
    }
}
