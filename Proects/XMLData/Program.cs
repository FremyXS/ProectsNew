using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XMLData
{
    public enum ElementsType
    {
        project, member,
    }
    class Program
    {
        public static List<Project> Projects { get; private set; } = new List<Project>();
        public static List<Member> Members { get; private set; } = new List<Member>();
        static void Main(string[] args)
        {
            GetInformation();
            SetMembers();
            WriteMembers();
        }
        private static void GetInformation()
        {
            var fileInfo = File.ReadAllLines("projects.xml");

            Project project = null;

            foreach(var line in fileInfo)
            {
                IsProject(line.Split(new char[] { '<', '>', '=', '"', ' ' }, StringSplitOptions.RemoveEmptyEntries), ref project);
                IsMember(line.Split(new char[] { '<', '>', '=', '"', ' ' }, StringSplitOptions.RemoveEmptyEntries), project);
            }
        }
        private static void IsProject(string[] line, ref Project project)
        {
            if(line[0] == ElementsType.project.ToString())
            {
                project = new Project(line[2]);
            }
            else if(line[0] == "/" + ElementsType.project.ToString())
            {
                Projects.Add(project);
            }

        }
        private static void IsMember(string[] line, Project project)
        {
            if (line[0] == ElementsType.member.ToString())
            {
                project.AddPerson(new Person(line[4], line[2]));
            }
        }
        private static void SetMembers()
        {
            foreach(var project in Projects)
            {
                GetMembers(project);
            }
        }
        private static void GetMembers(Project project)
        {
            foreach(var person in project.Persons)
            {
                IsPerson(person, project.Name);
            }
        }
        private static void IsPerson(Person person, string projectName)
        {
            if(Members.Count > 0)
            {
                if(Members.Any(e => e.Name == person.Name))
                {
                    AddRoleToMember(person, projectName);
                }
                else
                {
                    AddMember(person, projectName);
                }
            }
            else
            {
                AddMember(person, projectName);
            }
        }
        private static void AddMember(Person person, string projectName)
        {
            var member = new Member(person.Name);
            member.AddRole(new Role(person.Role, projectName));
            Members.Add(member);
        }
        private static void AddRoleToMember(Person person, string projectName)
        {
            var member = Members.Single(e => e.Name == person.Name);
            Members.Remove(member);
            member.AddRole(new Role(person.Role, projectName));
            Members.Add(member);
        }
        private static void WriteMembers()
        {
            string[] membersInfo = new string[GetSizeArray()];
            membersInfo[0] = "<members>";
            membersInfo[^1] = "</members>";

            int ind = 1;
            foreach (var member in Members)
            {
                WriteOneMember(member, ref ind, membersInfo);
            }

            File.WriteAllLines("members.xml", membersInfo);
        }
        private static int GetSizeArray()
        {
            int size = 2 + (Members.Count * 2);

            foreach(var member in Members)
            {
                size += member.Roles.Count;
            }

            return size;
        }
        private static void WriteOneMember(Member member, ref int ind, string[] membersInfo)
        {
            membersInfo[ind] = $"{new string(' ', 4)}<member name=\"{member.Name}\">";
            ind++;
            foreach(var role in member.Roles)
            {
                membersInfo[ind] = $"{new string(' ', 8)}<role name=\"{role.NameRole}\" project=\"{role.NameProject}\"/>";
                ind++;
            }
            membersInfo[ind] = $"{new string(' ', 4)}</member>";
            ind++;

        }

    }
}
