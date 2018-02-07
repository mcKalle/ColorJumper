using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MyAssets.Scripts.PowerUps
{
    public interface IPowerUp
    {
        int Count { get; set; }
        bool CanBeUsed { get; set; }
    }
}
