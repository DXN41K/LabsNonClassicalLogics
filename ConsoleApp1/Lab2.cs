using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    /// <summary>
    /// Лабораторная работа № 2 «Исследование ⊥-исчисления»
    /// </summary>
    public static class Lab2
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
                        ShowTask1BinaryDNFCNF();
                        break;
                    case "2":
                        ShowTask2ThreeVarDNFCNF();
                        break;
                    case "3":
                        ShowTask3SetsDNFCNF();
                        break;
                    case "4":
                        ShowTask4ComplexExpr();
                        break;
                    case "5":
                        InteractiveCalculator();
                        break;
                    default:
                        WriteColor("Неверный выбор. Нажмите любую клавишу...", ConsoleColor.Red);
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        // ====================== МЕНЮ ЛАБЫ 2 ======================
        static void ShowLabMenu()
        {
            Console.Clear();
            WriteColor("╔══════════════════════════════════════════════════════════╗\n", ConsoleColor.Cyan);
            WriteColor("║     ЛАБОРАТОРНАЯ РАБОТА № 2                              ║\n", ConsoleColor.Cyan);
            WriteColor("║     «Исследование ⊥-исчисления (ДНФ и КНФ)»              ║\n", ConsoleColor.Cyan);
            WriteColor("╚══════════════════════════════════════════════════════════╝\n", ConsoleColor.Cyan);
            Console.WriteLine();
            WriteColor("  1. ", ConsoleColor.Yellow); Console.WriteLine("Задание 1 — ДНФ и КНФ бинарных функций по таблице истинности");
            WriteColor("  2. ", ConsoleColor.Yellow); Console.WriteLine("Задание 2 — ДНФ и КНФ функций 3 переменных (f1–f8)");
            WriteColor("  3. ", ConsoleColor.Yellow); Console.WriteLine("Задание 3 — ДНФ и КНФ по заданным наборам (4 переменных)");
            WriteColor("  4. ", ConsoleColor.Yellow); Console.WriteLine("Задание 4 — ДНФ и КНФ сложных логических выражений");
            WriteColor("  5. ", ConsoleColor.Yellow); Console.WriteLine("Интерактивный калькулятор / Построитель СДНФ и СКНФ");
            WriteColor("  0. ", ConsoleColor.Yellow); Console.WriteLine("Назад в главное меню");
            Console.WriteLine();
            WriteColor("Выберите пункт: ", ConsoleColor.Green);
        }

        // ====================== ЗАДАНИЕ 1 ======================
        static void ShowTask1BinaryDNFCNF()
        {
            WriteHeader("ЗАДАНИЕ 1. ДНФ и КНФ бинарных функций");
            Console.WriteLine("Построение совершенных ДНФ (СДНФ) и КНФ (СКНФ) для базовых операций[cite: 5].\n");

            var funcs = new List<(string name, string symbol, string dnf, string cnf)>
            {
                ("Стрелка Пирса (NOR)", "x NOR y", "(!x & !y)", "(x | !y) & (!x | y) & (!x | !y)"),
                ("Исключающее ИЛИ", "x XOR y", "(!x & y) | (x & !y)", "(x | y) & (!x | !y)"),
                ("Штрих Шеффера (NAND)", "x | y", "(!x & y) | (x & !y) | (!x | !y)", "(x | y)"),
                ("Эквивалентность", "x == y", "(!x & !y) | (x & y)", "(x | !y) & (!x | y)"),
                ("Импликация", "x -> y", "(!x & !y) | (x & !y) | (x & y)", "(x | !y)")
            };

            foreach (var f in funcs)
            {
                WriteColor($"── {f.name} ({f.symbol}) ──────────────────────\n", ConsoleColor.Yellow);
                Console.Write("СДНФ: ");
                WriteColor(f.dnf + "\n", ConsoleColor.Green);
                Console.Write("СКНФ: ");
                WriteColor(f.cnf + "\n\n", ConsoleColor.Cyan);
            }

            Pause();
        }

        // ====================== ЗАДАНИЕ 2 ======================
        static void ShowTask2ThreeVarDNFCNF()
        {
            WriteHeader("ЗАДАНИЕ 2. ДНФ и КНФ функций 3 переменных");
            Console.WriteLine("Анализ функций f1(x,y,z) — f8(x,y,z) по таблицам истинности[cite: 5]:\n");

            var items = new List<(string name, string desc, string dnf, string cnf)>
            {
                ("f1(x,y,z)", "Наборы: f(1)=1, f(2)=1, f(4)=1, f(6)=1, f(7)=1", "(!x & !y & z) | (!x & y & !y) | (x & !y & !z) | ...", "(x | y | z) & (x | !y | z) & ..."),
                ("f2(x,y,z)", "Наборы: f(0)=1, f(2)=1, f(4)=1, f(5)=1, f(6)=1", "(!x & !y & !z) | (!x & y & !z) | (x & !y & !z) | ...", "(x | y | !z) & (x | !y | z) & ..."),
                ("f3(x,y,z)", "Наборы: f(1)=1, f(2)=1, f(6)=1, f(7)=1", "(!x & !y & z) | (!x & y & !z) | (x & y & !z) | (x & y & z)", "(x | y | z) & (x | !y | z) & ..."),
                ("f4(x,y,z)", "Наборы: f(3)=1, f(4)=1, f(6)=1", "(!x & y & z) | (x & !y & !z) | (x & y & !z)", "(x | y | z) & (x | y | !z) & ..."),
                ("f5(x,y,z)", "Наборы: 6 единичных значений", "СДНФ из 6 минтермов", "СКНФ из 2 макстермов")
            };

            foreach (var it in items)
            {
                WriteColor($"• {it.name}: {it.desc}\n", ConsoleColor.Yellow);
                Console.Write("  СДНФ: "); WriteColor(it.dnf + "\n", ConsoleColor.Green);
                Console.Write("  СКНФ: "); WriteColor(it.cnf + "\n\n", ConsoleColor.Cyan);
            }

            Pause();
        }

        // ====================== ЗАДАНИЕ 3 ======================
        static void ShowTask3SetsDNFCNF()
        {
            WriteHeader("ЗАДАНИЕ 3. ДНФ и КНФ по заданным наборам");
            Console.WriteLine("Построение форм по номерам единичных наборов для 4 переменных (x, y, z, w)[cite: 5]:\n");

            var tasks = new List<(string id, string sets)>
            {
                ("a", "f(x,y,z,w) = {0, 1, 4, 5, 9, 11, 14, 15}"),
                ("б", "f(x,y,z,w) = {3, 4, 6, 7, 8, 10, 12, 13}"),
                ("в", "f(x,y,z,w) = {4, 7, 9, 10, 15}"),
                ("г", "f(x,y,z,w) = {2, 5, 6, 10}"),
                ("д", "f(x,y,z,w) = {1, 3, 6, 8, 10}")
            };

            foreach (var t in tasks)
            {
                WriteColor($"Вариант ({t.id}): {t.sets}\n", ConsoleColor.Yellow);
                Console.WriteLine("  -> СДНФ формируется как дизъюнкция минтермов для указанных номеров наборов.");
                Console.WriteLine("  -> СКНФ формируется как конъюнкция макстермов для остальных номеров наборов.\n");
            }

            Pause();
        }

        // ====================== ЗАДАНИЕ 4 ======================
        static void ShowTask4ComplexExpr()
        {
            WriteHeader("ЗАДАНИЕ 4. ДНФ и КНФ сложных логических выражений");
            Console.WriteLine("Преобразование выражений с логическими операциями[cite: 5]:\n");

            var exprs = new List<(string name, string expr)>
            {
                ("a", "!((x | y) ^ (x | y)) -> !(x | y)"),
                ("б", "!(a <- b) NOR (!a ^ !b)"),
                ("в", "((!a ^ b) -> (!(a | b) NOR c)) | (a <- !(b | c))"),
                ("г", "!(a | (b <- c)) <- (!b ^ !c)")
            };

            foreach (var e in exprs)
            {
                WriteColor($"• Выражение ({e.name}): ", ConsoleColor.Yellow);
                Console.WriteLine(e.expr);
                Console.WriteLine("  Алгоритм: построение таблицы истинности -> выделение единичных/нулевых строк -> запись СДНФ и СКНФ.\n");
            }

            Pause();
        }

        // ====================== ИНТЕРАКТИВНЫЙ КАЛЬКУЛЯТОР ======================
        static void InteractiveCalculator()
        {
            while (true)
            {
                Console.Clear();
                WriteHeader("ИНТЕРАКТИВНЫЙ ПОСТРОИТЕЛЬ СДНФ/СКНФ (2 переменные x, y)");
                Console.WriteLine("Введите значения функции F для наборов (00, 01, 10, 11):\n");

                int f00 = ReadBit("F(0,0)");
                int f01 = ReadBit("F(0,1)");
                int f10 = ReadBit("F(1,0)");
                int f11 = ReadBit("F(1,1)");

                Console.WriteLine();
                WriteColor("Результаты построения:\n", ConsoleColor.Cyan);

                List<string> minterms = new List<string>();
                if (f00 == 1) minterms.Add("(!x & !y)");
                if (f01 == 1) minterms.Add("(!x & y)");
                if (f10 == 1) minterms.Add("(x & !y)");
                if (f11 == 1) minterms.Add("(x & y)");

                Console.Write("СДНФ: ");
                if (minterms.Count == 0) WriteColor("0 (тождественный ноль)\n", ConsoleColor.DarkGray);
                else if (minterms.Count == 4) WriteColor("1 (тождественная единица)\n", ConsoleColor.Green);
                else WriteColor(string.Join(" | ", minterms) + "\n", ConsoleColor.Green);

                List<string> maxterms = new List<string>();
                if (f00 == 0) maxterms.Add("(x | y)");
                if (f01 == 0) maxterms.Add("(x | !y)");
                if (f10 == 0) maxterms.Add("(!x | y)");
                if (f11 == 0) maxterms.Add("(!x | !y)");

                Console.Write("СКНФ: ");
                if (maxterms.Count == 0) WriteColor("1 (тождественная единица)\n", ConsoleColor.Green);
                else if (maxterms.Count == 4) WriteColor("0 (тождественный ноль)\n", ConsoleColor.DarkGray);
                else WriteColor(string.Join(" & ", maxterms) + "\n", ConsoleColor.Cyan);

                Console.WriteLine();
                WriteColor("Хотите проверить другую функцию? (y/n): ", ConsoleColor.Yellow);
                string ans = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (ans == "n" || ans == "н") break;
            }
        }

        // ====================== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ======================
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
            WriteColor("Нажмите любую клавишу для возврата в меню...", ConsoleColor.DarkGray);
            Console.ReadKey(true);
        }

        static int ReadBit(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name} (0 или 1): ");
                string s = Console.ReadLine()?.Trim() ?? "";
                if (s == "0") return 0;
                if (s == "1") return 1;
                WriteColor("Ошибка: введите 0 или 1\n", ConsoleColor.Red);
            }
        }
    }
}