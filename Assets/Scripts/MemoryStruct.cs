using UnityEngine;

public class MemoryStruct : MonoBehaviour
{
    private void Awake()
    {
        TestStruect testStruct;        //����ü(Struect)�� �� Ÿ���̶� �ʱ�ȭ���� �ʾƵ� �ʵ���� ����Ʈ���� ����
        testStruct.name = "test struct";

        TestClass testClass;          //���� �ϰ� �ʱ�ȭ���� ������ testClass�� null
        //string testString = testClass.name; // testClass�� �ʱ�ȭ���� �ʾұ� ����, Ŭ������ ���� Ÿ���̱� ������ new Ű����� �ν��Ͻ��� �������� ������ ����� �� ����
        //testClass.name = "test class";   

        TestClass newTestClass = new TestClass(0);
        newTestClass.name = "new test class";

        Debug.Log("classsssssss");
        TestClass c1 = new TestClass(10);
        TestClass c2 = c1;
        c1.name = "100";
        Debug.Log(c1.name + "/" + c2.name);   // 100/100 ������ ���̹ٲ�

        Debug.Log("structtttttttt");
        TestStruect s1 = new TestStruect(10);
        TestStruect s2 = s1;
        s1.name = "100";
        Debug.Log(s1.name + "/" + s2.name); // 100/10 ������ ����� ����������

    }
}

//struct = ����ü  / ���ø޸� ��� (������ ����ü�� ������ �߾ ��ü�� �������� �޸𸮸� �Ҵ��� ���� �ִ�. / ��� �ʵ��� ���� 16byte�� �Ѵ� ���, ����ü�ȿ� Ŭ���� Ÿ���� �ʵ�� ���� ���)
//�ܼ��� Ÿ���� ����� ���� �����ڸ� �����ϰ� ������ �ʿ��� ������ new�� ȣ���ϴ� ���� ��ô ���ŷο�� ���� 8����Ʈ�ۿ� �ȵǴ� �޸𸮸� ���� �Ҵ��Ͽ� ������ �÷��͸� ������ �ʿ䰡 ����.
//16����Ʈ�� ���� �ʴ´ٸ� ����ü�� ����ϴ� ������ ���̴� ���� �޸𸮰��� �κп��� ����.


struct TestStruect //���ø޸� ��� // ��� Ÿ��
{
    public string name;

    //������
    public TestStruect(int number)
    {
        this.name = number.ToString();
    }
}

class TestClass //���޸� ��� // ���� Ÿ��(���۷��� Ÿ��)
{
    public string name;

    //������
    public TestClass(int number)
    {
        this.name = number.ToString();
    }
}
