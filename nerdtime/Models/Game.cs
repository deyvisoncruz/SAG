using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace nerdtime.Models
{
    public class Game
    {
        public Game()
        {
           
        }
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }


        public virtual ICollection<GameConsoleType> GamesConsolesTypes { get; set; }

        // public int ConsoleTypesId { get; set; }
        //public virtual List<ConsoleType> Consoles { get; set; }


        public int CategorysId { get; set; }
        [ForeignKey("CategorysId")]
        public virtual GameCategory Categories { get; set; }

        public String Cover { get; set; }
        public byte[] CoverIByteData { get; set; }
        public byte[] Img1 { get; set; }

        public String Img2 { get; set; }

        public String Img3 { get; set; }

        public String Video { get; set; }

        public String Synopsis { get; set; }

        public DateTime Date_Launch { get; set; }
        
        public String Language { get; set; }

        public String Gamers { get; set; }


    }
}