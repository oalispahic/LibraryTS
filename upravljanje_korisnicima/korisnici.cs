using System;
using System.Text.Json;
using System.IO;
using System.Linq;

public enum Uloge
{
    Administrator,
    Student,
    Profesor,
}

public class Korisnik
{
    string[] uloge = { "Administrator", "Student", "Profesor" };

    private static int lastAssignedId = 0;
    public string Ime { get; private set; }
    public string Prezime { get; private set; }
    public Uloge Uloga { get; private set; }

    public int userID { get; private set; }

    public Korisnik(string ime, string prezime, Uloge uloga)
    {
        this.Ime = ime;
        this.Prezime = prezime;
        this.Uloga = uloga;
        this.userID = ++lastAssignedId;

    }

    public static void InitializeLastId()
    {
        string korisnicka_baza = "korisnici.json";
        if (File.Exists(korisnicka_baza))
        {
            string jsonString = File.ReadAllText(korisnicka_baza);
            var korisnici = JsonSerializer.Deserialize<Korisnik[]>(jsonString);
            if (korisnici != null && korisnici.Length > 0)
            {
                lastAssignedId = korisnici.Max(k => k.userID);
            }
        }
    }
}