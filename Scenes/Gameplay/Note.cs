using Raylib_cs;
using RhythmGame.Logic;
using RhythmGame.Objects;
using System.Numerics;

namespace RhythmGame.Scenes.Gameplay
{
    public class Note : IObject
    {
        public static int Size => Program.Settings.NoteSize;
        public static int Spacing => Program.Settings.NoteSpacing;

        public int Track;
        public float Position;
        public bool Removed;
        public bool HitNote;

        public Note(int track, bool hitNote = false)
        {
            Track = track;
            HitNote = hitNote;
        }

        public void Render()
        {
            Raylib.DrawTexturePro(AssetLoader.GetTexture("Note"), AssetLoader.GetBounds("Note"), GetBounds(), new Vector2(Size / 2f, Size / 2f), 0, HitNote ? Color.White : NoteColors[Track]);
            //Raylib.DrawRectangleLinesEx(GetBounds(false), 2f, Color.Red);
        }

        public void Tick()
        {
            Position += Raylib.GetFrameTime() / (1 / (60f * Program.Settings.NoteSpeed));
            if (Position > Raylib.GetScreenHeight() + Size + Spacing)
            {
                ((GameplayScene)Program.CurrentScene).Combo = 0;
                ((GameplayScene)Program.CurrentScene).Misses++;
                Removed = true;
            }
        }

        public Rectangle GetBounds(bool adjustOrigin = true)
        {
            int posX = (int)(Raylib.GetScreenWidth() / 2f);
            posX -= ((Size + Spacing) * 2) - ((Size + Spacing) * (3 - Track));
            return new Rectangle(posX + (adjustOrigin ? Size / 2f : 0),
                                 -Size + Position + (adjustOrigin ? Size / 2f : 0),
                                 Size,
                                 Size);
        }

        public static Dictionary<int, Color> NoteColors = new()
        {
            { 0, Color.Gold    }, // Left
            { 1, Color.SkyBlue }, // Up
            { 2, Color.Gold    }, // Down
            { 3, Color.SkyBlue }  // Right
        };

        public static Dictionary<int, float> NoteRotations = new()
        {
            { 0, 270f }, // Left
            { 1, 0f   }, // Up
            { 2, 180f }, // Down
            { 3, 90f  }  // Right
        };
    }
}
