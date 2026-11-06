
namespace Arkanoid
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        public int Size { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public Ball(double x, double y, double velocityX, double velocityY)
        {
            X = x;
            Y = y;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public void Move()
        {
            X += VelocityX;
            Y += VelocityY;
        }
    }
}
