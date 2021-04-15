using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shop.Models
{
    public class Computer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
        public int Price { get; set; }
        public int Memory { get; set; }
        public string Colour { get; set; }
    }
}