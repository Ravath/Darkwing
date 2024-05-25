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
            // Check for agents to remove :
            var toremove = new List<Agent>();
            foreach (Agent agent in agents)
            {
                // - no life
                if(agent.Life <= 0)
                {
                    toremove.Add(agent);
                }
            }
            // The removal
            RemoveAgents(toremove);

            // Check for collisions between agents
            for(int i=0; i<agents.Count; i++)
            {
                // specific check for the player
                if(agents[i].Collision(game.player))
                {
                    agents[i].Life --;
                    game.player.Life --;
                }
                // between agents
                for(int j=i+1; j<agents.Count; j++)
                {
                    if(agents[i].Collision(agents[j]))
                    {
                        agents[i].Collided(agents[j]);
                        agents[j].Collided(agents[i]);
                    }
                }
                // remove agents without life
                if(agents[i].Life <= 0)
                {
                    toremove.Add(agents[i]);
                    continue;
                }
            }
            // The removal
            RemoveAgents(toremove);
        }

        private void RemoveAgents(List<Agent> toremove)
        {
            foreach (Agent agent in toremove)
            {
                agents.Remove(agent);
            }
            toremove.Clear();
        }

        public Agent? Collision(Agent ag)
        {
            // Check for collisions with agents
            if(ag != game.player && ag.Collision(game.player))
            {
                return game.player;
            }
            foreach (Agent agent in agents)
            {
                if(ag != agent && ag.Collision(agent))
                {
                    return agent;
                }
            }
            return null;
        }
    }
}