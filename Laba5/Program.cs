using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите метод:\n1) Метод покоординатного спуска\n" +
                "2)Метод Хука-Дживса\n3) Метод Нелдера-Мида");
            int met = int.Parse(Console.ReadLine());
            switch(met)
            {
                case 1:
                    Method_Coordinate_Descent();
                    break;
                case 2:
                    hookJeeves hookJ = new hookJeeves();
                    hookJeeves.Hook_Jeeves_Method();
                    break;
                case 3:
                    NelderMid nelderM = new NelderMid();
                    NelderMid.Nelder_Mead_Method();
                    break;
                default:
                    return;
                    
            }
            Console.ReadKey();
        }
        static double[] A;
        static void Method_Coordinate_Descent()
        {
            Console.WriteLine("Минимизация функции n переменных методом покоординатного спуска");
            Console.Write("Задайте точность результата E = ");
            double E = double.Parse(Console.ReadLine());
            Console.Write("Задайте число переменных N = ");
            int N = int.Parse(Console.ReadLine());
            A = new double[N];
            Console.WriteLine("Задайте исходную точку поиска");
            for (int I = 0; I < N; I++)
            {
                Console.Write($"X{I + 1} = ");
                A[I] = double.Parse(Console.ReadLine());
            }
            int iter = 0;
            for (int J = 0; J < N; J++)
            {
                iter++;
                double Prev = A[J];
                A[J] = Gold_Section_Method(A[J], 5, E, J);
                if (Math.Abs(A[J] - Prev) <= E)
                    break;
                if (J == N - 1)
                    J = -1;
            }
            Console.WriteLine();
            Console.WriteLine($"Количество итераций: {iter}");
            for (int I = 0; I < N; I++)
                Console.WriteLine($"X{I + 1} = {A[I]}");
        }

        static double F()
        {

            return Math.Pow(A[0] - 1, 2) + Math.Pow(A[1] - 2, 2) + Math.Pow(A[2] - 3, 2);
        }

        static double Gold_Section_Method(double a, double b, double e, int index)
        {
            double T1, T2;
            T1 = 0.3819660113;
            T2 = 1 - T1;
            double X0, X1, X2, X3;
            X0 = a; X1 = a + T1 * (b - a);
            X2 = a + T2 * (b - a); X3 = b;
            A[index] = X1;
            double F1, F2, I;
            F1 = F();
            A[index] = X2; F2 = F();
            do
            {
                if (F1 < F2)
                {
                    I = X2 - X0; X3 = X2; X2 = X1; X1 = X0 + T1 * I;
                    F2 = F1; A[index] = X1; F1 = F();
                }
                else
                {
                    I = X3 - X1; X0 = X1; X1 = X2; X2 = X0 + T2 * I;
                    F1 = F2; A[index] = X2; F2 = F();
                }
            } while (I > e);
            return X1;
        }

    }

}

