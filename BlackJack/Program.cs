using System;
using System.Runtime.InteropServices.Marshalling;
class Program
{
    
    static List<string> all_cards = new List<string> {"kupaAS", "kupa2", "kupa3", "kupa4", "kupa5", "kupa6", "kupa7", "kupa8", "kupa9", "kupa10", "kupaJ","kupaQ", "kupaK",
                                            "sinekAS", "sinek2", "sinek3", "sinek4", "sinek5", "sinek6","sinek7","sinek8","sinek9","sinek10", "sinekJ", "sinekQ", "sinekK",
                                            "karoAS","karo2", "karo3","karo4","karo5","karo6","karo7","karo8","karo9","karo10","karoJ","karoQ","karoK",
                                            "macaAS","maca2","maca3","maca4", "maca5", "maca6","maca7","maca8","maca9","maca10","macaJ","macaQ","macaK"};
    static List<string> player_deck = new List<string>{};
    static List<string> croupier_deck = new List<string>{};
    

    static string deal(List<string> list) //STRİNG OLARAK DESTEDEN KART VERİR
    {
        Random random = new Random();
        int index = random.Next(list.Count);
        string saved =  list[index];
        list.RemoveAt(index);
        return saved;
    }

    static int score(List<string> list)
    {
        // son chara bakarak puanlandırma yap -> S = As, 0 = 10
        int sum = 0;
        int acecount = 0;
        for (int i = 0; i < list.Count; i++) //eldeki tüm kartlar için
        {
            string theCard = list[i];
            char lastChar = theCard[theCard.Length - 1];
            switch (lastChar) //AS için henüz bakmadık
            {
                case '2':
                    sum += 2;
                    break;
                case '3':
                    sum += 3;
                    break;
                case '4':
                    sum += 4;
                    break;
                case '5':
                    sum += 5;
                    break;
                case '6':
                    sum += 6;
                    break;
                case '7':
                    sum += 7;
                    break;
                case '8':
                    sum += 8;
                    break;
                case '9':
                    sum += 9;
                    break;
                case '0':
                    sum += 10;
                    break;
                case 'J':
                    sum += 10;
                    break;
                case 'Q':
                    sum += 10;
                    break;
                case 'K':
                    sum += 10;
                    break;
                case 'S':
                    acecount++;
                    break;
                    
            }
        }
        //as durumunu gözeticez ama birden fazla as gelebilir
        sum += acecount * 11; //önce asları 11 olarak aldık

        
        while (sum > 21 && acecount > 0)
        {
                sum -= 10; //şimdi 1 olması gereken asları 1 yaptık
                acecount--;
        }
        
 
        return sum;
    }

