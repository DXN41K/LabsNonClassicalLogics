using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    /// <summary>
    /// Лабораторная работа № 4 «Исследование алгебраических законов логики»
    /// </summary>
    public static class Lab4
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
                        ShowTask1DualFunctions();
                        break;
                    case "2":
                        ShowTask2PostCriterion();
                        break;
                    case "3":
                        ShowTask3Bases();
                        break;
                    case "4":
                        ShowTask4Axioms();
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

        // ====================== МЕНЮ ЛАБЫ 4 ======================
        static void ShowLabMenu()
        {
            Console.Clear();
            WriteColor("╔══════════════════════════════════════════════════════════╗\n", ConsoleColor.Cyan);
            WriteColor("║     ЛАБОРАТОРНАЯ РАБОТА № 4                              ║\n", ConsoleColor.Cyan);
            WriteColor("║     «Исследование алгебраических законов логики»         ║\n", ConsoleColor.Cyan);
            WriteColor("╚══════════════════════════════════════════════════════════╝\n", ConsoleColor.Cyan);
            Console.WriteLine();
            WriteColor("  1. ", ConsoleColor.Yellow); Console.WriteLine("Задание 1 — Двойственные функции");
            WriteColor("  2. ", ConsoleColor.Yellow); Console.WriteLine("Задание 2 — Функционально полные системы (Критерий Поста)");
            WriteColor("  3. ", ConsoleColor.Yellow); Console.WriteLine("Задание 3 — Выражение операций в базисах");
            WriteColor("  4. ", ConsoleColor.Yellow); Console.WriteLine("Задание 4 — Аксиомы алгебры логики");
            WriteColor("  5. ", ConsoleColor.Yellow); Console.WriteLine("Интерактивный калькулятор / Проверка двойственности");
            WriteColor("  0. ", ConsoleColor.Yellow); Console.WriteLine("Назад в главное меню");
            Console.WriteLine();
            WriteColor("Выберите пункт: ", ConsoleColor.Green);
        }

        // ====================== ЗАДАНИЕ 1 ======================
        static void ShowTask1DualFunctions()
        {
            WriteHeader("ЗАДАНИЕ 1. Двойственные функции");
            Console.WriteLine("Принцип двойственности: f*(x1, ..., xn) = NOT(f(NOT x1, ..., NOT xn)).\n");

            var examples = new List<(string name, string expr, string dual, string desc)>
            {
                ("Конъюнкция", "x & y", "x | y", "Замена & на |"),
                ("Дизъюнкция", "x | y", "x & y", "Замена | на &"),
                ("Импликация", "x -> y", "x & !y", "Через отрицания и закон де Моргана"),
                ("Шеффер (И-НЕ)", "x | y", "x NOR y", "Шеффер двойственен Стрелке Пирса"),
                ("Пирс (ИЛИ-НЕ)", "x NOR y", "x | y", "Стрелка Пирса двойственна Шефферу"),
                ("Сложение по мод 2", "x XOR y", "x == y", "Сумма по модулю 2 и эквивалентность")
            };

            foreach (var ex in examples)
            {
                WriteColor($"── {ex.name} ──────────────────────────────\n", ConsoleColor.Yellow);
                Console.Write("Исходная:     ");
                WriteColor(ex.expr + "\n", ConsoleColor.White);
                Console.Write("Двойственная: ");
                WriteColor(ex.dual + "\n", ConsoleColor.Green);
                Console.WriteLine($"Правило:      {ex.desc}\n");
            }

            Pause();
        }

        // ====================== ЗАДАНИЕ 2 ======================
        static void ShowTask2PostCriterion()
        {
            WriteHeader("ЗАДАНИЕ 2. Функционально полные системы (Критерий Поста)");
            Console.WriteLine("Система полна <=> она не содержится целиком ни в одном из классов T0, T1, L, S, M.\n");

            WriteColor("5 дополнительных функционально полных систем (базисов):\n", ConsoleColor.Cyan);

            var systems = new List<(int num, string sys, string desc)>
            {
                (1, "{ ->, XOR }", "Импликация и сложение по модулю 2"),
                (2, "{ ==, | }", "Эквивалентность и дизъюнкция"),
                (3, "{ |, 1 }", "Шеффер с константой единицей"),
                (4, "{ NOR, 0 }", "Стрелка Пирса с константой нулем"),
                (5, "{ ==, ->, 0 }", "Эквивалентность, импликация и константа 0")
            };

            foreach (var s in systems)
            {
                WriteColor($"  {s.num}. Базис {s.sys}:\n", ConsoleColor.Yellow);
                Console.WriteLine(s.desc + "\n");
            }

            WriteColor("Справка по классам Поста:\n", ConsoleColor.DarkCyan);
            Console.WriteLine("• T0 — сохраняющие 0 (f(0..0) = 0)");
            Console.WriteLine("• T1 — сохраняющие 1 (f(1..1) = 1)");
            Console.WriteLine("• L  — линейные (полином Жегалкина)");
            Console.WriteLine("• S  — самодвойственные (f*(x) = f(x))");
            Console.WriteLine("• M  — монотонные (рост аргументов не уменьшает результат)");

            Pause();
        }

        // ====================== ЗАДАНИЕ 3 ======================
        static void ShowTask3Bases()
        {
            WriteHeader("ЗАДАНИЕ 3. Выражение операций в базисах");
            Console.WriteLine("Выражение основных логических операций через стандартные системы:\n");

            WriteColor("А) Базис { &, ! }:\n", ConsoleColor.Yellow);
            Console.WriteLine("  x | y   = !(!x & !y)");
            Console.WriteLine("  x -> y  = !(x & !y)");
            Console.WriteLine("  x XOR y = (x & !y) | (!x & y)\n");

            WriteColor("Б) Базис { |, ! }:\n", ConsoleColor.Yellow);
            Console.WriteLine("  x & y   = !(!x | !y)");
            Console.WriteLine("  x -> y  = !x | y");
            Console.WriteLine("  x == y  = (!x | y) & (x | !Y)\n");

            WriteColor("В) Базис { NAND } (Шеффер — |):\n", ConsoleColor.Yellow);
            Console.WriteLine("  !x      = x | x");
            Console.WriteLine("  x & y   = (x | y) | (x | y)");
            Console.WriteLine("  x | y   = (x | x) | (y | y)\n");

            WriteColor("Г) Базис { NOR } (Пирс):\n", ConsoleColor.Yellow);
            Console.WriteLine("  !x      = x NOR x");
            Console.WriteLine("  x | y   = (x NOR y) NOR (x NOR y)");
            Console.WriteLine("  x & y   = (x NOR x) NOR (y NOR y)");

            Pause();
        }

        // ====================== ЗАДАНИЕ 4 ======================
        static void ShowTask4Axioms()
        {
            WriteHeader("ЗАДАНИЕ 4. Аксиомы алгебры логики");
            Console.WriteLine("Носитель B = {0, 1} с операциями &, |, !:\n");

            var axioms = new List<(string name, string andForm, string orForm)>
            {
                ("Идемпотентность", "x & x = x", "x | x = x"),
                ("Коммутативность", "x & y = y & x", "x | y = y | x"),
                ("Ассоциативность", "(x & y) & z = x & (y & z)", "(x | y) | z = x | (y | z)"),
                ("Дистрибутивность", "x & (y | z) = (x & y) | (x & z)", "x | (y & z) = (x | y) & (x | z)"),
                ("Поглощение", "x & (x | y) = x", "x | (x & y) = x"),
                ("Нейтральный элемент", "x & 1 = x", "x | 0 = x"),
                ("Контрадикция", "x & 0 = 0", "x | 1 = 1"),
                ("Исключенного третьего", "x & !x = 0", "x | !x = 1"),
                ("Законы Де Моргана", "!(x & y) = !x | !y", "!(x | y) = !x & !y")
            };

            foreach (var ax in axioms)
            {
                WriteColor($"• {ax.name}:\n", ConsoleColor.Yellow);
                Console.WriteLine($"  Конъюнкция: {ax.andForm}");
                Console.WriteLine($"  Дизъюнкция: {ax.orForm}\n");
            }

            Pause();
        }

        // ====================== ИНТЕРАКТИВНЫЙ КАЛЬКУЛЯТОР ======================
        static void InteractiveCalculator()
        {
            while (true)
            {
                Console.Clear();
                WriteHeader("ИНТЕРАКТИВНЫЙ КАЛЬКУЛЯТОР ДВОЙСТВЕННОСТИ");
                Console.WriteLine("Проверим закон двойственности для функции: f(x, y) = (x & !y) | y");
                Console.WriteLine("Её двойственная: f*(x, y) = (x | !y) & y\n");

                bool x = ReadBit("x");
                bool y = ReadBit("y");

                bool f = (x && !y) || y;
                bool fDual = (x || !y) && y;

                Console.WriteLine();
                Console.Write("Значение исходной f(x, y) = ");
                WriteColor((f ? 1 : 0) + "\n", f ? ConsoleColor.Green : ConsoleColor.DarkGray);

                Console.Write("Значение двойственной f*(x, y) = ");
                WriteColor((fDual ? 1 : 0) + "\n", fDual ? ConsoleColor.Green : ConsoleColor.DarkGray);

                Console.WriteLine();
                WriteColor("Хотите проверить другой набор? (y/n): ", ConsoleColor.Yellow);
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
            WriteColor("Нажмите любую клавишу для продолжения...", ConsoleColor.DarkGray);
            Console.ReadKey(true);
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