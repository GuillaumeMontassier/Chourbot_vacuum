using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chourbot_vacuum
{
    public class Case
    {
        // Position
       /* public int x, y;*/

        public bool is_there_an_object = true;
        private bool jewelry = false;
        private bool dust = false;
        private bool is_vacuum = false;

        public bool marked = false;

        public DataGridViewCell cell;

        public Case(DataGridViewCell new_cell)
        {
            cell = new_cell;
        }

        /*        public Case(DataGridViewCell new_cell, int new_x, int new_y)
                {
                    cell = new_cell;
                    x = new_x;
                    y = new_y;
                }*/


        /*        public void set_position(int new_x, int new_y)
                {
                    x = new_x;
                    y = new_y;
                }*/

        public bool get_is_vacuum()
        {
            return is_vacuum;
        }
        public void set_is_vacuum(bool value_is_vacuum)
        {
            is_vacuum = value_is_vacuum;
            case_text();

        }

        public bool get_jewelry_status()
        {
            return jewelry;
        }
        public void spawn_jewelry()
        {
            jewelry = true;
            case_text();
        }
        public void clean_jewelry()
        {
            jewelry = false;
            case_text();
        }

        public bool get_dust_status()
        {
            return dust;
        }
        public void spawn_dust()
        {
            dust = true;
            case_text();
        }
        public void clean_dust()
        {
            dust = false;
            case_text();
        }

        public void case_text()
        {
            if ((jewelry == true) && (dust== true) && (is_vacuum == true))
            {
                cell.Value = "jewelry dust vacuum";
            }
            else if ((jewelry == true) && (dust == false) && (is_vacuum == false))
            {
                cell.Value = "jewelry";
            }
            else if ((jewelry == false) && (dust == true) && (is_vacuum == false))
            {
                cell.Value = "dust";
            }
            else if ((jewelry == false) && (dust == true) && (is_vacuum == true))
            {
                cell.Value = "dust vacuum";
            }
            else if ((jewelry == true)&& (dust == false) && (is_vacuum == true))
            {
                cell.Value = "jewelry vacuum";
            }
            else if((jewelry == false) && (dust == false) && (is_vacuum == true))
            {
                cell.Value = "vacuum";
            }
            else
            {
                cell.Value = "";
            }
        }
    }
}
