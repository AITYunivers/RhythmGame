using Raylib_cs;

namespace RhythmGame.Logic
{
    public static class Helper
    {
        public static Color Add(this Color color1, Color color2)
        {
            float a = color2.A;
            byte rBy = (byte)Math.Min(255, color1.R + a);
            byte gBy = (byte)Math.Min(255, color1.G + a);
            byte bBy = (byte)Math.Min(255, color1.B + a);
            return new Color(rBy, gBy, bBy, color1.A);
        }
    }
}
