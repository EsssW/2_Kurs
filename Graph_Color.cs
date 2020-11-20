using System;

namespace Graph_Color
{
    public class Program
    {
        // Осноновная функция. Возвращает массив цветов
        public static int[] GetColors(int[,] matrix)
        {
            // массив цветов
            int[] colors = new int[matrix.GetLength(0)];
            // изначально все вершины не окрашены 0 (т.е. =0)
            for (int i = 1; i < colors.Length; i++) colors[i] = 0;
            // первую вершину окрашиваем в 1й цвет
            colors[0] = 1;
            // проходим по всем вершинам начиная со 2й тк 1я уже верно окрашена
            for( int index=1; index >= 1 && index < colors.Length; )
            {
                // если следющая вершина выходит за пределы цветов делаем его равным 0
                if (colors[index] + 1 > colors.Length)  colors[index--] = 0; 
                else  // иначе нынешнеей вершине присваиваем цвет отлиынй от предыдущей
                {
                    colors[index] = colors[index] + 1;
                    // если текущая вершина более не смежна не содной и различна цветами со всеми смежными вершинами
                    if (!CheckАdjacency(index,colors,matrix))  index++;
                }
            }
            return colors;
        }
        // Функция для проверка смежности заданной вершины с остальными и сходства цветов этих вершин
        public static bool CheckАdjacency(int PoinIndex,int[] colors,int[,] matrix)
        {
            bool f = false;
            for (int i = 0; i < matrix.GetLength(0); i++)
                if (colors[PoinIndex] == colors[i] && matrix[PoinIndex, i] == 1) { f = true; break; }
            return f;
        }

        static void Main(string[] args)
        {
            int[,] matrix =
            {  { 0,1,0,0,0 },
               { 1,0,1,1,0 },
               { 0,1,0,1,1},
               { 0,1,1,0,1 },
               { 0,0,1,1,0 }  };
            int[] colors = GetColors(matrix); // сохраняем массив цветов для нашего графа
            for (int i = 0; i < colors.Length; i++)
                Console.WriteLine($"Вершина[{i+1}] имеет цвет:{colors[i]} ");
        }

    }

    
}
