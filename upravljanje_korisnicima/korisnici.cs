using System;
using System.IO;
using System.Linq;
using System.Text.Json;

public enum Uloge
{
    Administrator,
    Student,
    Profesor,
}

public class Korisnik
{
    string[] uloge = { "Administrator", "Student", "Profesor" };

    public static int lastAssignedId = 0;
    public string Ime { get; private set; }
    public string Prezime { get; private set; }
    public Uloge Uloga { get; set; }

    public int userID { get; private set; }

    public Korisnik(string ime, string prezime, Uloge uloga)
    {
        this.Ime = ime;
        this.Prezime = prezime;
        this.Uloga = uloga;
        this.userID = ++lastAssignedId;
    }
}

/*
using System;
using System.Text.Json.Serialization;

public enum Uloge
{
    Administrator,
    Student,
    Profesor,
}

public class Korisnik
{
    private static int lastAssignedId = 0;

    [JsonInclude]
    public string Ime { get; private set; }

    [JsonInclude]
    public string Prezime { get; private set; }

    [JsonInclude]
    public Uloge Uloga { get; set; } // now public setter ✅

    [JsonInclude]
    public int userID { get; private set; }

    public Korisnik(string ime, string prezime, Uloge uloga)
    {
        this.Ime = ime;
        this.Prezime = prezime;
        this.Uloga = uloga;
        this.userID = ++lastAssignedId;
    }

    // Needed for JSON deserialization
    public Korisnik() { }

    public static void SetLastAssignedId(int value)
    {
        if (value > lastAssignedId)
            lastAssignedId = value;
    }
}
*/