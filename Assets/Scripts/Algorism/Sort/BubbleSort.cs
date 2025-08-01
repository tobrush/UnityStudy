using UnityEngine;

//버블 정렬 (Bubble Sort) → O(n²)

public class BubbleSort : MonoBehaviour
{
    private int[] array = { 5, 2, 1, 8, 3, 7, 6, 4 };

    void Start()
    {
        Debug.Log($"정렬 전 : {string.Join(", ", array)}");

        BSort(array);
        Debug.Log($"정렬 후 : {string.Join(", ", array)}");
    }

    void BSort(int[] arr)
    {
        int n = arr.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }


    //✅ 개선형 버블 정렬 O(n)
    void OptimizedBubbleSort(int[] arr)
    {
        int n = arr.Length;

        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;  // swap이 발생했는지 체크

            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // swap
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;

                    swapped = true; // 변경 발생 기록
                }
            }

            // 이 회차에 아무것도 바뀌지 않았으면 정렬 완료!
            if (!swapped)
                break;
        }
    }
}