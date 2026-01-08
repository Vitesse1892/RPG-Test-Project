using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgUiTests.Models
{
    public record CharacterOverviewDto
    {
        public string CharacterName { get; set; }
        public BuildType BuildType { get; set; }

        //Stats associated with the character build
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Wisdom { get; set; }
        public int Magic { get; set; }
        public int Level { get; set; }
    }

    public enum BuildType
    {
        Thief,
        Knight,
        Mage,
        Brigadier
    }
}
