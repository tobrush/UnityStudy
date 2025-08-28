using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteAlways] // �����Ϳ����� ����ǵ���
public class CharacterCircleSelector : MonoBehaviour
{
    [Header("UI ��ư")]
    public Button leftButton;
    public Button rightButton;

    [Header("��ġ ����")]
    public float radius = 2f;             // �߽ɰ��� �Ÿ�
    public float rotationSpeed = 5f;      // ȸ�� �ε巴��
    public float startAngle = 0f;         // ù ĳ���� ���� ����(180�̸� Z- ����, 0�̸� Z+ ����)

    [Header("Index")]
    public int currentIndex = 0;
    private int childCount;
    private float targetAngle = 0f;

    Animator anim;
    Coroutine rotateCoroutine; 

    void Start()
    {
        childCount = transform.childCount;
        ArrangeCharacters();

        // ��ư �̺�Ʈ ����
        if (leftButton != null) leftButton.onClick.AddListener(() => RotateSelection(-1));
        if (rightButton != null) rightButton.onClick.AddListener(() => RotateSelection(1));
    }

    void ArrangeCharacters()
    {
        if (childCount == 0) return;

        // �ݽð� ���� ���� ó��, �ð�����̶�� ���
        float direction = -1f;
        float angleStep = (360f / childCount) * direction;
   

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // 0�ư� Z+ ����(ī�޶� ����)���� �����ϵ��� ����
            float angle = startAngle + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            // ���� ��ǥ ��� (Z+ ������ ����)
            Vector3 pos = new Vector3(Mathf.Sin(rad), 0f, Mathf.Cos(rad)) * radius;
            child.localPosition = pos;

            // �ٱ� ���� �ٶ󺸰�
            Vector3 dirFromCenter = (child.position - transform.position).normalized;
            child.rotation = Quaternion.LookRotation(dirFromCenter, Vector3.up);
        }
    }


    void RotateSelection(int direction)
    {
        currentIndex += direction; 
        if (currentIndex < 0) currentIndex += childCount;
        if (currentIndex >= childCount) currentIndex -= childCount;

        float angleStep = 360f / childCount;
        targetAngle = angleStep * currentIndex;

        rotateCoroutine = StartCoroutine(RotateToTarget());
    }
    IEnumerator RotateToTarget()
    {
        float currentY = transform.rotation.eulerAngles.y;

        while (Mathf.Abs(Mathf.DeltaAngle(currentY, targetAngle)) > 0.01f)
        {
            currentY = Mathf.LerpAngle(currentY, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, currentY, 0f);
            yield return null;
        }

        // ��ǥ ���� ���� ����
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        rotateCoroutine = null;
    }

    public void CharacterSelect()
    {
        Debug.Log($"���� ������ ĳ���ʹ� {currentIndex} ��° ĳ���� �Դϴ�.");
        anim = transform.GetChild(currentIndex).GetComponent<Animator>();
       
        LoadSceneManager.Instance.SetCharacterIndex(currentIndex);
       

        StartCoroutine(SelectRoutine());

    }
    IEnumerator SelectRoutine()
    {
        anim.SetTrigger("Select");

      
        Fade.onFadeAction?.Invoke(3, Color.white, true, null);
        // yield return new WaitForSeconds(3f);
        yield return null;
        LoadSceneManager.Instance.OnLoadScene();
    }

}
