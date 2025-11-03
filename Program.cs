using System;

class program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Dobrodošli u biblioteka system!");
        Console.WriteLine("-------------------------------");
        var izbor = "0";
        var userManager = new upravljanje_korisnicima();
        while (izbor != "9")
        {
            Console.WriteLine("1. Prikazi sve korisnike");
            Console.WriteLine("2. Dodaj korisnika");
            Console.WriteLine("3. Pronadji korisnika po ID-u");
            Console.WriteLine("9. Izlaz");
            Console.Write("Izaberite opciju: ");
            izbor = Console.ReadLine();

            switch (izbor)
            {
                case "1":
                    userManager.IspisiSve();
                    break;
                case "2":
                    Console.Write("Unesite ime: ");
                    var ime = Console.ReadLine();
                    Console.Write("Unesite prezime: ");
                    var prezime = Console.ReadLine();
                    Console.Write("Unesite ulogu (Administrator, Student, Profesor): ");
                    var ulogaInput = Console.ReadLine();
                    if (Enum.TryParse<Uloge>(ulogaInput, out var uloga))
                    {
                        var noviKorisnik = new Korisnik(ime, prezime, uloga);
                        userManager.DodajKorisnika(noviKorisnik);
                        Console.WriteLine("Korisnik uspješno dodan!");
                    }
                    else
                    {
                        Console.WriteLine("Neispravna uloga!");
                    }
                    break;
                    case "3":
                    Console.Write("Unesite ID korisnika: ");
                    var idInput = Console.ReadLine();
                    if (int.TryParse(idInput, out var id))
                    {
                        var korisnik = userManager.korisnici.Find(k => k.userID == id);
                        if (korisnik != null)
                        {
                            Console.WriteLine($"ID: {korisnik.userID}, Ime: {korisnik.Ime}, Prezime: {korisnik.Prezime}, Uloga: {korisnik.Uloga}");
                        }
                        else
                        {
                            Console.WriteLine("Korisnik nije pronađen.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Neispravan ID!");
                    }
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