using UnityEngine;

public sealed class SealedBaseClass : MonoBehaviour
{
    public void Method()
    {
        Debug.Log("�Ϲ� �Լ�");
    }
}


// Ŭ���� �������� ��� �Ұ�
/* sealed

�� ���� �뵵**

- Ŭ������ ������� ���ϵ��� �����մϴ�.
- �޼��带 �������̵����� ���ϵ��� ���� ���� ����
- Ŭ���� �� �� �� �̻� ��� �Ұ�
- �������̵�� �޼��� �� �� �ڽĿ��� override �Ұ�

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
        Debug.Log("�������̵� �Լ�");
    }
}




public class BaseClass2 : MonoBehaviour
{
    public virtual void Method()
    {
        Debug.Log("���� �Լ�");
    }
}

//���� �Լ�
public class MiddleClass : BaseClass
{
    public sealed override void Method()
    {
        Debug.Log("���ε� �������̵� �Լ�");
    }
}

/*
public class overrideDerivedClass : MiddleClass
{
    // sealed�� �Լ��� �ڽ� Ŭ�������� override �Ұ�
    public override void Method()
    {
        Debug.Log("�������̵� �Լ�");
    }
}
*/