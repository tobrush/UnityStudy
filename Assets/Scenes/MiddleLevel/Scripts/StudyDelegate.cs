using UnityEngine;

public class StudyDelegate : MonoBehaviour
{
    public delegate void MyDelegate();
    public MyDelegate myDelegate;

    private void Start()
    {
        //��������� ��������Ʈ �Ҵ�
        //myDelegate = new MyDelegate(MethodA);

        //ǥ�ع���� �Ҵ�
        myDelegate = MethodA;

        myDelegate += MethodB;
        myDelegate += MethodC;

        myDelegate -= MethodB;

        // ��������Ʈ ȣ��
        //myDelegate();
        // myDelegate.Invoke();

        //�Ҵ���������� null������ �ߴ� �̷���
        //if (myDelegate != null)
        //{
        //    myDelegate.Invoke();
        //}

        myDelegate?.Invoke(); // ��üũ������




    }

    private void MethodA()
    {
        Debug.Log("��������Ʈ�� ���� MethodA ����");
    }
    private void MethodB()
    {
        Debug.Log("��������Ʈ�� ���� MethodB ����");
    }
    private void MethodC()
    {
        Debug.Log("��������Ʈ�� ���� MethodC ����");
    }
}
