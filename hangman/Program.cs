using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Wisielec
{
    class Program
    {
        public static string _word = (char)32 + (char)32 + WordRandomizer();  // zwraca losowo wybrane słowo z pliku --> return "string";
        static char[] WordChar = new char[_word.Length];               
        static char[] WisielecChar = { ' ', 'w', 'i', 's', 'i', 'e', 'l', 'e', 'c' }; // ' ' -> puste miejsce żeby 'w' wyświetliło się z opóźnieniem (bo index 0)
        static char[] WisielecChar2 = new char[9];
        static char[] id = new char[WordChar.Length];
        static void Main(string[] args)
        {
            int counter = -1;
            int hlpr = 0;
            char letter;
            int check = 0;
            for (int i = 2; i < WordChar.Length; i++) // WordChar.Length / 4
            {
                WordChar[i] = '-';
            }
            for (int i = 1; i < WisielecChar.Length; i++)
            {
                WisielecChar2[i] = '-';
            }
            Console.WriteLine("---------WPROWADZAJ LITERY POWODZENIA!--------\n");
            do
            {
                ++counter;
                Console.WriteLine();
                try
                {
                    letter = Convert.ToChar(Console.ReadLine());
                    checkLetter(letter);
                    if (checkLetter(letter) == false || letter == '4') // cz.1 c.d. w checkLetter() Z NIEZNANEGO MI POWODU GDY PODAWAŁEM 4 TO PRZYPISYWAŁO TĄ PRZEKONVERTOWANĄ LICZBĘ NA POCZĄTKU ODGADYWANEGO SŁOWA
                    {
                        ++hlpr; // Nabija pkty wisielca!!! i komunikat o przegranej
                    }
                    else
                    {
                        check++; // nabija pkty szukanego słowa (gdy wartość true)
                    }
                }
                catch          // Ten blok powoduje, że gdy pojawia się błąd ArgumentException to nie wywala programu tylko pojawia się komunikat i można kontynuować grę                                
                {
                    Console.WriteLine("Musisz podać dokładnie jedną literę!");
                }
                if (hlpr == 8) //hlpr nabija się tylko wtedy gdy litera nie znajduje się w słowie
                {
                    Console.WriteLine("Przegrałeś!!!\nNaciśnij enter żeby wyjść");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                if (check == WordChar.Length - 2) //jeśli wartość licznika jest równa długości słowa (czyli wszystkie litery zostały odgadnięte) to wyświetl komunikat
                {
                    Console.WriteLine($"Wygrałeś!\nNaciśnij ENTER żeby wyjść");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                WisielecChar2[hlpr] = WisielecChar[hlpr];  // zamieniamy "-------" na "wisielec"
                foreach (char l in WisielecChar2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(l);
                    Console.ResetColor();
                }
            }
            while (true); 
        }
        public static string WordRandomizer()
        {
            string plik = @"../../words/wordlist.txt";
            Random rnd = new Random();
            int ile = 1001;                        
            int counter = 0;                        
            byte[] dane = new byte[ile];
            char[] dane2 = new char[20];
            char[] error = { 'b', 'ł', 'ą', 'd' };
            string word = "";
            string wrd = "";
            int r = 0;
            FileStream fs;
            try
            {
                fs = new FileStream(plik, FileMode.Open);
            }
            catch (Exception)
            {
                Console.WriteLine("BŁĄD: Otwarcie pliku {0} nie powiodło się.", plik);
                return ""; // return musi zwracać wartość string, bo przy def. metody jest string a nie void
            }
            try
            {
                fs.Read(dane, 0, ile);
                fs.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("BŁĄD: Odczyt pliku {0} nie został dokonany.", plik);
                return ""; // return musi zwracać wartość string, bo przy def. metody jest string a nie void
            }
            for(int i = 0; i < ile; i++)
            {
                    word += (char)(dane[i]);
            }
            while (word[r] != (char)10)
            {
                r = rnd.Next(0, 1000);
            }
            for (int i = r + 1; i < word.IndexOf((char)10, r + 1); i++) //instrukcja przypisania słowa poznanego po tym, że przed i po ciągu liter jest nowa linia((char)10)
            {
                counter++;
                dane2[counter] += word[i];
            }
            for(int i = 0; i < dane2.Length; i++)
            {
                if(dane2[i] != (char)0)  //nie przypisuje pustych pól, stosując instrukcję null != (char)0 
                {
                    wrd += dane2[i];
                }
            }
        //!!!!!!!!!    //Console.WriteLine(wrd); // JEŚLI CHCESZ ZROBIĆ TEST DZIAŁANIA ODKOMENTUJ!!!!!!!!!!
            return wrd; // zwraca utworzony string - czyli zmienną zawierającą wylosowane słowo 
        }
        public static bool checkLetter(char letter) 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 1; i < _word.Length; i++)
            {
                if (_word[i] == letter && letter != '4')     // cz.2 Z NIEZNANEGO MI POWODU GDY PODAWAŁEM 4 TO PRZYPISYWAŁO TĄ PRZEKONVERTOWANĄ LICZBĘ NA POCZĄTKU ODGADYWANEGO SŁOWA
                {
                    WordChar[i] = letter;
                }
            }
            for (int i = 1; i < _word.Length; i++)     // NOWA LINIA W ASCII = 10
            { 
                Console.Write(WordChar[i]);
            }
            Console.WriteLine();
            Console.ResetColor();
            if (_word.IndexOf(letter) != -1) // jeśli index istnieje to wartość jest różna od -1 na tej podstawie stwiedzam czy ktoś trafił literę czy nie
            {
                return true;
            }
            else
            {
                return false;
            }
            //--------------------------inne rozwiązanie dla przeszukiwania elementów---------------------------
            /*foreach (char l in WordChar)
            {
                if (l == letter)
                {
                    checker = true;
                }
            }*/

            //return checker;
        }
    }
}
