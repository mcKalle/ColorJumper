using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MyAssets.Scripts.PowerUps
{
    public class PowerUpImpl : IPowerUp
    {
        private bool _canBeUsed;
        public bool CanBeUsed
        {
            get
            {
                return _canBeUsed;
            }

            set
            {
                _canBeUsed = value;
            }
        }

        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }

            set
            {
                _count = value;
            }
        }
    }
}
