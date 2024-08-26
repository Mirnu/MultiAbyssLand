using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public static class AngleUtils
    {
        private static readonly Vector2 _middle = new(Screen.width / 2, Screen.height / 2);

        private static Dictionary<Vector2, int> _cache = new();

        public static int GetAngle()
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            Vector2 mousePosition = new(x, y);

            if (_cache.ContainsKey(mousePosition))
            {
                return _cache[mousePosition];
            }

            Vector2 between = mousePosition - _middle;
            float angle = Mathf.Atan2(between.y, between.x) * Mathf.Rad2Deg;
            angle = angle > 135 ? -(360 - angle) : angle;
            int result = 135 - (int)angle;

            _cache[mousePosition] = result;

            return result;
        }

        public static int GetHours()
        {
            int angle = GetAngle();
            int hours = angle / 30;
            return hours;
        }

        /// <summary>
        /// Возвращает интервал от 0 до 3, где 0 - 12:00, 1 - 3:00, 2 - 6:00, 3 - 9:00
        /// (Все означает "От", например 0 от 12:00 до 3:00)
        /// </summary>
        public static int GetInterval()
        {
            int hours = GetHours();
            int interval = hours / 3;
            return interval;
        }
    }
}
