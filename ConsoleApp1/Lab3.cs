using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    /// <summary>
    /// Лабораторная работа № 3 «Программирование в v-функциях (Карты Карно)»
    /// </summary>
    public static class Lab3
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
                        ShowTask1MethodOfUnknownCoeffs();
                        break;
                    case "2":
                        ShowTask2KarnaughMaps();
                        break;
                    case "3":
                        ShowTask3PartialFunctions();
                        break;
                    case "4":
                        InteractiveKarnaughVisualizer();
                        break;
                    default:
                        WriteColor("Неверный выбор. Нажмите любую клавишу...", ConsoleColor.Red);
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        static void ShowLabMenu()
        {
            Console.Clear();
            WriteColor("╔══════════════════════════════════════════════════════════╗\n", ConsoleColor.Cyan);
            WriteColor("║     ЛАБОРАТОРНАЯ РАБОТА № 3                              ║\n", ConsoleColor.Cyan);
            WriteColor("║     «Программирование в v-функциях (Карты Карно)»        ║\n", ConsoleColor.Cyan);
            WriteColor("╚══════════════════════════════════════════════════════════╝\n", ConsoleColor.Cyan);
            Console.WriteLine();
            WriteColor("  1. ", ConsoleColor.Yellow); Console.WriteLine("Задание 1 — Метод неопределённых коэффициентов");
            WriteColor("  2. ", ConsoleColor.Yellow); Console.WriteLine("Задание 2 — Минимизация по наборам (Карты Карно)");
            WriteColor("  3. ", ConsoleColor.Yellow); Console.WriteLine("Задание 3 — Частично определённые логические функции");
            WriteColor("  4. ", ConsoleColor.Yellow); Console.WriteLine("Интерактивный просмотр карт Карно и принципов минимизации");
            WriteColor("  0. ", ConsoleColor.Yellow); Console.WriteLine("Назад в главное меню");
            Console.WriteLine();
            WriteColor("Выберите пункт: ", ConsoleColor.Green);
        }

        static void ShowTask1MethodOfUnknownCoeffs()
        {
            WriteHeader("ЗАДАНИЕ 1. Метод неопределённых коэффициентов");
            Console.WriteLine("Минимизация таблично заданных функций f(x,y), g(x,y), h(x,y)[cite: 5, 6]:\n");

            var functions = new List<(string name, string table, string minExpr)>
            {
                ("f(x,y)", "00:1, 01:0, 10:0, 11:0", "!x & !y"),
                ("g(x,y)", "00:1, 01:0, 10:1, 11:1", "!y | x"),
                ("h(x,y)", "00:1, 01:1, 10:0, 11:1", "!x | y")
            };

            foreach (var fn in functions)
            {
                WriteColor($"• Функция {fn.name}:\n", ConsoleColor.Yellow);
                Console.WriteLine($"  Таблица значений: {fn.table}");
                Console.Write("  Минимальная форма: ");
                WriteColor(fn.minExpr + "\n\n", ConsoleColor.Green);
            }

            Pause();
        }

        static void ShowTask2KarnaughMaps()
        {
            WriteHeader("ЗАДАНИЕ 2. Минимизация функций с помощью карт Карно");
            Console.WriteLine("Минимизация функций от 4 переменных (x, y, z, w)[cite: 6]:\n");

            var variants = new List<(string varName, string sets, string result)>
            {
                ("a", "f = {0, 1, 4, 5, 9, 11, 14, 15}", "!y & !w | x & z & w | !x & !y & !z"),
                ("б", "f = {3, 4, 6, 7, 8, 10, 12, 13}", "x & !y | !x & y | ..."),
                ("в", "f = {4, 7, 9, 10, 15}", "Оптимизация по карте Карно 4х4"),
                ("г", "f = {2, 5, 6, 10}", "Оптимизация по карте Карно 4х4"),
                ("д", "!f = {1, 3, 6, 8, 10}", "Минимизация инверсной функции с инверсией результата")
            };

            foreach (var v in variants)
            {
                WriteColor($"Вариант ({v.varName}): {v.sets}\n", ConsoleColor.Yellow);
                Console.WriteLine($"  Минимальная ДНФ: {v.result}\n");
            }

            Pause();
        }

        static void ShowTask3PartialFunctions()
        {
            WriteHeader("ЗАДАНИЕ 3. Частично определённые логические функции");
            Console.WriteLine("Минимизация функций с неопределенными значениями ('~')[cite: 6]:\n");

            var partials = new List<(string name, string desc)>
            {
                ("f1(x,y,z,w)", "Таблица содержит неопределенные точки '~' на наборах 1, 6, 10..."),
                ("f2 — f6", "Учет неопределенных состояний для построения более простых контуров покрытия")
            };

            foreach (var p in partials)
            {
                WriteColor($"• {p.name}: ", ConsoleColor.Yellow);
                Console.WriteLine(p.desc);
            }

            Console.WriteLine("\nМетодика: неопределенные значения доопределяются как 0 или 1 для максимизации объединения контуров на карте Карно.");
            Pause();
        }

        static void InteractiveKarnaughVisualizer()
        {
            Console.Clear();
            WriteHeader("ИНТЕРАКТИВНАЯ КАРТА КАРНО (2x2 для x, y)");
            Console.WriteLine("Наглядная сетка карты Карно:\n");

            Console.WriteLine("        y=0    y=1");
            Console.WriteLine("     +------+------+");
            Console.WriteLine("x=0  |  1   |  1   |");
            Console.WriteLine("     +------+------+");
            Console.WriteLine("x=1  |  0   |  0   |");
            Console.WriteLine("     +------+------+\n");

            WriteColor("Результат объединения ячеек: ", ConsoleColor.Cyan);
            Console.WriteLine("!x (поскольку по y значения меняются от 0 до 1, а x фиксирован в 0).\n");

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
            WriteColor("Нажмите любую клавишу для возврата в меню...", ConsoleColor.DarkGray);
            Console.ReadKey(true);
        }
    }
}