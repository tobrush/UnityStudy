using UnityEngine;

public class StudyStatic : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"���� �ʵ� ���� : {StaticClass.number}");
    }
}

public class StaticClass
{
    public static StaticClass instance = new StaticClass();
    public static int number = 10;

    public StaticClass()
    {
        Debug.Log($"���� Ŭ���� ������ ���� : {number}");  //  Static�� �帧 0 -> 10 
    }
}