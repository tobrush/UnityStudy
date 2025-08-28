using System;
using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private enum PlantState { Level1, Level2, Level3 }
    private PlantState plantState;

    private DateTime startTime, growthTime, harvestTime; // ������ ���ϴ� �ð� ����

    public int plantIndex; // �Ĺ� �ѹ�
    public bool isHarvest = false;

    void Awake()
    {
        startTime = DateTime.Now;
        growthTime = startTime.AddSeconds(5);
        harvestTime = startTime.AddSeconds(10);

        // DateTime.Now : ���� �ð��� Ȱ���� ���
        // Time.time : ���� ���� �ð�
        // Time.deltTime : �ð� ����
    }

    void OnEnable()
    {
        WeatherSystem.weatherAction += SetGrowth;
    }

    void OnDisable()
    {
        WeatherSystem.weatherAction -= SetGrowth;
    }

    IEnumerator Start()
    {
        SetState(PlantState.Level1);

        while (plantState != PlantState.Level3)
        {
            if (DateTime.Now >= harvestTime)
            {
                SetState(PlantState.Level3);
                isHarvest = true;
            }
            else if (DateTime.Now >= growthTime)
            {
                SetState(PlantState.Level2);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void SetState(PlantState newState)
    {
        if (plantState != newState || plantState == PlantState.Level1)
        {
            plantState = newState;

            for (int i = 0; i < 3; i++)
                transform.GetChild(i).gameObject.SetActive(false);

            transform.GetChild((int)plantState).gameObject.SetActive(true);
        }
    }

    // ����. ������ ���� ���� �Ķ���� ����
    private void SetGrowth(WeatherType weatherType)
    {
        switch (weatherType)
        {
            case WeatherType.Sun:
                // ���� �ִ�
                break;
            case WeatherType.Rain:
                // ���� �߰�
                break;
            case WeatherType.Snow:
                // ���� �ּ�
                break;
        }
    }
}