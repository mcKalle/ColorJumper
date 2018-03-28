using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.MyAssets.Scripts.PowerUps
{
    
    public interface IPowerUp
    {
        GameObject Prefab { get; set; }
        Transform Parent { get; set; }

        int Count { get; set; }
        bool CanBeUsed { get; set; }
        bool Placed { get; set; }

        float XPos { get; set; }
        float YPos { get; set; }
    }
}
