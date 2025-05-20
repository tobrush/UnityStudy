using UnityEngine;

public class MemoryStruct : MonoBehaviour
{
    private void Awake()
    {
        TestStruect testStruct;        //구조체(Struect)는 값 타입이라 초기화하지 않아도 필드들이 디폴트값을 가짐
        testStruct.name = "test struct";

        TestClass testClass;          //선언만 하고 초기화하지 않으면 testClass는 null
        //string testString = testClass.name; // testClass가 초기화되지 않았기 때문, 클래스는 참조 타입이기 때문에 new 키워드로 인스턴스를 생성하지 않으면 사용할 수 없음
        //testClass.name = "test class";   

        TestClass newTestClass = new TestClass(0);
        newTestClass.name = "new test class";

        Debug.Log("classsssssss");
        TestClass c1 = new TestClass(10);
        TestClass c2 = c1;
        c1.name = "100";
        Debug.Log(c1.name + "/" + c2.name);   // 100/100 참조라서 같이바뀜

        Debug.Log("structtttttttt");
        TestStruect s1 = new TestStruect(10);
        TestStruect s2 = s1;
        s1.name = "100";
        Debug.Log(s1.name + "/" + s2.name); // 100/10 각각의 밸류라서 개별데이터

    }
}

//struct = 구조체  / 스택메모리 사용 (하지만 구조체로 선언을 했어도 객체를 힙영역에 메모리를 할당할 때가 있다. / 모든 필드의 합이 16byte를 넘는 경우, 구조체안에 클래스 타입을 필드로 가질 경우)
//단순한 타입을 만들기 위해 생성자를 정의하고 변수가 필요할 때마다 new를 호출하는 것은 무척 번거로우며 고작 8바이트밖에 안되는 메모리를 힙에 할당하여 가비지 컬렉터를 괴롭힐 필요가 없다.
//16바이트가 넘지 않는다면 구조체를 사용하는 버릇을 들이는 것이 메모리관리 부분에서 좋다.


struct TestStruect //스택메모리 사용 // 밸류 타입
{
    public string name;

    //생성자
    public TestStruect(int number)
    {
        this.name = number.ToString();
    }
}

class TestClass //힙메모리 사용 // 참조 타입(레퍼런스 타입)
{
    public string name;

    //생성자
    public TestClass(int number)
    {
        this.name = number.ToString();
    }
}
