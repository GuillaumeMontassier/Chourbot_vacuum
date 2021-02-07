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
        private bool jewelry = false;
        private bool dust = false;
        private String case_name = "";
        public Button button = new Button();

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
            if ((jewelry == true) && (dust== true))
            {
                button.Text = "jewelry dust";
            }
            else if (jewelry && !dust)
            {
                button.Text = "jewelry";
            }
            else if (!jewelry && dust)
            {
                button.Text = "dust";
            }
            else
            {
                button.Text = "";
            }
        }
    }
}
