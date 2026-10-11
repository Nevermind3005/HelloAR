using UnityEngine;

public abstract class TSingleton<T> : Singleton where T : MonoBehaviour
{
    [SerializeField]
    protected bool m_bPersistent = true;

    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (bQuitting)
            {
                Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Instance will not be returned because the application is quitting.");
                return null;
            }
            if (m_instance != null)
            {
                return m_instance;
            }
            var instances = FindObjectsByType<T>();
            var num = instances.Length;
            if (num > 0)
            {
                for (int i = 1; i < instances.Length; i++)
                {
                    Debug.LogWarning($"[Singleton<{typeof(T)}>] Duplicate found; destroying '{instances[i].name}'.");
                    Destroy(instances[i]);
                }
                return m_instance = instances[0];
            }
            Debug.Log($"[{nameof(Singleton)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
            return m_instance = new GameObject($"{nameof(Singleton)}_{typeof(T)}").AddComponent<T>();
        }
    }

    private void Awake()
    {
        OnAwake();
        if (m_bPersistent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnAwake() { }
}

public abstract class Singleton : MonoBehaviour
{
    public static bool bQuitting { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        bQuitting = false;
    }

    private void OnApplicationQuit()
    {
        bQuitting = true;
    }
}