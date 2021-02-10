using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chourbot_vacuum
{
    class Vacuum
    {

        Case vacuum_case;
        int jewelry_picked_up = 0;

        public Vacuum(Case vacuum_case)
        {
            Case vacuum_position = vacuum_case;
            vacuum_case.set_is_vacuum(true);
        }

        // Liste des capteurs
        List<Sensor> sensors = new List<Sensor>();

        public void move(Case new_case)
        {
            vacuum_case.set_is_vacuum(false);
            vacuum_case = new_case;
            new_case.set_is_vacuum(true);
        }

        // Aspire les objets de la case
        public void clean_case(Case a_case)
        {
            a_case.clean_jewelry();
            a_case.clean_dust();
        }
        // Ramasser le bijou de la case
        public void pick_up_jewelry()
        {
            vacuum_case.clean_jewelry();
            jewelry_picked_up++;
        }

        
    }
}
