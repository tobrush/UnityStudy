using Unity.VisualScripting;
using UnityEngine;
using static SimpleSingleton;

public class SimpleSingleton : MonoBehaviour
{
    /*
    //1. �ܼ��� ����� Singleton
    // �̱��� �ν��Ͻ��� ������ ���� ����
    public static SimpleSingleton Instance { get; private set; }

    private void Awake()
    {
        // �ν��Ͻ��� �̹� �����ϸ� ���� ������ ��ü�� �ı�
        if (Instance == null)
            Instance = this; // ���� ��ü�� �̱��� �ν��Ͻ��� ����
        else
            Destroy(gameObject); // �ߺ� ���� ����
    }
    */


    /*
    //1. ��� �ʱ�ȭ Singleton
    public static SimpleSingleton instance = new SimpleSingleton();
    public static SimpleSingleton Instance
    {
        get
        {
            if (instance == null)
                instance = new SimpleSingleton();

            return instance;
        }
    }
    */


    /*
    //2. ������ �ʱ�ȭ Singleton
    public static SimpleSingleton instance;
    public static SimpleSingleton Instance
    {
        get
        {
            if (instance == null)
                instance = new SimpleSingleton();

            return instance;
        }
    }
    */

    /*
    //3. �Ϲ� Singleton
    private static SimpleSingleton instance;
    public static SimpleSingleton Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    */

    //4. ���ϼ��� ������ Singleton
    private static SimpleSingleton instance;
    public static SimpleSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindFirstObjectByType<SimpleSingleton>();

                if (obj != null)
                    instance = obj;
                else
                {
                    var newObj = new GameObject();
                    newObj.AddComponent<SimpleSingleton>();
                    instance = newObj.GetComponent<SimpleSingleton>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /*
    //Singleton�� ������ ������ ���� Generic
    public class Singleton<T> : MonoBehaviour where T : Component
    {

    }
   

    //5. Generic�� Ȱ���� Singleton 1
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<T>();

                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
                instance = this as T;
            else if (instance != this)
                Destroy(gameObject);
        }
    }


    //6. Generic�� Ȱ���� Singleton 2
    public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        var newObj = new GameObject(typeof(T).ToString());
                        instance = newObj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }
    }
     */
}
