using System;
using UnityEngine;

namespace Assets.Scripts.Misc.CD
{
    public static class CDUtils
    {
        /// <summary>
        /// Обычный ждун, после того как функция сработает, счетчик обнулится
        /// </summary>
        public static Func<bool> CycleWait(float time, Action callback)
        {
            float start = Time.time;

            return () =>
            {
                if (start + time > Time.time) return false;
                start = Time.time;
                callback();
                return true;
            };
        }

        //пока не робит
        public static Action CycleWaitEvent(Action callback, Action action) 
        {
            bool called = true;
            action += () => called = true;

            return () =>
            {
                if (!called) return;
                called = false;
                callback();
            };
        }

        /// <summary>
        /// Накапливающий ждун, его нужно вызвать столько раз по времени, указанному в параметре time
        /// Также после того как функция сработает, счетчик обнулится
        /// </summary>
        public static Action CycleAccumulatingWait(int time, Action callback)
        {
            float timeWalk = 0;

            return () =>
            {
                timeWalk += Time.deltaTime;
                if (timeWalk > time)
                {
                    timeWalk = 0;
                    callback();
                }
            }; 
        }
    }
}
