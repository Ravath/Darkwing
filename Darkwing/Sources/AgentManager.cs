using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    public class AgentManager
    {
        private readonly Game game;
        private readonly List<Agent> agents = [];

        private readonly Timer gliderSpawn = new(200);

        public AgentManager(Game game)
        {
            this.game = game;
            gliderSpawn.OnTimer += (t) => {
                SpawnAtRandom(BotLibrary.GliderMissile);
            };
        }

        public void Init()
        {
            agents.Clear();
            gliderSpawn.Init();
        }

        public void DoAction()
        {
            gliderSpawn.ExecuteAction();
            CheckAgents();
            foreach (var agent in agents)
            {
                agent.DoAction();
            }
        }

        public void Display()
        {
            foreach (var agent in agents)
            {
                agent.Display();
            }
        }

        public void SpawnAtRandom(Agent agent)
        {
            int min = game.background.left[0];
            int max = game.background.right[0];
            AddAgent(agent,
                new Position(game.background.rand.Next(min,max),0));
        }

        public void AddAgent(Agent agent, Position position)
        {
            agent = agent.Duplicate();
            agent.GoTo(position.x, position.y);
            agents.Add(agent);
        }

        public void CheckAgents()
        {
            var toremove = new List<Agent>();
            foreach (Agent agent in agents)
            {
                if(agent.Life <= 0)
                {
                    toremove.Add(agent);
                }
            }
            foreach (Agent agent in toremove)
            {
                agents.Remove(agent);
            }
        }
    }
}