using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamDare.Core;

namespace TeamDare.Tests
{
    [TestClass]
    public class TestCreateGame
    {
        [TestMethod]
        public void TestGameCreation()
        {
            var factory = new GameMasterFactory();
            var gameMaster = factory.CreateGameMaster();
            Assert.IsNotNull(gameMaster);
            Assert.IsNotNull(gameMaster.Adventures);
            Assert.IsNotNull(gameMaster.Players);
        }

        [TestMethod]
        public void TestPlayerCreation()
        {
            var factory = new GameMasterFactory();
            var nick = "test";
            var player = factory.CreatePlayer(nick);
            Assert.IsNotNull(player);
            Assert.AreEqual(player.Nick, nick);
            Assert.IsTrue(player.Level == 0);
        }

        [TestMethod]
        public void TestInitializeGameForPlayer()
        {
            var factory = new GameMasterFactory();
            var nick = "test";
            var player = factory.CreatePlayer(nick);
            var gameMaster = factory.CreateGameMaster();

            player = factory.InitializeGameForPlayer(player, gameMaster);
            Assert.IsNotNull(player);
            Assert.IsNotNull(player.GamesHistory);
            Assert.IsTrue(player.GamesHistory.Count == 3);
            Assert.IsTrue(player.GamesHistory.All(x => x.Id != Guid.Empty));
            Assert.IsTrue(player.GamesHistory.Count(x => x.Order == 0) == 1);
            Assert.IsTrue(player.GamesHistory.Count(x => x.Order == 1) == 1);
            Assert.IsTrue(player.GamesHistory.Count(x => x.Order == 2) == 1);
            Assert.IsTrue(player.GamesHistory.All(x => x.Title != string.Empty));
            Assert.IsTrue(player.GamesHistory.All(x => x.Hero == player));

            Assert.IsTrue(player.GamesHistory.Select(g => g.Challenges).All(c => c.Count == 3));
            Assert.IsTrue(player.GamesHistory.SelectMany(g => g.Challenges).Count(x => x.Order == 0) == 3);
            Assert.IsTrue(player.GamesHistory.SelectMany(g => g.Challenges).Count(x => x.Order == 1) == 3);
            Assert.IsTrue(player.GamesHistory.SelectMany(g => g.Challenges).Count(x => x.Order == 2) == 3);
            Assert.IsTrue(player.GamesHistory.SelectMany(g => g.Challenges).All(x => x.Id != Guid.Empty));

            Assert.IsTrue(player.GamesHistory.SelectMany(g => g.Challenges).All(x => x.Hero == player));
            Assert.IsTrue(player.GamesHistory.SelectMany(g => g.Challenges).All(x => x.Title != string.Empty));
        }
    }
}
