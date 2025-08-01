using UnityEngine;

public class LinearSearch : MonoBehaviour
{
    private int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public int target = 7;

    void Start()
    {
        LSearch(array, target);
    }


    //순차탐색 (Linear Search) → O(n) = 배열에서 순차적으로 값을 탐색
    private void LSearch(int[] arr, int t)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == t)
            {
                Debug.Log($"{t}은 {i}번째에 있습니다.");
            }
        }
    }
}