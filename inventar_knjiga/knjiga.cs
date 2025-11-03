using System;
using System.Text.Json.Serialization;

public enum Zanr
{
    Roman,
    Nauka,
    Historija,
    Fantastika,
    Drugi
}

public class Knjiga
{
    public static int lastAssignedId = 0;

    [JsonInclude]
    public int ID { get; private set; }

    [JsonInclude]
    public string Naslov { get; private set; }

    [JsonInclude]
    public string Autor { get; private set; }

    [JsonInclude]
    public Zanr Zanr { get; set; }

    [JsonInclude]
    public bool Dostupna { get; set; }

    public Knjiga(string naslov, string autor, Zanr zanr, bool dostupna = true)
    {
        this.ID = ++lastAssignedId;
        this.Naslov = naslov;
        this.Autor = autor;
        this.Zanr = zanr;
        this.Dostupna = dostupna;
    }
}
