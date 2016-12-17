namespace MVC_CartDemo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}
