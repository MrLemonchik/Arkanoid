
namespace Arkanoid
{
    public class Paddle
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; } = 10;

        public int Speed { get; set; } = 2;

        public Paddle(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveLeft()
        {
            X -= Speed;

            if (X < 1)
                X = 1;
        }

        public void MoveRight(int fieldWidth)
        {
            X += Speed;

            if (X + Width > fieldWidth - 1)
                X = fieldWidth - Width - 1;
        }
    }
}
