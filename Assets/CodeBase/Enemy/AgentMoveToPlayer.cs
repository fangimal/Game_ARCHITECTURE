﻿using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1; //dell
        
        public NavMeshAgent Agent;
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Update()
        {
            SetDestinationForAgent();
        }

        private void SetDestinationForAgent()
        {
            if (_heroTransform && HeroNotReached()) //dell HeroNotReached
                Agent.destination = _heroTransform.position;
        }


        private bool HeroNotReached() => //dell
            Vector3.Distance(Agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}