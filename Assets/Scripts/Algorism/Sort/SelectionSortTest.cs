using UnityEngine;

//✅선택 정렬 (Selection Sort) → O(n²)

//시간 복잡도 O(n²) → 느림(버블 정렬과 유사) 
// 정렬 대상이 많아지면 성능이 급격히 저하됨

public class SelectionSortTest : MonoBehaviour
{
    private int[] array = { 5, 2, 1, 8, 3, 7, 6, 4 };

    void Start()
    {
        Debug.Log("정렬 전: " + string.Join(", ", array));

        SelectionSort(array);
        Debug.Log("정렬 후: " + string.Join(", ", array));
    }

    void SelectionSort(int[] arr)
    {
        int n = arr.Length; //n은 전체갯수(배열 길이 저장) n=8

        for (int i = 0; i < n - 1; i++)  //배열의 앞에서부터 n-1번째까지 반복 (마지막 원소는 자동 정렬됨) n-1 = 7보다 크지않으니 0~6
        {
            int minIdx = i; //현재 i번째를 최솟값 위치 후보로 설정

            for (int j = i + 1; j < n; j++) //1~7
            {
                if (arr[j] < arr[minIdx])
                    minIdx = j; //현재 위치 i 이후의 원소들을 살펴보며, 더 작은 값을 찾으면 minIdx를 갱신
            }

            int temp = arr[i]; //최솟값과 현재 위치의 값을 swap (교환)
            arr[i] = arr[minIdx];
            arr[minIdx] = temp;
        }
    }
}
