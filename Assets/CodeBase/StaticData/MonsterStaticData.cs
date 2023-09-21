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
        
        [Range(0.5f,1f)]
        public float Cleavage = 0.5f;
        
        [Range(0.5f,1f)]
        public float EffectiveDistance = 0.5f;
        
        [Range(0f, 10f)]
        public float MoveSpeed = 1;
        
        public GameObject Prefab;
    }
}