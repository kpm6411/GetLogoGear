using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetLogoGear.Models
{
    public class BaseItem
    {
        [Key]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Color> Colors { get; set; }
        public bool Sizes { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }

    public enum Sizes
    {
        XS = 1, S, M, L, XL, XXL
    }
}