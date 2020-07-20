using System;

namespace Services.LifeCircle.Services
{
    class RandomCounter : ICounter
    {
        static Random Rnd = new Random();
        private int _value;

        public RandomCounter()
        {
            _value = Rnd.Next(0, 10000);
        }

        public int Value => _value;
    }
}
