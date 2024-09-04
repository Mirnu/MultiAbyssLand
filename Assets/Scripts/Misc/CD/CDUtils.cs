using System;
using UnityEngine;

namespace Assets.Scripts.Misc.CD
{
    public static class CDUtils
    {
        public static Action CycleWait(int time, Action callback)
        {
            float start = Time.time;

            return () =>
            {
                if (start + time > Time.time) return;
                start = Time.time;
                callback();
            };
        }
    }
}
