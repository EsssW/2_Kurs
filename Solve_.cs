using System;

namespace Delegate_DZ_1
{
    class Solve_
    {

        public delegate double Function(double d);
        public delegate double Solve(double a, double b, double eps, Function f, ref int count);

        public static double fun(double x) => (Math.Pow(x, 3) + 2 * Math.Pow(x, 2) - 6 * x + 2);
        public static double Pfun(double x) => 3*Math.Pow(x, 2) + 4 * x - 6 ;

        // Solve Type
        public static double method_chord(double A, double B, double e, Function f, ref int count)
        {
            double tmp, res = 0;
            count = 0;
            do
            {
                count++;
                tmp = res;
                res = B - f(B) * (A - B) / (f(A) - f(B));
                A = B;
                B = tmp;
            } while (Math.Abs(res - B) > e);

            return res;
        }
        public static double method_half(double A, double B, double e, Function f,ref int count)
        {
            double res = B;
            count = 0;
            do
            {
                count++;
                res = (A+B) / 2;
                if (f(A) * f(res) >= 0) A=res;
                else B = res;
            }
            while (Math.Abs(f(res)) > e);
            return res;
        }
        public static double method_tangent(double A, double B, double e, Function f, ref int count)
        {
            double res;
            if(f(A) *Pfun(A) > 0)
            {
                Console.WriteLine($"Условие на сходимость выполненно для x=a={A}");
                res = A;
            }
            else
            {
                if(f(B)*Pfun(B)>0)
                {
                    Console.WriteLine($"Условие на сходимость выполненно для x=b={B}");
                    res = B;
                }
                else
                {
                    Console.WriteLine("Условие сходисоти не выполненно");
                    res = -10E10;
                }
            }
            if (res > -10E10)
            {
                count = 0;
                while (true)
                {
                    res -= f(res) / Pfun(res);
                    count++;
                    if (Math.Abs(f(res)) < e) break;
                    
                }
            }
            return res;

        }

        static void Main(string[] args)
        {
            Solve _solve = null;
            Function _func;

            Console.WriteLine("Введите левую точку отерзка: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите правую точку отерзка: ");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите приближение: ");
            double eps = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("\n\nВыбере способ решения функции: ");
            Console.WriteLine("\t1)Метод Хорд");
            Console.WriteLine("\t2)Метод деления пополам");
            Console.WriteLine("\t3)Метод Касательной(Ньютона)");
            Console.WriteLine("\t4)Выход");
            int solve_num = Convert.ToInt32(Console.ReadLine());
            switch (solve_num)
            {
                case 1: _solve = method_chord; break;
                case 2: _solve = method_half; break;
                case 3: _solve = method_tangent; break;
                case 4: Environment.Exit(0); break;
            }
            _func = fun;

            int count = 0;
            int method_chord_Count = 0;
            int method_half_Count = 0;
            int method_tangent_Count = 0;
            Console.WriteLine("X="+ _solve(a, b, eps, _func, ref count)+ "Число итераций="+ count);
        }
    }
}
