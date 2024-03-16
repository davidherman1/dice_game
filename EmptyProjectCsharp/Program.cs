class Program
{
    static void Main()
    {
        Console.WriteLine("Vítejte ve hře s kostkami!");

        int pocetVyhierHrac1 = 0;
        int pocetVyhierHrac2 = 0;

        while (true)
        {
            Console.Write("Zadejte počet kostek: ");
            int pocetKostek = Convert.ToInt32(Console.ReadLine().Trim());

            Console.Write("Zadejte počet stran kostek: ");
            int pocetStran = Convert.ToInt32(Console.ReadLine().Trim());

            Hra kostkovani = new Hra(pocetKostek, pocetStran);

            int vitez = kostkovani.Hraj();

            if (vitez == 1)
            {
                pocetVyhierHrac1++;
            }
            else if (vitez == 2)
            {
                pocetVyhierHrac2++;
            }
            Console.WriteLine($"\nStav výher:\nHráč 1: {pocetVyhierHrac1}\nHráč 2: {pocetVyhierHrac2}");

            Console.Write("Chcete hrát znovu? (ano/ne): ");
            string odpoved = Console.ReadLine().Trim();

            if (odpoved.ToLower() != "ano")
            {
                break;
            }}
        Console.WriteLine("\nCelkové výsledky hry:");
        Console.WriteLine($"Hráč 1: {pocetVyhierHrac1} výher");
        Console.WriteLine($"Hráč 2: {pocetVyhierHrac2} výher");

        if (pocetVyhierHrac1 > pocetVyhierHrac2)
        {
            Console.WriteLine("Celkový vítěz: Hráč 1");
        }
        else if (pocetVyhierHrac2 > pocetVyhierHrac1)
        {
            Console.WriteLine("Celkový vítěz: Hráč 2");
        }
        else
        {
            Console.WriteLine("Celkový vítěz: Remíza");
        }

        Console.WriteLine("Konec hry. Děkujeme za hraní!");
    }
}

class Hra
{
    private int pocetKostek;
    private int pocetStran;
    private List<int> hodnotyHrac1;
    private List<int> hodnotyHrac2;
    private Random random;

    public Hra(int pocetKostek, int pocetStran)
    {
        this.pocetKostek = pocetKostek;
        this.pocetStran = pocetStran;
        this.hodnotyHrac1 = new List<int>();
        this.hodnotyHrac2 = new List<int>();
        this.random = new Random();
    }

    public int Hraj()
    {
        
        HracHraje(1, hodnotyHrac1);

        
        HracHraje(2, hodnotyHrac2);

     
        return ZjistiViteze();
    }

    private void HracHraje(int cisloHrace, List<int> hodnotyHrace)
    {
        Console.WriteLine($"\nHraje hráč {cisloHrace}:");

        for (int i = 0; i < pocetKostek; i++)
        {
            int hodnotaKostky = random.Next(1, pocetStran + 1);
            Console.WriteLine($"Hod kostkou {i + 1}: {hodnotaKostky}");
            hodnotyHrace.Add(hodnotaKostky);
        }

        Console.Write("Chcete některé kostky přehodit? (ano/ne): ");
        string odpoved = Console.ReadLine().Trim();

        if (odpoved.ToLower() == "ano")
        {
            PřehodKostky(hodnotyHrace);
        }
    }
    private void PřehodKostky(List<int> hodnotyHrace)
    {
        Console.Write("Zadejte čísla kostek, které chcete přehodit (oddělené čárkou): ");
        string[] cislaKostek = Console.ReadLine().Trim().Split(',');

        foreach (string cisloKostky in cislaKostek)
        {
            if (int.TryParse(cisloKostky.Trim(), out int cislo) && cislo > 0 && cislo <= pocetKostek)
            {
        hodnotyHrace[cislo - 1] = random.Next(1, pocetStran + 1);
            }
            else
            {
                Console.WriteLine("Neplatný vstup. Zadejte platná čísla kostek.");
                PřehodKostky(hodnotyHrace);
                return;
            }
        }
        Console.WriteLine("\nAktualizované hodnoty kostek po přehazování:");
        for (int i = 0; i < pocetKostek; i++)
        {
            Console.WriteLine($"Hod kostkou {i + 1}: {hodnotyHrace[i]}");
        }
    }
    private int ZjistiViteze()
    {
        int soucetHrac1 = hodnotyHrac1.Sum();
        int soucetHrac2 = hodnotyHrac2.Sum();

        Console.WriteLine($"\nCelkový součet hráče 1: {soucetHrac1}");
        Console.WriteLine($"Celkový součet hráče 2: {soucetHrac2}");

        if (soucetHrac1 > soucetHrac2)
        {
            Console.WriteLine("\nHráč 1 vyhrál toto kolo!");
            return 1;
        }
        else if (soucetHrac2 > soucetHrac1)
        {
            Console.WriteLine("\nHráč 2 vyhrál toto kolo!");
            return 2;
        }
        else
        {
            Console.WriteLine("\nRemíza, žádný hráč nevyhrál toto kolo.");
            return 0;
        }
    }
}