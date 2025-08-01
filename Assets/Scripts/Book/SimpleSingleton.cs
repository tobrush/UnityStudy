using Unity.VisualScripting;
using UnityEngine;
using static SimpleSingleton;

public class SimpleSingleton : MonoBehaviour
{
    /*
    //1. 단순한 방식의 Singleton
    // 싱글톤 인스턴스를 저장할 정적 변수
    public static SimpleSingleton Instance { get; private set; }

    private void Awake()
    {
        // 인스턴스가 이미 존재하면 새로 생성된 객체를 파괴
        if (Instance == null)
            Instance = this; // 현재 객체를 싱글톤 인스턴스로 설정
        else
            Destroy(gameObject); // 중복 생성 방지
    }
    */


    /*
    //1. 즉시 초기화 Singleton
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
    //2. 게으른 초기화 Singleton
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
    //3. 일반 Singleton
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

    //4. 유일성을 보장한 Singleton
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
    //Singleton의 간편한 구현을 위한 Generic
    public class Singleton<T> : MonoBehaviour where T : Component
    {

    }
   

    //5. Generic을 활용한 Singleton 1
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


    //6. Generic을 활용한 Singleton 2
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
