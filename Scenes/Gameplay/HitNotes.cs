using Raylib_cs;

namespace RhythmGame.Scenes.Gameplay
{
    public class HitNotes
    {
        public Note[] TrackHitNotes = new Note[4];
        public string[] Debug = new string[4];

        public HitNotes()
        {
            for (int i = 0; i < TrackHitNotes.Length; i++)
            {
                TrackHitNotes[i] = new Note(i, true);
                TrackHitNotes[i].Position = Raylib.GetScreenHeight() - Note.Spacing;
            }
        }

        public void Render()
        {
            for (int i = 0; i < TrackHitNotes.Length; i++)
                TrackHitNotes[i].Render();
            for (int i = 0; i < Debug.Length; i++)
                Raylib.DrawText(Debug[i], 450 + (i * 100), 100, 20, Color.White);
        }

        public void Tick(List<Note> notes)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Raylib.IsKeyPressed(Program.Settings.Keybinds[i]))
                {
                    Note[] track = notes.Where(x => x.Track == 3 - i).ToArray();
                    if (track.Length == 0)
                        continue;
                    Array.Sort(track, new NoteSort());
                    Note first = track.First();
                    int distance = (int)Math.Abs(first.Position - TrackHitNotes.First().Position);
                    if (distance < 120)
                    {
                        ((GameplayScene)Program.CurrentScene).Combo++;
                        first.Removed = true;
                    }
                }
            }
        }

        public class NoteSort : IComparer<Note>
        {
            public int Compare(Note? x, Note? y)
            {
                if (x == null || y == null) return 0;
                if (x.Position > y.Position) return -1;
                return 1;
            }
        }
    }
}
