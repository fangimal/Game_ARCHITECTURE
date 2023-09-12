using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PositionOnlevel
    {
        public string Level;
        public Vector3Data Position;

        public PositionOnlevel(string level, Vector3Data position)
        {
            Level = level;
            Position = position;
        }

        public PositionOnlevel(string initiallevel)
        {
            Level = initiallevel;
        }
    }
}