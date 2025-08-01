using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HanoiTower : MonoBehaviour
{
    public TextMeshProUGUI SelectText;

    public TextMeshProUGUI MoveText;
    public static int score = 0;
    public enum HanoiLevel { LV1 = 3, LV2, LV3 }
    public HanoiLevel hanoiLevel = HanoiLevel.LV1;

    public Transform boardTf;
    public GameObject[] donutPrefabs;

    public BoardBar[] bars;

    public static GameObject selectedDonut;
    public static bool isSelected;

    public bool GameStarted = false;

    public int LastBar;

    IEnumerator Start()
    {
        MoveText.text = "Move : 0";

        for (int i = (int)hanoiLevel - 1; i >= 0; i--)
        {
            var donut = Instantiate(donutPrefabs[i]);
            bars[0].PushDonut(donut);
            yield return new WaitForSeconds(0.2f);
        }

        GameStarted = true;
     
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HanoiAnswer();
        }
    }

    public void HanoiAnswer()
    {
        //HanoiRoutine((int)hanoiLevel, 0, 1, 2);
        StartCoroutine(HanoiCoroutine((int)hanoiLevel, 0, 1, 2));
    }

    public IEnumerator HanoiCoroutine(int n, int from, int temp, int to)
    {
        if (n == 1)
        {
            bars[from].Action();
            yield return new WaitForSeconds(0.5f);
            bars[to].Action();
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return StartCoroutine(HanoiCoroutine(n - 1, from, to, temp));
            bars[from].Action();
            yield return new WaitForSeconds(0.5f);
            bars[to].Action();
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(HanoiCoroutine(n - 1, temp, from, to));
        }
    }

    private void HanoiRoutine(int n, int from, int temp, int to)
    {
        if (n == 1)
        {
            Debug.Log($"{n}번 도넛을 {from}에서 {to}로 이동");
        }
        else
        {
            HanoiRoutine(n - 1, from, to, temp);
            Debug.Log($"{n}번 도넛을 {from}에서 {to}로 이동");
            HanoiRoutine(n - 1, temp, from, to);
        }
    }


}