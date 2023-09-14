using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{ 
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public Follow Follow;

        public float Cooldown;
        private Coroutine _agrroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            SwithFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAgroCoroutine();
                SwithFollowOn() ;
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                _agrroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(Cooldown); 
            SwithFollowOff();
        }

        private void StopAgroCoroutine()
        {
            if (_agrroCoroutine != null)
            {
                StopCoroutine(_agrroCoroutine);
                SwithFollowOn();
            }
        }

        private void SwithFollowOn() => 
            Follow.enabled = true;

        private void SwithFollowOff() => 
            Follow.enabled = false;
    }
}