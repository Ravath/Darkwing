using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    public class AgentManager(Game game)
    {
        private Game game = game;
        private List<Agent> agents = [];

        public void Init()
        {
            agents.Clear();
        }

        public void DoAction()
        {
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