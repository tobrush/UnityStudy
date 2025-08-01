using UnityEngine;

public class BinarySearch : MonoBehaviour
{
    private int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // 정렬된 데이터만 탐색 가능
    private int target = 7;

    void Start()
    {
        int result = BSearch(); // Target의 인덱스 값
        Debug.Log($"{target}은 {result}번째에 있습니다.");
    }


    //이진 탐색 (Binary Search) → O(logn) = 중간 값 기준으로 반 씩 줄어드는 탐색
    private int BSearch()
    {
        int left = 0; // 처음 Left
        int right = array.Length - 1; // 처음 Right

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (array[mid] == target)
                return mid;
            else if (array[mid] < target)
                left = mid + 1;
            else // array[mid] > target
                right = mid - 1;
        }

        return -1;
    }

}