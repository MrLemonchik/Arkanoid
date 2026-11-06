
namespace Arkanoid
{
    public class Level
    {
        public void Generate(GameState state)
        {
            state.Bricks.Clear();

            int rows = 5;
            int columns = 10;

            int startX = 8;
            int startY = 3;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    int x = startX + column * 7;
                    int y = startY + row * 2;

                    state.Bricks.Add(new Brick(x, y));
                }
            }
        }
    }
}
