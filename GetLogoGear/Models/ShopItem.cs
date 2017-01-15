using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GetLogoGear.Models
{
    public class ShopItem
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType ItemType { get; set; }
        public string Color { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }

        [ForeignKey("Storefront")]
        public int StoreID { get; set; }
        public virtual Storefront Storefront { get; set; }
    }

    public enum ItemType
    {

    }

    public enum Size
    {
        XS = 1, S, M, L, XL, XXL
    }



}