using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStackManager : MonoBehaviour
{
    public Stack<GameObject> uiStack = new Stack<GameObject>();

    public Button[] buttons;
    public GameObject[] popupUIs;

    void Start()
    {
        for (int i = 0; i < popupUIs.Length; i++)
        {
            int j = i;
            buttons[i].onClick.AddListener(() => PopupUIOn(j));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (uiStack.Count > 0)
            {
                GameObject ui = uiStack.Pop();
                ui.SetActive(false);
            }
           
        }
    }

    private void PopupUIOn(int index)
    {
        popupUIs[index].SetActive(true);
        uiStack.Push(popupUIs[index]);
    }

    public void PopSpecific(GameObject target)
    {
        //GameObject target = popupUIs[targetNumber];    //int targetNumber를 0,1,2로 상속받아 처리할수도있음.

        Stack<GameObject> tempStack = new Stack<GameObject>();
        bool found = false;

        // Stack에서 하나씩 꺼내면서 target이 아닌 것은 임시 스택에 저장
        while (uiStack.Count > 0)
        {
            GameObject item = uiStack.Pop();
            if (item == target)
            {
                found = true;
                break; // 타겟을 찾았으므로 루프 종료
            }
            tempStack.Push(item);
        }

        // 나머지 요소들을 다시 원래 스택에 넣기 (순서 유지)
        while (tempStack.Count > 0)
        {
            uiStack.Push(tempStack.Pop());
        }

        if (!found)
        {
            Debug.Log("Target not found: " + target.name);
        }
    }
}