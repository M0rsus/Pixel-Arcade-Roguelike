using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
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
            foreach (var onUpdateListener in _onUpdateListeners)
                onUpdateListener.OnUpdate(deltaTime);
        }

        void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            foreach (var onFixedUpdateListener in _onFixedUpdateListeners)
                onFixedUpdateListener.OnFixedUpdate(fixedDeltaTime);
        }

        void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            foreach (var onLateUpdateListener in _onLateUpdateListeners)
                onLateUpdateListener.OnLateUpdate(deltaTime);
        }
    }
}