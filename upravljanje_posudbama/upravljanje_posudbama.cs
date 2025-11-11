using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class upravljanje_posudbama
{
    private upravljanje_korisnicima userManager;
    private upravljanje_knjigama bookManager;

    private void cekanje()
    {
        Console.WriteLine("\nPritisnite bilo koju tipku za povratak u glavni meni...");
        Console.ReadKey();
    }

    public void SpasiPosudbe()
    {
        string json = JsonSerializer.Serialize(
            posudbe,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(FilePath, json);
    }

    private string FilePath = Path.Combine(AppContext.BaseDirectory, "loans.json");
    public List<posudba> posudbe { get; set; }

    public List<posudba> UcitajPosudbe()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                var loaded = JsonSerializer.Deserialize<List<posudba>>(json);
                return loaded ?? new List<posudba>();
            }
            catch
            {
                return new List<posudba>();
            }
        }
        return new List<posudba>();
    }

    public upravljanje_posudbama(upravljanje_korisnicima userManager, upravljanje_knjigama bookManager)
    {
        this.userManager = userManager;
        this.bookManager = bookManager;
        {
            posudbe = UcitajPosudbe();
            if (posudbe.Count > 0)
                posudba.lastAssignedId = posudbe.Max(p => p.posudbaID);
        }
    }

    public void posudi()
    {
        Console.Clear();
        Console.WriteLine("=====POSUDJIVANJE KNJIGE=====\n");
        var userID = 0;
        Console.Write("Unesite ID korisnika koji iznajmljuje knjigu: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out userID))
        {
            var korisnik = userManager.korisnici.Find(k => k.userID == userID);
            if (korisnik != null)
            {
                Console.Write("Unesite ID knjige za iznajmljivanje: ");
                var bookInput = Console.ReadLine();
                if (int.TryParse(bookInput, out var bookID))
                {
                    var knjiga = bookManager.knjige.Find(b => b.bookID == bookID);
                    if (knjiga != null && knjiga.Dostupna)
                    {
                        var novaPosudba = new posudba();
                        {
                            novaPosudba.posudbaID = ++posudba.lastAssignedId;
                            novaPosudba.userID = userID;
                            novaPosudba.bookID = bookID;
                            novaPosudba.datumPosudbe = DateTime.Now;
                            novaPosudba.datumVracanja = DateTime.Now.AddDays(14); // Defaultni rok vracanja 14 dana
                        }
                        ;
                        posudbe.Add(novaPosudba);
                        knjiga.Dostupna = false;
                        SpasiPosudbe();
                        bookManager.SpasiKnjige();
                        Console.WriteLine("Knjiga je uspješno iznajmljena." + "\n");
                        cekanje();
                    }
                    else
                    {
                        Console.WriteLine("Knjiga nije dostupna za iznajmljivanje." + "\n");
                        cekanje();
                    }
                }
                else
                {
                    Console.WriteLine("Neispravan ID knjige." + "\n");
                    cekanje();
                }
            }
            else
            {
                Console.WriteLine("Korisnik s unesenim ID-om nije pronađen." + "\n");
                cekanje();
            }
        }
    }

   public void vrati()
    {
        Console.Clear();
        Console.WriteLine("=====VRAĆANJE KNJIGE=====\n");
        Console.Write("Unesite ID posudbe za vraćanje knjige: ");
        var input = Console.ReadLine();
        if (int.TryParse(input, out var posudbaID))
        {
            var posudbaZaVracanje = posudbe.Find(p => p.posudbaID == posudbaID);
            if (posudbaZaVracanje != null)
            {
                var knjiga = bookManager.knjige.Find(b => b.bookID == posudbaZaVracanje.bookID);
                if (knjiga != null)
                {
                    knjiga.Dostupna = true;
                    posudbe.Remove(posudbaZaVracanje);
                    SpasiPosudbe();
                    bookManager.SpasiKnjige();
                    Console.WriteLine("Knjiga je uspješno vraćena." + "\n");
                    cekanje();
                }
            }
            else
            {
                Console.WriteLine("Posudba s unesenim ID-om nije pronađena." + "\n");
                cekanje();
            }
        }
    }
    
    public void izvjestaj_posudbi()
    {   
        Console.Clear();
        Console.WriteLine("=====IZVJEŠTAJ O POSUDBAMA=====\n");
        foreach (var posudba in posudbe)
        {
            var korisnik = userManager.korisnici.Find(k => k.userID == posudba.userID);
            var knjiga = bookManager.knjige.Find(b => b.bookID == posudba.bookID);
            Console.WriteLine($"Posudba ID: {posudba.posudbaID}");
            Console.WriteLine($"Korisnik: {korisnik?.Ime} {korisnik?.Prezime} (ID: {korisnik?.userID})");
            Console.WriteLine($"Knjiga: {knjiga?.Naslov} (ID: {knjiga?.bookID})");
            Console.WriteLine($"Datum posudbe: {posudba.datumPosudbe.ToShortDateString()}");
            Console.WriteLine($"Datum vraćanja: {posudba.datumVracanja.ToShortDateString()}\n");
        }
        cekanje();
    }
}
