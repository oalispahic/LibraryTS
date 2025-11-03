/*using System;
using System.Collections.Generic;
using LibraryTS;

public class upravljanje_korisnicima
{
    public List<Korisnik> korisnici { get; private set; }

    private void cekanje()
    {
        Console.WriteLine("\nPritisnite bilo koju tipku za povratak u glavni meni...");
        Console.ReadKey();
    }

    public upravljanje_korisnicima()
    {
        korisnici = new List<Korisnik>
        {
            new Korisnik("Omar", "Alispahic", Uloge.Administrator),
            new Korisnik("Jane", "Smith", Uloge.Student),
            new Korisnik("Alice", "Johnson", Uloge.Profesor),
        };
    }

    public void DodajKorisnika()
    {
        Console.Write("Unesite ime: ");
        var ime = Console.ReadLine();
        Console.Write("Unesite prezime: ");
        var prezime = Console.ReadLine();
        Console.Write("Unesite ulogu (Administrator, Student, Profesor): ");
        var ulogaInput = Console.ReadLine();
        if (Enum.TryParse<Uloge>(ulogaInput, out var uloga))
        {
            var noviKorisnik = new Korisnik(ime, prezime, uloga);
            korisnici.Add(noviKorisnik);
            Console.WriteLine("Korisnik uspješno dodan! /n");
            cekanje();
        }
        else
        {
            Console.WriteLine("Neispravna uloga! /n");
            cekanje();
        }
    }

    public void ObrisiKorisnikaID()
    {
        var userID = 0;
        Console.Write("Unesite ID korisnika za brisanje: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out userID))
        {
            var korisnikZaBrisanje = korisnici.Find(k => k.userID == userID);
            if (korisnikZaBrisanje != null)
            {
                korisnici.Remove(korisnikZaBrisanje);
                Console.WriteLine("Korisnik uspješno izbrisan! /n");
                cekanje();
            }
            else
            {
                Console.WriteLine("Korisnik sa unesenim ID-om nije pronađen! /n");
                cekanje();
            }
        }
    }

    public void ObrisiKorisnikaIme()
    {
        Console.Write("Unesite ime korisnika za brisanje: ");
        var ime = Console.ReadLine();
        Console.Write("Unesite prezime korisnika za brisanje: ");
        var prezime = Console.ReadLine();
        var korisnikZaBrisanje = korisnici.Find(k => k.Ime == ime && k.Prezime == prezime);
        if (korisnikZaBrisanje != null)
        {
            korisnici.Remove(korisnikZaBrisanje);
            Console.WriteLine("Korisnik uspješno izbrisan! /n");
            cekanje();
        }
        else
        {
            Console.WriteLine("Korisnik sa unesenim imenom i prezimenom nije pronađen! /n");
            cekanje();
        }
    }

    public void IspisiSve()
    {
        Console.Clear();
        if (korisnici.Count == 0)
        {
            Console.WriteLine("Nema registrovanih korisnika.");
            return;
        }
        Console.WriteLine("=====SPISAK KORISNIKA=====\n");
        foreach (var k in korisnici)
        {
            Console.WriteLine(
                $"ID: {k.userID}, Ime: {k.Ime}, Prezime: {k.Prezime}, Uloga: {k.Uloga}"
            );
        }
        cekanje();
    }

    public void promijeniUlogu()
    {
        Console.Write("Unesite ID korisnika kojem mijenjate ulogu: ");
        var input = Console.ReadLine();
        int userID = 0;
        if (int.TryParse(input, out userID))
        {
            var korisnik = korisnici.Find(k => k.userID == userID);
            if (korisnik != null)
            {
                Console.Write("Korisnik sa ID-em {userID} pronadjen.");
                Console.Write(
                    korisnik.Ime + " " + korisnik.Prezime + " ima ulogu " + korisnik.Uloga + "\n"
                );
                Console.Write("Unesite novu ulogu (Administrator, Student, Profesor): ");
                var ulogaInput = Console.ReadLine();
                if (Enum.TryParse<Uloge>(ulogaInput, out var novaUloga))
                {
                    korisnik.Uloga = novaUloga;
                    Console.WriteLine("Uloga uspješno promijenjena! /n");
                    cekanje();
                }
                else
                {
                    Console.WriteLine("Korisnik sa unesenim ID-om nije pronađen! /n");
                    cekanje();
                }
            }
        }
    }
}
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LibraryTS;

public class upravljanje_korisnicima
{
    

    private string FilePath = Path.Combine(AppContext.BaseDirectory, "users.json");

    public List<Korisnik> korisnici { get; private set; }

    private void cekanje()
    {
        Console.WriteLine("\nPritisnite bilo koju tipku za povratak u glavni meni...");
        Console.ReadKey();
    }

    public List<Korisnik> UcitajKorisnike()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                var loaded = JsonSerializer.Deserialize<List<Korisnik>>(json);
                return loaded ?? new List<Korisnik>();
            }
            catch
            {
                return new List<Korisnik>();
            }
        }
        return new List<Korisnik>();
    }

    public upravljanje_korisnicima()
    {
        {
            korisnici = UcitajKorisnike();
            if (korisnici.Count > 0)
                Korisnik.lastAssignedId = korisnici.Max(k => k.userID);
        }
    }

    public void SpasiKorisnike()
    {
        string json = JsonSerializer.Serialize(
            korisnici,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(FilePath, json);
    }

    public void DodajKorisnika()
    {
        Console.Clear();
        Console.WriteLine("=====DODAVANJE KORISNIKA=====\n");
        Console.Write("Unesite ime: ");
        var ime = Console.ReadLine();
        Console.Write("Unesite prezime: ");
        var prezime = Console.ReadLine();
        Console.Write("Unesite ulogu (Administrator, Student, Profesor): ");
        var ulogaInput = Console.ReadLine();
        if (Enum.TryParse<Uloge>(ulogaInput, out var uloga))
        {
            var noviKorisnik = new Korisnik(ime, prezime, uloga);
            korisnici.Add(noviKorisnik);
            SpasiKorisnike();
            Console.WriteLine("Korisnik uspješno dodan!");
            cekanje();
        }
        else
        {
            Console.WriteLine("Neispravna uloga!");
            cekanje();
        }
    }

    public void IspisiSve()
    {
        Console.Clear();
        Console.WriteLine("=====SPISAK KORISNIKA=====\n");
        if (korisnici.Count == 0)
        {
            Console.WriteLine("Nema registrovanih korisnika.");
            return;
        }
        foreach (var k in korisnici)
        {
            Console.WriteLine(
                $"ID: {k.userID}, Ime: {k.Ime}, Prezime: {k.Prezime}, Uloga: {k.Uloga}"
            );
        }
        cekanje();
    }

    public void ObrisiKorisnikaID()
    {
        var userID = 0;
        Console.Write("Unesite ID korisnika za brisanje: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out userID))
        {
            var korisnikZaBrisanje = korisnici.Find(k => k.userID == userID);
            if (korisnikZaBrisanje != null)
            {
                korisnici.Remove(korisnikZaBrisanje);
                SpasiKorisnike();
                Console.WriteLine("Korisnik uspješno izbrisan!" + "\n");
                cekanje();
            }
            else
            {
                Console.WriteLine("Korisnik sa unesenim ID-om nije pronađen!" + "\n");
                cekanje();
            }
        }
    }

    public void ObrisiKorisnikaIme()
    {
        Console.Write("Unesite ime korisnika za brisanje: ");
        var ime = Console.ReadLine()?.Trim();
        Console.Write("Unesite prezime korisnika za brisanje: ");
        var prezime = Console.ReadLine()?.Trim();
        var korisnikZaBrisanje = korisnici.Find(k => k.Ime == ime && k.Prezime == prezime);
        if (korisnikZaBrisanje != null)
        {
            korisnici.Remove(korisnikZaBrisanje);
            SpasiKorisnike();
            Console.WriteLine("Korisnik uspješno izbrisan!"+ "\n");
            cekanje();
        }
        else
        {
            Console.WriteLine("Korisnik sa unesenim imenom i prezimenom nije pronađen!"+ "\n");
            cekanje();
        }
    }
}
