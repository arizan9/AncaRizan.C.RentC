namespace AncaRizan.C.RentC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool Enabled { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }
    }
}
