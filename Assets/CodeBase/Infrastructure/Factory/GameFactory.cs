﻿using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assetses;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IWindowService _windowService;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        private GameObject HeroGameObject { get; set; }
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameFactory(
            IAssets assetses, 
            IStaticDataService staticData, 
            IRandomService randomService,
            IPersistentProgressService persistentProgressService, IWindowService windowService)
        {
            _assetses = assetses;
            _staticData = staticData;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
            _windowService = windowService;
        }

        public GameObject CreateHero(Vector3 at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at);
            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hub = InstantiateRegistered(AssetPath.HUDPath);
            hub.GetComponentInChildren<LootCounter>().Construct(_persistentProgressService.Progress.WorldData);

            foreach (OpenWindowButton openWindowButton in hub.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);
            
            return hub;
        }

        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateRegistered(AssetPath.Loot).
                GetComponent<LootPiece>();

            lootPiece.Costruct(_persistentProgressService.Progress.WorldData);
            
            return lootPiece;
        }

        public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
        {
            var spawner = InstantiateRegistered(AssetPath.Spawner, at).
                GetComponent<SpawnPoint>();
            
            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.MonsterTypeId = monsterTypeId;
        }

        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeId);

            GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;
            
            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            LootSpawner lootSpawners = monster.GetComponentInChildren<LootSpawner>();
            lootSpawners.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
            lootSpawners.Construct(this, _randomService);

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;
            
            monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);
            
            return monster;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assetses.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetses.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progeressWriter) 
                ProgressWriters.Add(progeressWriter);
            
            ProgressReaders.Add(progressReader);
        }
    }
}