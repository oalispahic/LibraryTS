using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    [JsonInclude]
    public string Ime { get; private set; }

    [JsonInclude]
    public string Prezime { get; private set; }

    [JsonInclude]
    public Uloge Uloga { get; set; }

    [JsonInclude]
    public int userID { get; private set; }

    public Korisnik(string ime, string prezime, Uloge uloga)
    {
        this.Ime = ime;
        this.Prezime = prezime;
        this.Uloga = uloga;
        this.userID = ++lastAssignedId;
    }
}
