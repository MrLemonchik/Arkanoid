
using System.Threading;

namespace Arkanoid
{
    public class Game
    {
        private readonly SaveManager saveManager;
        private readonly GameState state;
        private readonly Level level;
        private readonly CollisionHandler collisionHandler;
        private readonly InputHandler inputHandler;
        private readonly ConsoleRenderer renderer;
        private readonly Random random;

        private bool exitRequested;


        public Game(GameState? loadedState = null)
        {
            state = loadedState ?? new GameState();

            level = new Level();
            collisionHandler = new CollisionHandler();
            inputHandler = new InputHandler();
            renderer = new ConsoleRenderer();
            random = new Random();
            saveManager = new SaveManager();

            if (loadedState == null)
            {
                InitializeGame();
            }
            else
            {
                state.Status = GameStatus.Running;
            }
        }


        private void InitializeGame()
        {
            level.Generate(state);

            state.Balls.Clear();

            state.Balls.Add(new Ball(
                state.FieldWidth / 2,
                state.FieldHeight - 4,
                0.5,
                -0.5
            ));

            state.Status = GameStatus.Running;
        }

        public void Start()
        {
            Console.CursorVisible = false;

            while (state.Status == GameStatus.Running &&
                   !exitRequested)
            {
                ProcessInput();
                Update();
                renderer.Draw(state);

                Thread.Sleep(40);
            }

            Console.CursorVisible = true;

            if (exitRequested)
            {
                saveManager.Save(state);
                Console.WriteLine("\nИгра сохранена. До встречи!");
            }
            else if (state.Status == GameStatus.Won)
            {
                Console.WriteLine("\nПОБЕДА! Все кирпичи уничтожены!");
            }
            else if (state.Status == GameStatus.Lost)
            {
                Console.WriteLine("\nИГРА ОКОНЧЕНА! Все мячи потеряны.");
            }
        }


        private void ProcessInput()
        {
            List<ConsoleKey> keys = inputHandler.ReadKeys();

            foreach (ConsoleKey key in keys)
            {
                if (key == ConsoleKey.LeftArrow ||
                    key == ConsoleKey.A)
                {
                    state.Paddle.MoveLeft();
                }
                else if (key == ConsoleKey.RightArrow ||
                         key == ConsoleKey.D)
                {
                    state.Paddle.MoveRight(state.FieldWidth);
                }
                else if (key == ConsoleKey.Q)
                {
                    exitRequested = true;
                }
            }
        }



        private void ApplyBonus(Bonus bonus)
        {
            switch (bonus.Type)
            {
                case BonusType.ExpandPaddle:
                    state.Paddle.Width += 6;

                    if (state.Paddle.Width > 24)
                        state.Paddle.Width = 24;

                    Console.Beep(600, 100);
                    break;

                case BonusType.ExpandBall:
                    foreach (var ball in state.Balls)
                    {
                        ball.Size = 2;
                    }

                    Console.Beep(800, 100);
                    break;

                case BonusType.ExtraBalls:
                    state.Balls.Add(new Ball(
                        state.Paddle.X + state.Paddle.Width / 2,
                        state.Paddle.Y - 2,
                        -0.7,
                        -0.7
                    ));

                    state.Balls.Add(new Ball(
                        state.Paddle.X + state.Paddle.Width / 2,
                        state.Paddle.Y - 2,
                        0.7,
                        -0.7
                    ));

                    Console.Beep(1000, 100);
                    break;
            }
        }

        private void Update()
        {
            // Движение мячей
            foreach (var ball in state.Balls)
            {
                if (!ball.IsActive)
                    continue;

                ball.Move();

                collisionHandler.HandleWallCollision(ball, state);
                collisionHandler.HandlePaddleCollision(ball, state.Paddle);
                collisionHandler.HandleBrickCollision(ball, state);
            }

            // Удаление потерянных мячей
            state.Balls.RemoveAll(ball =>
                ball.Y >= state.FieldHeight - 1);

            // Движение бонусов
            foreach (var bonus in state.Bonuses)
            {
                if (!bonus.IsActive)
                    continue;

                bonus.Move();

                // Проверка подбора бонуса платформой
                if (bonus.Y >= state.Paddle.Y - 1 &&
                    bonus.Y <= state.Paddle.Y &&
                    bonus.X >= state.Paddle.X &&
                    bonus.X <= state.Paddle.X + state.Paddle.Width)
                {
                    ApplyBonus(bonus);
                    bonus.IsActive = false;
                }

                // Бонус упал за пределы поля
                if (bonus.Y >= state.FieldHeight - 1)
                {
                    bonus.IsActive = false;
                }
            }

            state.Bonuses.RemoveAll(bonus => !bonus.IsActive);

            // Проверка проигрыша
            if (state.Balls.Count == 0)
            {
                state.Status = GameStatus.Lost;
                return;
            }

            // Проверка победы
            state.Bricks.RemoveAll(brick => brick.IsDestroyed);

            if (state.Bricks.Count == 0)
            {
                state.Status = GameStatus.Won;
            }
        }

    }
}
