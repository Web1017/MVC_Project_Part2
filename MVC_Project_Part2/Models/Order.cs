namespace MVC_Project_Part2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Required!")]
        public string Username { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Required!")]
        public string Firstname { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Required!")]
        public string Lastname { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Required!")]
        public string Address { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "Required!")]
        public string City { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "Required!")]
        public string State { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Required!")]
        public string Postalcode { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Required!")]
        public string Country { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "Required!")]
        public string Phone { get; set; }

        [StringLength(20)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Required!")]
        public string Email { get; set; }

        public decimal? Total { get; set; }

        public DateTime? Orderdate { get; set; }
    }
}
