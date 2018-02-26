using System;
using Interfaces;
using UnityEngine;
using Random = System.Random;

namespace Foe
{
    public class SimpleWalkerBoss : SimpleWalkerEnemy, IKillable, IBoss, IPowerful
    {
        public BossPowerUpType Type = BossPowerUpType.NONE;
        public int BuffedSpeedModifier = 2;
        public int BuffedRangeModifier = 10;
        public int BuffedScaleModifier = 3;

        private bool _nimblicizing;
        private int _rpm;

        public new void Update()
        {
            base.Update();
            if (_nimblicizing)
            {
                Nimblicize();
            }
        }

        public new void Kill()
        {
            base.Kill();
            Invoke("BossDeath", 3);
        }

        private void BossDeath()
        {
            Destroy(gameObject);
        }

        public void PowerUp()
        {
            if (Dead)
            {
                return;
            }
            IsMoonwalking = true;
            Speed *= BuffedSpeedModifier;
            Range *= BuffedRangeModifier;
            switch (Type)
            {
                case BossPowerUpType.NIMBLY:
                    _nimblicizing = true;
                    break;
                case BossPowerUpType.GARGANTUAN:
                    gameObject.transform.localScale *= BuffedScaleModifier;
                    break;
                case BossPowerUpType.SPEEDY_GONZALES:
                    Speed *= 3;
                    break;
            }

            ResetBounds();
        }

        private void Nimblicize()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            int low = 400;
            int high = 1200;
            int direction = 1;

            if (_rpm < high)
            {
                low *= 3;
                high *= 3;
            }

            if (rnd.Next(0, 1) == 1)
            {
                direction *= -1;
            }

            _rpm = rnd.Next(low, high);

            gameObject.transform.Rotate(0, 0, _rpm * direction * Time.deltaTime);
        }

        private void StopNimblicizing()
        {
            _nimblicizing = false;
        }
    }
}