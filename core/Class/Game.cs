using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using core.Class;

namespace core
{
    public class Game   
    {
        public Game()
        {
        }
        public int Id { get; set; }
        public String Name { get; set; }

        [ForeignKey("ConsoleId")]
        public virtual ConsoleType Console { get; set; }

    }
}

