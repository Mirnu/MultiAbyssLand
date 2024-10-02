using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Components;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Cow
{

    public class CowFacade : EntityFacade
    {
        [SerializeField] protected new CowStateMachine stateMachine;

        [Server]
        private void Start()
        {
            FacadeLocator.Singleton.RegisterFacade(this);
            /*stateMachine.Initialize();*/
            Debug.Log(pathfindingStrategy);
            var agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = statsModel.Speed;
            transform.rotation = new Quaternion();
        }

        private void Update()
        {
            /*stateMachine.Update();*/
        }
    }
}


