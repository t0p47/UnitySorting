using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : P_AbstractSort {

    private int[] array;
    private int length;

    public void sort(int[] inputArr)
    {

        if (inputArr == null || inputArr.Length == 0)
        {
            return;
        }
        this.array = inputArr;
        length = inputArr.Length;
        quickSort(0, length - 1);
    }

    private void quickSort(int lowerIndex, int higherIndex)
    {

        int i = lowerIndex;
        int j = higherIndex;
        // calculate pivot number, I am taking pivot as middle index number
        int pivot = array[lowerIndex + (higherIndex - lowerIndex) / 2];
        // Divide into two arrays
        while (i <= j)
        {
            /**
             * In each iteration, we will identify a number from left side which 
             * is greater then the pivot value, and also we will identify a number 
             * from right side which is less then the pivot value. Once the search 
             * is done, then we exchange both numbers.
             */
            while (array[i] < pivot)
            {
                i++;
            }
            while (array[j] > pivot)
            {
                j--;
            }
            if (i <= j)
            {
                exchangeNumbers(i, j);
                //yield return new WaitForSeconds(2f);
                Debug.Log("After WaitForSeconds");
                //move index to next position on both sides
                i++;
                j--;
            }
        }
        // call quickSort() method recursively
        if (lowerIndex < j)
            quickSort(lowerIndex, j);
        if (i < higherIndex)
            quickSort(i, higherIndex);

        /*for (int l = 0; l < arrayPlaces.Count; l++)
        {
            Debug.Log("Sorted TmpIntArray: index " + l + ", value " + tmpIntArr[l]);
        }*/
    }

    private void exchangeNumbers(int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
        //changeBallsPlace(arrayBalls[i].transform, arrayBalls[j].transform);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            for (int i = 0; i < arrayPlaces.Count; i++)
            {
                //Debug.Log("Shuffled TmpIntArray: index " + i + ", value " + tmpIntArr[i]);
                //arrayBalls[tmpIntArr[i]].transform.position = arrayPlaces[i].transform.position;
            }
            sort(tmpIntArr);
            Debug.Log("Sorted");
            for (int i = 0; i < arrayPlaces.Count; i++)
            {
                Debug.Log("Sorted TmpIntArray: index " + i + ", value " + tmpIntArr[i]);
                //arrayBalls[tmpIntArr[i]].transform.position = arrayPlaces[i].transform.position;
            }

        }
    }

    /*public static void main(String a[])
    {

        MyQuickSort sorter = new MyQuickSort();
        int[] input = { 24, 2, 45, 20, 56, 75, 2, 56, 99, 53, 12 };
        sorter.sort(input);
        for (int i:input)
        {
            System.out.print(i);
            System.out.print(" ");
        }
    }*/
}
