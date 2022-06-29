using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace ZombiesAttack
{
    internal class Game
    {
        private static int round = 0;
        private static int enemies = 5;
        private List<Ped> attackers = new List<Ped>();

        public Game()
        {
            while (GTA.Game.Player.IsAlive)
            {
                OnTick();
                Script.Wait(1);
            }
        }

        public void OnTick()
        {
            if (attackers.Count == 0)
            {
                round += 1;
                enemies = (int)Math.Ceiling(enemies * 1.2);
                SpawnEnemies(enemies, (int)(enemies/5));
            };
            for (int i = attackers.Count - 1; i >= 0; i--)
            {
                if (attackers[i].IsDead) attackers.RemoveAt(i);
            }
            GTA.UI.Screen.ShowSubtitle($"~r~Enemies left:{attackers.Count} ~s~|~b~ Round: {round}", 1);
        }

        public void SpawnEnemies(int total, int step)
        {
            for (int i = 0; i < total; i += step)
            {
                for (int j = 0; j < step; j++)
                {
                    Ped Attacker = World.CreatePed(PedHash.Zombie01, GTA.Game.Player.Character.Position.Around(25));
                    attackers.Add(Attacker);
                    Attacker.Task.FightAgainst(GTA.Game.Player.Character);
                }
                GTA.UI.Screen.ShowSubtitle($"~b~Enemies Spawning... ~r~Enemies left:{attackers.Count} ~s~|~b~ Round: {round}", 2000);
                Script.Wait(2000);
            };
        }
    }
}