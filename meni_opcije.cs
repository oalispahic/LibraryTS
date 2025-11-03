namespace LibraryTS
{
    public class MeniOpcije
    {
        public upravljanje_korisnicima userManager;

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
    }
}
