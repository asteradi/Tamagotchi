using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public class Tama:TamaBase
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }

        protected bool Satisfied { get; set; }
        public int HungerRate { get; set; }
        public int HappinessRate { get; set; }
        public int FullnessRate { get; set; }
        public int TirednessRate { get; set; }

        protected string FoodItem;

        public string Stage { get; set; }
        ConsoleColor Color { get; set; }

        public Tama(string name, string owner)
        {
            var r = new Random();
            Name = name;
            OwnerName = owner;
            Stage = "egg";
            Color = ConsoleColor.DarkMagenta;
            Full = r.Next(0, 100);
            Hunger = 100 - Full;
            Happy = r.Next(0, 100);
            Tired = r.Next(0, 100);
            HungerRate = 10;
            HappinessRate = 10;
            FullnessRate = 10;
            TirednessRate = 10;
        }

        public void Start(Tama tama, string owner)
        {
            Utility.Hatching(Name, Color);
            ChangeStage("baby");

            // BABY
            Utility.WriteTamasState(tama);
            Utility.TamaTalks("Hi " + owner + "! *tummy rumbling*", Color, Name);
            Utility.WriteMsg(tama.Name + " is very hungry, you have to feed it!");

            Command(tama, Tamagotchi.Command.Feed);

            Utility.TamaTalks("Play with me!", Color, Name);
            bool play = Utility.GetResponse(tama, owner);

            if (play)
                Command(tama, Tamagotchi.Command.Play);

            Utility.TamaTalks(play ? "YAY!" : "Boooo!", Color, Name);
            Utility.TamaTalks("I'm still hungry! Feed me!", Color, Name);
            Command(tama, Tamagotchi.Command.Feed);

            Utility.TamaTalks(tama.FoodItem != "nothing" ? "Nom nom nom, delicious " + tama.FoodItem + "!" : "Iiih I'm just a baby, I need food!", Color, Name);
            Utility.TamaTalks("It's getting late and I'm really sleepy. \r\nWill you turn off the lights for me?", Color, Name);

            bool lights = Utility.GetResponse(tama, owner);
            if (lights)
            {
                Command(tama, Tamagotchi.Command.Sleep);
            }
            else
            {
                Utility.WriteTamasState(tama);
                Utility.TamaTalks("Please, I'm really sleepy! *yawn*", Color, Name);
                lights = Utility.GetResponse(tama, owner);

                if (lights)
                {
                    Command(tama, Tamagotchi.Command.Sleep);
                }
                else
                {
                    Utility.WriteTamasState(tama);
                    Utility.TamaTalks("*throws temper tantrum*", Color, Name);
                    Utility.WriteMsg("Sorry, but now both you and " + tama.Name + " will be upp all night...");
                    System.Threading.Thread.Sleep(2000);
                    Utility.DrawNight();
                }
            }

            Utility.WriteTamasState(tama);
            Utility.TamaTalks(tama.Happy < 50 ? "Good morning " + owner + "!" : "I don't like you " + owner + " very much *sob sob*", Color, Name);
            Utility.WriteMsg("Time for breakfast!");
            Command(tama, Tamagotchi.Command.Feed);

            if (tama.Hunger == 100)
            {
                tama.ChangeStage("dead");
                Utility.WriteTamasState(tama);
                Utility.WriteMsg("Poor " + tama.Name + " starved to death!");
                Utility.WriteMsg("You shouldn't have pets, " + owner + "...");
                Console.WriteLine();
                Utility.WriteMsg("Hit ENTER to shut down.");
                Console.ReadLine();
                return;

            }

            Utility.WriteMsg("Looks like " + tama.Name + " made a doo-doo, will you clean it?");
            var poop = Utility.GetResponse(tama, owner);
            if (poop) Command(tama, Tamagotchi.Command.Poop);

            if (tama.Happy > 60) tama.ChangeStage("goodTeen");
            else tama.ChangeStage("badTeen");

            //TEEN
            Utility.WriteTamasState(tama);
            if (tama.Satisfied)
            {
                Utility.TamaTalks(poop ? "Thank you " + owner + "!" : "I guess it can be taken care of later...", Color, Name);
                Utility.WriteMsg("Oh! " + tama.Name + " just grew!");
                Utility.WriteMsg("And it looks like it's healthy and well disciplined.\r\nKeep raising it this way!");
            }
            else
            {
                Utility.TamaTalks("What ever...", Color, Name);
                Utility.WriteMsg("Uh oh! Looks like " + tama.Name + " just grew into a spoiled teen!");
                Utility.WriteMsg("You should really step up your parenting game...");
            }

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine();

            Utility.TamaTalks(tama.Satisfied ? "Want to play a game!?" : "Entertain me!", Color, Name);
            play = Utility.GetResponse(tama, owner);

            if (play)
                Command(tama, Tamagotchi.Command.Play);

            if (tama.Satisfied)
                Utility.TamaTalks(play ? "You're so much fun! All that playing made me hungry!" : "Ok, next time...", Color, Name);
            else
            {
                Utility.TamaTalks(play ? "You call that fun? Now - FEED ME!" : " FEED ME!", Color, Name);
                Command(tama, Tamagotchi.Command.Feed);
            }

            Utility.WriteMsg("It's getting late, you should put " + tama.Name + " to bed and turn off the lights!");
            lights = Utility.GetResponse(tama, owner);

            if (tama.Satisfied)
            {
                if (lights)
                {
                    Command(tama, Tamagotchi.Command.Sleep);
                }
                else
                {
                    Command(tama, Tamagotchi.Command.Sleep);
                    Utility.TamaTalks("So I can stay up all night!?", Color, Name);
                    var stayUp = Utility.GetResponse(tama, owner);
                    if (stayUp)
                    {
                        Command(tama, Tamagotchi.Command.Denied);
                        Utility.WriteTamasState(tama);
                        Utility.WriteMsg("Not a wise decision...");
                        System.Threading.Thread.Sleep(2000);
                    }
                    else
                    {
                        Command(tama, Tamagotchi.Command.Sleep);
                    }
                }
            }
            else
            {
                var complaint = new List<string>();
                complaint.AddRange(new String[] {
                        "I'm not going to bed!",
                        "You can't make me!",
                        "Look at me - NOT SLEEPING! You're not the boss of me!",
                        });

                for (int i = 0; i < 3; i++)
                {
                    if (lights)
                        Command(tama, Tamagotchi.Command.Sleep);
                    else
                        Command(tama, Tamagotchi.Command.Denied);

                    Utility.TamaTalks(complaint[i], Color, Name);
                    Utility.WriteMsg("Send " + tama.Name + " to bed?");
                    lights = Utility.GetResponse(tama, owner);
                }

                if (!lights)
                {
                    Utility.TamaTalks("Good, I'm never going to sleep.", Color, Name);
                    Utility.WriteMsg("You have to put " + tama.Name + " to bed!");
                    lights = Utility.GetResponse(tama, owner);
                    if (!lights)
                    {
                        Command(tama, Tamagotchi.Command.Denied);
                        Utility.TamaTalks("*going berserk*", Color, Name);
                        Utility.WriteMsg("Suit yourself...");
                        System.Threading.Thread.Sleep(2000);
                    }
                    else
                    {
                        Command(tama, Tamagotchi.Command.Sleep);
                    }
                }
            }

            if (!lights)
            {
                Console.Clear();
                Utility.WriteMsg("Since you let " + tama.Name + " stay up all night it's not waking up.");
                Utility.WriteMsg("If you don't want to end up with a bad pet \r\nyou need to let it know who's the boss!");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine();
                Utility.WriteMsg("Wake " + tama.Name + " up!");
                bool wake = Utility.GetResponse(tama, owner);

                while (!wake)
                {
                    var wakeIt = new List<string>();
                    wakeIt.AddRange(new String[] {
                        "You should really wake "+tama.Name+" up...",
                        "Wake it!",
                        "Wake "+tama.Name+" or it will become lazy!",
                        "Really, you should take some responsibility for your pet!",
                        owner+", wake it!!",
                        "Come on, wake "+tama.Name+" up!",
                        "Wake it up now!"
                        });
                    Random r = new Random();
                    int index = r.Next(wakeIt.Count);

                    Utility.TamaTalks("Zzzzzzz...", Color, Name);
                    Utility.WriteMsg(wakeIt[index]);
                    wake = Utility.GetResponse(tama, owner);
                }

                if (wake)
                    Command(tama, Tamagotchi.Command.Sleep);
            }

            Utility.WriteTamasState(tama);
            Utility.TamaTalks(tama.Satisfied ? "Good morning " + owner + "! \r\nCan I have some breakfast, please?" : "Why did you wake me up!? \r\nYou better give me something tasty for breakfast... \r\n I only want candy!", Color, Name);
            Command(tama, Tamagotchi.Command.Feed);

            if (!tama.Satisfied)
            {
                var i = 0;

                while (tama.FoodItem == "bread")
                {
                    var complaint = new List<string>();
                    complaint.AddRange(new String[] {
                        "Bread isn't tasty... I want candy!",
                        "I told you! I don't want bread, I want candy!!",
                        "NOOOO BREEEAAAD!!!"
                        });

                    Command(tama, Tamagotchi.Command.Denied);
                    Utility.TamaTalks(complaint[i], Color, Name);
                    i += 1;
                    if (i == 3) break;
                    Command(tama, Tamagotchi.Command.Feed);
                }

                Utility.WriteMsg(tama.FoodItem == "candy" ? "You shouldn't reward such bad behavior with candy..." : "Good, you're starting to make some progress.");
                System.Threading.Thread.Sleep(2000);
            }

            Utility.WriteTamasState(tama);
            if (tama.FoodItem == "bread" && !tama.Satisfied) Utility.TamaTalks(tama.FoodItem + "Bread... *pout*", Color, Name);
            if (tama.FoodItem != "nothing" && tama.Satisfied) Utility.TamaTalks("YUMMM, " + tama.FoodItem + "!", Color, Name);
            if (tama.FoodItem == "nothing") Utility.TamaTalks(tama.Satisfied ? "Ok, but I'm really hungry..." : "Nothing, what! You're not feeding me...?", Color, Name);

            if (tama.Full > 60)
            {
                Utility.WriteMsg("Looks like you need to clean up after " + tama.Name + ". \r\nWill you do it");
                poop = Utility.GetResponse(tama, owner);
                if (poop) Command(tama, Tamagotchi.Command.Poop);
            }

            if (tama.Tired > 60 || tama.Happy < 60)
            {
                Utility.WriteMsg("Oh no, " + tama.Name + " isn't doing so well... He's tired and unhappy \r\nYou have to give it some medicine!");
                bool meds = Utility.GetResponse(tama, owner);
                if (!meds)
                {
                    tama.ChangeStage("dead");
                    Utility.WriteTamasState(tama);
                    Utility.WriteMsg("Why, " + owner + "!? \r\nNow " + tama.Name + " is dead...");
                    Utility.WriteMsg("You really shouldn't have pets, " + owner + "...");
                    Console.WriteLine();
                    Utility.WriteMsg("Hit ENTER to shut down.");
                    Console.ReadLine();
                    return;
                }
                Command(tama, "Medicated");
            }
            else System.Threading.Thread.Sleep(2000);

            tama.ChangeStage(tama.Happy > 60 ? "goodAdult" : "badAdult");
            Utility.WriteTamasState(tama);
            Utility.WriteMsg(tama.Satisfied ? "Good job " + owner + ", \r\nyou've raised your " + tama.Name + " to become good and well behaved pet!" : "Sorry, " + owner + ". You haven't done such a good job in raising " + tama.Name + "...");
            if (tama.Satisfied)
            {
                Utility.TamaTalks("Would you like to play with me?", Color, Name);
                play = Utility.GetResponse(tama, owner);

                int i = 0;
                while (!play)
                {
                    var wannaPlay = new List<string>();
                    wannaPlay.AddRange(new String[] {
                        "But I thought we had fun together,\r\nwon't you play with me?",
                        "You don't like me anymore? I want to play with you!",
                        "Now I'm very sad... Please play with me?",
                        "*cries*"
                        });

                    Command(tama, Tamagotchi.Command.Denied);
                    Utility.TamaTalks(wannaPlay[i], Color, Name);
                    i += 1;
                    if (i == 4) break;
                    Utility.GetResponse(tama, owner);
                }

                if (play)
                {
                    Command(tama, Tamagotchi.Command.Play);
                }
            }
            else
            {
                Utility.WriteMsg("You should play some with " + tama.Name + ".");
                play = Utility.GetResponse(tama, owner);

                int i = 0;
                while (play)
                {
                    var wannaPlay = new List<string>();
                    wannaPlay.AddRange(new String[] {
                        "I don't wanna play with you...",
                        "Didn't you hear me? I don't want to play!",
                        "NOOOOO!"
                        });
                    if (poop) Command(tama, Tamagotchi.Command.Play);

                    Utility.TamaTalks(wannaPlay[i], Color, Name);
                    Utility.WriteMsg("Play with " + tama.Name);
                    i += 1;
                    if (i == 3) break;
                    Utility.GetResponse(tama, owner);
                }
            }

            Utility.WriteTamasState(tama);
            if (tama.Satisfied) Utility.TamaTalks(play ? "That was so much fun " + owner + " !" : "I'm sad now...", Color, Name);
            else Utility.TamaTalks(play ? "That was so much fun... NOT!" : "What ever...", Color, Name);

            Console.WriteLine();
            Utility.TamaTalks("I'm not feeling very well...", Color, Name);

            System.Threading.Thread.Sleep(2000);

            tama.ChangeStage(tama.Happy > 80 ? "angel" : "dead");
            Utility.WriteTamasState(tama);
            if (tama.Satisfied)
            {
                Utility.WriteMsg(tama.Name + " has passed... You took good care of it. It had a happy life!");
                Utility.WriteMsg("Hit ENTER to exit the game.");
                Console.ReadLine();
                return;
            }
            else
            {
                Utility.WriteMsg(tama.Name + " has passed... \r\nSorry, but you're a terrible pet owner " + owner);
                Utility.WriteMsg("Hit ENTER to exit the game.");
                Console.ReadLine();
                return;
            }

        }

        public void ChangeStage(string stage)
        {
            Stage = stage;

            if (Stage == "goodTeen" || Stage == "goodAdult" || Stage == "angel")
            {
                Satisfied = true;
            }
            else
                Satisfied = false;

            ChangeRate();
        }

      

        public override void Poop()
        {
            Full = 0;
            Utility.DrawPoop();
            System.Threading.Thread.Sleep(1000);
            Utility.TamaTalks($"{Name} is feeling really well now.", Color, Name);
            System.Threading.Thread.Sleep(1500);
        }


        public override void Feed()
        {
            bool fed = false;

            if (Hunger > 20)
            {
                Utility.WriteMsg("Choose from [ BREAD ] [ CANDY ] [ NOTHING ]");
                Utility.YouTalk(OwnerName);

                while (fed == false)
                {
                    FoodItem = Console.ReadLine()?.ToLower();

                    if (FoodItem == FoodItems.bread.ToString())
                    {
                        Full = Utility.GetSumValue(Full, FullnessRate, 1);
                        Hunger = Utility.GetSubValue(Hunger, HungerRate, 2);
                        fed = true;
                    }
                    else if (FoodItem == FoodItems.candy.ToString())
                    {
                        Full = Utility.GetSumValue(Full, FullnessRate, 2);
                        Hunger = Utility.GetSubValue(Hunger, HungerRate, 3);
                        fed = true;
                    }
                    else if (FoodItem == FoodItems.nothing.ToString())
                    {
                        Hunger = Utility.GetSumValue(HungerRate, HungerRate, 1);
                        Full = Utility.GetSubValue(Full, FullnessRate, 1);
                        fed = true;
                    }
                    else
                    {
                        Utility.WriteMsg("Choose from [ BREAD ] [ CANDY ] [ NOTHING ]");
                        Utility.YouTalk(OwnerName);
                    }
                }

                Utility.TamaTalks(FoodItem != "nothing" ? "Yum yum yum, " + FoodItem + "!" : "*rumble rumble*", Color, Name);
            }
            else
            {
                Console.WriteLine($"Hello {OwnerName}! I was playing with you.. Am not hungry!");
            }
        }

        public override void Play()
        {
            Happy = Utility.GetSumValue(Happy, HappinessRate, 4);
            Tired = Utility.GetSumValue(Tired, TirednessRate, 2);
        }

        public override void Sleep()
        {
            Tired = 0;
            Utility.TamaTalks("Good night " + OwnerName + "!", Color, Name);
            System.Threading.Thread.Sleep(1000);
        }

        public void Command(Tama tama, string attribute)
        {
            switch (attribute)
            {
                case Tamagotchi.Command.Sleep:
                    Sleep();
                    Utility.DrawNight();
                    break;
                case Tamagotchi.Command.Poop:
                    Poop();
                    break;
                case Tamagotchi.Command.Play:
                    Play();
                    break;
                case Tamagotchi.Command.Feed:
                    Feed();
                    break;
                case Tamagotchi.Command.Denied:
                    Denied();
                    break;
                default:
                    {
                        Happy = Utility.GetSumValue(Happy, HappinessRate, 1);
                        break;
                    }
            }

            System.Threading.Thread.Sleep(1000);
            Utility.WriteTamasState(tama);
            CheckUp(tama);
        }

        private void CheckUp(Tama tama)
        {
            if (Tired > 90)
            {
                Utility.TamaTalks($"{Name} is feeling really tired. \r\nWill you turn off the lights for him?", Color, Name);

                bool response = Utility.GetResponse(tama, OwnerName);
                if (response)
                {
                    Command(tama, Tamagotchi.Command.Sleep);
                    Utility.TamaTalks(Happy < 50 ? $"Good morning { OwnerName} !" : $"I don't like you {OwnerName} very much *sob sob*", Color, Name);

                }
                return;
            }
            if (Full > 90)
            {
                Utility.TamaTalks($"{Name} is feeling really full. \r\nWill you take him to poop?", Color, Name);

                bool response = Utility.GetResponse(tama, OwnerName);
                if (response) Command(tama, Tamagotchi.Command.Poop);
                return;
            }
            if (Hunger > 90)
            {
                Utility.TamaTalks($"{Name} is feeling really Hungry. \r\nPlease feed him?", Color, Name);

                bool response = Utility.GetResponse(tama, OwnerName);
                if (response) Command(tama, Tamagotchi.Command.Feed);
                return;
            }
            if (Happy < 10)
            {
                Utility.TamaTalks($"{Name} is feeling really Sad. \r\nPlease play with him?", Color, Name);

                bool response = Utility.GetResponse(tama, OwnerName);
                if (response) Command(tama, Tamagotchi.Command.Play);
                Utility.TamaTalks(response ? "YAY!" : "Boooo!", Color, Name);
            }
        }

        private void Denied()
        {
            Happy = Utility.GetSubValue(Happy, HappinessRate, 1);
        }

        private void ChangeRate()
        {
            HungerRate = (int)(HungerRate * 1.1);
            HappinessRate = (int)(HappinessRate * 1.2);
            FullnessRate = (int)(FullnessRate * 1.1);
            TirednessRate = (int)(TirednessRate * 1.3);
        }
    }
}