using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    /// <summary>
    /// Лабораторная работа № 1 «Вывод в многозначных логиках»
    /// </summary>
    public static class Lab1
    {
        public static void Run()
        {
            while (true)
            {
                ShowLabMenu();
                string choice = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (choice == "0" || choice == "q" || choice == "й" || choice == "назад")
                {
                    return; // Возврат в главное меню Program.cs
                }

                Console.Clear();
                switch (choice)
                {
                    case "1":
                        ShowTask1();
                        break;
                    case "2":
                        ShowTask2Menu();
                        break;
                    case "3":
                        InteractiveCalculator();
                        break;
                    case "4":
                        ShowAllTruthTables();
                        break;
                    default:
                        WriteColor("Неверный выбор. Нажмите любую клавишу...", ConsoleColor.Red);
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        // ====================== МЕНЮ ЛАБЫ 1 ======================
        static void ShowLabMenu()
        {
            Console.Clear();
            WriteColor("╔══════════════════════════════════════════════════════════╗\n", ConsoleColor.Cyan);
            WriteColor("║     ЛАБОРАТОРНАЯ РАБОТА № 1                              ║\n", ConsoleColor.Cyan);
            WriteColor("║     «Вывод в многозначных логиках»                       ║\n", ConsoleColor.Cyan);
            WriteColor("╚══════════════════════════════════════════════════════════╝\n", ConsoleColor.Cyan);
            Console.WriteLine();
            WriteColor("  1. ", ConsoleColor.Yellow); Console.WriteLine("Задание 1 — Упрощение 11 выражений");
            WriteColor("  2. ", ConsoleColor.Yellow); Console.WriteLine("Задание 2 — Подробный разбор 5 выражений");
            WriteColor("  3. ", ConsoleColor.Yellow); Console.WriteLine("Интерактивный калькулятор (Задание 2)");
            WriteColor("  4. ", ConsoleColor.Yellow); Console.WriteLine("Все таблицы истинности Задания 2");
            WriteColor("  0. ", ConsoleColor.Yellow); Console.WriteLine("Назад в главное меню");
            Console.WriteLine();
            WriteColor("Выберите пункт: ", ConsoleColor.Green);
        }

        // ====================== ЗАДАНИЕ 1 ======================
        static void ShowTask1()
        {
            WriteHeader("ЗАДАНИЕ 1. Упрощение логических выражений");

            var items = new List<(string name, string original, string simplified, int before, int after, string comment)>
            {
                ("а", "a(!a!b + !c + ac) + !abc + !ab!c", "a + b", 17, 1, "Поглощение + Блейка-Порецкого"),
                ("б", "xy + !x!yz + !y", "x + !y", 8, 2, "Поглощение + Блейка-Порецкого"),
                ("в", "xz + xyz + !z + !x!y!z", "x + !z", 12, 2, "Поглощение"),
                ("г", "!(x!y!z)*x + !z", "x + !z", 8, 2, "Де Морган + поглощение"),
                ("д", "ab(a + b) + !b + !a", "1", 7, 0, "Поглощение -> константа 1"),
                ("е", "ab(c + d) + !(ac) + !b", "1", 8, 0, "Поглощение -> константа 1"),
                ("ж", "!(!a + !b) + !(!a + !b) + !a", "!a + b", 11, 2, "Де Морган + поглощение"),
                ("з", "(!a + b)(a + !b)(b + bc)", "ab", 8, 1, "Упрощение скобок + поглощение"),
                ("и", "!(x + xy + xyz + xyz!t) + xt", "!x + t", 13, 2, "Поглощение внутри + Блейка"),
                ("к", "!(xy + !x!y) + !x!y", "!x + !y", 10, 3, "Де Морган + поглощение"),
                ("л", "!((x + xy)(!x + y)) + !((z + !x)(!z + y)(!z + !y))", "1", 17, 0, "Два независимых слагаемых -> 1")
            };

            foreach (var item in items)
            {
                WriteColor($"\n── Пункт {item.name} ──────────────────────────────\n", ConsoleColor.Yellow);
                Console.Write("Исходное:     ");
                WriteColor(item.original + "\n", ConsoleColor.White);
                Console.Write("Упрощённое:   ");
                WriteColor(item.simplified + "\n", ConsoleColor.Green);
                Console.Write($"Операций:     {item.before} -> ");
                WriteColor(item.after.ToString(), ConsoleColor.Cyan);
                Console.WriteLine($"   ({item.comment})");
            }

            Pause();
        }

        // ====================== ЗАДАНИЕ 2 — МЕНЮ ======================
        static void ShowTask2Menu()
        {
            while (true)
            {
                Console.Clear();
                WriteHeader("ЗАДАНИЕ 2. Подробный разбор");

                Console.WriteLine("  1.  !xy + x!y");
                Console.WriteLine("  2.  !(ab) + !a!b");
                Console.WriteLine("  3.  !(!x + y + !z) + y!z");
                Console.WriteLine("  4.  !a & b + !(ab) & c + a & !(bc)");
                Console.WriteLine("  5.  !(a + bc) + !b!c");
                Console.WriteLine();
                WriteColor("  0.  Назад в меню лабы\n", ConsoleColor.DarkGray);
                WriteColor("\nВыберите номер выражения (1-5): ", ConsoleColor.Green);

                string ch = Console.ReadLine()?.Trim() ?? "";
                if (ch == "0") return;
                Console.Clear();

                switch (ch)
                {
                    case "1": ShowExprA(); break;
                    case "2": ShowExprB(); break;
                    case "3": ShowExprV(); break;
                    case "4": ShowExprG(); break;
                    case "5": ShowExprD(); break;
                    default:
                        WriteColor("Неверный выбор. Нажмите клавишу...\n", ConsoleColor.Red);
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        static void ShowExprA()
        {
            WriteHeader("1)  !x & y + x & !y   (исключающее ИЛИ)");
            Console.WriteLine("Упрощённое:     !xy + x!y   (уже минимальная форма)");
            Console.WriteLine("Операций:       5 -> 5\n");

            WriteColor("Таблица истинности:\n", ConsoleColor.Cyan);
            PrintTable2("x", "y", (x, y) => (!x && y) || (x && !y));
            Pause();
        }

        static void ShowExprB()
        {
            WriteHeader("2)  !(a & b) + !a & !b");
            Console.WriteLine("Упрощённое:     !a + !b");
            Console.WriteLine("Операций:       6 -> 3\n");

            WriteColor("Таблица истинности:\n", ConsoleColor.Cyan);
            PrintTable2("a", "b", (a, b) => !a || !b);
            Pause();
        }

        static void ShowExprV()
        {
            WriteHeader("3)  !(!x + y + !z) + y & !z");
            Console.WriteLine("Упрощённое:     x & !y & z + y & !z");
            Console.WriteLine("Операций:       8 -> 6\n");

            WriteColor("Таблица истинности:\n", ConsoleColor.Cyan);
            PrintTable3("x", "y", "z", (x, y, z) => (x && !y && z) || (y && !z));
            Pause();
        }

        static void ShowExprG()
        {
            WriteHeader("4)  !a & b + !(ab) & c + a & !(bc)");
            Console.WriteLine("Упрощённое:     !a & (b + c) + a & (!b + !c)");
            Console.WriteLine("Операций:       10 -> 8\n");

            WriteColor("Таблица истинности:\n", ConsoleColor.Cyan);
            PrintTable3("a", "b", "c", (a, b, c) => (!a && (b || c)) || (a && (!b || !c)));
            Pause();
        }

        static void ShowExprD()
        {
            WriteHeader("5)  !(a + b & c) + !b & !c");
            Console.WriteLine("Упрощённое:     !(a & b + a & c + b & c)");
            Console.WriteLine("Операций:       7 -> 6\n");

            WriteColor("Таблица истинности:\n", ConsoleColor.Cyan);
            PrintTable3("a", "b", "c", (a, b, c) => !(a || (b && c)) || (!b && !c));
            Pause();
        }

        static void InteractiveCalculator()
        {
            while (true)
            {
                Console.Clear();
                WriteHeader("ИНТЕРАКТИВНЫЙ КАЛЬКУЛЯТОР ЛР №1");
                Console.WriteLine("  1.  !xy + x!y");
                Console.WriteLine("  2.  !a + !b");
                Console.WriteLine("  3.  x!yz + y!z");
                Console.WriteLine("  4.  !a(b+c) + a(!b+!c)");
                Console.WriteLine("  5.  !(ab + ac + bc)");
                Console.WriteLine();
                WriteColor("  0.  Назад\n", ConsoleColor.DarkGray);
                WriteColor("\nВыберите номер выражения (1-5): ", ConsoleColor.Green);

                string ch = Console.ReadLine()?.Trim() ?? "";
                if (ch == "0") return;

                Console.WriteLine();
                switch (ch)
                {
                    case "1":
                        Calc2("x", "y", (x, y) => (!x && y) || (x && !y));
                        break;
                    case "2":
                        Calc2("a", "b", (a, b) => !a || !b);
                        break;
                    case "3":
                        Calc3("x", "y", "z", (x, y, z) => (x && !y && z) || (y && !z));
                        break;
                    case "4":
                        Calc3("a", "b", "c", (a, b, c) => (!a && (b || c)) || (a && (!b || !c)));
                        break;
                    case "5":
                        Calc3("a", "b", "c", (a, b, c) => !(a || (b && c)) || (!b && !c));
                        break;
                    default:
                        WriteColor("Неверный выбор\n", ConsoleColor.Red);
                        break;
                }
                Pause();
            }
        }

        static void ShowAllTruthTables()
        {
            WriteHeader("Все таблицы истинности Задания 2");

            Console.WriteLine("1) !xy + x!y");
            PrintTable2("x", "y", (x, y) => (!x && y) || (x && !y));

            Console.WriteLine("\n2) !a + !b");
            PrintTable2("a", "b", (a, b) => !a || !b);

            Console.WriteLine("\n3) x!yz + y!z");
            PrintTable3("x", "y", "z", (x, y, z) => (x && !y && z) || (y && !z));

            Console.WriteLine("\n4) !a(b+c) + a(!b+!c)");
            PrintTable3("a", "b", "c", (a, b, c) => (!a && (b || c)) || (a && (!b || !c)));

            Console.WriteLine("\n5) !(ab + ac + bc)");
            PrintTable3("a", "b", "c", (a, b, c) => !(a || (b && c)) || (!b && !c));

            Pause();
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
            WriteColor("Нажмите любую клавишу для продолжения...", ConsoleColor.DarkGray);
            Console.ReadKey(true);
        }

        static void PrintTable2(string v1, string v2, Func<bool, bool, bool> f)
        {
            Console.WriteLine($"  {v1}  {v2} │ F");
            Console.WriteLine("  ─────┼──");
            for (int i = 0; i < 4; i++)
            {
                bool a = (i & 2) != 0;
                bool b = (i & 1) != 0;
                int res = f(a, b) ? 1 : 0;
                Console.Write($"  {(a ? 1 : 0)}  {(b ? 1 : 0)} │ ");
                WriteColor(res.ToString() + "\n", res == 1 ? ConsoleColor.Green : ConsoleColor.DarkGray);
            }
        }

        static void PrintTable3(string v1, string v2, string v3, Func<bool, bool, bool, bool> f)
        {
            Console.WriteLine($"  {v1}  {v2}  {v3} │ F");
            Console.WriteLine("  ───────┼──");
            for (int i = 0; i < 8; i++)
            {
                bool a = (i & 4) != 0;
                bool b = (i & 2) != 0;
                bool c = (i & 1) != 0;
                int res = f(a, b, c) ? 1 : 0;
                Console.Write($"  {(a ? 1 : 0)}  {(b ? 1 : 0)}  {(c ? 1 : 0)} │ ");
                WriteColor(res.ToString() + "\n", res == 1 ? ConsoleColor.Green : ConsoleColor.DarkGray);
            }
        }

        static void Calc2(string n1, string n2, Func<bool, bool, bool> f)
        {
            bool a = ReadBit(n1);
            bool b = ReadBit(n2);
            int result = f(a, b) ? 1 : 0;
            Console.Write("Результат F = ");
            WriteColor(result.ToString() + "\n", result == 1 ? ConsoleColor.Green : ConsoleColor.Yellow);
        }

        static void Calc3(string n1, string n2, string n3, Func<bool, bool, bool, bool> f)
        {
            bool a = ReadBit(n1);
            bool b = ReadBit(n2);
            bool c = ReadBit(n3);
            int result = f(a, b, c) ? 1 : 0;
            Console.Write("Результат F = ");
            WriteColor(result.ToString() + "\n", result == 1 ? ConsoleColor.Green : ConsoleColor.Yellow);
        }

        static bool ReadBit(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name} (0 или 1): ");
                string s = Console.ReadLine()?.Trim() ?? "";
                if (s == "0") return false;
                if (s == "1") return true;
                WriteColor("Ошибка: введите 0 или 1\n", ConsoleColor.Red);
            }
        }
    }
}