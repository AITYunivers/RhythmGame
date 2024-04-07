namespace RhythmGame.Scenes
{
    public interface IScene
    {
        public void Render();
        public void Tick();
        public void DrawDebug();
    }
}
