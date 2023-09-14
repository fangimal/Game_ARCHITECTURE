using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;
        
        public float Speed;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (HeroExists())
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += InitializeHeroTransform;
            }
        }

        private void Update()
        {
            if (Initialized() )//&& HeroNotReached()) 
                RotateTowardsHero();
        }

        private bool HeroExists() => 
            _gameFactory.HeroGameObject != null;

        private void InitializeHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position);

        private float SpeedFactor() => 
            Speed * Time.deltaTime;

        private bool Initialized() => 
            _heroTransform != null;
    }
}