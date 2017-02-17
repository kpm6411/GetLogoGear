﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GetLogoGear.Models
{
    public class ShopItem
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public bool HasSizes { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Color> Colors { get; set; }
        
        [ForeignKey("Logo")]
        public int LogoID { get; set; }
        public virtual Logo Logo { get; set; }
    }

    public enum Size
    {
        XS = 1, S, M, L, XL, XXL
    }



}