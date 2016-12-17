namespace MVC_Project_Part2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu_List
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        public int ItemPrice { get; set; }

        [Required]
        [StringLength(100)]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(450)]
        public string LongDescription { get; set; }

        [Required]
        [StringLength(200)]
        public string IconImage { get; set; }

        [Required]
        [StringLength(20)]
        public string ItemName { get; set; }

        [StringLength(200)]
        public string ImagePath { get; set; }
    }
}
