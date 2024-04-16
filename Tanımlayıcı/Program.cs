// See https://aka.ms/new-console-template for more information
// Durumlar

Console.WriteLine("Doğruluğunu kontrol etmek istediğiniz matematiksel ifadeyi giriniz");
string ifade = Console.ReadLine();
char[] karakterler = ifade.ToCharArray();

State state = State.Baslangic;
int acikParantezSayisi = 0;
bool dogru = true;
bool islemlerKapandiMi = true;

for (int i = 0; i < karakterler.Length; i++)
{

    switch (state)
    {
        case State.Baslangic:
            if (char.IsDigit(karakterler[i]))
            {
                state = State.Rakam;
            }
            else if (karakterler[i] == '(')
            {
                acikParantezSayisi++;
                state = State.ParantezAc;
            }            
            else
            {
                dogru = false;
                Console.WriteLine("Girilen ifade matematiksel bir ifade değildir.");
            }
            break;
        case State.Rakam:
            if (char.IsDigit(karakterler[i]))
            {             
                state = State.Rakam;
            }
            else if (IsOperator(karakterler[i]))
            {
                islemlerKapandiMi = false;
                state = State.Islem;
            }
            else if (karakterler[i]=='/')
            {
                islemlerKapandiMi = false;
                state = State.Bolme;
            }
            else if (karakterler[i] == '(')
            {
                acikParantezSayisi++;
                state = State.ParantezAc;
            }
            else if (karakterler[i]==')'&& acikParantezSayisi>0)
            {
                acikParantezSayisi--;
                state = State.ParantezKapa;
            }
            else
            {
                dogru = false;
                Console.WriteLine("Girilen ifade matematiksel bir ifade değildir.");//hata
            }
            break;
        case State.Islem:
            if (char.IsDigit(karakterler[i]))
            {
                islemlerKapandiMi = true;
                state = State.Rakam;
            }
            else if (karakterler[i] == '(')
            {                
                acikParantezSayisi++;
                state = State.ParantezAc;
            }
            else if (karakterler[i] == ')' && acikParantezSayisi > 0)
            {
                acikParantezSayisi--;
                state = State.ParantezKapa;
            }            
            else
            {
                dogru = false;
                Console.WriteLine("Girilen ifade matematiksel bir ifade değildir.");
            }
            break;
        case State.Bolme:
            if (karakterler[i] == '0')
            {
                dogru = false;
                Console.WriteLine("Girilen ifade matematiksel bir ifade değildir.");
            }
            else if (char.IsDigit(karakterler[i]))
            {
                islemlerKapandiMi = true;
                state = State.Rakam;
            }
            else if (karakterler[i] == '(')
            {
                acikParantezSayisi++;
                state = State.ParantezAc;
            }
            else if (karakterler.Length==i)
            {
                dogru = false;
                Console.WriteLine("Girilen ifade matematiksel bir ifade değildir.");
            }
            else
            {
                dogru = false;
                Console.WriteLine("Girilen ifade matematiksel bir ifade değildir.");
            }
            break;
        case State.ParantezAc:
            if (char.IsDigit(karakterler[i]))
            {
                islemlerKapandiMi = true;
                state = State.Rakam;
            }
            else if (karakterler[i] == '(')
            {
                acikParantezSayisi++;
                state = State.ParantezAc;
            }
            else if (IsOperator(karakterler[i]))
            {
                state = State.Islem;
            }
            else if (karakterler[i] == '/')
            {
                islemlerKapandiMi = false;
                state = State.Bolme;
            }
            else if (karakterler[i] == ')' && acikParantezSayisi > 0)
            {
                acikParantezSayisi--;
                state = State.ParantezKapa;
            }
            break;
        case State.ParantezKapa:
            if (char.IsDigit(karakterler[i]))
            {
                islemlerKapandiMi = true;
                state = State.Rakam;
            }
            else if (karakterler[i] == '(')
            {
                acikParantezSayisi++;
                state = State.ParantezAc;
            }
            else if (IsOperator(karakterler[i]))
            {
                state = State.Islem;
            }
            else if (karakterler[i] == '/')
            {
                islemlerKapandiMi = false;
                state = State.Bolme;
            }
            else if (karakterler[i] == ')' && acikParantezSayisi > 0)
            {
                acikParantezSayisi--;
                state = State.ParantezKapa;
            }
            break;


    }
}
if ( dogru && acikParantezSayisi == 0 && islemlerKapandiMi && karakterler.Length > 0 )
{
    Console.WriteLine("Girilen ifade matematiksel bir ifadedir.");
}

    
bool IsOperator(char karakter)
{
    switch (karakter)
    {
        case '+':
        case '-':
        case '*':
            return true;
        default:
            return false;
    }
}

enum State
{
    Baslangic = 0,
    Rakam = 1,
    Islem = 2, // +-x
    Bolme = 3,
    ParantezAc = 4,
    ParantezKapa = 5
}


//           2+3*(4-1)        doğru
//           12+((3-2)*4)/2   doğru
//           3*(4+2/3         yanlış    
//           (10-5)/(2+3*2)   doğru 
//           2*3/0            yanlış