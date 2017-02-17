using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GetLogoGear.Models
{
    public class Logo
    {
        [Key]
        public int LogoID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Image { get; set; }

        [ForeignKey("Storefront")]
        public int StoreID { get; set; }
        public virtual Storefront Storefront { get; set; }

        public virtual ICollection<ShopItem> ShopItems { get; set; }
    }
}