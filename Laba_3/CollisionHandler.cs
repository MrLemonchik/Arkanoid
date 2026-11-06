
namespace Arkanoid
{
    public class CollisionHandler
    {
        private readonly Random random = new Random();

        public void HandleWallCollision(Ball ball, GameState state)
        {
            if (ball.X <= 1 || ball.X >= state.FieldWidth - 2)
            {
                ball.VelocityX *= -1;
            }

            if (ball.Y <= 1)
            {
                ball.VelocityY *= -1;
            }
        }

        public void HandlePaddleCollision(Ball ball, Paddle paddle)
        {
            if (ball.VelocityY > 0 &&
                ball.Y >= paddle.Y - 1 &&
                ball.Y <= paddle.Y &&
                ball.X >= paddle.X &&
                ball.X <= paddle.X + paddle.Width)
            {
                ball.VelocityY *= -1;

                double hitPosition =
                    (ball.X - paddle.X) / paddle.Width;

                ball.VelocityX = (hitPosition - 0.5) * 4;
            }
        }

        public void HandleBrickCollision(Ball ball, GameState state)
        {
            foreach (var brick in state.Bricks)
            {
                if (brick.IsDestroyed)
                    continue;

                if (ball.X >= brick.X &&
                    ball.X <= brick.X + brick.Width &&
                    ball.Y >= brick.Y &&
                    ball.Y <= brick.Y + 1)
                {
                    brick.Health--;
                    ball.VelocityY *= -1;

                    if (brick.IsDestroyed)
                    {
                        state.Score += brick.Points;

                        // Шанс выпадения бонуса - 25%
                        if (random.Next(100) < 25)
                        {
                            BonusType type = (BonusType)
                                random.Next(0, 3);

                            state.Bonuses.Add(
                                new Bonus(
                                    brick.X + brick.Width / 2.0,
                                    brick.Y,
                                    type
                                )
                            );
                        }
                    }

                    break;
                }
            }
        }
    }
}