    static void showcase(List<string> croupierdeck, List<string> playerdeck)
    {
        Console.Clear();
        Console.SetCursorPosition(0, 8);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Croupier total: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(score(croupier_deck));
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 9);
        Console.Write("Your total: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(score(player_deck));
        Console.ForegroundColor = ConsoleColor.White;
        List<string> list = new List<string>{};
        int j = 0;
        for (int c = 0; c <= 1; c++)
        {
            if (c == 0)
             {
                  list = croupierdeck;
                  if (list.Count == 1) //KAPALI KARTI YAZDIRICAZ (DÜMENDEN)
                  {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.SetCursorPosition(14, 0);
                    Console.Write("+----------+");
                    for (int i = 1; i <= 5; i++)
                    {
                        Console.SetCursorPosition(14, i);
                        Console.WriteLine("|          |");
                    }
                    Console.SetCursorPosition(14, 6);
                    Console.Write("+----------+");
                    Console.SetCursorPosition(16, 3);
                    Console.Write("????????");
                  }
                Console.ForegroundColor = ConsoleColor.White;
             }
            else{
             list = playerdeck;
             j = 11;
             }            //kimin elini yazdırdığımız
            //ilk croupier sonra oyuncu yazdırcaz
            for (int i = 0; i < list.Count; i++) //eldeki her kart için
            {
                Console.SetCursorPosition(i * 14 , j);  //o kartı (1adet) yazmaya nerden başlayacağı
                string writtenNo = "";
                string cardClass = "";
                string thatCard = list[i];
                char lastChar = thatCard[thatCard.Length - 1];

                if (list[i].Contains("AS")) writtenNo = "A";
                else if (list[i].Contains("10")) writtenNo = "10";
                else writtenNo = Convert.ToString(lastChar); //KART PUANI OLARAK NE YAZILCAĞINI BULDUK

                if (thatCard.Contains("kupa"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    cardClass = " ♥";    //BU CARDCLASSLAR SONRA SEMBOLLERLE DEĞİŞECEK
                }
                else if (thatCard.Contains("karo"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    cardClass = " ♦";
                }
                else if (thatCard.Contains("sinek"))
                {
                    cardClass = " ♣";
                }
                else //maça
                {
                    cardClass = " ♠";
                } // şimdi kartı çizicez
                Console.WriteLine("+----------+");
                for (int n = 1; n <= 5; n++)
                {
                    Console.SetCursorPosition(14 * i, j + n);
                    Console.Write("|");
                    Console.SetCursorPosition((14 * i) - 1 + 12, j + n);
                    Console.Write("|");
                }
                Console.SetCursorPosition(14*i, j + 6);
                Console.WriteLine("+----------+");

                Console.SetCursorPosition((14 * i) + 4, j + 2);
                Console.Write(cardClass);
                Console.SetCursorPosition((14 * i) + 5, j + 3);
                Console.Write(writtenNo);
                Console.ForegroundColor = ConsoleColor.White; 
                Console.SetCursorPosition(0, 18);
            }
            
        }
    }

    static byte hit_or_stay() //sadece soruyor, listeye falan bi şey eklemiyor, 1 hit; 2 stay demek
    {
        byte playerchoice;
        bool flag = false;
        do{
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("(1)Hit or (2)Stay ? :   ");
            Console.ForegroundColor = ConsoleColor.White;

            if(Byte.TryParse(Console.ReadLine(), out playerchoice) && (playerchoice == 1 || playerchoice == 2))
                flag = true;
            else{
                Console.ForegroundColor = ConsoleColor.DarkRed;
             Console.WriteLine("Invalid input, please type 1 to hit or 2 to stay!");
            }
        }while(!flag);
        return playerchoice;
    }
    
    static void again()
    {
           Console.WriteLine("Do you want to play again?");

         while(true){
              Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Type 1 to play again or 2 to exit: ");
                Console.ForegroundColor = ConsoleColor.White;

                string? input = Console.ReadLine();
                if (input == "1")
                { 
                    all_cards.AddRange(player_deck);
                    all_cards.AddRange(croupier_deck); //ellerdeki kartları tekrardan desteye koy
                    player_deck.Clear();
                    croupier_deck.Clear();
                   Main();
                }
                else if (input == "2") Environment.Exit(0);
              }
    }
    static void Main()
    {
        
        croupier_deck.Add(deal(all_cards));
        player_deck.Add(deal(all_cards));
        player_deck.Add(deal(all_cards));
        

        showcase(croupier_deck, player_deck);
        Thread.Sleep(100);

        if(score(player_deck) == 21)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("!!!!!!       21      !!!!!!");
            Console.Write("Croupier's turn...");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(2000);
            croupier_deck.Add(deal(all_cards));
            showcase(croupier_deck, player_deck);

            if (score(croupier_deck) == 21)
            {
              Console.ForegroundColor = ConsoleColor.Gray;
              Console.WriteLine("!!!!!!       DRAW      !!!!!!");
              again(); //tekrar oynamak isteyip istemediğini sor
            }
            else //oyuncu kazanmış demek
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("!!!!!!       YOU WIN      !!!!!!");
                again();
            }
        }
        else //NŞA oynanan bir oyun
        {
            byte choice;
            do{
                choice = hit_or_stay();
                if(choice == 1) //hit denmiş
                {
                    player_deck.Add(deal(all_cards));
                    showcase(croupier_deck, player_deck);

                    if (score(player_deck) > 21) //geçtiyse iflas ettircez
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                         Console.WriteLine("!!!!!!       YOU LOST      !!!!!!");
                         Console.ForegroundColor = ConsoleColor.White;
                         again();

                    }
                    else if (score(player_deck) == 21)
                    {
                        choice = 2; //stay choice seçilsin ve sıra croupier geçsin
                    }

                    // !!!!!! 21DEN KÜÇÜKSE HİÇ ELLEMİYORUZ WHİLE TEKRAR DÖNÜYOR
                }
            }while(choice == 1);

            // burdan sonrası otomatik olarak stay komutları
            while(score(croupier_deck) < 17)
            {
                croupier_deck.Add(deal(all_cards));
                showcase(croupier_deck, player_deck);
            }

            if (score(croupier_deck) == score(player_deck))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
              Console.WriteLine("!!!!!!       DRAW      !!!!!!");
            }
            else if (score(croupier_deck) > 21)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("!!!!!!       YOU WIN      !!!!!!");
            
            }
            else if (score(croupier_deck) < score(player_deck))
            {
              Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("!!!!!!       YOU WIN      !!!!!!");

            }
            else if (score(croupier_deck) > score(player_deck)) //oyuncu kaybetmiş demek
            {
                Console.ForegroundColor = ConsoleColor.Red;
                 Console.WriteLine("!!!!!!       YOU LOST      !!!!!!");
                 Console.ForegroundColor = ConsoleColor.White;
                
            }
              again(); //tekrar oynamak isteyip istemediğini sor

        }

        
        

    }
    

}


