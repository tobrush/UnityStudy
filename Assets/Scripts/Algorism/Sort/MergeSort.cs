using UnityEngine;

//✅병합 정렬 (Merge Sort) →** O(nlogn) = 계속해서 반으로 나누고 정렬한 다음 합치는 알고리즘 // 고성능 정렬 알고리즘

public class MergeSort : MonoBehaviour
{
    private int[] array = { 5, 2, 1, 8, 3, 7, 6, 4 };

    void Start()
    {
        Debug.Log("정렬 전: " + string.Join(", ", array));

        MSort(array, 0, array.Length - 1);
        Debug.Log("정렬 후: " + string.Join(", ", array));
    }

    void MSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;

            MSort(arr, left, mid);   // 왼쪽 반 정렬
            MSort(arr, mid + 1, right); // 오른쪽 반 정렬

            Merge(arr, left, mid, right); // 정렬된 두 배열 병합
        }
    }

    void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int i = 0; i < n1; i++)
            L[i] = arr[left + i];

        for (int j = 0; j < n2; j++)
            R[j] = arr[mid + 1 + j];


        int i1 = 0;
        int i2 = 0;
        int k = left;

        /*
        while (i1 < n1 && i2 < n2)
            arr[k++] = (L[i1] <= R[i2]) ? L[i1++] : R[i2++];
        while (i1 < n1)
            arr[k++] = L[i1++];
        while (i2 < n2)
            arr[k++] = R[i2++];
        */

        while (i1 < n1 && i2 < n2)
        {
            if (L[i1] <= R[i2])
            {
                arr[k] = L[i1];
                i1++;
            }
            else
            {
                arr[k] = R[i2];
                i2++;
            }
            k++;
        }
        while (i1 < n1)
        {
            arr[k] = L[i1];
            i1++;
            k++;
        }

        while (i2 < n2)
        {
            arr[k] = R[i2];
            k++;
            i2++;
        }

    }
}