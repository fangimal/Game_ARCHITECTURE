using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public WorldData WorldData;
        public Stats HeroStats;

        public PlayerProgress(string initiallevel)
        {
            WorldData = new WorldData(initiallevel);
            HeroState = new State();
            HeroStats = new Stats();
        }
    }
}