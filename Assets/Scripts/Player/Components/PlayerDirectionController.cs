using Assets.Scripts.Misc;
using Mirror;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class PlayerDirectionController : PlayerComponent
    {
        private Direction _direction = Direction.Down;

        public event Action<Direction> DirectionChanged;
        public Direction Direction
        {
            get => _direction;
            set
            {
                if (_direction == value) return;
                _direction = value;
                DirectionChanged?.Invoke(_direction);
            }
        }

        private void FixedUpdate()
        {
            int interval = AngleUtils.GetInterval();
            Direction = (Direction)interval;
        }
    }
}