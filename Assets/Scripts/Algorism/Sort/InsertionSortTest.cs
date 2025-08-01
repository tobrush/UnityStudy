using UnityEngine;

//✅ 삽입 정렬 (Insertion Sort) →** O(n²) = 이미 정렬된 부분에 새로운 값을 "삽입"해서 위치 맞추기

public class InsertionSortTest : MonoBehaviour
{
    private int[] array = { 5, 2, 1, 8, 3, 7, 6, 4 };

    void Start()
    {
        Debug.Log("정렬 전: " + string.Join(", ", array));

        InsertionSort(array);
        Debug.Log("정렬 후: " + string.Join(", ", array));
    }

    void InsertionSort(int[] arr)
    {
        int n = arr.Length;

        for (int i = 1; i < n; i++)  // i = 1부터 시작
        {
            int key = arr[i]; // 꺼낸 값 (비교 대상)
            int j = i - 1;  // 정렬된 구간의 끝 인덱스

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];// 큰 값을 한 칸 뒤로 밀기
                j--;
            }

            arr[j + 1] = key;  // 알맞은 위치에 key 삽입
        }
    }
}