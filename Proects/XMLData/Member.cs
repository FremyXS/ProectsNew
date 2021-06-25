using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class Member
    {
        public string Name { get; }
        public List<Role> Roles { get; private set; } = new List<Role>();
        public Member(string name)
        {
            Name = name;
        }
        public void AddRole(Role role)
        {
            Roles.Add(role);
        }
    }
    public class Role
    {
        public string NameRole { get; }
        public string NameProject { get; }
        public Role(string nameRole, string nameProject)
        {
            NameRole = nameRole;
            NameProject = nameProject;
        }
    }
}
