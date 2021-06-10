﻿using System;
using System.Text.RegularExpressions;

namespace RTSLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            String answer;
            while (true) // infinite loop
            {
                Console.WriteLine("Welcome to Moran Rappaport's coding excercise!");
                Console.WriteLine("1 - Count the number of integers above & below a value");
                Console.WriteLine("2 - Rotating a string");
                Console.WriteLine("3 - Exit");
                Console.Write("Enter the option number that you'd like to test:  ");
                answer = Console.ReadLine();
                
                switch (answer.Trim())
                {
                    case "1":
                        CountOfIntsAboveAndBelowInput();
                        break;

                    case "2":
                        RotateString();
                        break;

                    case "3":
                        Console.WriteLine("Thank you for your consideration! Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid input - please try again.");
                        break;
                }

                Console.WriteLine("------------");
            } 
        }


        /// <summary>
        /// Prompts the user for an array of integers, and then one more integer. It then prints the number of integers in the 
        /// array that are above the given input and the number that are below.  It also shows the amount of invalid int32's. 
        /// e.g. for the array [1, 5, 2, 1, 10] with input 6, print “above: 1, below: 4”.
        /// </summary>
        static void CountOfIntsAboveAndBelowInput()
        {
            // get the input array
            String inputArray_str;
            String regexPattern = "\\[([-+]?\\d+)(,\\s*[-+]?\\d+)*\\]"; // a comma separated list of integers (also allows negative nums), surrounded by brackets []
            do
            {
                Console.Write("Please enter the array in the format [1,5,2,10,3]: ");
                inputArray_str = Console.ReadLine();
                inputArray_str = inputArray_str.Trim();

            } while (Regex.Matches(inputArray_str, regexPattern).Count <= 0);


            // get the input integer
            String inputInteger_str;
            int inputInteger;
            do
            {
                Console.Write("Please enter the integer: ");
                inputInteger_str = Console.ReadLine();

            } while (false == Int32.TryParse(inputInteger_str, out inputInteger));

            
            int countLessThanInput = 0;
            int countMoreThanInput = 0;
            int countErrors = 0;
            int currentValue;

            // remove the brackets from the start & end of our array, and then get the individual values
            inputArray_str = inputArray_str.TrimStart('[').TrimEnd(']');
            String[] arrayValues = inputArray_str.Split(",");
            for (int i = 0; i < arrayValues.Length; i++)
            {
                // Parse will fail if the user input a very large number (more than int32)
                // We know the values are all numbers because of our regex above, but some could be more than int32
                if (Int32.TryParse(arrayValues[i], out currentValue) == false)
                {
                    countErrors++;
                    continue;
                }

                // increment the proper counter
                if (inputInteger < currentValue)
                    countMoreThanInput++;
                else if (inputInteger > currentValue)
                    countLessThanInput++;
            }

            Console.WriteLine(">> above: {0}, below: {1}", countMoreThanInput, countLessThanInput);
            if (countErrors > 0)
                Console.WriteLine("   Additionally, there were " + countErrors + " integers that could not be parsed.");

        }


        /// <summary>
        /// Rotates the characters in a string by a given input and places the overflow at the beginning of the string.
        /// e.g. “MyString” rotated by 2 is “ngMyStri”.
        /// </summary>
        static void RotateString()
        {
            String userInputString;
            do
            {
                Console.Write("Please enter the string you want to reverse: ");
                userInputString = Console.ReadLine();

            } while (userInputString.Length <= 0); // make sure they entered at least one character


            String userInputRotationAmount_str;
            int userInputRotationAmount;
            do
            {
                Console.Write("How many digits should it be rotated? ");
                userInputRotationAmount_str = Console.ReadLine();
                userInputRotationAmount_str = userInputRotationAmount_str.Trim();

            } while (false == Int32.TryParse(userInputRotationAmount_str, out userInputRotationAmount)  // ensure it's an integer
                            || userInputRotationAmount > userInputString.Length                         // it must be less than the the input string length
                            || userInputRotationAmount < 0 );                                           // it must be a positive number


            // get the new beginning & end of the string, and re-construct the new rotated string
            String beginningOfString = userInputString.Substring(userInputString.Length - userInputRotationAmount);
            String endOfString = userInputString.Substring(0, userInputString.Length - userInputRotationAmount);
            String rotatedString = beginningOfString + endOfString;

            Console.WriteLine(">> Rotated String = " + rotatedString);
        }
    }
}