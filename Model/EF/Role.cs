namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rolde")]
    public class Role
    {
        [Key]
        [StringLength(50)]
        public string ID { set; get; }
        [StringLength(50)]
        public string Name { set; get; }

    }
}
