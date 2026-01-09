using RpgUiTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpGuiTests.Domain
{
    public static class BuildStatsRepository
    {
        public static readonly Dictionary<BuildType, BuildStats> Stats =
            new()
            {
                [BuildType.Thief] = new BuildStats
                {
                    Strength = 1,
                    Agility = 6,
                    Wisdom = 2,
                    Magic = 1,
                    Level = 1
                },
                [BuildType.Knight] = new BuildStats
                {
                    Strength = 6,
                    Agility = 2,
                    Wisdom = 1,
                    Magic = 1,
                    Level = 1
                },
                [BuildType.Mage] = new BuildStats
                {
                    Strength = 0,
                    Agility = 1,
                    Wisdom = 3,
                    Magic = 6,
                    Level = 1
                },
                [BuildType.Brigadier] = new BuildStats
                {
                    Strength = 3,
                    Agility = 1,
                    Wisdom = 6,
                    Magic = 1,
                    Level = 1
                }
            };
    }
}
