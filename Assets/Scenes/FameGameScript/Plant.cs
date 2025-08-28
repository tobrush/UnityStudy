using System;
using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private enum PlantState { Level1, Level2, Level3 }
    private PlantState plantState;

    private DateTime startTime, growthTime, harvestTime; // 레벨이 변하는 시간 설정

    public int plantIndex; // 식물 넘버
    public bool isHarvest = false;

    void Awake()
    {
        startTime = DateTime.Now;
        growthTime = startTime.AddSeconds(5);
        harvestTime = startTime.AddSeconds(10);

        // DateTime.Now : 현재 시간을 활용한 방법
        // Time.time : 게임 실행 시간
        // Time.deltTime : 시간 조각
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

    // 예시. 날씨에 따라 성장 파라미터 변경
    private void SetGrowth(WeatherType weatherType)
    {
        switch (weatherType)
        {
            case WeatherType.Sun:
                // 성장 최대
                break;
            case WeatherType.Rain:
                // 성장 중간
                break;
            case WeatherType.Snow:
                // 성장 최소
                break;
        }
    }
}