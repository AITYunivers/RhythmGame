using ImGuiNET;
using System.Diagnostics;

namespace RhythmGame.Scenes.Gameplay
{
    public class GameplayScene : IScene
    {
        public HitNotes HitNotes = new HitNotes();
        public List<Note> Notes = new List<Note>();
        public int Combo = 0;
        public int Misses = 0;

        public void Render()
        {
            HitNotes.Render();
            foreach (Note note in Notes)
                note.Render();
        }

        Stopwatch bs = Stopwatch.StartNew();
        Stopwatch bs2 = Stopwatch.StartNew();
        int lastTrack = 0;
        int jack = 0;
        public void Tick()
        {
            HitNotes.Tick(Notes);

            if (bs.ElapsedMilliseconds >= new Random().Next(100, 200))
            {
                Notes.Add(new Note(GetRandomTrack()));
                bs.Restart();
            }

            for (int i = 0; i < Notes.Count; i++) 
            {
                Notes[i].Tick();
                if (Notes[i].Removed)
                    Notes.RemoveAt(i);
            }
        }

        public int GetRandomTrack()
        {
            Random rand = new Random();
            int track = rand.Next(4);
            while (track == lastTrack && jack > 1)
                track = rand.Next(4);
            if (track == lastTrack)
                jack++;
            lastTrack = track;
            return track;
        }

        public void DrawDebug()
        {
            int total = Notes.Count;
            ImGui.DragInt("Notes Visible", ref total);
            ImGui.DragInt("Combo", ref Combo);
            ImGui.DragInt("Misses", ref Misses);
            if (ImGui.CollapsingHeader("Tracks"))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (ImGui.TreeNode("Track " + i))
                    {
                        Note[] track = Notes.Where(x => x.Track == 3 - i).ToArray();
                        int count = track.Length;
                        ImGui.DragInt("Note Count", ref count);
                        Array.Sort(track, new HitNotes.NoteSort());
                        int distance = -1;
                        if (count > 0)
                        {
                            Note first = track.First();
                            distance = (int)Math.Abs(first.Position - HitNotes.TrackHitNotes.First().Position);
                        }
                        bool inRange = distance > 0 && distance < 120;
                        ImGui.DragInt("Note Distance", ref distance);
                        ImGui.Checkbox("Within Range", ref inRange);
                        ImGui.TreePop();
                    }
                }
            }
        }
    }
}
