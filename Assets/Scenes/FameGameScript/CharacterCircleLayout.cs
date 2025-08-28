using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteAlways] // 에디터에서도 실행되도록
public class CharacterCircleSelector : MonoBehaviour
{
    [Header("UI 버튼")]
    public Button leftButton;
    public Button rightButton;

    [Header("배치 설정")]
    public float radius = 2f;             // 중심과의 거리
    public float rotationSpeed = 5f;      // 회전 부드럽게
    public float startAngle = 0f;         // 첫 캐릭터 기준 각도(180이면 Z- 방향, 0이면 Z+ 방향)

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

        // 버튼 이벤트 연결
        if (leftButton != null) leftButton.onClick.AddListener(() => RotateSelection(-1));
        if (rightButton != null) rightButton.onClick.AddListener(() => RotateSelection(1));
    }

    void ArrangeCharacters()
    {
        if (childCount == 0) return;

        // 반시계 방향 반전 처리, 시계방향이라면 양수
        float direction = -1f;
        float angleStep = (360f / childCount) * direction;
   

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // 0°가 Z+ 방향(카메라 정면)에서 시작하도록 설정
            float angle = startAngle + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            // 원형 좌표 계산 (Z+ 방향이 시작)
            Vector3 pos = new Vector3(Mathf.Sin(rad), 0f, Mathf.Cos(rad)) * radius;
            child.localPosition = pos;

            // 바깥 방향 바라보게
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

        // 목표 각도 스냅 고정
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        rotateCoroutine = null;
    }

    public void CharacterSelect()
    {
        Debug.Log($"현재 선택한 캐릭터는 {currentIndex} 번째 캐릭터 입니다.");
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
