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
                        ColorName = "노랑";
                        break;
                    case 2:
                        ColorName = "주황";
                        break;
                    case 3:
                        ColorName = "빨강";
                        break;
                    case 4:
                        ColorName = "초록";
                        break;
                    case 5:
                        ColorName = "파랑";
                        break;
                    default:
                        ColorName = "???";
                        break;
                }

                hanoiTower.SelectText.text = $" {number}번 {ColorName} 도넛 선택 됨";

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
                hanoiTower.SelectText.text = $"넣으려는 도넛은 {pushNumber}이고, 해당 기둥의 마지막 도넛은 {peekNumber}입니다.";
                Debug.Log($"넣으려는 도넛은 {pushNumber}이고, 해당 기둥의 마지막 도넛은 {peekNumber}입니다.");
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