
namespace Arkanoid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Arkanoid";

            SaveManager saveManager = new SaveManager();

            Console.WriteLine("==============================");
            Console.WriteLine("          ARKANOID");
            Console.WriteLine("==============================");
            Console.WriteLine();

            GameState? loadedState = null;

            if (saveManager.HasSave())
            {
                Console.WriteLine("Найдено сохранение!");
                Console.WriteLine();
                Console.WriteLine("1. Продолжить игру");
                Console.WriteLine("2. Начать новую игру");
                Console.WriteLine();

                Console.Write("Ваш выбор: ");

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    loadedState = saveManager.Load();
                }
            }

            Console.WriteLine();
            Console.WriteLine("Управление:");
            Console.WriteLine("Стрелки или A/D - движение");
            Console.WriteLine("Q - сохранить и выйти");
            Console.WriteLine();

            Console.WriteLine("Нажмите любую клавишу для начала...");
            Console.ReadKey(true);

            Game game = new Game(loadedState);
            game.Start();

            Console.WriteLine();
            Console.WriteLine("Спасибо за игру!");
        }
    }
}
