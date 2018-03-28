using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.MyAssets.Scripts.PowerUps
{
    [Serializable]
    public class SplitPowerUp : IPowerUp
    {
        public GameObject InspectorPrefab;

        private GameObject _prefab;
        public GameObject Prefab
        {
            get
            {
                return InspectorPrefab;
            }

            set
            {
                InspectorPrefab = value;
            }
        }

        private Transform _parent;
        public Transform Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
            }
        }

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

        private bool _placed;
        public bool Placed
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

        private float _xPos;
        public float XPos
        {
            get
            {
                return _xPos;
            }

            set
            {
                _xPos = value;
            }
        }

        private float _yPos;
        public float YPos
        {
            get
            {
                return _yPos;
            }

            set
            {
                _yPos = value;
            }
        }
    }
}
