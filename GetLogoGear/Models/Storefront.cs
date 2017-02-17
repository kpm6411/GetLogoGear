using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GetLogoGear.Models
{
    public class Storefront
    {
        [Key]
        public int StoreID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TimeCreated { get; set; }
        
       // public Markup Markup { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
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