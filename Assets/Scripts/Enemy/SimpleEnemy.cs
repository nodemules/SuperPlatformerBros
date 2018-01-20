using Interfaces;

namespace Enemy
{
    public class SimpleEnemy : Enemy, IBoundary
    {
        protected override void Move()
        {
            // do nothing
        }

        protected override void StopMoving()
        {
            // do nothing
        }

        protected override void TurnAround()
        {
            // do nothing
        }

        public bool IsObstacle
        {
            get { return true; }
        }
    }
}