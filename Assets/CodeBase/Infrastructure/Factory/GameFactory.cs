using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assetses;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        
        public GameObject HeroGameObject { get; set; }
        public event Action HeroCreated;
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameFactory(IAssets assetses)
        {
            _assetses = assetses;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroCreated?.Invoke();
            return HeroGameObject;

        }

        public GameObject CreateHud() => 
            InstantiateRegistered(AssetPath.HUDPath);

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

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progeressWriter) 
                ProgressWriters.Add(progeressWriter);
            
            ProgressReaders.Add(progressReader);
        }
    }
}