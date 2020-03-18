using System;
using System.Collections.Generic;

namespace Tamagotchi
{
    class Program
    {
        static void Main(string[] args)
        {
            string owner = "";
            string petName = "";

            while (owner == "")
            {
                Console.Write("Hi owner, what's your name?");
                Console.WriteLine();
                owner = Console.ReadLine();
                Console.WriteLine();
            }

            while (petName == "")
            {
                Console.Write("Name your own Tamagotchi!");
                Utility.YouTalk(owner);
                petName = Console.ReadLine();
            }
            var tama = new Tama(petName, owner);
            tama.Start(tama, owner);
        }

    }




}