using System;

namespace DAS;

public class LinkedList
{
    public Node? Head { get; set; }
    public Node? Tail { get; set; }
    public int Count { get; set; }

    public void AddLast(int val)
    {
        if (Head == null)
        {
            Head = new Node(val);
            Tail = Head;
            Count = 1;
        }
        else
        {
            Node temp = new Node(val);
            Tail.Next= temp;
            Tail = temp;
            Count++;
        }
    }

    public void AddFront(int val)
    {
        if (Head == null)
        {
            Head = new Node(val);
            Tail = Head;
            Count = 1;
        }
        else
        {
            Node temp = new Node(val);
            temp.Next = Head;
            Head = temp;
            Count++;
        }
    }
    public void RemoveFirst()
    {
        if(Head == null)
        {
            return;
        }
        else if(Head.Next == null)
        {
            Head = null;
            Tail = null;
            Count--;
        }
        else
        {
            Head = Head.Next;
            Count--;
        }
    }
    public void RemoveLast()
    {
        if (Head == null)
        {
            return;
        }
        else if (Head.Next == null)
        {
            Head = null;
            Tail = null;
            Count--;
        }
        else
        {
            Node temp = Head;
            while(temp.Next != Tail)
            {
                temp = temp.Next;
            }
            temp.Next = null;
            Tail = temp;
            Count--;
        }
    }

    public void InsertAt(int index, int val)
    {
        if(index < 0 || index > Count)
        {
            return;
        }
        if (index ==0)
        {
            AddFront(val);
        }
        if(index == Count)
        {
            AddLast(val);
        }
        else
        {
            Node? temp = Head;
            for(int i=0; i<index-1;i++)
            {
                temp = temp.Next;
            }
            Node newNode = new Node(val);
            newNode.Next = temp.Next;
            temp.Next = newNode;
            Count++;
        }
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index > Count)
        {
            return;
        }

        if (index == 0)
        {
            RemoveFirst();
        }

        else if (index == Count)
        {
            RemoveLast();
        }
        else
        {
            Node? temp = Head;
            for(int i=0; i<index-1; i++)
            {
                temp = temp.Next;
            }
            temp.Next = temp.Next.Next;
            Count--;
        }
    }

    public void Reverse()
    {
        if(Head == null)
        {
            return;
        }
        if(Head.Next == null)
        {
            return;
        }
        Node? prev = null;
        Node? curr = Head;
        Node? next = null;
        Tail = Head;

        while(curr != null)
        {
            next = curr.Next;
            curr.Next =prev;
            prev = curr;
            curr = next;
        }
        Head = prev;
    }

    public bool Search(int val)
    {
        Node? temp = Head;
        while(temp != null)
        {
            if(temp.Data == val)
            {
                //Node newNode = new Node(temp.Data);
                //return newNode.Data;
                return true;
            }
            temp = temp.Next;
        }
        return false;
    }

    public void Print()
    {
        Node? temp = Head;
        while (temp != null)
        {
            Console.WriteLine(temp.Data);
            temp = temp.Next;
        }
    }
}



public class Node
{
    public int Data { get; set; }
    public Node? Next { get; set; }
    public Node(int data)
    {
        Data = data;
        Next = null;
    }
}
