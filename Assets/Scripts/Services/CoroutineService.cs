using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.Services
{
    public class CoroutineService : MonoBehaviour
    {
        private Dictionary<float, WaitForSeconds> delays = new Dictionary<float, WaitForSeconds>();

        public void DelayedCall(float delay, Action callback)
        {
            StartCoroutine(DelayedCallCoroutine(delay, callback));
        }

        private IEnumerator DelayedCallCoroutine(float delay, Action callback)
        {
            yield return WaitTime(delay);
            callback?.Invoke();
        }

        public IEnumerator WaitTime(float time)
        {
            yield return delays.ContainsKey(time) ? delays[time] : NewDelay(time);
        }

        private WaitForSeconds NewDelay(float time)
        {
            var delay = new WaitForSeconds(time);
            delays.Add(time, delay);
            return delay;
        }

        public void DelayedDespawn(float delay, GameObject obj)
        {
            DelayedCall(delay, () => SimplePool.Despawn(obj));
        }
    }
}
