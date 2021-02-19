using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chourbot_vacuum
{
    class Node
    {
		private Node m_parent_node;
		public Node Parent_Node
    {
		get { return m_parent_node; }
		set { m_parent_node = value; }
    }

		// Liste d'actions pour arriver jusqu'à ce noeud.
		private List<string> m_action = new List<String>();
		public List<string> Action
		{
			get { return m_action; }
			set { m_action = value; }
		}

		private State m_state;
			public State State
		{
			get { return m_state; }
			set { m_state = value; }
		}

		// Coût résultant de la fonction heuristique dans la classe agent Vacuum
		private double m_path_cost = 100.0;
		public double Path_Cost
		{
			get { return m_path_cost; }
			set { m_path_cost = value; }
		}

		private int m_step_cost;
		public int Step_Cost
		{
			get { return m_step_cost; }
			set { m_step_cost = value; }
		}

		private int m_depth;
		public int Depth
		{
			get { return m_depth; }
			set { m_depth = value; }
		}

		public Node(Node parent_node)
        {
			m_parent_node = parent_node;
			m_state = new State(parent_node.State);
			m_depth = parent_node.m_depth + 1;
			foreach(String action in parent_node.Action)
			{
				m_action.Add(action);
			}
		}
		public Node(State a_state)
        {
			m_state = new State(a_state);
			m_depth = 0;
        }
	}
}
