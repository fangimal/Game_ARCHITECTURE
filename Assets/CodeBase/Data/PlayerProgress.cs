using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public WorldData WorldData;
        public Stats HeroStats;
        public KillData KillData;

        public PlayerProgress(string initiallevel)
        {
            WorldData = new WorldData(initiallevel);
            HeroState = new State();
            HeroStats = new Stats();
            KillData = new KillData();
        }
    }
}