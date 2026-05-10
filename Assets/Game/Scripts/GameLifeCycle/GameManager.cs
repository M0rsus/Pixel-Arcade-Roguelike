using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private List<IOnUpdateListener> _onUpdateListeners = new();
        private List<IOnFixedUpdateListener> _onFixedUpdateListeners = new();
        private List<IOnLateUpdateListener> _onLateUpdateListeners = new();
        
        public static event Action<IOnUpdateListener> OnRegister;
        public static void Register(IOnUpdateListener onUpdateListener) =>
            OnRegister?.Invoke(onUpdateListener);

        void Awake()
        {
            OnRegister += RegisterOnUpdateListener;
        }

        void OnDestroy()
        {
            OnRegister -= RegisterOnUpdateListener;
        }

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

        private void RegisterOnUpdateListener(IOnUpdateListener onUpdateListener) =>
            _onUpdateListeners.Add(onUpdateListener);

        private void RegisterOnFixedUpdateListener(IOnFixedUpdateListener onFixedUpdateListener) =>
            _onFixedUpdateListeners.Add(onFixedUpdateListener);

        private void RegisterOnLateUpdateListener(IOnLateUpdateListener onLateUpdateListener) =>
            _onLateUpdateListeners.Add(onLateUpdateListener);

        private void UnregisterOnUpdateListener(IOnUpdateListener onUpdateListener) =>
            _onUpdateListeners.Remove(onUpdateListener);

        private void UnregisterOnFixedUpdateListener(IOnFixedUpdateListener onFixedUpdateListener) =>
            _onFixedUpdateListeners.Remove(onFixedUpdateListener);

        private void UnregisterOnLateUpdateListener(IOnLateUpdateListener onLateUpdateListener) =>
            _onLateUpdateListeners.Remove(onLateUpdateListener);
    }
}