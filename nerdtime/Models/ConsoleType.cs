using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nerdtime.Models
{
    public class ConsoleType
    {
        public ConsoleType()
        {

        }
        
        public int Id { get; set; }
        public String Name { get; set; }

        public virtual ICollection<GameConsoleType> GamesConsolesTypes { get; set; }
    }
}
