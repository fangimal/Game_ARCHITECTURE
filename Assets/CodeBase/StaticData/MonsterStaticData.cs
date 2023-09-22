using UnityEngine;

namespace CodeBase.StaticData
{[CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData: ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [Range(1,100)]
        public int Hp;
        
        [Range(1,30)]
        public int Damage;

        public int MaxLoot;
        public int MinLoot;
        
        [Range(0.5f,1f)]
        public float Cleavage = 0.5f;
        
        [Range(0.5f,1f)]
        public float EffectiveDistance = 0.5f;
        
        [Range(0f, 10f)]
        public float MoveSpeed = 3;
        
        public GameObject Prefab;
    }
}