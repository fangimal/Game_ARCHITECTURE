using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;
        private IRandomService _random;

        public void Construct(IGameFactory factory, IRandomService randomService)
        {
            _factory = factory;
            _random = randomService;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private void Start() => 
            EnemyDeath.Happened += SpawnLoot;

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            
            loot.transform.position = transform.position;

            Loot lootItem = GenerateLoot();
            loot.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new()
            {
                Value = _random.Next(_lootMin, _lootMax)
            };
        }
    }
}