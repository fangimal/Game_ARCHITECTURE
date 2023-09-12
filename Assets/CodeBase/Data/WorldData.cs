using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnlevel PositionOnLevel;

        public WorldData(string initiallevel)
        {
            PositionOnLevel = new PositionOnlevel(initiallevel);
        }
    }
}