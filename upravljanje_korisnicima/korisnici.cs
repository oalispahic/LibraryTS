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


}