using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spisok
{
    class Element
    {
        int x;
        Element next;
        public Element()
        {
            x = 0;
            next = null;
        }
        public Element(int k)
        {
            x = k;
            next = null;
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public Element Next
        {
            get { return next; }
            set { next = value; }
        }
    }

    //класс список
    class Spisok
    {
        Element first;
        public Spisok()
        {
            first = null;
        }
        //добавление элемента в конец списка
        public void Append(int k)
        {
            Element p = first;
            if (first == null)
            {
                first = new Element(k);
                p = first;
            }
            else
            {
                while (p.Next != null)
                {
                    p = p.Next;
                }
                p.Next = new Element(k);
            }
        }
        //печать элементов списка
        public void Print()
        {
            Console.WriteLine("=============");
            Element p = first;
            while (p != null)
            {
                Console.Write(p.X + "->");
                p = p.Next;
            }
            Console.WriteLine();
        }

        // индекс элемента с заданным значением k (или -1, если нет)
        public int IndexOf(int k)
        {
            Element p = first;
            for (int i = 0; p != null; i++, p = p.Next)
                if (p.X == k)
                    return i;
            return -1;
        }

        //добавление элемента k в позицию pos
        public void Insert(int pos, int k)
        {
            Element p = first, prev = first;
            if (pos == 0)
            {
                first = new Element(k);
                first.Next = p;
            }
            else
            {
                int i;
                for (i = 0; i < pos; i++)
                {
                    prev = p;
                    p = p.Next;
                    if (p == null)
                        break;
                }
                prev.Next = new Element(k);
                prev.Next.Next = p;

            }
        }

        // удаление первого элемента со значением k (true - если удален, false - если такого эл-та нет)
        public bool Remove(int k)
        {
            Element prev = first, p = first;
            if (first.X == k)
            {
                first = first.Next;
                return true;
            }
            for (; p != null; prev = p, p = p.Next)
                if (p.X == k)
                {
                    prev.Next = p.Next;
                    return true;
                }
            return false;
        }
        //подсчет кол-ва элементов с заданным значением k
        public int Count(int k)
        {
            int kol = 0;
            for (Element p = first; p != null; p = p.Next)
                if (p.X == k)
                    kol++;
            return kol;
        }
        //очищение списка
        public void Clear()
        {
            first = null;
        }

        //копия списка
        public Spisok Copy()
        {
            Spisok sp = new Spisok();
            for (Element p = first; p != null; p = p.Next)
                sp.Append(p.X);
            return sp;
        }

        //добавление в конец списка другого списка
        public void Extends(Spisok sp)
        {
            Element p = sp.first;
            for (; p != null; p = p.Next)
                Append(p.X);
        }

        //переворот списка
        public void Reverse()
        {
            Element p1 = first, p2 = p1.Next, p3 = first;
            p1.Next = null;
            while (p2 != null)
            {
                p3 = p2.Next;
                p2.Next = p1;
                p1 = p2;
                p2 = p3;
            }
            first = p1;
        }
        //удаление последнего эл-та списка и возвращение его значения
        public int Pop()
        {
            if (first == null)
                throw new Exception("empty list!");
            if (first.Next == null)
            {
                int k1 = first.X;
                first = null;
                return k1;
            }
            Element p = first, prev = first;
            for (; p.Next != null; p = p.Next) ;
            int k = p.X;
            prev.Next = null;
            return k;
        }

        public int Min()
        {
            int min = first.X;
            Element p = first.Next;
            for (; p != null; p = p.Next)
                if (p.X < min)
                    min = p.X;
            return min;
        }

        public int Max()
        {
            int max = first.X;
            Element p = first.Next;
            for (; p != null; p = p.Next)
                if (p.X > max)
                    max = p.X;
            return max;
        }
        //сравнение 2ух списков
        public static bool operator ==(Spisok sp1, Spisok sp2)
        {
            Element p1 = sp1.first, p2 = sp2.first;
            for (; p1 != null && p2 != null; p1 = p1.Next, p2 = p2.Next)
                if (p1.X != p2.X)
                    return false;
            if (p1 == p2)
                return true;
            return false;
        }
        public static bool operator !=(Spisok sp1, Spisok sp2)
        {
            if (sp1 == sp2)
                return false;
            return true;
        }
        //содержится ли заданный список в исходном
        public bool Contains(Spisok sp)
        {
            Element p = first, p1 = first, p2 = sp.first;
            while (true)
            {
                for (; p1 != null && p1.X != sp.first.X; p1 = p1.Next) ;
                if (p1 != null)
                {
                    p2 = sp.first;
                    p = p1;
                    for (; p1 != null && p2 != null; p1 = p1.Next, p2 = p2.Next)
                        if (p1.X != p2.X)
                        {
                            p = p1 = p.Next;
                            p2 = sp.first;
                            break;
                        }
                    if (p2 == null)
                        return true;


                }
                else
                    return false;
            }
            return false;
        }




















        //СОРТИРОВКА СПИСКА
        public void Sort()
        {
            Spisok sp = Copy();
            Spisok sp2 = Copy();
            Clear();
            Element p = sp2.first;
            for (; p != null; p = p.Next)
            {
                int k = sp.Min();
                if (sp.Remove(sp.Min()))
                    Append(k);
            }
        }
        //ПОРАЗРЯДНАЯ СОРТИРОВКА СПИСКА
        public void RadixSort()
        {
            int k = 1; //разрядность
            Spisok sp = Copy();
            Clear();
            Element p = sp.first;
            for (; p != null; p = p.Next)
            {
                int c = p.X;
                int i = 1;
                for (i = 1; c / 10 >= 1; i++)
                    c /= 10;
                if (i > k)
                    k = i;
            }
            p = sp.first;
            for (int i = 1; i <= k; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (; p != null; p = p.Next)
                    {
                        if ((p.X % ((int)Math.Pow(10, i))) / ((int)Math.Pow(10, i - 1)) == j)
                            Append(p.X);
                    }
                    p = sp.first;
                }
                sp = Copy();
                p = sp.first;
                if (i < k)
                    Clear();
            }
        }










    }


    class Program
    {
        static void Main(string[] args)
        {
            Spisok spisok = new Spisok();
            string s = "";
            while ((s = Console.ReadLine()) != "")
            {
                int k = int.Parse(s);
                spisok.Append(k);

            }
            spisok.Print();
            //spisok.Sort();
            //spisok.Print();
            spisok.RadixSort();
            spisok.Print();
            //Spisok sp1 = new Spisok();
            //sp1.Append(1);
            //sp1.Append(2);
            //if (spisok.Contains(sp1))
            //    Console.WriteLine("Yes!");
            //else
            //    Console.WriteLine("No");

            //перевод числа в двоичную систему счисления
            //Spisok spis = new Spisok();
            //int x = 100;
            //while (x > 0)
            //{
            //    spis.Insert(0, x % 2);
            //    x /= 2;
            //}
            //spis.Print();
            //spis.Sort();
            //spis.Print();
        }
    }
}
