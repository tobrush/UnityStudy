using UnityEngine;

public class BinarySearch : MonoBehaviour
{
    private int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // ���ĵ� �����͸� Ž�� ����
    private int target = 7;

    void Start()
    {
        int result = BSearch(); // Target�� �ε��� ��
        Debug.Log($"{target}�� {result}��°�� �ֽ��ϴ�.");
    }


    //���� Ž�� (Binary Search) �� O(logn) = �߰� �� �������� �� �� �پ��� Ž��
    private int BSearch()
    {
        int left = 0; // ó�� Left
        int right = array.Length - 1; // ó�� Right

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