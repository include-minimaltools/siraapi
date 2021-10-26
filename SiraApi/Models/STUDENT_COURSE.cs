namespace SiraApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class STUDENT_COURSE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ID_STUDENT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string ID_COURSE { get; set; }

        
        [Column(Order = 2)]
        [StringLength(20)]
        public string USER_CREATE { get; set; }

        
        [Column(Order = 3)]
        public DateTime DATE_CREATE { get; set; }

        [StringLength(20)]
        public string USER_UPDATE { get; set; }

        public DateTime? DATE_UPDATE { get; set; }

        public virtual COURSE COURSE { get; set; }

        public virtual STUDENT STUDENT { get; set; }
    }
}
