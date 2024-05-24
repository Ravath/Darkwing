using System.Dynamic;

namespace DarkWing
{
    public static class BotLibrary
    {
        public static readonly Agent PlayerMissile = new(new Sprite(), new GoStraight(-2), 0, 0);
        public static readonly Agent BasicMissile = new(new Sprite(), new GoStraight(2), 0, 0);
        public static readonly Agent GliderMissile = new(new Sprite(), new GoStraight(1), 0, 0);

        static BotLibrary()
        {
            PlayerMissile.sprite.SetChar(new Position(0, 0), '|');
            BasicMissile.sprite.SetChar(new Position(0, 0), '|');
            GliderMissile.sprite.SetChar(new Position(0, 0), '<');
            GliderMissile.sprite.SetChar(new Position(1, 0), '>');
        }
    }
}