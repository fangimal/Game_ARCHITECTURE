using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnlevel PositionOnLevel;
        public LootData LootData;

        public WorldData(string initiallevel)
        {
            PositionOnLevel = new PositionOnlevel(initiallevel);
            LootData = new LootData();
        }
    }
}