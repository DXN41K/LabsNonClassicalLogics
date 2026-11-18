using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public static class Lab3
    {
        static readonly string[] N2 = { "x", "y" };
        static readonly string[] N4 = { "x", "y", "z", "w" };

        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                WriteHeader("ЛР 3. Минимизация");
                Item("1", "Метод неопределённых коэффициентов, f g h");
                Item("2", "Карты Карно, функции по наборам а–з");
                Item("3", "Частично определённые f1–f6");
                Item("4", "Как читать карту и схему на защите");
                Item("0", "Назад");
                Console.WriteLine();
                WriteColor("Пункт: ", ConsoleColor.Green);
                string c = (Console.ReadLine() ?? "").Trim().ToLower();
                Console.Clear();
                if (c == "0" || c == "q" || c == "й") return;
                if (c == "1") Task1();
                else if (c == "2") Task2();
                else if (c == "3") Task3();
                else if (c == "4") HowTo();
                else WriteColor("Неверный пункт.\n", ConsoleColor.Red);
                Pause();
            }
        }

        static void Task1()
        {
            WriteHeader("Задание 1. Неопределённые коэффициенты");
            Console.WriteLine("В таблице методички сначала столбец y, потом x.");
            Console.WriteLine("Многочлен Жегалкина: f = a0 ⊕ a1·x ⊕ a2·y ⊕ a3·x·y.");
            Console.WriteLine("a0 = f(0,0)");
            Console.WriteLine("a1 = f(1,0) ⊕ f(0,0)");
            Console.WriteLine("a2 = f(0,1) ⊕ f(0,0)");
            Console.WriteLine("a3 = f(1,1) ⊕ f(1,0) ⊕ f(0,1) ⊕ f(0,0)");
            Console.WriteLine("Рядом — минимальная ДНФ в базисе И, ИЛИ, НЕ. Это разные записи одной функции.");
            Console.WriteLine();

            Show2("f", 1, 0, 0, 0);
            Show2("g", 1, 0, 1, 1);
            Show2("h", 1, 1, 0, 1);
        }

        static void Show2(string name, int f00, int f10, int f01, int f11)
        {
            int a0 = f00;
            int a1 = f10 ^ f00;
            int a2 = f01 ^ f00;
            int a3 = f11 ^ f10 ^ f01 ^ f00;
            WriteColor(name + "(x,y)\n", ConsoleColor.Yellow);
            Console.WriteLine("  f(0,0)=" + f00 + "  f(1,0)=" + f10 + "  f(0,1)=" + f01 + "  f(1,1)=" + f11);
            Console.WriteLine("  коэффициенты: a0=" + a0 + " a1=" + a1 + " a2=" + a2 + " a3=" + a3);
            Console.WriteLine("  Жегалкин: " + Zheg(a0, a1, a2, a3));
            int[] v = new int[4];
            v[0] = f00; v[1] = f01; v[2] = f10; v[3] = f11;
            Console.WriteLine("  минимальная ДНФ: " + Lab2.MinimizeText(2, v, null, N2));
            Scheme(2, v, null, N2);
            Console.WriteLine();
        }

        static string Zheg(int a0, int a1, int a2, int a3)
        {
            var p = new List<string>();
            if (a0 == 1) p.Add("1");
            if (a1 == 1) p.Add("x");
            if (a2 == 1) p.Add("y");
            if (a3 == 1) p.Add("x·y");
            if (p.Count == 0) return "0";
            return string.Join(" ⊕ ", p.ToArray());
        }

        static void Task2()
        {
            WriteHeader("Задание 2. Карты Карно");
            Console.WriteLine("Единицы — где f=1. Чёрточка над f значит заданы нули, единицами заполняется остальное.");
            Console.WriteLine("Склейка клеток карты и есть то, что Куайн–Мак-Класки делает таблицей.");
            One("а", Ones(new[] { 0, 1, 4, 5, 9, 11, 14, 15 }), null);
            One("б", Ones(new[] { 3, 4, 6, 7, 8, 10, 12, 13 }), null);
            One("в", Ones(new[] { 4, 7, 9, 10, 15 }), null);
            One("г", Ones(new[] { 2, 5, 6, 10 }), null);
            One("д  нули {1,3,6,8,10}", Zeros(new[] { 1, 3, 6, 8, 10 }), null);
            One("е  нули {8,11,12,14}", Zeros(new[] { 8, 11, 12, 14 }), null);
            One("ж  нули {0,1,2,10,11}", Zeros(new[] { 0, 1, 2, 10, 11 }), null);
            One("з  нули {4,7,8,9}", Zeros(new[] { 4, 7, 8, 9 }), null);
        }

        static void Task3()
        {
            WriteHeader("Задание 3. Частичные функции");
            Console.WriteLine("~ можно доопределить и 0, и 1. В покрытие обязательны только единицы.");
            Console.WriteLine("На карте ~ — пустая клетка, её можно захватить склейкой, но можно и не покрывать.");
            string[] rows = {
                "0 ~ 0 0 1 0",
                "1 ~ ~ ~ 1 1",
                "1 1 1 ~ ~ 1",
                "0 0 ~ 1 0 ~",
                "1 1 ~ 1 0 1",
                "~ 1 0 ~ ~ ~",
                "~ 1 ~ 1 1 1",
                "1 0 1 0 1 0",
                "1 0 1 ~ 0 ~",
                "1 1 ~ 0 1 0",
                "~ 0 ~ 1 ~ ~",
                "~ 0 0 1 ~ 1",
                "~ ~ 0 0 1 0",
                "1 1 ~ 1 0 1",
                "0 ~ ~ 0 ~ ~",
                "1 ~ 1 0 0 ~"
            };
            for (int f = 0; f < 6; f++)
            {
                int[] v = new int[16];
                int[] dc = new int[16];
                for (int r = 0; r < 16; r++)
                {
                    string[] p = rows[r].Split(' ');
                    if (p[f] == "~") dc[r] = 1;
                    else v[r] = int.Parse(p[f]);
                }
                One("f" + (f + 1), v, dc);
            }
        }

        static void HowTo()
        {
            WriteHeader("Что говорить на защите");
            Console.WriteLine("1. Карта 4 переменных: строки xy в коде Грея 00,01,11,10, столбцы zw так же.");
            Console.WriteLine("2. Соседние единицы (и через край карты) склеиваются. Прямоугольник 2^k клеток");
            Console.WriteLine("   убирает k переменных. Это простой импликант.");
            Console.WriteLine("3. Берутся прямоугольники, без которых какая-то единица останется голой.");
            Console.WriteLine("   Потом добирается покрытие поменьше. Это таблица покрытия Куайна.");
            Console.WriteLine("4. Схема: каждый ¬ — элемент НЕ, каждая точка · — цепочка 2И,");
            Console.WriteLine("   плюсы между термами — цепочка 2ИЛИ.");
            Console.WriteLine("5. Для частичной функции ~ не обязана войти в покрытие. Её берут, только если");
            Console.WriteLine("   склейка становится крупнее и формула короче.");
        }

        static void One(string title, int[] values, int[] dc)
        {
            Console.WriteLine();
            WriteColor(title + "\n", ConsoleColor.Yellow);
            Map(values, dc);
            Console.WriteLine("  минимум: " + Lab2.MinimizeText(4, values, dc, N4));
            Scheme(4, values, dc, N4);
        }

        static void Map(int[] v, int[] dc)
        {
            int[] row = { 0, 1, 3, 2 };
            int[] col = { 0, 1, 3, 2 };
            Console.WriteLine("        zw  00  01  11  10");
            for (int r = 0; r < 4; r++)
            {
                int xy = row[r];
                Console.Write("  xy " + Bin2(xy) + "   ");
                for (int c = 0; c < 4; c++)
                {
                    int zw = col[c];
                    int idx = (xy << 2) | zw;
                    char ch = '0';
                    if (v[idx] == 1) ch = '1';
                    else if (dc != null && dc[idx] == 1) ch = '~';
                    Console.Write("  " + ch + " ");
                }
                Console.WriteLine();
            }
        }

        static void Scheme(int n, int[] values, int[] dc, string[] names)
        {
            List<int> bits, dashes, cover;
            Lab2.BuildMinimum(n, values, dc, out bits, out dashes, out cover);
            if (cover.Count == 0)
            {
                Console.WriteLine("  схема: константа 0, элементов нет");
                return;
            }
            Console.WriteLine("  схема в базисе 2И, 2ИЛИ, НЕ:");
            var outputs = new List<string>();
            for (int t = 0; t < cover.Count; t++)
            {
                int b = bits[cover[t]];
                int d = dashes[cover[t]];
                var lits = new List<string>();
                for (int i = 0; i < n; i++)
                {
                    int shift = n - 1 - i;
                    if (((d >> shift) & 1) == 1) continue;
                    if (((b >> shift) & 1) == 1) lits.Add(names[i]);
                    else
                    {
                        string inv = "n_" + names[i];
                        Console.WriteLine("    НЕ(" + names[i] + ") → " + inv);
                        lits.Add(inv);
                    }
                }
                string acc = lits[0];
                for (int k = 1; k < lits.Count; k++)
                {
                    string nxt = "t" + (t + 1) + "_" + k;
                    Console.WriteLine("    2И(" + acc + ", " + lits[k] + ") → " + nxt);
                    acc = nxt;
                }
                if (lits.Count == 1 && cover.Count > 1)
                    Console.WriteLine("    терм " + (t + 1) + " = " + acc);
                outputs.Add(acc);
            }
            string or = outputs[0];
            for (int k = 1; k < outputs.Count; k++)
            {
                string nxt = k == outputs.Count - 1 ? "F" : "s" + k;
                Console.WriteLine("    2ИЛИ(" + or + ", " + outputs[k] + ") → " + nxt);
                or = nxt;
            }
            if (outputs.Count == 1)
                Console.WriteLine("    F = " + or + "   (" + Lab2.FormatTerm(n, bits[cover[0]], dashes[cover[0]], names) + ")");
        }

        static int[] Ones(int[] idx)
        {
            int[] v = new int[16];
            for (int i = 0; i < idx.Length; i++) v[idx[i]] = 1;
            return v;
        }

        static int[] Zeros(int[] idx)
        {
            int[] v = new int[16];
            for (int i = 0; i < 16; i++) v[i] = 1;
            for (int i = 0; i < idx.Length; i++) v[idx[i]] = 0;
            return v;
        }

        static string Bin2(int v)
        {
            return ((v >> 1) & 1).ToString() + (v & 1).ToString();
        }

        static void Item(string n, string text)
        {
            WriteColor("  " + n + ". ", ConsoleColor.Yellow);
            Console.WriteLine(text);
        }
        static void WriteHeader(string text)
        {
            WriteColor("══════════════════════════════════════════════════════\n", ConsoleColor.DarkCyan);
            WriteColor("  " + text + "\n", ConsoleColor.Cyan);
            WriteColor("══════════════════════════════════════════════════════\n\n", ConsoleColor.DarkCyan);
        }
        static void WriteColor(string text, ConsoleColor color)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = old;
        }
        static void Pause()
        {
            Console.WriteLine();
            WriteColor("Нажмите любую клавишу...", ConsoleColor.DarkGray);
            Console.ReadKey(true);
        }
    }
}