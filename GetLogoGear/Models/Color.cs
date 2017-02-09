using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetLogoGear.Models
{
    public class Color
    {
        public int ColorID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<BaseItem> BaseItems { get; set; }
    }
}