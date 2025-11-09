using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

public enum Zanr
{
    Roman,
    Nauka,
    Historija,
    Fantastika,
    Drugi,
}

public class Knjiga
{
    string[] zanrovi = {"Roman", "Nauka", "Historija", "Fantastika", "Drugi"};

    public static int lastAssignedId = 0;

    [JsonInclude]
    public int bookID { get; private set; }

    [JsonInclude]
    public string Naslov { get; set; }

    [JsonInclude]
    public string Autor { get; set; }

    [JsonInclude]
    public Zanr Zanr { get; set; }

    [JsonInclude]
    public bool Dostupna { get; set; }

    public Knjiga(string naslov, string autor, Zanr zanr, bool dostupna = true)
    {
        this.bookID = ++lastAssignedId;
        this.Naslov = naslov;
        this.Autor = autor;
        this.Zanr = zanr;
        this.Dostupna = dostupna;
    }
}
