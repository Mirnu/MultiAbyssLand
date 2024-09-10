using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;
using Assets.Scripts.Entity.Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Assets.Scripts.Entity.Zombie
{
    public class ZombieSearchState : EntityState
    {
        [SerializeField] private new ZombieStateMachine stateMachine;
        [SerializeField] private new ZombieFacade entityModel;
        public override void Tick()
        {
            // Ходим рандомно и ковыряемся в носу
        }
        public override bool Exit()
        {
            return true;
        }

        public override void Enter() { }
    }
}
