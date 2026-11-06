
namespace Arkanoid
{
    public class Bonus
    {
        public double X { get; set; }
        public double Y { get; set; }

        public BonusType Type { get; set; }

        public double FallSpeed { get; set; } = 0.5;

        public bool IsActive { get; set; } = true;

        public Bonus(double x, double y, BonusType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public void Move()
        {
            Y += FallSpeed;
        }
    }
}
