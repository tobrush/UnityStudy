using UnityEngine;

public sealed class SealedBaseClass : MonoBehaviour
{
    public void Method()
    {
        Debug.Log("일반 함수");
    }
}


// 클래스 봉인으로 상속 불가
/* sealed

→ 봉인 용도**

- 클래스를 상속하지 못하도록 봉인합니다.
- 메서드를 오버라이딩하지 못하도록 막을 수도 있음
- 클래스 앞 → 더 이상 상속 불가
- 오버라이드된 메서드 앞 → 자식에서 override 불가

public class DerivedClass : SealedBaseClass
{

}

*/

public abstract class BaseClass : MonoBehaviour
{
    public abstract void Method();
}

public class DerivedClass : BaseClass
{
    public override void Method()
    {
        Debug.Log("오버라이드 함수");
    }
}




public class BaseClass2 : MonoBehaviour
{
    public virtual void Method()
    {
        Debug.Log("가상 함수");
    }
}

//봉인 함수
public class MiddleClass : BaseClass
{
    public sealed override void Method()
    {
        Debug.Log("봉인된 오버라이드 함수");
    }
}

/*
public class overrideDerivedClass : MiddleClass
{
    // sealed된 함수는 자식 클래스에서 override 불가
    public override void Method()
    {
        Debug.Log("오버라이드 함수");
    }
}
*/