using System;
using System.Collections.Generic;

public class upravljanje_korisnicima
{
    public List<Korisnik> korisnici { get; private set; }

    public upravljanje_korisnicima()
    {
        korisnici = new List<Korisnik>
        {
            new Korisnik("Omar", "Alispahic", Uloge.Administrator),
            new Korisnik("Jane", "Smith", Uloge.Student),
            new Korisnik("Alice", "Johnson", Uloge.Profesor)
        };
    }

    public void DodajKorisnika(Korisnik korisnik)
    {
        korisnici.Add(korisnik);
    }

    public void IspisiSve()
    {
        foreach (var k in korisnici)
        {
            Console.WriteLine($"ID: {k.userID}, Ime: {k.Ime}, Prezime: {k.Prezime}, Uloga: {k.Uloga}");
        }
    }
}
