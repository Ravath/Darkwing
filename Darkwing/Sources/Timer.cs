
namespace DarkWing
{
    public class Timer(int delay = 100)
    {
        public delegate void OnTimerHandler(Timer timer);

        public event OnTimerHandler OnTimer;

        private DateTime last = DateTime.Now;
        public int Delay = delay;// In ms.


        public void Init(){
            last = DateTime.Now;
        }

        public void ExecuteAction()
        {
            TimeSpan t = DateTime.Now - last;
            if (t.Milliseconds < Delay) {
                OnTimer?.Invoke(this);
                last = DateTime.Now;
            }
        }
    }
}