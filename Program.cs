using ImGuiNET;
using Newtonsoft.Json;
using Raylib_cs;
using RhythmGame.Logic;
using RhythmGame.Scenes;
using RhythmGame.Scenes.Gameplay;
using rlImGui_cs;

/* TODO:
 * Fix HitNotes having inconsistent hit timings on different Scroll Speeds
 * Make notes consistently end up at the end-point regardless of Scroll Speed
 * Steal Friday Night Funkin maps
 * A lot more stuff
 */

namespace RhythmGame
{
    public class Program
    {
        public static UserSettings Settings = null!;
        public static IScene CurrentScene = null!;

        public static void Main(string[] args)
        {
            LoadUserSettings();

            Raylib.InitWindow(1280, 720, "Hello, Raylib!");
            rlImGui.Setup(true);
            AssetLoader.LoadAssets();
            CurrentScene = new GameplayScene();

            while (!Raylib.WindowShouldClose())
            {
                Raylib.ClearBackground(Color.Black);
                Raylib.BeginDrawing();

                Raylib.DrawFPS(10, 10);
                CurrentScene.Tick();
                CurrentScene.Render();

                // Render debug

                rlImGui.Begin();
                if (ImGui.Begin("Debug"))
                {
                    CurrentScene.DrawDebug();
                }
                ImGui.End();
                rlImGui.End();

                Raylib.EndDrawing();
            }

            rlImGui.Shutdown();
            Raylib.CloseWindow();
        }

        public static void LoadUserSettings()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "userSettings.json");
            if (File.Exists(filePath))
                Settings = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(filePath))!;
            else
                Settings = new UserSettings();
            File.WriteAllText(filePath, JsonConvert.SerializeObject(Settings, Formatting.Indented));
        }
    }
}
