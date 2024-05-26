using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Darkwing;

namespace DarkWing
{
    public class Player : Agent
    {
        const int MAX_LIFE = 3;

        private bool invincible_mode = false;

        private readonly Game game;
        public Player(Game game) : base(new Sprite())
        {
            this.game = game;
            Life = 3;
            sprite.SetChar(new Position(0, 0), 'X');
            sprite.SetChar(new Position(-1, 0), '<');
            sprite.SetChar(new Position(1, 0), '>');
            sprite.SetChar(new Position(0, -1), '^');
            sprite.SetChar(new Position(0, 1), '^');
        }

        public void Init()
        {
            Life = MAX_LIFE;
            invincible_mode = false;
        }

        public override void DoAction()
        {
            if(game.inputmap.RisedAction("up"))
            {
                if (CanMove(0, -1))
                    Shift(0, -1);
            }
            if(game.inputmap.RisedAction("down"))
            {
                if (CanMove(0, 1))
                    Shift(0, 1);
            }
            if(game.inputmap.RisedAction("right"))
            {
                if (CanMove(1, 0))
                    Shift(1, 0);
            }
            if(game.inputmap.RisedAction("left"))
            {
                if (CanMove(-1, 0))
                    Shift(-1, 0);
            }
            if(game.inputmap.RisedAction("shoot"))
            {
                game.agents.AddAgent(BotLibrary.PlayerMissile,
                    new Position(X,Y - 2));
                Game.Instance.AddScore(-2);
            }
        }

        public override void Display()
        {
            if(invincible_mode)
            {
                Animation.PlayerHit.X = X;
                Animation.PlayerHit.Y = Y;
                Animation.PlayerHit.Display();
                if(!Animation.PlayerHit.Running)
                {
                    invincible_mode = false;
                }
            }else{
                base.Display();
            }
        }

        public override void Collided(Agent agent)
        {
            if(!invincible_mode)
            {
                base.Collided(agent);
                invincible_mode = true;
                Animation.PlayerHit.Start();
            }
        }
    }
}
