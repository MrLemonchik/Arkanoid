
namespace Arkanoid
{
    public class InputHandler
    {
        public List<ConsoleKey> ReadKeys()
        {
            List<ConsoleKey> keys = new List<ConsoleKey>();

            while (Console.KeyAvailable)
            {
                keys.Add(Console.ReadKey(true).Key);
            }

            return keys;
        }
    }
}
