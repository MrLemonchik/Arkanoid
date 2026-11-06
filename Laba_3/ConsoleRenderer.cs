
namespace Arkanoid
{
    public class ConsoleRenderer
    {
        public void Draw(GameState state)
        {
            Console.SetCursorPosition(0, 0);

            char[,] field = new char[state.FieldHeight, state.FieldWidth];

            for (int y = 0; y < state.FieldHeight; y++)
            {
                for (int x = 0; x < state.FieldWidth; x++)
                {
                    field[y, x] = ' ';
                }
            }

            // Границы поля
            for (int x = 0; x < state.FieldWidth; x++)
            {
                field[0, x] = '-';
                field[state.FieldHeight - 1, x] = '-';
            }

            for (int y = 0; y < state.FieldHeight; y++)
            {
                field[y, 0] = '|';
                field[y, state.FieldWidth - 1] = '|';
            }

            // Кирпичи
            foreach (var brick in state.Bricks)
            {
                if (brick.IsDestroyed)
                    continue;

                for (int x = brick.X; x < brick.X + brick.Width; x++)
                {
                    if (x > 0 && x < state.FieldWidth - 1)
                        field[brick.Y, x] = '#';
                }
            }

            // Мячи

            foreach (var ball in state.Balls)
            {
                if (!ball.IsActive)
                    continue;

                int x = (int)Math.Round(ball.X);
                int y = (int)Math.Round(ball.Y);

                if (x > 0 && x < state.FieldWidth - 1 &&
                    y > 0 && y < state.FieldHeight - 1)
                {
                    field[y, x] = 'O';

                    if (ball.Size > 1)
                    {
                        if (x - 1 > 0)
                            field[y, x - 1] = 'O';

                        if (x + 1 < state.FieldWidth - 1)
                            field[y, x + 1] = 'O';
                    }
                }
            }


            // Бонусы
            foreach (var bonus in state.Bonuses)
            {
                if (!bonus.IsActive)
                    continue;

                int x = (int)Math.Round(bonus.X);
                int y = (int)Math.Round(bonus.Y);

                if (x > 0 && x < state.FieldWidth - 1 &&
                    y > 0 && y < state.FieldHeight - 1)
                {
                    field[y, x] = '+';
                }
            }

            // Платформа
            for (int x = state.Paddle.X;
                 x < state.Paddle.X + state.Paddle.Width;
                 x++)
            {
                if (x > 0 && x < state.FieldWidth - 1)
                    field[state.Paddle.Y, x] = '=';
            }

            // Вывод поля
            for (int y = 0; y < state.FieldHeight; y++)
            {
                for (int x = 0; x < state.FieldWidth; x++)
                {
                    Console.Write(field[y, x]);
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Счёт: {state.Score}");
            Console.WriteLine("Управление: ← → или A/D | Q — сохранить и выйти");
        }
    }
}
