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
        //GameObject target = popupUIs[targetNumber];    //int targetNumber�� 0,1,2�� ��ӹ޾� ó���Ҽ�������.

        Stack<GameObject> tempStack = new Stack<GameObject>();
        bool found = false;

        // Stack���� �ϳ��� �����鼭 target�� �ƴ� ���� �ӽ� ���ÿ� ����
        while (uiStack.Count > 0)
        {
            GameObject item = uiStack.Pop();
            if (item == target)
            {
                found = true;
                break; // Ÿ���� ã�����Ƿ� ���� ����
            }
            tempStack.Push(item);
        }

        // ������ ��ҵ��� �ٽ� ���� ���ÿ� �ֱ� (���� ����)
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