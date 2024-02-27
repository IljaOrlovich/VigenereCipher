using System;

class VigenereCipher
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Iveskite teksta:");
            string inputText = Console.ReadLine();

            Console.WriteLine("Iveskite rakta:");
            string key = Console.ReadLine();

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
                        string encryptedText = Encrypt(inputText.ToUpper(), key.ToUpper());
                        string encryptedAscii = ASCII(inputText, key);
                        Console.WriteLine($"Sifruotas tekstas: {encryptedText}");
                        Console.WriteLine($"Sifruotas ASCII tekstas: {encryptedAscii}");
                        break;

                    case 2:
                        string decryptedText = Decrypt(inputText.ToUpper(), key.ToUpper());
                        Console.WriteLine($"Desifruotas tekstas: {decryptedText}");
                        string decryptedAscii = DeASCII(inputText, key);
                        Console.WriteLine($"Desifruotas ASCII tekstas: {decryptedAscii}");
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

    public static string ASCII(string inputText, string key)
    {
        string result = "";
        int keyIndex = 0;
        int alphabetSize = 95;

        foreach (char c in inputText)
        {
            if(c >= '!' && c<='~')
            {
                int cIndex = c - '!';
                int kIndex = key[keyIndex] - '!';
                int letterIndex = (cIndex + kIndex) % alphabetSize;
                result += (char)('!' + letterIndex);
                keyIndex = (keyIndex + 1) % key.Length;
            }
            else
            {
                result += c;
            }
        }
        return result;
        
    }

    public static string DeASCII(string inputText, string key)
    {
        string result = "";
        int keyIndex = 0;
        int alphabetSize = 95;

        foreach (char c in inputText)
        {
            if (c >= '!' && c <= '~')
            {
                int cIndex = c - '!';
                int kIndex = key[keyIndex] - '!';
                int letterIndex = (cIndex - kIndex + alphabetSize) % alphabetSize;
                result += (char)('!' + letterIndex);
                keyIndex = (keyIndex + 1) % key.Length;
            }
            else
            {
                result += c;
            }
        }
        return result;

    }
}
