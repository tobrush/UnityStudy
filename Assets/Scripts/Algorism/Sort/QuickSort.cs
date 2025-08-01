using UnityEngine;


//�� ���� (Quick Sort) �� O(nlogn) = �������� ��Ƽ� �����ϰ� �ٽ� �������� ��� �˰��� //  �⺻ ���� �Լ��� ä�õ� ������ ������ ����

public class QuickSort : MonoBehaviour
{
    private int[] array = { 5, 2, 1, 8, 3, 7, 6, 4 };

    void Start()
    {
        Debug.Log($"���� �� : {string.Join(", ", array)}");
        QSort(array, 0, array.Length - 1);
        Debug.Log($"���� �� : {string.Join(", ", array)}");
    }

    void QSort(int[] arr, int left, int right)
    {
        if (left < right) // element�� 1�� ���� ������
        {
            int pivot = Partition(array, left, right);

            QSort(arr, left, pivot - 1);
            QSort(arr, pivot + 1, right);
        }
    }

    int Partition(int[] arr, int left, int right) // ���� �� �����ϴ� ���
    {
        int pivot = arr[right];
        int index = left - 1;

        for (int i = left; i < right; i++)
        {
            if (arr[i] < pivot)
            {
                index++;

                int temp = arr[i];
                arr[i] = arr[index];
                arr[index] = temp;
            }
        }

        int temp2 = arr[index + 1];
        arr[index + 1] = arr[right];
        arr[right] = temp2;

        return index + 1;
    }
}