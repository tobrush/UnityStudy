using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int x, y;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI cellText;

    public void SetText(string text)
    {
        cellText.text = text;
    }

    public void SetUp(int x, int y, Action<int, int> onCellClicked)
    {
        this.x = x;
        this.y = y;
        button.onClick.AddListener(() => onCellClicked(x, y));
    }
}