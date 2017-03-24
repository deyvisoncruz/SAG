using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace nerdtime.Models
{
    public class GameConsoleType
    {
        

        [Key, Column(Order = 0)]
        public int GamesId { get; set; }
       /// [ForeignKey("GamesId")]
        public virtual Game Games { get; set; }

        [Key, Column(Order = 1)]
        public int ConsoleTypesId { get; set; }
        [ForeignKey("ConsoleTypesId")]
        public virtual ConsoleType ConsoleTypes { get; set; }

        [Range(0,100)]
        public int NumberCopys { get; set; } = 0;
        public int Viewer { get; set; } = 1;
    }
}