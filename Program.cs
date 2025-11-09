using System;
using LibraryTS;

namespace LibraryTS
{
    class Program
    {
        static void Main(string[] args)
        {
            var meni = new MeniOpcije();
            var userManager = new upravljanje_korisnicima();
            var bookManager = new upravljanje_knjigama();
           

            meni.userManager = userManager;
            meni.bookManager = bookManager;

            Console.Clear();
            var izbor = "0";
            bool prvi_ispis = true;
            while (izbor != "9")
            {
                Console.Clear();
                if (prvi_ispis)
                {
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Dobrodošli u biblioteka system!");
                    Console.WriteLine("-------------------------------");
                }
                Console.WriteLine("=====GLAVNI MENI=====\n");
                Console.WriteLine("1. Ažuriranje korisnika");
                Console.WriteLine("2. Izlistaj sve korisnike");
                Console.WriteLine("3. Ažuriranje knjiga");
                Console.WriteLine("4. Izlistaj sve knjige");
                Console.WriteLine("9. Izlaz");
                Console.Write("Izaberite opciju: ");
                prvi_ispis = false;
                izbor = Console.ReadLine();

                switch (izbor)
                {
                    case "1":
                        meni.azuriranje_korisnika();
                        break;
                    case "2":
                        userManager.IspisiSve();
                        break;
                    case "3":
                        meni.azuriranje_knjiga();
                        break;
                    case "4":
                        bookManager.IspisiSve();
                        break;
                    case "5":
                        meni.Najam();
                        break;
                    case "9":
                        Console.WriteLine("Izlaz iz programa. Doviđenja!");
                        break;
                    default:
                        Console.WriteLine("Neispravan izbor, pokušajte ponovo.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
