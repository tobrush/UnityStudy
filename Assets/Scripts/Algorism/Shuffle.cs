using UnityEngine;
using Random = UnityEngine.Random;

public class Shuffle : MonoBehaviour
{
    public int[] array = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public int[] array2 = new int[4] { 1, 2, 3, 4 };


    private void Start()
    {
        // ShuffleFunction();

        //Fibonacci();
        //Factorial();
        Permutation();
    }

    private void ShuffleFunction()
    {
        for (int i = 0; i < 100; i++)
        {
            int ranInt1 = Random.Range(0, array.Length);
            int ranInt2 = Random.Range(0, array.Length);

            Swap(ranInt1, ranInt2);

        }
    }
    public void Swap(int i, int j)
    {
        var temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    //피보나치 수열
    void Fibonacci()
    {
        for (int i = 0; i < 10; i++)
        {
            FibonacciFunction(i);
        }
    }

    //재귀함수
    private int FibonacciFunction(int n)
    {
        if(n <= 1)
            return n;

        return FibonacciFunction(n - 1) + FibonacciFunction(n - 2);
    }

    //계승
    void Factorial()
    {
        for (int i = 0; i < 10; i++)
        {
            int result = FactorialFunction(i);

            Debug.Log(result);
        }
    }

    //재귀함수
    private int FactorialFunction(int n)
    {
        if (n == 0)
            return 1;
        else
            return n * FactorialFunction(n - 1);
    }

    //순열 (모든경우의 수)
    void Permutation()
    {
        PermutationFunction(array2, 0);
    }

    void PermutationFunction(int[] arr, int start)
    {
        if(start == arr.Length)
        {
            Debug.Log(string.Join(",", arr));
            return;
        }

        for (int i = start; i < arr.Length; i++)
        {
            //swap
            var temp = arr[start];
            arr[start] = arr[i];
            arr[i] = temp;

            PermutationFunction(arr, start + 1);

            //원상복구
            temp = arr[start];
            arr[start] = arr[i];
            arr[i] = temp;
        }
    }
}
