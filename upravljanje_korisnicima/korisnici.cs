using System;

public enum Uloga
{
    Administrator,
    Student,
    Profesor,
}

public class Korisnik
{
    string []uloge = { "Administrator", "Student", "Profesor" };    
    public string Ime;
    public string Prezime;
    public Uloga KorisnickaUloga;

    public int userID;

    public Korisnik(string ime, string prezime, Uloga uloga)
    {
        this.Ime = ime;
        this.Prezime = prezime;
        this.KorisnickaUloga = uloga;

    }
}