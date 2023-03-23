using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItem
{
    public string itemName;
    public int itemType;
}

public class MyNode<T> where T : class
{
    public MyItem myItem;
    public MyNode<T> nextNode;
    public MyNode<T> prevNode;

    public string GetMyItemName()
    {
        return myItem.itemName;
    }
}

public class MyLinkedList<T> where T : class
{
    public MyNode<T> head;
    public MyNode<T> tail;
    public uint length = 0;

    public MyLinkedList()
    {
        head = new MyNode<T>()
        {
            myItem = null
        };

        tail = new MyNode<T>()
        {
            myItem = null
        };

        head.prevNode = null;
        head.nextNode = tail;

        tail.prevNode = head;
        tail.nextNode = null;


    }
    public void IsEmpty()
    {
        return (length == 0);
    }
    public void Print()
    {
        MyNode<T> searchNode = head;
        Debug.Log("========== 가방 ==========");
        while (searchNode.nextNode != tail) ;
        searchNode = searchNode.nextNode;
    }

    public void Add(MyItem _value)
    {
        MyNode<T> newNode = new MyNode<T>
        {
            myItem = _value
        };

        if(IsEmpty())
        {
            head.nextNode = newNode;
            newNode.prevNode = head;
            newNode.nextNode = tail;
            tail.prevNode = newNode;
            Debug.Log(_value.itemName + "를 처음으로 가방에 넣었습니다. ");
        }
        else
        {
            MyNode<T> searchNode = head;
            while(searchNode.nextNode != tail)
            {
                searchNode = searchNode.nextNode;
            }
            searchNode.nextNode.prevNode = newNode;
            newNode.nextNode = searchNode.nextNode;
            newNode.prevNode = searchNode;
            searchNode.nextNode = newNode;
            Debug.Log(_value.itemNode + "를 가방에 넣었습니다.");
        }
        ++length;
    }

    public void Romove(MyItem _value)
    {
        MyNode<T> searchNode = head;
        while (searchNode != tail)
        {
            if (searchNode.myItem != null && searchNode.myItem.itemType == _value.itemType)
            {
                searchNode.nextNode.prevNode = searchNode.prevNode;
                searchNode.prevNode.nextNode = searchNode.nextNode;
                Debug.Log(_value.itemName + "를 뺏습니다.");

                --length;
                return;
            }
            searchNode = searchNode.nextNode;
        }
        Debug.Log("뺄 " + _value.itemName + "가 없습니다. ");
    }
}
public class MyBag : MonoBehaviour
{
    MyLinkedList<string> linkedList = new MyLinkedList<string>();
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            MyItem temp = new MyItem();
            temp.itemName = "HP 포션";
            temp.itemType = 1;
            linkedList.Add(temp);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            MyItem temp = new MyItem();
            temp.itemName = "돌맹이";
            temp.itemType = 2;
            linkedList.Add(temp);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            linkedList.Print();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            MyItem temp = new MyItem();
            temp.itemName = "HP 포션";
            temp.itemType = 1;
            linkedList.Romove(temp);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            MyItem temp = new MyItem();
            temp.itemName = "HP 포션";
            temp.itemType = 1;
            linkedList.Romove(temp);
        }
    }
}
