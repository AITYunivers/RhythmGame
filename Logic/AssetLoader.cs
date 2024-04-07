using Raylib_cs;

namespace RhythmGame.Logic
{
    public static class AssetLoader
    {
        public const string AssetPath = "Assets\\";
        private static Dictionary<string, Tuple<Image, Texture2D>> _textures = new();

        public static void LoadAssets()
        {
            _textures.Clear();
            // Gets the path of every file in the Assets folder
            foreach (string asset in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), AssetPath)))
            {
                switch (Path.GetExtension(asset))
                {
                    case ".png":
                        // Loads the image into the dictionary/lookup table
                        Image img = Raylib.LoadImage(asset);
                        Texture2D tx = Raylib.LoadTextureFromImage(img);
                        _textures.Add(Path.GetFileNameWithoutExtension(asset), Tuple.Create(img, tx));
                        break;
                }
            }
        }

        /// <summary>
        /// Returns an image from the dictionary/lookup table
        /// </summary>
        /// <param name="asset">Asset name</param>
        public static Image GetImage(string asset)
        {
            return _textures[asset].Item1;
        }

        /// <summary>
        /// Returns a texture from the dictionary/lookup table
        /// </summary>
        /// <param name="asset">Asset name</param>
        public static Texture2D GetTexture(string asset)
        {
            return _textures[asset].Item2;
        }

        public static Rectangle GetBounds(string asset)
        {
            Texture2D tx = GetTexture(asset);
            return new Rectangle(0, 0, tx.Width, tx.Height);
        }
    }
}
