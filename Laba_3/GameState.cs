
namespace Arkanoid
{
    public class GameState
    {
        public List<Ball> Balls { get; set; } = new();

        public Paddle Paddle { get; set; }

        public List<Brick> Bricks { get; set; } = new();

        public List<Bonus> Bonuses { get; set; } = new();

        public int Score { get; set; }

        public GameStatus Status { get; set; }

        public int FieldWidth { get; set; } = 80;

        public int FieldHeight { get; set; } = 25;

        public GameState()
        {
            Paddle = new Paddle(35, 22);
            Status = GameStatus.NotStarted;
        }
    }
}
