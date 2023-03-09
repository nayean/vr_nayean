using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstArray : MonoBehaviour
{

    //배열을 선언한다.
    public int[] numbers = new int[] { 9, -11, 6, -12, 1 };
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(numbers[0]); //배열의 0번째 요소를 Log창에 보여준다.
    }
}
