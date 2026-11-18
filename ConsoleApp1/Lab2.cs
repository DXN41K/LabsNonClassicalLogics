using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public static class Lab2
    {
        static readonly string[] N2 = { "x", "y" };
        static readonly string[] N3 = { "x", "y", "z" };
        static readonly string[] N4 = { "x", "y", "z", "w" };

        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                WriteHeader("ЛР 2. ДНФ и КНФ");
                Item("1", "Бинарные функции по таблице");
                Item("2", "Функции f1–f8 от трёх переменных");
                Item("3", "Функции по единичным и нулевым наборам");
                Item("4", "Сложные выражения");
                Item("5", "Калькулятор: своя таблица (2 или 3 переменные)");
                Item("0", "Назад");
                Console.WriteLine();
                WriteColor("Пункт: ", ConsoleColor.Green);
                string c = (Console.ReadLine() ?? "").Trim().ToLower();
                Console.Clear();
                if (c == "0" || c == "q" || c == "й") return;
                if (c == "1") Task1();
                else if (c == "2") Task2();
                else if (c == "3") Task3();
                else if (c == "4") Task4();
                else if (c == "5") Interactive();
                else WriteColor("Неверный пункт.\n", ConsoleColor.Red);
                Pause();
            }
        }

        static void Task1()
        {
            WriteHeader("Задание 1. Бинарные функции");
            Console.WriteLine("Строки xy: 00, 01, 10, 11.");
            Console.WriteLine("СДНФ — дизъюнкция минтермов (строки с 1). СКНФ — конъюнкция макстермов (строки с 0).");
            Console.WriteLine();
            Bin("x ↓ y   Пирс, NOR", new[] { 1, 0, 0, 0 });
            Bin("¬x·y", new[] { 0, 1, 0, 0 });
            Bin("x·¬y", new[] { 0, 0, 1, 0 });
            Bin("x ⊕ y", new[] { 0, 1, 1, 0 });
            Bin("x | y   Шеффер, NAND", new[] { 1, 1, 1, 0 });
            Bin("x ≡ y", new[] { 1, 0, 0, 1 });
            Bin("x → y", new[] { 1, 1, 0, 1 });
            Bin("x ← y", new[] { 1, 0, 1, 1 });
        }

        static void Task2()
        {
            WriteHeader("Задание 2. Три переменные");
            Console.WriteLine("Набор xyz идёт как 000 … 111.");
            int[,] t = {
                {0,1,0,0,1,0,0,0},
                {1,0,1,0,1,1,0,1},
                {1,1,1,1,1,1,1,0},
                {0,0,0,1,0,1,0,1},
                {1,1,0,1,0,1,1,0},
                {0,1,0,0,1,1,1,0},
                {1,1,1,1,1,1,0,1},
                {1,0,1,0,1,0,0,0}
            };
            for (int f = 0; f < 8; f++)
            {
                int[] v = new int[8];
                for (int i = 0; i < 8; i++) v[i] = t[i, f];
                Console.WriteLine();
                WriteColor("f" + (f + 1) + "\n", ConsoleColor.Yellow);
                Report(3, v, null, N3);
            }
        }

        static void Task3()
        {
            WriteHeader("Задание 3. Наборы xyzw");
            Console.WriteLine("Номер набора — двоичное число xyzw.");
            Console.WriteLine("а–г заданы единицами, д–е — нулями (в методичке черта над f).");
            Console.WriteLine("Эти же наборы повторены в ЛР 3, оттуда они читаются однозначно.");
            Set("а  единицы {0,1,4,5,9,11,14,15}", new[] { 0, 1, 4, 5, 9, 11, 14, 15 }, false);
            Set("б  единицы {3,4,6,7,8,10,12,13}", new[] { 3, 4, 6, 7, 8, 10, 12, 13 }, false);
            Set("в  единицы {4,7,9,10,15}", new[] { 4, 7, 9, 10, 15 }, false);
            Set("г  единицы {2,5,6,10}", new[] { 2, 5, 6, 10 }, false);
            Set("д  нули {1,3,6,8,10}", new[] { 1, 3, 6, 8, 10 }, true);
            Set("е  нули {8,11,12,14}", new[] { 8, 11, 12, 14 }, true);
        }

        static void Task4()
        {
            WriteHeader("Задание 4. Выражения");
            Console.WriteLine("В скане приоритет ↓ | ⊕ → не зафиксирован, поэтому скобки стоят явно.");
            Console.WriteLine("Форма строится перебором таблицы, а не подстановкой готовой строки.");
            Console.WriteLine();
            WriteColor("а)  ((x ↓ y) ⊕ (x | y)) → (x ↓ y)\n", ConsoleColor.Yellow);
            Report(2, Table(2, delegate (int i)
            {
                int x = Bit(i, 1), y = Bit(i, 0);
                return Imp(Xor(Nor(x, y), Nand(x, y)), Nor(x, y));
            }), null, N2);

            WriteColor("\nб)  (a ← b) ↓ (¬a ⊕ ¬b)\n", ConsoleColor.Yellow);
            Report(2, Table(2, delegate (int i)
            {
                int a = Bit(i, 1), b = Bit(i, 0);
                return Nor(Conv(a, b), Xor(1 - a, 1 - b));
            }), null, new[] { "a", "b" });

            WriteColor("\nв)  (¬(a ⊕ b) → ((a | b) ↓ c)) | (a ← (b | c))\n", ConsoleColor.Yellow);
            Report(3, Table(3, delegate (int i)
            {
                int a = Bit(i, 2), b = Bit(i, 1), c = Bit(i, 0);
                int eq = 1 - Xor(a, b);
                int left = Imp(eq, Nor(Nand(a, b), c));
                int right = Conv(a, Nand(b, c));
                return Nand(left, right);
            }), null, new[] { "a", "b", "c" });

            WriteColor("\nг)  (a | (b ← c)) ← (¬b ⊕ ¬c)\n", ConsoleColor.Yellow);
            Report(3, Table(3, delegate (int i)
            {
                int a = Bit(i, 2), b = Bit(i, 1), c = Bit(i, 0);
                return Conv(Nand(a, Conv(b, c)), Xor(1 - b, 1 - c));
            }), null, new[] { "a", "b", "c" });
        }

        static void Interactive()
        {
            WriteHeader("Своя таблица");
            Console.Write("Число переменных 2 или 3: ");
            int n;
            if (!int.TryParse(Console.ReadLine(), out n) || (n != 2 && n != 3))
            {
                WriteColor("Нужно 2 или 3.\n", ConsoleColor.Red);
                return;
            }
            string[] names = n == 2 ? N2 : N3;
            int rows = 1 << n;
            int[] v = new int[rows];
            for (int i = 0; i < rows; i++)
            {
                while (true)
                {
                    Console.Write("F(" + Bin(i, n) + ") = ");
                    string s = (Console.ReadLine() ?? "").Trim();
                    if (s == "0" || s == "1") { v[i] = s[0] - '0'; break; }
                    WriteColor("Введите 0 или 1.\n", ConsoleColor.Red);
                }
            }
            Console.WriteLine();
            Report(n, v, null, names);
        }

        static void Bin(string title, int[] values)
        {
            WriteColor(title + "\n", ConsoleColor.Yellow);
            Console.WriteLine("  таблица: " + Join(values));
            Report(2, values, null, N2);
            Console.WriteLine();
        }

        static void Set(string title, int[] nums, bool zeros)
        {
            Console.WriteLine();
            WriteColor(title + "\n", ConsoleColor.Yellow);
            int[] v = new int[16];
            if (zeros) { for (int i = 0; i < 16; i++) v[i] = 1; }
            for (int i = 0; i < nums.Length; i++) v[nums[i]] = zeros ? 0 : 1;
            Report(4, v, null, N4);
        }

        static void Report(int n, int[] values, int[] dc, string[] names)
        {
            Console.WriteLine("  СДНФ: " + Perfect(n, values, true, names));
            Console.WriteLine("  СКНФ: " + Perfect(n, values, false, names));
            Console.WriteLine("  минимальная ДНФ: " + MinimizeText(n, values, dc, names));
        }

        static int[] Table(int n, Func<int, int> f)
        {
            int[] v = new int[1 << n];
            for (int i = 0; i < v.Length; i++) v[i] = f(i);
            return v;
        }

        static int Bit(int pack, int shift) { return (pack >> shift) & 1; }
        static int Nor(int a, int b) { return (a | b) == 0 ? 1 : 0; }
        static int Nand(int a, int b) { return (a & b) == 0 ? 1 : 0; }
        static int Imp(int a, int b) { return (a == 1 && b == 0) ? 0 : 1; }
        static int Conv(int a, int b) { return Imp(b, a); }
        static int Xor(int a, int b) { return a ^ b; }

        static string Perfect(int n, int[] values, bool dnf, string[] names)
        {
            var parts = new List<string>();
            for (int m = 0; m < values.Length; m++)
            {
                bool take = dnf ? values[m] == 1 : values[m] == 0;
                if (!take) continue;
                var lits = new List<string>();
                for (int i = 0; i < n; i++)
                {
                    int bit = (m >> (n - 1 - i)) & 1;
                    bool pos = dnf ? bit == 1 : bit == 0;
                    lits.Add(pos ? names[i] : "¬" + names[i]);
                }
                parts.Add("(" + string.Join(dnf ? "·" : " + ", lits.ToArray()) + ")");
            }
            if (parts.Count == 0) return dnf ? "0" : "1";
            return string.Join(dnf ? " + " : " · ", parts.ToArray());
        }

        public static string MinimizeText(int n, int[] values, int[] dc, string[] names)
        {
            List<int> bits, dashes, cover;
            BuildMinimum(n, values, dc, out bits, out dashes, out cover);
            if (cover.Count == 0) return "0";
            var terms = new List<string>();
            for (int i = 0; i < cover.Count; i++)
                terms.Add(FormatTerm(n, bits[cover[i]], dashes[cover[i]], names));
            return string.Join(" + ", terms.ToArray());
        }

        public static void BuildMinimum(int n, int[] values, int[] dc, out List<int> bits, out List<int> dashes, out List<int> cover)
        {
            var onset = new List<int>();
            var dont = new List<int>();
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == 1) onset.Add(i);
                else if (dc != null && dc[i] == 1) dont.Add(i);
            }
            bits = new List<int>();
            dashes = new List<int>();
            cover = new List<int>();
            if (onset.Count == 0) return;
            var current = new HashSet<int>();
            for (int i = 0; i < onset.Count; i++) current.Add(Pack(onset[i], 0));
            for (int i = 0; i < dont.Count; i++) current.Add(Pack(dont[i], 0));
            var primes = new HashSet<int>();
            while (current.Count > 0)
            {
                var used = new HashSet<int>();
                var next = new HashSet<int>();
                var items = new List<int>(current);
                for (int i = 0; i < items.Count; i++)
                    for (int j = i + 1; j < items.Count; j++)
                    {
                        int b1, d1, b2, d2;
                        Unpack(items[i], out b1, out d1);
                        Unpack(items[j], out b2, out d2);
                        if (d1 != d2) continue;
                        int diff = b1 ^ b2;
                        if (diff != 0 && (diff & (diff - 1)) == 0)
                        {
                            used.Add(items[i]);
                            used.Add(items[j]);
                            next.Add(Pack(b1 & b2, d1 | diff));
                        }
                    }
                foreach (int t in current)
                    if (!used.Contains(t)) primes.Add(t);
                current = next;
            }
            foreach (int p in primes)
            {
                int b, d;
                Unpack(p, out b, out d);
                bool useful = false;
                for (int i = 0; i < onset.Count; i++)
                    if (Covers(b, d, onset[i])) { useful = true; break; }
                if (useful) { bits.Add(b); dashes.Add(d); }
            }
            cover = BestCover(n, bits, dashes, onset);
        }

        static List<int> BestCover(int n, List<int> bits, List<int> dashes, List<int> onset)
        {
            int p = bits.Count;
            int m = onset.Count;
            int[] cov = new int[p];
            int[] lit = new int[p];
            for (int i = 0; i < p; i++)
            {
                int mask = 0;
                for (int j = 0; j < m; j++)
                    if (Covers(bits[i], dashes[i], onset[j])) mask |= 1 << j;
                cov[i] = mask;
                lit[i] = n - BitCount(dashes[i]);
            }
            var search = new CoverSearch();
            search.cov = cov;
            search.lit = lit;
            search.full = (1 << m) - 1;
            search.p = p;
            search.bestCount = 99;
            search.bestLit = 99;
            search.best = new List<int>();
            search.chosen = new List<int>();
            search.Go(0, 0, 0, 0);
            return search.best;
        }

        sealed class CoverSearch
        {
            public int[] cov;
            public int[] lit;
            public int full, p, bestCount, bestLit;
            public List<int> best, chosen;
            public void Go(int i, int covered, int count, int litsum)
            {
                if (count > bestCount || (count == bestCount && litsum >= bestLit)) return;
                if (covered == full)
                {
                    bestCount = count;
                    bestLit = litsum;
                    best = new List<int>(chosen);
                    return;
                }
                if (i == p) return;
                Go(i + 1, covered, count, litsum);
                int nc = covered | cov[i];
                if (nc != covered)
                {
                    chosen.Add(i);
                    Go(i + 1, nc, count + 1, litsum + lit[i]);
                    chosen.RemoveAt(chosen.Count - 1);
                }
            }
        }

        public static string FormatTerm(int n, int bits, int dashes, string[] names)
        {
            var lits = new List<string>();
            for (int i = 0; i < n; i++)
            {
                int shift = n - 1 - i;
                if (((dashes >> shift) & 1) == 1) continue;
                lits.Add(((bits >> shift) & 1) == 1 ? names[i] : "¬" + names[i]);
            }
            if (lits.Count == 0) return "1";
            return string.Join("·", lits.ToArray());
        }

        static bool Covers(int bits, int dashes, int m)
        {
            return ((m ^ bits) & ~dashes) == 0;
        }

        static int Pack(int bits, int dashes) { return (dashes << 8) | bits; }
        static void Unpack(int p, out int bits, out int dashes)
        {
            bits = p & 255;
            dashes = p >> 8;
        }
        static int BitCount(int x)
        {
            int c = 0;
            while (x != 0) { c += x & 1; x >>= 1; }
            return c;
        }
        static string Bin(int v, int n)
        {
            var s = new char[n];
            for (int i = n - 1; i >= 0; i--) { s[i] = ((v & 1) == 1) ? '1' : '0'; v >>= 1; }
            return new string(s);
        }
        static string Join(int[] a)
        {
            var s = new string[a.Length];
            for (int i = 0; i < a.Length; i++) s[i] = a[i].ToString();
            return string.Join(" ", s);
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