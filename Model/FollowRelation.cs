using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileBackend.Model
{
    public partial class FollowRelation
    {
        [Column("from", TypeName = "int(11)")]
        public int From { get; set; }
        [Column("to", TypeName = "int(11)")]
        public int To { get; set; }
        [Column("createDate", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }

        [ForeignKey("From")]
        [InverseProperty("FollowRelationFromNavigation")]
        public User FromNavigation { get; set; }
        [ForeignKey("To")]
        [InverseProperty("FollowRelationToNavigation")]
        public User ToNavigation { get; set; }
    }
}
