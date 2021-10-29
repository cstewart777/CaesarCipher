//Caesar Cipher Project
//Collin Stewart
//February 2021
//Purpose: To decrypt a caesar cipher encrypted text file. 

using System;
using System.IO;

namespace Project1Stewart
{
    class Program
    {
        static string ShiftVal(string lines, int shiftValue)
        {
            //Declare variables. 
            const int NUM_LETTERS = 26; //number of letters in alphabet
            string quote = " ";

            //For loop to shift each character in the substring. 
            for (int i=0; i<lines.Length; i++)
            {
                string firstLetter = lines.Substring(i, 1);
                char letter = char.Parse(firstLetter);

                //If statement to ensure that the shifted value is a letter. 
                if (char.IsLetter(letter))
                {
                    //Code provided by the one and only, Dr. Coy. 
                    int alphabetLoc = letter - 'a';
                    int shiftedLoc = (alphabetLoc - shiftValue) + NUM_LETTERS;
                    int newAlphaLoc = (shiftedLoc % NUM_LETTERS + 'a');
                    char decryptedLetter = Convert.ToChar(newAlphaLoc);
                    letter = decryptedLetter;
                }
                //Adds the letter to the quote variable declared earlier. 
                quote += letter;      
            }
            //Returns quote, which is our decrypted letter. 
            return quote;
        }

        static void Main(string[] args)
        {
            //Message that tells what the program is doing.
            Console.WriteLine("Decrypt a file with the Caesar Cipher, using brute force.");

            //Prompt the user to enter the file to decrypt.
            Console.WriteLine("Enter the name of the file");
            string enteredFile = Console.ReadLine();

            //Declare StreamReader.
            StreamReader fileStream = null;
            string path = "/Users/collinstewart777/Desktop/" + enteredFile;

            //Atempt to open file.
            try
            {
                fileStream = new StreamReader(path);
            }
            catch (IOException e)
            {
                //If the file does not exist. 
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return;
            }
 
            //Reads the first line of the file.
            string line1 = " ";
            line1 = fileStream.ReadLine();
           
            //Declare Variables.
            string userInput = ""; //users input 
            bool tOrF = false; //variable to run while loop
            int shiftVal = 0; //initialize shift value
            string dLine = line1; //decrypted line equaling first line of filestream. 
 
            //While loop to run through the program until complete.
            //I worked with Josh Westrum on tackling this part.
            //Prior errors before consulting Josh: console would print infinite
            //amount of decrypted lines, and was also having trouble with exiting
            //the while loop. 
            while (tOrF != true)
            {
                //Code to write out first line and ask user if correct. 
                Console.WriteLine("Shift " + shiftVal + " decryption:");
                Console.WriteLine(dLine);
                Console.WriteLine("Does this look correct? 'y' for yes or 'n' for no or 'q' to quit.");
                userInput = Console.ReadLine();

                //If the user inputs yes, then the program will
                //quit and output the whole text file. 
                if (userInput == "y")
                {
                    tOrF = true;
                    //Gives user information about the shift value and
                    //then displays text file. 
                    Console.WriteLine("Caesar Cipher Shift = " + shiftVal);
                    Console.WriteLine("The decrypted file is: ");
                    Console.WriteLine(ShiftVal(line1, shiftVal));
                    //While loop to print out text file. 
                    while((line1 = fileStream.ReadLine()) != null)
                    {
                        //Invoke method to decrypt entire file. 
                        string end = ShiftVal(line1, shiftVal);
                        Console.Write(end);  
                    }  
                }

                //Else if for when the user inputs a q it will quit the program. 
                else if (userInput == "q")
                {
                    tOrF = true;

                    //Exits while loop. 
                    return;  
                }

                //When user enters anything in besides a "y or q"
                //the else loop will attempt to shift the text
                //until y or q is called to finish the program. 
                else
                {
                    tOrF = false;
                    shiftVal++;
                    //Invoke shift method. 
                    dLine = ShiftVal(line1, shiftVal);
                }
            }
            //Close file.
            fileStream.Close();
        }
    }
}
