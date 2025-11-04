using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class UpravljanjeKnjigama
{
    private const string FilePath = "books.json";
    public List<Knjiga> Knjige { get; private set; }

    private void Cekanje()
    {
        Console.WriteLine("\nPritisnite bilo koju tipku za povratak u meni...");
        Console.ReadKey();
    }

    public UpravljanjeKnjigama()
    {
        Knjige = UcitajKnjige();
        if (Knjige.Count > 0)
            Knjiga.lastAssignedId = Knjige.Max(k => k.ID);
    }

    public List<Knjiga> UcitajKnjige()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Knjiga>>(json) ?? new List<Knjiga>();
            }
            catch
            {
                return new List<Knjiga>();
            }
        }
        return new List<Knjiga>();
    }

    public void SpasiKnjige()
    {
        string json = JsonSerializer.Serialize(Knjige, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public void DodajKnjigu()
    {
        Console.Clear();
        Console.WriteLine("===== DODAVANJE KNJIGE =====\n");
        Console.Write("Unesite naslov: ");
        var naslov = Console.ReadLine()?.Trim();
        Console.Write("Unesite autora: ");
        var autor = Console.ReadLine()?.Trim();
        Console.Write("Unesite zanr (Roman, Nauka, Historija, Fantastika, Drugi): ");
        var zanrInput = Console.ReadLine()?.Trim();

        if (Enum.TryParse<Zanr>(zanrInput, out var zanr))
        {
            var knjiga = new Knjiga(naslov, autor, zanr);
            Knjige.Add(knjiga);
            SpasiKnjige();
            Console.WriteLine("Knjiga uspjesno dodana!");
        }
        else
        {
            Console.WriteLine("Neispravan zanr!");
        }
        Cekanje();
    }

    public void ObrisiKnjiguID()
    {
        Console.Write("Unesite ID knjige za brisanje: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var knjiga = Knjige.Find(k => k.ID == id);
            if (knjiga != null)
            {
                Knjige.Remove(knjiga);
                SpasiKnjige();
                Console.WriteLine("Knjiga uspjesno izbrisana!");
            }
            else
            {
                Console.WriteLine("Knjiga sa tim ID-om nije pronadjena!");
            }
        }
        Cekanje();
    }

    public void IspisiSve()
    {
        Console.Clear();
        Console.WriteLine("===== SPISAK KNJIGA =====\n");
        if (Knjige.Count == 0)
        {
            Console.WriteLine("Nema knjiga u inventaru.");
            Cekanje();
            return;
        }

        foreach (var k in Knjige)
        {
            Console.WriteLine($"ID: {k.ID}, Naslov: {k.Naslov}, Autor: {k.Autor}, Zanr: {k.Zanr}, Dostupna: {k.Dostupna}");
        }
        Cekanje();
    }

    public void PretragaPoNaslovu()
    {
        Console.Write("Unesite naslov za pretragu: ");
        var query = Console.ReadLine()?.Trim().ToLower();
        var results = Knjige.Where(k => k.Naslov.ToLower().Contains(query)).ToList();
        IspisiRezultatePretrage(results);
    }

    public void PretragaPoAutoru()
    {
        Console.Write("Unesite autora za pretragu: ");
        var query = Console.ReadLine()?.Trim().ToLower();
        var results = Knjige.Where(k => k.Autor.ToLower().Contains(query)).ToList();
        IspisiRezultatePretrage(results);
    }

    public void PretragaPoZanru()
    {
        Console.Write("Unesite zanr za pretragu: ");
        var query = Console.ReadLine()?.Trim();
        if (Enum.TryParse<Zanr>(query, out var zanr))
        {
            var results = Knjige.Where(k => k.Zanr == zanr).ToList();
            IspisiRezultatePretrage(results);
        }
        else
        {
            Console.WriteLine("Neispravan zanr!");
            Cekanje();
        }
    }

    private void IspisiRezultatePretrage(List<Knjiga> results)
    {
        if (results.Count == 0)
        {
            Console.WriteLine("Nema rezultata.");
        }
        else
        {
            foreach (var k in results)
            {
                Console.WriteLine($"ID: {k.ID}, Naslov: {k.Naslov}, Autor: {k.Autor}, Zanr: {k.Zanr}, Dostupna: {k.Dostupna}");
            }
        }
        Cekanje();
    }
}
