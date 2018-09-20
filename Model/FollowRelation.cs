using System;
using System.Collections.Generic;

namespace NetCoreApi.Model
{
    public partial class FollowRelation
    {
        public int From { get; set; }
        public int To { get; set; }
        public DateTime? CreateDate { get; set; }

        public User FromNavigation { get; set; }
        public User ToNavigation { get; set; }
    }
}
