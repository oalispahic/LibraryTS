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
        Console.Write("Unesite žanr (Roman, Nauka, Historija, Fantastika, Drugi): ");
        var zanrInput = Console.ReadLine()?.Trim();

        if (Enum.TryParse<Zanr>(zanrInput, out var zanr))
        {
            var knjiga = new Knjiga(naslov, autor, zanr);
            Knjige.Add(knjiga);
            SpasiKnjige();
            Console.WriteLine("Knjiga uspješno dodana!");
        }
        else
        {
            Console.WriteLine("Neispravan žanr!");
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
                Console.WriteLine("Knjiga uspješno izbrisana!");
            }
            else
            {
                Console.WriteLine("Knjiga sa tim ID-om nije pronađena!");
            }
        }
        Cekanje();
    }

    public void ObrisiKnjiguNaslov()
{
    Console.Write("Unesite naslov knjige za brisanje: ");
    var naslov = Console.ReadLine()?.Trim();
    var knjiga = Knjige.Find(k => 
        k.Naslov != null && 
        k.Naslov.Equals(naslov, StringComparison.OrdinalIgnoreCase));

    if (knjiga != null)
    {
        Knjige.Remove(knjiga);
        SpasiKnjige();
        Console.WriteLine("Knjiga uspješno izbrisana!");
    }
    else
    {
        Console.WriteLine("Knjiga sa tim naslovom nije pronađena!");
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
            Console.WriteLine($"ID: {k.ID}, Naslov: {k.Naslov}, Autor: {k.Autor}, Žanr: {k.Zanr}, Dostupna: {k.Dostupna}");
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
        Console.Write("Unesite žanr za pretragu: ");
        var query = Console.ReadLine()?.Trim();
        if (Enum.TryParse<Zanr>(query, out var zanr))
        {
            var results = Knjige.Where(k => k.Zanr == zanr).ToList();
            IspisiRezultatePretrage(results);
        }
        else
        {
            Console.WriteLine("Neispravan žanr!");
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
                Console.WriteLine($"ID: {k.ID}, Naslov: {k.Naslov}, Autor: {k.Autor}, Žanr: {k.Zanr}, Dostupna: {k.Dostupna}");
            }
        }
        Cekanje();
    }

    public void AzurirajKnjigu()
{
    Console.Write("Unesite ID knjige koju želite ažurirati: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var knjiga = Knjige.Find(k => k.ID == id);
        if (knjiga == null)
        {
            Console.WriteLine("Knjiga nije pronađena!");
            Cekanje();
            return;
        }

        Console.WriteLine($"Trenutni naslov: {knjiga.Naslov}");
        Console.Write("Novi naslov (Enter za preskok): ");
        var noviNaslov = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(noviNaslov))
            knjiga.GetType().GetProperty("Naslov")?.SetValue(knjiga, noviNaslov);

        Console.WriteLine($"Trenutni autor: {knjiga.Autor}");
        Console.Write("Novi autor (Enter za preskok): ");
        var noviAutor = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(noviAutor))
            knjiga.GetType().GetProperty("Autor")?.SetValue(knjiga, noviAutor);

        Console.WriteLine($"Trenutni žanr: {knjiga.Zanr}");
        Console.Write("Novi žanr (Enter za preskok): ");
        var noviZanrInput = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(noviZanrInput) && Enum.TryParse<Zanr>(noviZanrInput, out var noviZanr))
            knjiga.Zanr = noviZanr;

        SpasiKnjige();
        Console.WriteLine("Knjiga uspješno ažurirana!");
    }
    else
    {
        Console.WriteLine("Neispravan ID!");
    }
    Cekanje();
}

public void PromijeniDostupnost()
{
    Console.Write("Unesite ID knjige: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var knjiga = Knjige.Find(k => k.ID == id);
        if (knjiga == null)
        {
            Console.WriteLine("Knjiga nije pronađena!");
            Cekanje();
            return;
        }

        knjiga.Dostupna = !knjiga.Dostupna;
        SpasiKnjige();
        Console.WriteLine(knjiga.Dostupna
            ? $"Knjiga '{knjiga.Naslov}' je sada dostupna."
            : $"Knjiga '{knjiga.Naslov}' je označena kao izdata.");
    }
    else
    {
        Console.WriteLine("Neispravan ID!");
    }
    Cekanje();
}
}
