using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public  abstract class TamaBase
    {
        public int Tired { get; set; }
        public int Hunger { get; set; }
        public int Happy { get; set; }
        public int Full { get; set; }

        public abstract void Play();
        public abstract void Feed();
        public abstract void Poop();
        public abstract void Sleep();
    }
}
