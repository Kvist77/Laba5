using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba5
{
    class hookJeeves
    {
        static float[] X;
        static float _Z;
        static double FE;
       public static void Hook_Jeeves_Method()
        {
            Console.WriteLine("Метод Хука-Дживса");
            Console.WriteLine("Введите число переменных");
            int N = int.Parse(Console.ReadLine());
            X = new float[N];
            float[] B = new float[N];
            float[] Y = new float[N];
            float[] P = new float[N];
            Console.WriteLine("Введите начальную точку X1,X2,...XN ");
            for (int I = 0; I < N; I++)
                X[I] = float.Parse(Console.ReadLine());
            Console.WriteLine("Введите длину шага");
            float H = float.Parse(Console.ReadLine());
            float K = H, FI;
            for (int I = 0; I < N; I++)
            {
                Y[I] = X[I];
                P[I] = X[I];
                B[I] = X[I];
            }
            Z();
            FI = _Z;
            Console.WriteLine($"Начальные значения функции {_Z}");
            for (int I = 0; I < N; I++)
                Console.Write($"{X[I]}   ");
            Console.WriteLine();
            float PS = 0, BS = 1;
            //исследование вокруг базисной точки
            int J = 0;
            float FB = FI;
            do
            {
                X[J] = Y[J] + K;
                Z();
                if (_Z >= FI)
                {
                    X[J] = Y[J] - K;
                    Z();
                    if (_Z >= FI)
                        X[J] = Y[J];
                    else Y[J] = X[J];
                }
                else Y[J] = X[J];
                Z();
                FI = _Z;
                Console.WriteLine($"Исследующий поиск {_Z}");
                for (int I = 0; I < N; I++)
                    Console.Write($"{X[I]}   ");
                Console.WriteLine();
                if (J == N - 1)
                {
                    if (FI < FB - 1E-08)
                    {
                        for (int I = 0; I < N; I++)
                        {
                            P[I] = 2 * Y[I] - B[I];
                            B[I] = Y[I]; X[I] = P[I]; Y[I] = X[I];
                        }
                        FB = FI; PS = 1;
                        BS = 0; Z(); FI = _Z;
                        Console.WriteLine($"Поиск по образцу  {_Z}");
                        for (int I = 0; I < N; I++)
                            Console.Write($"{X[I]}  ");
                        Console.WriteLine();
                        J = 0;
                    }
                    else
                    {
                        if (PS == 1 && BS == 0)
                        {
                            for (int I = 0; I < N; I++)
                            {
                                P[I] = B[I];
                                Y[I] = B[I];
                                X[I] = B[I];
                            }
                            BS = 1;
                            PS = 0;
                            Z();
                            FI = _Z; FB = _Z;
                            Console.WriteLine($"Замена базисной точки {_Z}");
                            for (int I = 0; I < N; I++)
                                Console.Write($"{X[I]}  ");
                            Console.WriteLine();
                            J = 0;
                        }
                        else
                        {
                            K = K / 10;
                            Console.WriteLine("Уменьшить длину шага");
                            if (K <= 1E-08)
                                break;
                            J = 0;
                        }
                    }
                }
                else J = J + 1;
            } while (true);
            Console.WriteLine("\tМинимум найден");
            for (int I = 0; I < N; I++)
                Console.Write($"X{I + 1} = {P[I]}  ");
            Console.WriteLine();
            Console.WriteLine($"Минимум функции равен {FB}");
            Console.WriteLine($"Количество вычислений равно {FE}");
        }
        static void Z()
        {
            FE = FE + 1;
            _Z = (float)(100 * Math.Pow(X[1] - Math.Pow(X[0], 2), 2) + Math.Pow(1 - X[0], 2));
            
        }

    }
}
