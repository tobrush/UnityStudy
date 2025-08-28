using UnityEngine;

public class StudyDelegate : MonoBehaviour
{
    public delegate void MyDelegate();
    public MyDelegate myDelegate;

    private void Start()
    {
        //옛날방식의 델리게이트 할당
        //myDelegate = new MyDelegate(MethodA);

        //표준방식의 할당
        myDelegate = MethodA;

        myDelegate += MethodB;
        myDelegate += MethodC;

        myDelegate -= MethodB;

        // 델리게이트 호출
        //myDelegate();
        // myDelegate.Invoke();

        //할당되지않으면 null에러가 뜨니 이렇게
        //if (myDelegate != null)
        //{
        //    myDelegate.Invoke();
        //}

        myDelegate?.Invoke(); // 널체크연산자




    }

    private void MethodA()
    {
        Debug.Log("델리게이트에 의해 MethodA 실행");
    }
    private void MethodB()
    {
        Debug.Log("델리게이트에 의해 MethodB 실행");
    }
    private void MethodC()
    {
        Debug.Log("델리게이트에 의해 MethodC 실행");
    }
}
