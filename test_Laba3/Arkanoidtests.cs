
using System.Text.Json;
using Xunit;
using Arkanoid;

namespace Arkanoid.Tests
{
	public class ArkanoidTests
	{
		// Проверка движения мяча
		[Fact]
		public void Ball_Move_ChangesCoordinates()
		{
			Ball ball = new Ball(10, 10, 2, -1);

			ball.Move();

			Assert.Equal(12, ball.X);
			Assert.Equal(9, ball.Y);
		}

		// Проверка движения платформы влево
		[Fact]
		public void Paddle_MoveLeft_ChangesPosition()
		{
			Paddle paddle = new Paddle(20, 22);

			paddle.MoveLeft();

			Assert.Equal(18, paddle.X);
		}

		// Проверка ограничения движения платформы
		[Fact]
		public void Paddle_MoveLeft_DoesNotCrossBorder()
		{
			Paddle paddle = new Paddle(1, 22);

			paddle.MoveLeft();

			Assert.Equal(1, paddle.X);
		}

		// Проверка уничтожения кирпича
		[Fact]
		public void Brick_IsDestroyed_WhenHealthIsZero()
		{
			Brick brick = new Brick(10, 5);

			brick.Health--;

			Assert.True(brick.IsDestroyed);
		}

		// Проверка отскока мяча от стены
		[Fact]
		public void Ball_BouncesOffLeftWall()
		{
			Ball ball = new Ball(1, 10, -2, 1);
			GameState state = new GameState();

			CollisionHandler handler = new CollisionHandler();

			handler.HandleWallCollision(ball, state);

			Assert.Equal(2, ball.VelocityX);
		}

		// Проверка сериализации и десериализации
		[Fact]
		public void GameState_Serialization_RestoresObjects()
		{
			GameState state = new GameState();

			state.Score = 150;
			state.Balls.Add(new Ball(20, 10, 1, -1));
			state.Bricks.Add(new Brick(5, 3));
			state.Bonuses.Add(
				new Bonus(15, 8, BonusType.ExtraBalls)
			);

			string json = JsonSerializer.Serialize(state);

			GameState? restored =
				JsonSerializer.Deserialize<GameState>(json);

			Assert.NotNull(restored);

			Assert.Equal(150, restored.Score);
			Assert.Single(restored.Balls);
			Assert.Single(restored.Bricks);
			Assert.Single(restored.Bonuses);

			Assert.Equal(20, restored.Balls[0].X);
			Assert.Equal(
				BonusType.ExtraBalls,
				restored.Bonuses[0].Type
			);
		}
	}
}
