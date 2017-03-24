using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nerdtime.Models
{
    public class CheckboxViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public bool Checked { get; set; }
        public int NumberCopys { get; set; }
        public bool Viewer { get; set; }

    }
}