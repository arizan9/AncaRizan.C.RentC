namespace AncaRizan.C.RentC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Location")]
    public partial class Location
    {
        public int LocationID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
