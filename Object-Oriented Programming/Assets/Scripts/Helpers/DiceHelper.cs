using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = System.Random;

namespace MonsterQuest
{
    public class DiceHelper
    {
        static string ExtractionRegex = "^(.*)d(.+?)(([-+\\/*])(.+))?$";

        private static int Roll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {

            var random = new Random();
            int result = 0;
            for (int i = 0; i < numberOfRolls; i++)
            {
                int roll = random.Next(1, diceSides + 1);
                result += roll;
            }
            result += fixedBonus;
            return result;
        }

        public static int Roll(string diceRoll)
        {
            Match diceNotation = Regex.Match(diceRoll, ExtractionRegex);
            if (!diceNotation.Success)
            {
                throw new ArgumentException("A standard dice notaion needs at least a d followed by a number of sides.");
            }

            int numbersOfRolls;
            int diceSides;
            int fixedBonus = 0;


            if (diceNotation.Groups[1].Value == "")
            {
                numbersOfRolls = 1;
            }
            else
            {
                try
                {
                    numbersOfRolls = Int32.Parse(diceNotation.Groups[1].Value);
                }
                catch
                {
                    throw new ArgumentException($"Number of rolls ({diceNotation.Groups[1].Value}) was incorrect");
                }

                if (numbersOfRolls < 1)
                {
                    throw new ArgumentException($"Number of rolls ({diceNotation.Groups[1].Value}) needs to be positive");
                }
            }

            try
            {
                diceSides = Int32.Parse(diceNotation.Groups[2].Value);
            }
            catch
            {
                throw new ArgumentException($"Number of dice sides ({diceNotation.Groups[2].Value}) must be a diget whitout decimals");
            }

            if (diceSides < 1)
            {
                throw new ArgumentException($"Number of dice sides ({diceNotation.Groups[2].Value}) must be a positiv didget");
            }

            if (diceNotation.Groups[3].Success)
            {
                try
                {
                    fixedBonus = Int32.Parse(diceNotation.Groups[3].Value);
                }
                catch
                {
                    string operation = diceNotation.Groups[4].Value;
                    if (operation == "+" || operation == "-")
                    {
                        throw new ArgumentException($"Bonus ({diceNotation.Groups[5].Value} needs an integer)");
                    }
                    else
                    {
                        throw new ArgumentException("Bonus can only be added or subtracted");
                    }
                }

            }

            return Roll(numbersOfRolls, diceSides, fixedBonus);
        }
    }
}
