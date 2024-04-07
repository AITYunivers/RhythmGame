using Raylib_cs;

namespace RhythmGame
{
    public class UserSettings
    {
        public float NoteSpeed = 1f;
        public int NoteSize = 110;
        public int NoteSpacing = 20;
        public KeyboardKey[] Keybinds =
        [
            KeyboardKey.A,
            KeyboardKey.S,
            KeyboardKey.Semicolon,
            KeyboardKey.Apostrophe
        ];
    }
}
