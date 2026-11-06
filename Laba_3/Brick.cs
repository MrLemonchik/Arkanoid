
namespace Arkanoid
{
    public class Brick
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; } = 6;

        public int Health { get; set; } = 1;

        public int Points { get; set; } = 10;

        public bool IsDestroyed => Health <= 0;

        public Brick(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
