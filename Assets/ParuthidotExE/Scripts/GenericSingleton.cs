///-----------------------------------------------------------------------------
///
/// GenericSingleton
/// 
/// GenericSingleton 
///
///-----------------------------------------------------------------------------

using UnityEngine;

namespace ParuthidotExE
{
    public class GenericSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Inst
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }


        public virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


    }
}