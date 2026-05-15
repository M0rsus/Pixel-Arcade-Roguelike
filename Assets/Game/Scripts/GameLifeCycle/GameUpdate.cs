using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameUpdate : MonoBehaviour
    {
        private static readonly List<IOnUpdateListener> _onUpdateListeners = new();
        private static readonly List<IOnFixedUpdateListener> _onFixedUpdateListeners = new();
        private static readonly List<IOnLateUpdateListener> _onLateUpdateListeners = new();
        
        public static void Register(IOnUpdateListener onUpdateListener) =>
            _onUpdateListeners.Add(onUpdateListener);
        public static void Register(IOnFixedUpdateListener onFixedUpdateListener) =>
            _onFixedUpdateListeners.Add(onFixedUpdateListener);
        public static void Register(IOnLateUpdateListener onLateUpdateListener) =>
            _onLateUpdateListeners.Add(onLateUpdateListener);
        public static void Unregister(IOnUpdateListener onUpdateListener) =>
            _onUpdateListeners.Remove(onUpdateListener);
        public static void Unregister(IOnFixedUpdateListener onFixedUpdateListener) =>
            _onFixedUpdateListeners.Remove(onFixedUpdateListener);
        public static void Unregister(IOnLateUpdateListener onLateUpdateListener) =>
            _onLateUpdateListeners.Remove(onLateUpdateListener);

        void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = _onUpdateListeners.Count - 1; i >= 0; i--)
            {
                _onUpdateListeners[i].OnUpdate(deltaTime);
            }
        }

        void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            for (int i = _onFixedUpdateListeners.Count - 1; i >= 0; i--)
            {
                _onFixedUpdateListeners[i].OnFixedUpdate(fixedDeltaTime);
            }
        }

        void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = _onLateUpdateListeners.Count - 1; i >= 0; i--)
            {
                _onLateUpdateListeners[i].OnLateUpdate(deltaTime);
            }
        }
    }
}