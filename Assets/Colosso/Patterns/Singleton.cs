using UnityEngine;
namespace Colosso.Tools.Patterns
{
    /// <summary>
    /// A generic singleton base class for MonoBehaviour-based singletons.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _isShuttingDown = false;
        private static readonly object _lock = new object();

        /// <summary>
        /// Access the singleton instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_isShuttingDown) return null;

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindAnyObjectByType<T>();

                        if (_instance == null)
                        {
                            var singletonObject = new GameObject(typeof(T).Name);
                            _instance = singletonObject.AddComponent<T>();
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return _instance;
                }
            }
        }

        /// <summary>
        /// Prevent duplicates and assign the singleton instance.
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                GlobalLogger.Instance?.Log(LogChannel.LOW, $"[Singleton] Duplicate instance of {typeof(T)} detected. Destroying this instance.");
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Marks the instance as shutting down so we avoid creating new instances on quit.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }

        /// <summary>
        /// Clears the instance reference on destroy.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
                _isShuttingDown = true;
            }
        }
    }
}