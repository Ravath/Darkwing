using DarkWing;

namespace Darkwing
{
    public class Animation
    {
        public int X  { get; set; }
        public int Y { get; set; }
        public int Delay { get; set; } = 40;

        public bool Running { get; private set; }  = false;

        public readonly List<Sprite> Frames = [];

        private DateTime last = DateTime.Now;
        private int current_frame = 0;

        public void Start()
        {
            last = DateTime.Now;
            current_frame = 0;
            Running = true;
        }

        public void Display()
        {
            TimeSpan t = DateTime.Now - last;
            if (t.Seconds *1000 + t.Milliseconds > Delay) {
                current_frame++;
                last = DateTime.Now;
                if(current_frame >= Frames.Count)
                {
                    Running = false;
                }
            }
            if(Running)
            {
                Frames[current_frame].Display(X,Y);
            }
        }

        public Animation Duplicate(int x, int y)
        {
            Animation ret = new()
            {
                X = x,
                Y = y,
                Delay = Delay
            };
            foreach(Sprite s in Frames)
            {
                ret.Frames.Add(s);
            }
            return ret;
        }

        private void AddFrame(Position position, char[][] frame)
        {
            Sprite s = new();
            s.SetCharArray(position, frame);
            Frames.Add(s);
        }

        public static readonly Animation Explosion = new();
        public static readonly Animation PlayerHit = new();

        static Animation()
        {
            Explosion.Delay = 50;
            Explosion.AddFrame(new Position(0, 0), [['O']]);
            Explosion.AddFrame(new Position(-1, -1), [
                ['\\', ' ', '/'],
                [' ', 'o', ' '],
                ['/', ' ', '\\'],
            ]);
            Explosion.AddFrame(new Position(-1, -1), [
                [':', ':', ':'],
                [',', 'O', ','],
                [':', ':', ':'],
            ]);
            PlayerHit.Delay = 100;
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', ' ', ' '],
                [' ', ' ', ' '],
                [' ', ' ', ' '],
            ]);
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', '^', ' '],
                ['<', 'X', '>'],
                [' ', '^', ' '],
            ]);
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', ' ', ' '],
                [' ', ' ', ' '],
                [' ', ' ', ' '],
            ]);
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', '^', ' '],
                ['<', 'X', '>'],
                [' ', '^', ' '],
            ]);
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', ' ', ' '],
                [' ', ' ', ' '],
                [' ', ' ', ' '],
            ]);
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', '^', ' '],
                ['<', 'X', '>'],
                [' ', '^', ' '],
            ]);
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', ' ', ' '],
                [' ', ' ', ' '],
                [' ', ' ', ' '],
            ]);
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', '^', ' '],
                ['<', 'X', '>'],
                [' ', '^', ' '],
            ]);
            PlayerHit.AddFrame(new Position(-1, -1), [
                [' ', ' ', ' '],
                [' ', ' ', ' '],
                [' ', ' ', ' '],
            ]);
        }
    }
}