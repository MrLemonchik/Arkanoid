
using System.Text.Json;

namespace Arkanoid
{
    public class SaveManager
    {
        private const string SavePath = "savegame.json";

        private readonly JsonSerializerOptions options =
            new JsonSerializerOptions
            {
                WriteIndented = true
            };

        public bool HasSave()
        {
            return File.Exists(SavePath);
        }

        public void Save(GameState state)
        {
            try
            {
                string json = JsonSerializer.Serialize(state, options);

                File.WriteAllText(SavePath, json);

                Console.WriteLine("Игра успешно сохранена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка сохранения: " + ex.Message);
            }
        }

        public GameState? Load()
        {
            try
            {
                if (!File.Exists(SavePath))
                {
                    Console.WriteLine("Сохранение не найдено.");
                    return null;
                }

                string json = File.ReadAllText(SavePath);

                GameState? state =
                    JsonSerializer.Deserialize<GameState>(json, options);

                if (state == null ||
                    state.Paddle == null ||
                    state.Balls == null ||
                    state.Bricks == null ||
                    state.Bonuses == null)
                {
                    Console.WriteLine("Файл сохранения повреждён.");
                    return null;
                }

                Console.WriteLine("Игра успешно загружена!");

                return state;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка загрузки: " + ex.Message);
                return null;
            }
        }
    }
}
