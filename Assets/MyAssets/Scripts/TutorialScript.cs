using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.MyAssets.Scripts
{
    public class TutorialScript : MonoBehaviour
    {
        public GameObject playerObject;

        bool jumpSuccessful = false;
        bool colorChangeSuccessful = false;
        bool powerUpSuccessful = false;

        private float nextActionTime = 0.0f;
        public float JumpIntervall = 0.3f;


        void Start()
        {

        }

        void Update()
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += JumpIntervall;

                // play animation
            }


        }

    }
}
