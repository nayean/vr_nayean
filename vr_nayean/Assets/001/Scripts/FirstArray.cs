using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstArray : MonoBehaviour
{

    //�迭�� �����Ѵ�.
    public int[] numbers = new int[] { 9, -11, 6, -12, 1 };
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(numbers[0]); //�迭�� 0��° ��Ҹ� Logâ�� �����ش�.
    }
}
