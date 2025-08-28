using UnityEngine;

public class StudyPartial : MonoBehaviour
{
    //일반 매개변수 (Call by Value)
    //기본값이며, 값 형식인 경우 복사본이 전달됨
    //함수 내부에서 변경해도 원본은 변경되지 않음
    private void NomalParameter(int number)
    {
        number = 1;
    }

    //ref (Call by Reference)
    //초기화된 변수만 전달 가능
    //함수 내부에서 변경 시 원본도 변경됨
    private void RefParameter(ref int number)
    {
        number = 2;
    }

    //out (출력 전용 참조)
    //함수 내부에서 반드시 초기화해야 함
    //초기화되지 않은 변수도 전달 가능
    //입력값은 무시, 출력용 목적으로 사용됨 -> 반환의 개념
    private void OutParameter(out int number)
    {
        number = 3;
    }

    //Collection
    private void CollectionParameter(int[] numbers)
    {
        string s = "";
        foreach (var n in numbers)
            s += n.ToString() + " ";

        Debug.Log(s);
    }

    //params (가변 인자)
    //매개변수의 개수를 가변적으로 받을 때 사용
    //배열로 전달됨
    //메서드 정의 시 마지막 인자에만 사용 가능
    private void ParamsParameter(params int[] numbers)
    {
        string s = "";
        foreach (var n in numbers)
            s += n.ToString() + " ";

        Debug.Log(s);
    }


    /// <summary>
    /// 아래부턴 파셜
    /// </summary>
   
    void Start()
    {
        MethodA();
        MethodB();
    }

    public void MethodA()
    {
        Debug.Log("Method A");
    }

    public void MethodB()
    {
        Debug.Log("Method B");
    }
}



/*
## partial

→ 분리 용도

- 하나의 클래스, 구조체, 인터페이스를 여러 파일에 나누어 정의 가능
- partial로 Class를 분리하여 가독성을 올리거나, Class에 따로 기능을 추가하여 사용 가능
- 하나로 합쳐서 컴파일 진행

using UnityEngine;

public class StudyPartial : MonoBehaviour
{
    void Start()
    {
        MethodA();
        MethodB();
    }

    public void MethodA()
    {
        Debug.Log("Method A");
    }

    public void MethodB()
    {
        Debug.Log("Method B");
    }
}
*/



/*partial 함수

partial class StudyPartial
{
    partial void OnSomething(); // 선언만
}

partial class StudyPartial
{
    partial void OnSomething() // 구현
    {
        Debug.Log("OnSomething 호출됨");
    }
}

*/