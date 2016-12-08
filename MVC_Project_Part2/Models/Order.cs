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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ScaffoldColumn(false)]
        public int OrderID { get; set; }

        [ScaffoldColumn(false)]
        [StringLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(20)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(20)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(150)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(15)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(15)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [StringLength(10)]
        public string Postalcode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(20)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email ID is required")]
        [StringLength(20)]
        public string Email { get; set; }

        public decimal? Total { get; set; }

        public DateTime? Orderdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
