using System;
using System.Collections.Generic;

namespace NetCoreApi.Model
{
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public DateTime? CreateDate { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}
