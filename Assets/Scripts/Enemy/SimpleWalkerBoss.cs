using Interfaces;

namespace Enemy
{
    public class SimpleWalkerBoss : SimpleWalkerEnemy, IKillable, IBoss
    {
        public new void Kill()
        {
            base.Kill();
            Invoke("BossDeath", 3);
        }

        private void BossDeath()
        {
            Destroy(gameObject);
        }
    }
}