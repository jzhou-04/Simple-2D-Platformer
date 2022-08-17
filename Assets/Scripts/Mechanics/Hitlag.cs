using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Hitlag : MonoBehaviour
    {
        private float timer = 0;
        private float slowTo = 1;
        private float timeAmount = 0;
        private bool inHitlag = false;

        public static Hitlag Instance { get; private set; }

        public void TriggerHitLag(float slowTo, float timeAmount)
        {
            this.slowTo = slowTo;
            this.timeAmount = timeAmount;
            inHitlag = true;
            timer = 0;
        }

        public void Update()
        {
            Time.timeScale = slowTo;
            if (timer < timeAmount)
            {
                timer += Time.unscaledDeltaTime;
            }
            else
            {
                inHitlag = false;
            }

            if (inHitlag)
            {
                Time.timeScale = slowTo;
            }

            else
            {
                Time.timeScale = 1f;
            }
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
