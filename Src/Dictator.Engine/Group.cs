using System;

namespace Dictator.Engine
{
    public class Group
    {
        public string DisplayName { get; set; }
        public GroupType groupType { get; set; }
        public int Popularity { get; set; }
        public int Strength { get; set; }
        public GroupStatus Status { get; set; }

        public Group()
        {
        }
    }
}
