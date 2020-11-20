using System;
using System.Collections.Generic;
using System.Text;

namespace Para_Stack
{
    class SortEL<Type> where Type : IComparable
    {
        Type val;
        SortEL<Type> next, prev;
        public SortEL(Type v)
        { val = v; next = null; prev = null; }
        public Type Value
        {
            get => val;
            set { val = value; }
        }
        public SortEL<Type> Next
        {
            get => next;
            set { next = value; }
        }
        public SortEL<Type> Prev
        {
            get => prev;
            set { prev = value; }
        }
    }
    class SortedClass<Type> where Type : IComparable
    {
        public SortEL<Type> first;
        int count = 0;




        public SortedClass() { first = null; }
        public void Add(Type k)
        {
            SortEL<Type> tmp;
            if (first == null) { first = new SortEL<Type>(k); count++; return; }
            if (first.Value.CompareTo(k) > 0)
            {
                tmp = new SortEL<Type>(k); count++;
                tmp.Next = first; first.Prev = tmp; first = tmp; return;
            }
            SortEL<Type> p1, p2;
            for (p1 = first, p2 = first; p2 != null && p2.Value.CompareTo(k) < 0; p1 = p2, p2 = p2.Next) ;
            tmp = new SortEL<Type>(k);
            tmp.Next = p2;
            tmp.Prev = p1;
            if (p2 != null)
                p2.Prev = tmp;
            p1.Next = tmp;
            count++;
        }
        public void Remove(Type k)
        {
            if (first == null) return;
            if (k.CompareTo(first.Value) == 0)
            {
                first = first.Next;
                first.Prev = null;
                count--; return;
            }
            SortEL<Type> t1 = first, t2 = first;
            for (; t1 != null && t1.Value.CompareTo(k) != 0; t2 = t1, t1 = t1.Next)
                if (t1 == null) return;
            t2.Next = t1.Next;
            if(t1.Next!=null)
            t1.Next.Prev = t2;
            count--;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index > count)
                throw new Exception("index out");
            if (index == 0)
            {
                first = first.Next;
                first.Prev = null;
                count--; return;
            }
            SortEL<Type> removeEl = first, prevEl = first;
            for (int j = 0; j <= index; j++)
            {
                prevEl= removeEl;
                removeEl = removeEl.Next;
            }
            prevEl.Next = removeEl.Next;
            if (removeEl.Next != null)
                removeEl.Next.Prev = prevEl;
            count--; return;
        }
        public int IndexOf(Type k)
        {
            SortEL<Type> temp = first;
            int index = -1;
            for (int i = 0; temp != null;temp=temp.Next, i++)
            {
                if (temp.Value.CompareTo(k) == 0) { index = i; return index; }
            }
            return index;
        }
        public void Clear()
        {
            first = null;
        }
        public Type GetMax()
        {
            SortEL<Type> temp = first;
            Type MAX = first.Value;
            for (; ;temp=temp.Next)
            {
                if (temp == null) { MAX = temp.Value; return MAX; }
            }
        }
        public Type GetMin()
        {
            if (first == null)
                return default(Type);
             return first.Value;
        }

        public static SortedClass<Type> operator +(SortedClass<Type> l1, SortedClass<Type> l2)
        {
            SortedClass<Type> Result = new SortedClass<Type>();
            SortEL<Type> temp1 = l1.first;
            SortEL<Type> temp2 = l2.first;
            for (; temp1 != null; Result.Add(temp1.Value), temp1 = temp1.Next) ;
            for (; temp2 != null; Result.Add(temp2.Value), temp2 = temp2.Next) ;

            return Result;
        }
        public int Count { get => count; }

        public void Print()
        {
            Console.WriteLine();
            for (SortEL<Type> p = first; p != null; p = p.Next)
                Console.Write(p.Value + " -> ");
            Console.WriteLine();
        }
    }
}
