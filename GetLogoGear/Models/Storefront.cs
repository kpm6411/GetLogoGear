using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GetLogoGear.Models
{
    public class Storefront
    {
        public int StoreID { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public DateTime TimeCreated { get; set; }
        public Markup Markup { get; set; }
        public virtual ICollection<ShopItem> ShopItems { get; set; }

    }

    public enum Markup
    {
        [Description("20%")]
        Twenty = 1,
        [Description("30%")]
        Thirty,
        [Description("40%")]
        Forty
    }
}