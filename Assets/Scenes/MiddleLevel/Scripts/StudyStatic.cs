using UnityEngine;

public class StudyStatic : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"정적 필드 접근 : {StaticClass.number}");
    }
}

public class StaticClass
{
    public static StaticClass instance = new StaticClass();
    public static int number = 10;

    public StaticClass()
    {
        Debug.Log($"정적 클래스 생성자 접근 : {number}");  //  Static의 흐름 0 -> 10 
    }
}