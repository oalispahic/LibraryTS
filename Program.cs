using System;

class program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Dobrodošli u biblioteka system!");
        Console.WriteLine("-------------------------------");

        var userManager = new upravljanje_korisnicima();
        userManager.IspisiSve();
    }
}