namespace Dictator.Core.Models
{
    /// <summary>
    ///     Represents one of the factions or elements of the population of Ritimba or an external party such as another country.
    /// </summary>
    public class Group
    {
        public GroupType Type { get; set; }
        public int Popularity { get; set; }
        public int Strength { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public GroupStatus Status { get; set; }
        public Group Ally { get; set; }

        public Group(GroupType groupType, int popularity, int strength, string name, string displayName)
        {
            Type = groupType;
            Popularity = popularity;
            Strength = strength;
            Name = name;
            DisplayName = displayName;
            Status = GroupStatus.Default;
        }
    }
}
