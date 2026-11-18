using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8; 
            Console.Title = "Лабораторные работы — Неклассические логики";

            while (true)
            {
                ShowMainMenu();
                string choice = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (choice == "0" || choice == "q" || choice == "й" || choice == "exit")
                {
                    Console.Clear();
                    WriteColor("До свидания!\n", ConsoleColor.Cyan);
                    return;
                }

                Console.Clear();
                switch (choice)
                {
                    case "1":
                        Lab1.Run();
                        // ShowStub(1);
                        break;
                    case "2":
                        Lab2.Run();
                        //ShowStub(2);
                        break;
                    case "3":
                        Lab3.Run();
                        //ShowStub(3);
                        break;
                    case "4":
                        Lab4.Run();
                        // ShowStub(4);
                        break;
                    case "5":
                        // Lab5.Run();
                        ShowStub(5);
                        break;
                    case "6":
                        // Lab6.Run();
                        ShowStub(6);
                        break;
                    case "7":
                        // Lab7.Run();
                        ShowStub(7);
                        break;
                    case "8":
                        // Lab8.Run();
                        ShowStub(8);
                        break;
                    case "9":
                        // Lab9.Run();
                        ShowStub(9);
                        break;
                    case "10":
                        // Lab10.Run();
                        ShowStub(10);
                        break;
                    case "11":
                        // Lab11.Run();
                        ShowStub(11);
                        break;
                    default:
                        WriteColor("Неверный выбор. Нажмите любую клавишу...", ConsoleColor.Red);
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            WriteColor("╔═══════════════════════════════════════════════════════════════╗\n", ConsoleColor.Cyan);
            WriteColor("║     КУРС: «НЕКЛАССИЧЕСКИЕ ЛОГИКИ»                             ║\n", ConsoleColor.Cyan);
            WriteColor("║     ВЫБОР ЛАБОРАТОРНОЙ РАБОТЫ (1–11)                          ║\n", ConsoleColor.Cyan);
            WriteColor("╚═══════════════════════════════════════════════════════════════╝\n", ConsoleColor.Cyan);
            Console.WriteLine();
            WriteColor("   1. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 1  — Вывод в многозначных логиках");
            WriteColor("   2. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 2  — Исследование у - исчисления (ДНФ и КНФ)");
            WriteColor("   3. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 3  — Программирование в V - функциях (Карты Карно)");
            WriteColor("   4. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 4  — Исследование алгебраических законов логики");
            WriteColor("   5. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 5  — Разработка алгоритмов в интуиционистской логике");
            WriteColor("   6. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 6  — Исследование модальных логик (Сумматор Жегалкина)");
            WriteColor("   7. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 7  — Разработка гибридных логических систем (PIE)");
            WriteColor("   8. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 8  — Исследование вероятностного вывода (Рекурсия)");
            WriteColor("   9. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 9  — Построение нечётких множеств (Примитивная рекурсия)");
            WriteColor("  10. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 10 — Обработка нечётких множеств (μ-оператор)");
            WriteColor("  11. ", ConsoleColor.Yellow); Console.WriteLine("ЛР № 11 — Исследование нечёткого вывода (Машина Тьюринга)");
            Console.WriteLine();
            WriteColor("   0. ", ConsoleColor.Yellow); Console.WriteLine("Выход (или q / й)");
            Console.WriteLine();
            WriteColor("Выберите номер лабораторной: ", ConsoleColor.Green);
        }

        static void ShowStub(int labNumber)
        {
            WriteColor($"Лабораторная работа № {labNumber} пока не подключена.\n", ConsoleColor.Yellow);
            Pause();
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