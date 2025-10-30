using System;
using System.Text.Json;
using System.IO;

class upravljanje_korisnicima
{
    private string korisnicka_baza = "korisnici.json";
    //dodat knjige 

    private List<Korisnik> korisnici;

    public upravljanje_korisnicima()
    {
        korisnici = Load<List<Korisnik>>(korisnicka_baza) ?? new List<Korisnik>();
        //dodat za knjige isto
    }

    private static T? Load<T>(string path)
    {
        if (!File.Exists(path)) return default;
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json);
    }
}
    
   