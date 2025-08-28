using UnityEngine;

public class StudyPartial : MonoBehaviour
{
    //�Ϲ� �Ű����� (Call by Value)
    //�⺻���̸�, �� ������ ��� ���纻�� ���޵�
    //�Լ� ���ο��� �����ص� ������ ������� ����
    private void NomalParameter(int number)
    {
        number = 1;
    }

    //ref (Call by Reference)
    //�ʱ�ȭ�� ������ ���� ����
    //�Լ� ���ο��� ���� �� ������ �����
    private void RefParameter(ref int number)
    {
        number = 2;
    }

    //out (��� ���� ����)
    //�Լ� ���ο��� �ݵ�� �ʱ�ȭ�ؾ� ��
    //�ʱ�ȭ���� ���� ������ ���� ����
    //�Է°��� ����, ��¿� �������� ���� -> ��ȯ�� ����
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

    //params (���� ����)
    //�Ű������� ������ ���������� ���� �� ���
    //�迭�� ���޵�
    //�޼��� ���� �� ������ ���ڿ��� ��� ����
    private void ParamsParameter(params int[] numbers)
    {
        string s = "";
        foreach (var n in numbers)
            s += n.ToString() + " ";

        Debug.Log(s);
    }


    /// <summary>
    /// �Ʒ����� �ļ�
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

�� �и� �뵵

- �ϳ��� Ŭ����, ����ü, �������̽��� ���� ���Ͽ� ������ ���� ����
- partial�� Class�� �и��Ͽ� �������� �ø��ų�, Class�� ���� ����� �߰��Ͽ� ��� ����
- �ϳ��� ���ļ� ������ ����

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



/*partial �Լ�

partial class StudyPartial
{
    partial void OnSomething(); // ����
}

partial class StudyPartial
{
    partial void OnSomething() // ����
    {
        Debug.Log("OnSomething ȣ���");
    }
}

*/