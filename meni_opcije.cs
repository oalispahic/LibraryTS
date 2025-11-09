namespace LibraryTS
{
    public class MeniOpcije
    {
        public upravljanje_korisnicima userManager;
        public upravljanje_knjigama bookManager;

        public void azuriranje_korisnika()
        {
            Console.Clear();
            Console.Write("=====UPRAVLJANJE KORISNIKOM=====\n\n");
            Console.Write("1. Dodaj korisnika\n");
            Console.Write("2. Izbrisi korisnika po ID-u\n");
            Console.Write("3. Izbrisi korisnika po Imenu i prezimenu\n");
            Console.Write("4. Promjena uloge\n");
            Console.Write("5. Glavni meni\n");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    userManager.DodajKorisnika();
                    break;

                case "2":
                    userManager.ObrisiKorisnikaID();
                    break;

                case "3":
                    userManager.ObrisiKorisnikaIme();
                    break;

                case "4":
                    //  userManager.promijeniUlogu();
                    break;

                default:
                    break;
            }
        }

        public void azuriranje_knjiga()
        {
            Console.Clear();
            Console.WriteLine("=====UPRAVLJANJE KNJIGAMA=====\n");
            Console.WriteLine("1. Dodaj knjigu");
            Console.WriteLine("2. Izbriši knjigu po ID-u");
            Console.WriteLine("3. Izbriši knjigu po naslovu");
            Console.WriteLine("4. Ažuriraj knjigu");
            Console.WriteLine("5. Pretraga po naslovu");
            Console.WriteLine("6. Pretraga po autoru");
            Console.WriteLine("7. Pretraga po žanru");
            Console.WriteLine("8. Promijeni dostupnost (izdaj/vrati)");
            Console.WriteLine("9. Povratak u glavni meni");
            Console.Write("\nIzbor: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    bookManager.DodajKnjigu();
                    break;
                case "2":
                    bookManager.ObrisiKnjiguID();
                    break;
                case "3":
                    bookManager.ObrisiKnjiguNaslov();
                    break;
                case "4":
                    bookManager.AzurirajKnjigu();
                    break;
                case "5":
                    bookManager.PretragaPoNaslovu();
                    break;
                case "6":
                    bookManager.PretragaPoAutoru();
                    break;
                case "7":
                    bookManager.PretragaPoZanru();
                    break;
                case "8":
                    bookManager.PromijeniDostupnost();
                    break;
                default:
                    break;
             }
        }
    }
}
