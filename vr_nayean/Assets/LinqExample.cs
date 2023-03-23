using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqExample : MonoBehaviour
{
    public List<int> numbersA = new List<int>();
    public List<int> numbersB = new List<int>();
    public List<int> except = new List<int>();
    public List<int> intersect = new List<int>();
    public List<int> union = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        numbersA.Add(1);
        numbersA.Add(2);
        numbersA.Add(5);
        numbersA.Add(8);

        numbersB.Add(1);
        numbersB.Add(3);
        numbersB.Add(5);
        numbersB.Add(7);
        numbersB.Add(9);

        except = numbersA.Except(numbersB).ToList();
        intersect = numbersA.Intersect(numbersB).ToList();
        union = numbersA.Union(numbersB).ToList();
    }
}
