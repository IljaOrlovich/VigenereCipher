using System;

class VigenereCipher
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Iveskite teksta:");
            string inputText = Console.ReadLine().ToUpper();

            Console.WriteLine("Iveskite rakta:");
            string key = Console.ReadLine().ToUpper();

            Console.WriteLine("Pasirinkite veiksma:");
            Console.WriteLine("1 - Sifravimas");
            Console.WriteLine("2 - Desifravimas");
            Console.WriteLine("0 - Iseiti");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        string encryptedText = Encrypt(inputText, key);
                        Console.WriteLine($"Sifruotas tekstas: {encryptedText}");
                        break;

                    case 2:
                        string decryptedText = Decrypt(inputText, key);
                        Console.WriteLine($"Desifruotas tekstas: {decryptedText}");
                        break;

                    case 0:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Neteisingas pasirinkimas");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Neteisingas pasirinkimas");
            }

            Console.WriteLine("Norite tęsti? (Y/N)");
            if (Console.ReadLine().ToUpper() != "Y")
                break;
        }
    }

    static string Encrypt(string plaintext, string key)
    {
        string encryptedText = "";
        int keyIndex = 0;

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] digits = "0123456789".ToCharArray();

        for (int i = 0; i < plaintext.Length; i++)
        {
            char symbol = plaintext[i];
            char keyChar = key[keyIndex];

            if (Array.IndexOf(alphabet, symbol) != -1 || Array.IndexOf(digits, symbol) != -1)
            {
                char[] baseArray = Array.IndexOf(alphabet, symbol) != -1 ? alphabet : digits;
                int shift = keyChar - 'A';
                char baseChar = baseArray[0];

                char encryptedSymbol = (char)(((symbol - baseChar + shift) % baseArray.Length + baseArray.Length) % baseArray.Length + baseChar);
                encryptedText += encryptedSymbol;
                keyIndex = (keyIndex + 1) % key.Length;
            }
            else
            {
                encryptedText += symbol;
            }
        }

        return encryptedText;
    }

    static string Decrypt(string ciphertext, string key)
    {
        string decryptedText = "";
        int keyIndex = 0;

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] digits = "0123456789".ToCharArray();

        for (int i = 0; i < ciphertext.Length; i++)
        {
            char symbol = ciphertext[i];
            char keyChar = key[keyIndex];

            if (Array.IndexOf(alphabet, symbol) != -1 || Array.IndexOf(digits, symbol) != -1)
            {
                char[] baseArray = Array.IndexOf(alphabet, symbol) != -1 ? alphabet : digits;
                int shift = keyChar - 'A';
                char baseChar = baseArray[0];

                char decryptedSymbol = (char)(((symbol - baseChar - shift + baseArray.Length) % baseArray.Length + baseArray.Length) % baseArray.Length + baseChar);
                decryptedText += decryptedSymbol;
                keyIndex = (keyIndex + 1) % key.Length;
            }
            else
            {
                decryptedText += symbol;
            }
        }

        return decryptedText;
    }
}
