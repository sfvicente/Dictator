using System;

namespace Dictator.Core
{
    public class Group
    {
        public GroupType Type { get; set; }
        public int Popularity { get; set; }
        public int Strength { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public GroupStatus Status { get; set; }

        public Group(GroupType groupType, int popularity, int strength, string name, string displayName)
        {
            this.Type = groupType;
            this.Popularity = popularity;
            this.Strength = strength;
            this.Name = name;
            this.DisplayName = displayName;
            this.Status = GroupStatus.Default;
        }
    }
}
