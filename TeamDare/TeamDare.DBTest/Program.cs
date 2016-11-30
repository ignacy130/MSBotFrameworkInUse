using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamDare.Core;
using TeamDare.DAL;

namespace TeamDare.DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new GameContext())
            {
                var factory = new GameMasterFactory(); 
                Console.WriteLine("save gm");
                
                var gameMasters = from b in db.GameMasters
                            orderby b.Id
                            select b;
                Console.WriteLine("query gm");
                Console.WriteLine("All game masters in the database:");
                foreach (var item in gameMasters)
                {
                    Console.WriteLine(item.Id);
                    foreach (var player in item.Players)
                    {
                        Console.WriteLine(player.Id);
                        Console.WriteLine(player.Nick);
                        foreach (var adv in player.GamesHistory)
                        {
                            Console.WriteLine(adv.Id);
                            Console.WriteLine(adv.Title);
                            Console.WriteLine(adv.Order);
                            foreach (var ch in adv.Challenges)
                            {
                                Console.WriteLine(String.Format("\t{0}",ch.Id));
                                Console.WriteLine(String.Format("\t{0}", ch.Title));
                                Console.WriteLine(String.Format("\t{0}", ch.Order));
                            }
                        }
                        
                    }
                    //Console.WriteLine(String.Join(", ",(item.Players.Select(x=>((Reward)x.Rewards).Id).ToList())));
                }




                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
