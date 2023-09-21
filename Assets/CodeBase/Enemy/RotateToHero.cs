using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        public float Speed;
        
        private Transform _heroTransform;
        private Vector3 _positionToLook;
        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Update()
        {
            if (_heroTransform )
                RotateTowardsHero();
        }

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation();
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }

        private Quaternion SmoothedRotation() =>
            Quaternion.Lerp(transform.rotation, TargetRotation(_positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 position) =>
            Quaternion.LookRotation(position);

        private float SpeedFactor() => 
            Speed * Time.deltaTime;
    }
}