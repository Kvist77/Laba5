using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba5
{
    class NelderMid
    {
        static float[] F;
        static float[] X;
        static float _Z;
        static float TEV;
       public static void Nelder_Mead_Method()
        {
            Console.WriteLine("Симплексный метод Нелдера-Мида");
            Console.WriteLine("Функция Z=F(X1,X2,...,XN) вычисляется в строке 5000");
            Console.WriteLine("Введите число переменных");
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine("Начальное приближение");
            float[,] S = new float[N + 1, N];
            for (int J = 0; J < N; J++)
            {
                S[0, J] = float.Parse(Console.ReadLine());
            }
            Console.WriteLine("Введите длину шага");
            float K = float.Parse(Console.ReadLine());
            //построить первый симплекс вокруг начальной точки
            for (int I = 1; I < N + 1; I++)
            {
                for (int J = 0; J < N; J++)
                {
                    if (J == I - 1)
                    {
                        S[I, J] = S[0, J] + K;
                        continue;
                    }
                    S[I, J] = S[0, J];
                }
            }
            Console.WriteLine("Введите Alfa,Beta,Gamma");
            float AL = float.Parse(Console.ReadLine());
            float BE = float.Parse(Console.ReadLine());
            float GA = float.Parse(Console.ReadLine());
            X = new float[N];
            float[] XH = new float[N];
            float[] XG = new float[N];
            float[] XL = new float[N];
            float[] XO = new float[N];
            float[] XR = new float[N];
            float[] XC = new float[N];
            float[] XE = new float[N];
            F = new float[N + 1];
            //вычислить значение функции
            for (int I = 0; I < N + 1; I++)
            {
                for (int J = 0; J < N; J++)
                {
                    X[J] = S[I, J];
                }
                Z1();
                F[I] = _Z;
            }
            //найти наибольшее и наименьшее значения
            //функции и соотвествующие им точки
            M620:
            int H = 0, L = 0;
            double FH = -1E+20, FL = 1E+20;
            for (int I = 0; I < N + 1; I++)
            {
                if (F[I] > FH)
                {
                    FH = F[I];
                    H = I;
                }
                if (F[I] < FL)
                {
                    FL = F[I];
                    L = I;
                }
            }
            //найти второе наибольшее значение и 
            //соответствующую ему точки
            double FG = -1E+20;
            int G = 0;
            for (int I = 0; I < N + 1; I++)
            {
                if (I == H)
                    continue;
                if (F[I] > FG)
                {
                    FG = F[I];
                    G = I;
                }
            }
            for (int J = 0; J < N; J++)
            {
                XO[J] = 0;
                for (int I = 0; I < N + 1; I++)
                {
                    if (I == H)
                        continue;
                    XO[J] = XO[J] + S[I, J];
                }
                //определить точки XO,XH,XG,XL
                XO[J] = XO[J] / N;
                XH[J] = S[H, J];
                XG[J] = S[G, J];
                XL[J] = S[L, J];
            }
            for (int J = 0; J < N; J++)
            {
                X[J] = XO[J];
            }
            Z1();
            float FO = _Z;
            Console.WriteLine("Вычислите центр тяжести в строке 1120");
            //далее выполните отражение
            for (int J = 0; J < N; J++)
            {
                XR[J] = XO[J] + AL * (XO[J] - XH[J]);
                X[J] = XR[J];
            }
            Z1();
            float FR = _Z;
            Console.WriteLine($"Выполните отражение в строке 1220  {_Z}");
            //если FR<FL,то производится растяжение
            if (FR < FL)
                goto M1300;
            if (FR > FG)
                goto M1600;
            goto M1520;
            M1300:
            for (int J = 0; J < N; J++)
            {
                XE[J] = GA * XR[J] + (1 - GA) * XO[J];
                X[J] = XE[J];
            }
            Z1();
            float FE = _Z;
            if (FE < FL)
                goto M1440;
            goto M1520;
            M1440:
            for (int J = 0; J < N; J++)
            {
                S[H, J] = XE[J];
            }
            F[H] = FE;
            Console.WriteLine($"Выполните растяжение в строке 1480  {_Z}");
            if (!Check())
                goto M620;
            else goto M2220;
            M1520:
            for (int J = 0; J < N; J++)
            {
                S[H, J] = XR[J];
            }
            F[H] = FR;
            Console.WriteLine("Выполните отражение в строке 1560");
            if (!Check())
                goto M620;
            else goto M2220;
            M1600:
            if (FR > FH)
                goto M1700;
            for (int J = 0; J < N; J++)
            {
                XH[J] = XR[J];
            }
            F[H] = FR;
            //далее следует сжатие 
            M1700:
            for (int J = 0; J < N; J++)
            {
                XC[J] = BE * XH[J] + (1 - BE) * XO[J];
                X[J] = XC[J];
            }
            Z1();
            float FC = _Z;
            if (FC > FH)
                goto M1920;
            for (int J = 0; J < N; J++)
            {
                S[H, J] = XC[J];
            }
            F[H] = FC;
            Console.WriteLine($"Выполните сжатие в строке 1880  {_Z}");
            if (!Check())//1900
                goto M620;
            else goto M2220;
            //далее следует редукция симплекса
            M1920:
            for (int I = 0; I < N + 1; I++)
            {
                for (int J = 0; J < N; J++)
                {
                    S[I, J] = (S[I, J] + XL[J]) / 2;
                    X[J] = S[I, J];
                }
                Z1();
                F[I] = _Z;
            }
            Console.WriteLine("Выполните редукцию в строке 2040");
            //далее следует проверка сходимости 
            if (!Check())
                goto M620;
            M2220:
            Console.WriteLine("Минимум найден в точке ");
            for (int J = 0; J < N; J++)
            {
                Console.Write($"X{J + 1} = {XL[J]}  ");
            }
            Console.WriteLine();
            Console.WriteLine($"Значение минимума функции = {F[L]}");
            Console.WriteLine($"Количество вычислений функции = {TEV}");
        }
        //Проверка сходимости 
        static bool Check()
        {
            float S1 = 0, S2 = 0;
            for (int I = 0; I < F.Length; I++)
            {
                S1 = S1 + F[I];
                S2 = S2 + F[I] * F[I];
            }
            float SIG = S2 - S1 * S1 / F.Length;
            SIG = SIG / F.Length;
            if (SIG < 1E-10)
                return true;
            return false;
        }
        static void Z1()
        {
            TEV = TEV + 1;
            _Z = (float)(100 * Math.Pow(X[1] - Math.Pow(X[0], 2), 2) + Math.Pow(1 - X[0], 2));
        }

    }
}
