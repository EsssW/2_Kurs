using System;
using System.Collections.Generic;
using System.Text;

namespace Para_Stack
{
    class Element 
    {
        public int power { get; set; }
        public double kf { get; set; }
        public Element(double _kf,int _power)
        {
            power = _power;
            kf = _kf;
        }
        public Element() { }
        
    }
    
    class Polinom 
    {
        public List<Element> list = new List<Element>();
        public void makelist()
        {
            Element temp = new Element();
            Console.WriteLine("Введите полином по возрастанию степени | Для выхода ввыедите -1|");
            int power = 0, kf = 0;
            while (true)
            {
                Console.WriteLine("Введите степень");
                power = int.Parse(Console.ReadLine());
                if (power != -1)
                {
                    Console.WriteLine($"Введите коэфициент при X^{power}");
                    kf= int.Parse(Console.ReadLine());
                    temp.kf = kf;
                    temp.power = power;
                    list.Add(temp);
                }
                else return;
            }
        }
        public  void Add(Element temp )
        {
            list.Add(temp); 
        }
        public void Add(double kf,int power)
        {
            list.Add(new Element(kf,power));
        }
        public void Print()
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i < list.Count - 1)
                {
                    if (list[i].power == 0)
                        Console.Write($"{list[i].kf} + ");
                    else
                        if (list[i].kf == 0) i++;
                    else Console.Write($"{list[i].kf}X^{list[i].power} + ");
                }
                else
                {
                    if (list[i].power == 0) Console.Write($"{list[i].kf} + ");
                    else Console.Write($"{list[i].kf}X^{list[i].power}");
                }
            }
        }
        public static Polinom operator +(Polinom p1, Polinom p2)
        {
            //выбираем полином наибольшей и наименьшей степени
            Polinom maxPowerPoly = p1.list.Count <= p2.list.Count ? p2 : p1;
            Polinom minPowerPoly = p1.list.Count < p2.list.Count ? p1 : p2;

            //создаем массив для предварительного результата
            Polinom Result = new Polinom();

            Result.list=new List<Element>(maxPowerPoly.list.Count );

            //складываем члены начиная с нулевой до максимальной степени для полинома меньшей степени
            for (int i = 0; i < minPowerPoly.list.Count ; i++)
            {
                Result.Add(SumElement(minPowerPoly.list[i], maxPowerPoly.list[i]));
            }
            //дописываем старшие члены от полинома большей степени
            for (int i = minPowerPoly.list.Count; i < maxPowerPoly.list.Count ; i++)
            {
                Result.Add(maxPowerPoly.list[i]);
            }
            return Result;
        }
        // вспомогательный мето для сложения полиномов
        private static Element SumElement(Element el1, Element el2)
            => new Element(el1.kf + el2.kf, el1.power);

        public static Polinom operator *(Polinom p1, double digit)
        {
            Polinom npolinom = new Polinom();
            for (int i = 0; i < p1.list.Count; i++)
            {
                npolinom.Add(p1.list[i].kf * digit, p1.list[i].power);
            }
            return npolinom;
        }
        public static Polinom operator *(Polinom p1, Polinom p2)
        {
            //выбираем полином наибольшей и наименьшей степени
            Polinom maxPowerPoly = p1.list.Count <= p2.list.Count ? p2 : p1;
            Polinom minPowerPoly = p1.list.Count < p2.list.Count ? p1 : p2;

            //создаем массив для предварительного результата
            Polinom Result = new Polinom();

            Result.list = new List<Element>(maxPowerPoly.list.Count);

            //складываем члены начиная с нулевой до максимальной степени для полинома меньшей степени
            for (int i = 0; i < minPowerPoly.list.Count; i++)
            {
                Result.Add(MultElement(minPowerPoly.list[i], maxPowerPoly.list[i]));
            }
            //дописываем старшие члены от полинома большей степени
            for (int i = minPowerPoly.list.Count; i < maxPowerPoly.list.Count; i++)
            {
                Result.Add(maxPowerPoly.list[i]);
            }
            return Result;
        }
        // вспомогаельный метод для умножения полиномов
        private static Element MultElement(Element el1, Element el2)
            => new Element(el1.kf * el2.kf, el1.power);
        // Вычисление многочлена в определенной точке
        public double ResultInPoint(int point)
        {
            double sum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                sum += list[i].kf * Math.Pow(point, Convert.ToDouble(list[i].power));
            }
            return sum;
        }
        // Интегрирование полинома 
        public Polinom Integral()
        {
            Polinom integral = new Polinom();
            for (int i = 0; i < list.Count; i++)
            {
                integral.Add(Integral(list[i]));
            }
            return integral;
        }
        // Вспомогательный метод для интегрирования полинома
        private Element Integral(Element el)
            => new Element(Convert.ToDouble(el.kf / (el.power + 1)), el.power + 1);

        // Диффференцирование полинома
        public Polinom Differential()
        {
            Polinom differential = new Polinom();
            for (int i = 0; i < list.Count; i++)
            {
                differential.Add(Differential(list[i]));
            }
            return differential;
        }
        // Вспомогательный метод для диффиринцирвоания
        private Element Differential(Element el)
           => new Element(Convert.ToDouble(el.kf * el.power), el.power -1);

        

    }
}
