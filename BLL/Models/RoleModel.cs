#nullable disable

using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class RoleModel
    {
        public Role Record { get; set; }

        [DisplayName("Role Name")]
        public string RoleName => Record.RoleName;
    }
}
