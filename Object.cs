using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chourbot_vacuum
{
    class Object
    {
        public (int, int) position;
        public String type;


        public Object()
        {
            type = "";
        }
        public Object((int,int) new_position, String new_type)
        {
            position = new_position;
            type = new_type;
        } 
        
    }
}
