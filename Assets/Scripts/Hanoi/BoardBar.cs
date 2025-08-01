using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardBar : MonoBehaviour
{
    public enum BarType { LEFT, CENTER, RIGHT }
    public BarType barType;

    public HanoiTower hanoiTower;


    public Stack<GameObject> barStack = new Stack<GameObject>();

    void OnMouseDown()
    {
        Action();
    }

    public void Action()
    {
        if (!HanoiTower.isSelected)
        {

            HanoiTower.selectedDonut = OnPopRing();
            if (HanoiTower.selectedDonut)
            {
                int number = HanoiTower.selectedDonut.GetComponent<Donut>().donutNumber;

                string ColorName = "";
                switch (number)
                {
                    case 1:
                        ColorName = "���";
                        break;
                    case 2:
                        ColorName = "��Ȳ";
                        break;
                    case 3:
                        ColorName = "����";
                        break;
                    case 4:
                        ColorName = "�ʷ�";
                        break;
                    case 5:
                        ColorName = "�Ķ�";
                        break;
                    default:
                        ColorName = "???";
                        break;
                }

                hanoiTower.SelectText.text = $" {number}�� {ColorName} ���� ���� ��";

                MeshRenderer meshRenderer = HanoiTower.selectedDonut.GetComponent<MeshRenderer>();

                meshRenderer.material.EnableKeyword("_EMISSION");
                Color emissionColor = meshRenderer.material.color;
                meshRenderer.material.SetColor("_EmissionColor", emissionColor);
            }
        }
        else
            PushDonut(HanoiTower.selectedDonut);
    }

    public bool CheckRing(GameObject pushRing)
    {
        if (barStack.Count > 0)
        {
            int peekNumber = barStack.Peek().GetComponent<Donut>().donutNumber;
            int pushNumber = pushRing.GetComponent<Donut>().donutNumber;

            bool result = pushNumber < peekNumber ? true : false;
            if (!result)
            {
                hanoiTower.SelectText.text = $"�������� ������ {pushNumber}�̰�, �ش� ����� ������ ������ {peekNumber}�Դϴ�.";
                Debug.Log($"�������� ������ {pushNumber}�̰�, �ش� ����� ������ ������ {peekNumber}�Դϴ�.");
            }

            return result;
        }

        return true;
    }

    public void PushDonut(GameObject donut)
    {
        if (!CheckRing(donut))
        {
            return;
        }
   
        HanoiTower.isSelected = false;
        HanoiTower.selectedDonut = null;

        MeshRenderer meshRender = donut.GetComponent<MeshRenderer>();

        meshRender.material.DisableKeyword("_EMISSION");
        meshRender.material.SetColor("_EmissionColor", Color.white);
        hanoiTower.SelectText.text = $"";

        donut.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        donut.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        float distance = Vector3.Distance(donut.transform.position, transform.position);

        //Debug.Log(distance);
        if(distance > 1)
        {
            donut.transform.position = transform.position + Vector3.up * 5f;

            if(hanoiTower.GameStarted)
            {
                HanoiTower.score++;
                hanoiTower.MoveText.text = "Move : " + HanoiTower.score;
            }
            
        }
        
        barStack.Push(donut);
    }

    public GameObject OnPopRing()
    {
        if (barStack.Count > 0)
        {
            HanoiTower.isSelected = true;
            GameObject popObj = barStack.Pop();

            switch (barType)
            { 
                case BarType.LEFT:
                    hanoiTower.LastBar = 0;
                    break;
                case BarType.CENTER:
                    hanoiTower.LastBar = 1;
                    break;
                case BarType.RIGHT:
                    hanoiTower.LastBar = 2;
                    break;
            }
 
            return popObj;
        }

        return null;
    }

}