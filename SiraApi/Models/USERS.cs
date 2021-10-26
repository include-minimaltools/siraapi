namespace SiraApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USERS
    {
        [Key]
        [StringLength(20)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(20)]
        public string PASSWORD { get; set; }

        [StringLength(5)]
        public string ROLE { get; set; }

        [Required]
        [StringLength(20)]
        public string USER_CREATE { get; set; }

        public DateTime DATE_CREATE { get; set; }

        [StringLength(20)]
        public string USER_UPDATE { get; set; }

        public DateTime? DATE_UPDATE { get; set; }

        public virtual ROLE ROLE1 { get; set; }
    }
}
