using ImGuiNET;
using Newtonsoft.Json;
using Raylib_cs;
using RhythmGame.Logic.Mappings;
using System.Diagnostics;

namespace RhythmGame.Scenes.Gameplay
{
    public class GameplayScene : IScene
    {
        public HitNotes HitNotes = new HitNotes();
        public List<Note> Notes = new List<Note>();
        public int Combo = 0;
        public int Misses = 0;
        public FNFMap Mapping = new FNFMap();
        public Stopwatch SongPosition = new Stopwatch();

        public GameplayScene()
        {
            Mapping = JsonConvert.DeserializeObject<FNFMap>(File.ReadAllText(@"F:\Friday Night Funkin\Friday Night Funkin\assets\data\songs\milf\milf-chart.json"))!;
            SongPosition.Start();

            Sound inst = Raylib.LoadSound(@"F:\Friday Night Funkin\Friday Night Funkin\assets\songs\milf\Inst.ogg");
            Sound bfVocals = Raylib.LoadSound(@"F:\Friday Night Funkin\Friday Night Funkin\assets\songs\milf\Voices-bf.ogg");
            Sound momVocals = Raylib.LoadSound(@"F:\Friday Night Funkin\Friday Night Funkin\assets\songs\milf\Voices-mom.ogg");
            
            Raylib.PlaySound(inst);
            Raylib.PlaySound(bfVocals);
            Raylib.PlaySound(momVocals);
        }

        public void Render()
        {
            HitNotes.Render();
            foreach (Note note in Notes)
                note.Render();
        }
        
        public void Tick()
        {
            HitNotes.Tick(Notes);

            TickMap();

            for (int i = 0; i < Notes.Count; i++) 
            {
                Notes[i].Tick();
                if (Notes[i].Removed)
                    Notes.RemoveAt(i);
            }
        }

        private int _noteIndex;
        private void TickMap()
        {
            for (; _noteIndex < Mapping.Notes.Hard.Length; _noteIndex++)
            {
                FNFMap.Note noteData = Mapping.Notes.Hard[_noteIndex];
                if (noteData.Time > SongPosition.ElapsedMilliseconds)
                    break;

                if (noteData.Direction > 3) // Exclude enemy notes
                    continue;

                Note gameNote = new Note(3 - noteData.Direction); // Flip the tracks from right to left
                Notes.Add(gameNote);
            }
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
