using ISprogDerinimas;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// Įveskite didžiausią skaičių (pvz., 5)
Console.WriteLine("Įveskite didžiausią skaičių:");
int n = int.Parse(Console.ReadLine());

Console.WriteLine("Rekursyvus sprendimas: ");
// Generuojame visas kombinacijas mažėjimo tvarka
List<int> currentCombination = new List<int>(); // Sąrašas dabartinei kombinacijai
List<List<int>> allCombinations = new List<List<int>>(); // Visų kombinacijų sąrašas
GenerateCombinations(n, currentCombination, allCombinations, n);
FindHeronTriangles(allCombinations);

// Spausdiname rezultatus
Console.WriteLine($"Visos galimos unikalios 3 skaičių kombinacijos iki {n} mažėjimo tvarka:");
foreach (var combination in allCombinations)
{
    Console.WriteLine(string.Join(", ", combination));
}



Console.WriteLine("////////////////////////UZDUOTYS SU SIUNTA///////////////////////////");

int siuntuKiekis = 10;
int[] buvimoLaikai = { 5, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
int[] prioritetai = { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0 };
int[] svoriai = { 1, 1, 1, 2, 3, 1, 1, 2, 1, 1 };
List<Siunta> siuntaList = new List<Siunta>();
int Numeris = 0;
int Laikas = 0;
int Prioritetas = 0;
int Svoris = 0;
int Ivertis = 0;
Siunta siunta = null;

for(int i = 0; i  < siuntuKiekis; i++)
{
    Numeris = i + 1;
    Laikas = buvimoLaikai[i];
    Prioritetas = prioritetai[i];
    Svoris = svoriai[i];
    Ivertis = ApskaiciuotiIverti(buvimoLaikai[i], prioritetai[i]);
    siunta = new Siunta(Numeris, Laikas, Prioritetas, Svoris, Ivertis);
    siuntaList.Add(siunta);
    
}

List<int> siuntuEile = new List<int>();
int bendrasLaikas = 0;
List<int> galutiniaiLaikai = new List<int>();
while (siuntaList.Any())
{
    // Atrenkame 3 geriausias siuntas pervežimui
    var atrinktosSiuntos = siuntaList.OrderByDescending(s => s.Ivertis)
                                  .ThenByDescending(s => s.Laikas)
                                  .ThenBy(s => s.Numeris)
                                  .Take(3).ToList();

    // Kiekvienos kelionės trukmė: 3 dienos plius siuntų svoris
    int kelionesLaikas = 3 + atrinktosSiuntos.Sum(s => s.Svoris);
    

    // Fiksuojame galutinį laiką atrinktoms siuntoms, nepridedant kelionės laiko
    foreach (var siunta1 in atrinktosSiuntos)
    {
        //galutiniaiLaikai[siunta1.Numeris - 1] = siunta1.Laikas;  // Fiksuojame laiką prieš kelionę
        siuntuEile.Add(siunta1.Numeris);  // Pridedame siuntą į pervežimo eilę
        galutiniaiLaikai.Add(siunta1.Laikas);
        siuntaList.Remove(siunta1);  // Pašaliname pervežtą siuntą iš sąrašo
    }

    // Likusioms siuntoms pridedame kelionės laiką ir perskaičiuojame įverčius
    foreach (var siunta1 in siuntaList)
    {
        siunta1.Laikas += kelionesLaikas;  // Likusios siuntos praleidžia daugiau laiko pastotėje
        siunta1.Ivertis = ApskaiciuotiIverti(siunta1.Laikas, siunta1.Prioritetas);  // Perskaičiuojame įverčius
    }

    // Bendrą kelionės laiką pridedame tik po to, kai visos siuntos pervežamos
    bendrasLaikas += kelionesLaikas;

}


// Spausdiname rezultatus
Console.WriteLine(string.Join("   ", siuntuEile));
Console.WriteLine(string.Join("   ", galutiniaiLaikai));
Console.WriteLine(bendrasLaikas);


Console.WriteLine();
Console.WriteLine("////////////////////3 Uzduotis Iki mokykline istaiga /////////////////////////");

// Dabartinė vaikų situacija
int[] amziausGrupes = { 7, 15, 4, 12, 15, 15 }; // 1, 2, 3, 4, 5, 6 metų vaikai
int[] maxVietos = { 7, 15, 15 }; // Maksimalios vietos lopšelyje, darželyje, priešmokyklinėje grupėje

// Laukiantieji vaikai
List<(string vardas, int amzius)> laukiantysVaikai = new List<(string vardas, int amzius)>
        {
            ("Jonas", 1),
            ("Petras", 3),
            ("Deimantė", 5),
            ("Lina", 4)
        };

// Perkeliame vaikus iš jaunesnių grupių į vyresnes
MoveChildren(amziausGrupes, maxVietos);

// Priimame laukiančius vaikus, jei yra laisvų vietų
AcceptChildren(laukiantysVaikai, amziausGrupes, maxVietos);


//testuojam kitus variantus
Console.WriteLine();
Console.WriteLine("test///////////////////////");
int[] cp = amziausGrupes;
foreach (int i in cp)
{
    Console.Write(" " + i);
}
Console.WriteLine();
Console.WriteLine("Atliekame perkelima per viena grupe:");
mvchild(cp);
foreach (int variabl in cp)
{
    Console.Write(" " + variabl);
}

int[] lop = { cp[0], cp[1] };
int[] darz = { cp[2], cp[3], cp[4] };
int[] priesm = { cp[5] };
Console.WriteLine();
priimti(laukiantysVaikai, lop, darz, priesm, maxVietos);






// Funkcija generuoti visas unikalias kombinacijas su 3 skaičiais mažėjimo tvarka
static void GenerateCombinations(int n, List<int> currentCombination, List<List<int>> allCombinations, int start)
    {
        // Jei kombinacija turi 3 skaičius, pridedame ją į visų kombinacijų sąrašą
        if (currentCombination.Count == 3)
        {
            allCombinations.Add(new List<int>(currentCombination));
            return;
        }

        // Pradedame nuo didžiausio skaičiaus ir einame mažyn
        for (int i = start; i >= 1; i--)
        {
            if (currentCombination.Contains(i)) // Patikriname, ar skaičius jau yra kombinacijoje
                continue; // Jeigu yra, praleidžiame šį skaičių

            currentCombination.Add(i); // Pridedame skaičių į kombinaciją
            GenerateCombinations(n, currentCombination, allCombinations, i - 1); // Rekursyviai generuojame kombinacijas
            currentCombination.RemoveAt(currentCombination.Count - 1); // Grąžiname kombinaciją į pradinę būseną
        }
    }

static void Iterate(int n, List<List<int>> all)
{
    for(int i = n; i >= 3; i--)
    {
        for(int j = i - 1; j >= 2; j--)
        {
            for(int k = j - 1; k >= 1; k--)
            {
                List<int> currentCombo = new List<int>();
                currentCombo.Add(i);
                currentCombo.Add(j);
                currentCombo.Add(k);
                foreach(int l in currentCombo)
                {
                    Console.Write(" " + l);
                }
                Console.WriteLine();
                all.Add(currentCombo);
            }
        }
    }
}

// Herono trikampių radimas ir spausdinimas
static void FindHeronTriangles(List<List<int>> allCombinations)
{
    HashSet<string> uniqueTriangles = new HashSet<string>(); // Laikome tik unikalias kombinacijas

    foreach (var combo in allCombinations)
    {
        int a = combo[0], b = combo[1], c = combo[2];

        if (IsValidTriangle(a, b, c))
        {
            int area = CalculateHeronArea(a, b, c);
            if (area != -1)
            {
                // Naudojame raktą "a,b,c" unikalumui užtikrinti
                string key = $"{a},{b},{c}";
                if (!uniqueTriangles.Contains(key))
                {
                    Console.WriteLine($"{a} {b} {c}  S: {area}");
                    uniqueTriangles.Add(key);
                }
            }
        }
    }
}

// Tikrina, ar trikampis gali egzistuoti
static bool IsValidTriangle(int a, int b, int c)
{
    return a + b > c && a + c > b && b + c > a; // Trikampio nelygybė
}

// Skaičiuoja trikampio plotą pagal Herono formulę, grąžina -1 jei plotas nėra sveikas skaičius
static int CalculateHeronArea(int a, int b, int c)
{
    double s = (a + b + c) / 2.0; // Pusperimetras
    double areaSquared = s * (s - a) * (s - b) * (s - c);

    // Tikriname, ar plotas yra teigiamas ir sveikas skaičius
    if (areaSquared > 0)
    {
        double area = Math.Sqrt(areaSquared);
        if (area == (int)area) // Sveikas skaičius
        {
            return (int)area;
        }
    }
    return -1; // Nesveikas plotas
}

// Funkcija įverčiui apskaičiuoti
 static int ApskaiciuotiIverti(int laikas, int prioritetas)
{
    int ivertis = prioritetas * 2 + laikas;
    if (laikas >= 15)
    {
        ivertis += 10;
    }
    return ivertis;
}


// Funkcija perkelti vaikus į aukštesnes grupes
static void MoveChildren(int[] amziausGrupes, int[] maxVietos)
{
    // 1. Perkeliame 5 metų vaikus į priešmokyklinę grupę (jei yra vietos)
    int vietosPriesmokyklineje = maxVietos[2] - amziausGrupes[5]; // Laisvos vietos priešmokyklinėje
    int perkelti5mVaikus = Math.Min(vietosPriesmokyklineje, amziausGrupes[4]); // Kiek 5 metų vaikų galima perkelti
    amziausGrupes[5] += perkelti5mVaikus;
    amziausGrupes[4] -= perkelti5mVaikus;

    // 2. Perkeliame 4 metų vaikus į 5 metų grupę
    amziausGrupes[4] = amziausGrupes[3];

    // 3. Perkeliame 3 metų vaikus į 4 metų grupę
    amziausGrupes[3] = amziausGrupes[2];

    // 4. Perkeliame 2 metų vaikus į 3 metų grupę
    amziausGrupes[2] = amziausGrupes[1];
    amziausGrupes[1] = 0; // Lopšelio (2 metų) grupė ištuštėja

    // Lopšelio (1 metų) grupė paliekama, nes ten tik nauji vaikai
}


static void mvchild(int[] amziausGrupes)
{
    //kadangi priesmokykline grupe yra viena kurioje vaikai 6 metu, kita rugseji bus 7 ir eis i mokykla
    amziausGrupes[amziausGrupes.Length - 1] = 0; 
 
    //int[] amziausGrupes = { 7, 15, 4, 12, 15, 15 }; // Vaikų skaičius kiekvienoje grupėje (1, 2, 3, 4, 5, 6 metukai)
    for(int i = amziausGrupes.Length-1; i > 0; i--)
    {
        //perkeliame vaikus per viena grupe
        amziausGrupes[i] = amziausGrupes[i - 1];
    }
    //pirma grupe lieka laisva, nes 1 metu vaikuciai perkeliami i kita lopselio grupe kuri priklauso 2 metu vaikuciam.
    amziausGrupes[0] = 0; 

}




// Vaikų priėmimas iš laukiančiųjų sąrašo
static void AcceptChildren(List<(string vardas, int amzius)> laukiantysVaikai, int[] amziausGrupes, int[] maxVietos)
{
    foreach (var vaikas in laukiantysVaikai)
    {
        string vardas = vaikas.vardas;
        int amzius = vaikas.amzius;

        if (amzius == 1 || amzius == 2)
        {
            // Lopšelis
            if (amziausGrupes[amzius - 1] < maxVietos[0])
            {
                Console.WriteLine($"{vardas} pateks į lopšelio grupę po 1 metų");
                amziausGrupes[amzius - 1]++;
            }
            else
            {
                Console.WriteLine($"{vardas} į ikimokyklinę įstaigą nepateks");
            }
        }
        else if (amzius == 3 || amzius == 4 || amzius == 5)
        {
            // Darželis
            int grupesIndeksas = amzius - 3;
            if (amziausGrupes[grupesIndeksas + 2] < maxVietos[1])
            {
                int metai = 6 - amzius; // Skaičiuojame metus iki priešmokyklinės grupės
                Console.WriteLine($"{vardas} pateks į darželio grupę po {metai} metų");
                amziausGrupes[grupesIndeksas + 2]++;
            }
            else
            {
                Console.WriteLine($"{vardas} į ikimokyklinę įstaigą nepateks");
            }
        }
        else if (amzius == 6)
        {
            // Priešmokyklinė grupė
            if (amziausGrupes[5] < maxVietos[2])
            {
                Console.WriteLine($"{vardas} pateks į priešmokyklinę grupę po 1 metų");
                amziausGrupes[5]++;
            }
            else
            {
                Console.WriteLine($"{vardas} į ikimokyklinę įstaigą nepateks");
            }
        }
    }
}

static void priimti(List<(string vardas, int amzius)> laukiantys, int[] lop, int[] darz, int[] priesm, int[] max)
{
    foreach (var vaikas in laukiantys)
    {
        string vardas = vaikas.vardas;
        int amzius = vaikas.amzius + 1; //+1 kadangi skaiciuojam kita rugseji(po metu)
        bool pakeitimas = false;

        if (amzius == 1 || amzius == 2)
        {
            for (int i = 0; i < lop.Length - 1; i++)
            {
                if (lop[i] < max[0])
                {
                    Console.WriteLine($"{vardas} pateks i darzelio grupe lopselis po 1 metu.");
                    lop[i]++;
                    pakeitimas = true;
                }
            }
            if (!pakeitimas)
            {
                Console.WriteLine($"{vardas} nepateks i darzeli.");
            }
        }
        if (amzius == 3 || amzius == 4 || amzius == 5)
        {
            for (int j = 0; j < darz.Length - 1; j++)
            {
                if (darz[j] < max[1])
                {
                    Console.WriteLine($"{vardas} pateks i darzelio grupe po 1 metu.");
                    darz[j]++;
                    pakeitimas = true;
                }
            }
            if (!pakeitimas)
            {
                Console.WriteLine($"{vardas} i darzeli nepateks.");
            }
        }
        if (amzius == 6)
        {
            if (priesm[0] < max[2])
            {
                Console.WriteLine($"{vardas} pateks i darzeli po 1 metu");
                priesm[0]++;
                pakeitimas = (true);
            }
            if (!pakeitimas)
            {
                Console.WriteLine($"{vardas} i darzeli nepapuls.");
            }
        }
    }
}






