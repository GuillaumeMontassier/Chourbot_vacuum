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

		private string m_action;
		public string Action
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

		private string m_path_cost;
		public string Path_Cost
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
	}
}
