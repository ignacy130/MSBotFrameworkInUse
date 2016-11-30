using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamDare.Contract;
using TeamDare.Core;

namespace TeamDare.Tests
{

    [TestClass]
    public class TestGameMaster
    {
        private ICreateGame gameFactory = new GameMasterFactory();

        [TestMethod]
        public void TestCreateChallenge()
        {
            var gameMaster = this.gameFactory.CreateGameMaster();
            var nick = "test";
            var player = this.gameFactory.CreatePlayer(nick);
            var challange = gameMaster.CreateChallenge(player);
            Assert.IsNotNull(challange);
            Assert.AreEqual(challange.Hero, player);
        }

        [TestMethod]
        public void TestRewardPlayer()
        {
            var gameMaster = this.gameFactory.CreateGameMaster();
            var nick = "test";
            var player = this.gameFactory.CreatePlayer(nick);
            var challenges = new List<IChallenge>()
            {
                gameMaster.CreateChallenge(player),
                gameMaster.CreateChallenge(player),
                gameMaster.CreateChallenge(player),
            };
            var adventure = gameMaster.CreateAdventure(player, challenges);
            var reward = gameMaster.CreateReward(adventure);
            gameMaster.RewardPlayer(player, adventure, reward);
            Assert.IsTrue(player.Rewards.Any(x => x.Key.Id == adventure.Id));
        }


        [TestMethod]
        public void TestInvitePlayerToChallenge()
        {
            var gameMaster = this.gameFactory.CreateGameMaster();

            var nickFirst = "first";
            var playerFirst = this.gameFactory.CreatePlayer(nickFirst);

            var nick = "test";
            var player = this.gameFactory.CreatePlayer(nick);
            var challenges = new List<IChallenge>()
            {
                gameMaster.CreateChallenge(player),
                gameMaster.CreateChallenge(player),
                gameMaster.CreateChallenge(player),
            };
            var adventure = gameMaster.CreateAdventure(player, challenges);
            var challange = adventure.Challenges[0];
            gameMaster.InvitePlayerToChallenge(player, challange);

            Assert.IsTrue(player.GamesHistory.Any(x => x.Challenges.Any(c=>c.Id == adventure.Id)));
            Assert.IsTrue(challange.Participants.Any(x => x.Id == player.Id));
        }


        [TestMethod]
        public void TestAddPlayer()
        {
            var gameMaster = this.gameFactory.CreateGameMaster();
            var nick = "test";
            var player = this.gameFactory.CreatePlayer(nick);
            gameMaster.AddPlayer(player);
            Assert.IsTrue(gameMaster.Players.Any(x => x.Id == player.Id));
        }

        [TestMethod]
        public void TestCreateReward()
        {
            var gameMaster = this.gameFactory.CreateGameMaster();
            var nick = "test";
            var player = this.gameFactory.CreatePlayer(nick);
            var challenges = new List<IChallenge>()
            {
                gameMaster.CreateChallenge(player),
                gameMaster.CreateChallenge(player),
                gameMaster.CreateChallenge(player),
            };
            var adventure = gameMaster.CreateAdventure(player, challenges);
            var reward = gameMaster.CreateReward(adventure);
            Assert.IsNotNull(reward);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(reward.Title));
            Assert.IsTrue(reward.Value > 0);
        }

        [TestMethod]
        public void TestCreateAdventure()
        {

            var nick = "test";
            var player = this.gameFactory.CreatePlayer(nick);
            var gameMaster = this.gameFactory.CreateGameMaster();
            var challenges = new List<IChallenge>()
            {
                gameMaster.CreateChallenge(player),
                gameMaster.CreateChallenge(player),
                gameMaster.CreateChallenge(player),
            };
            var adventure = gameMaster.CreateAdventure(player, challenges);

            Assert.IsNotNull(adventure);
            Assert.IsNotNull(adventure.Challenges);
            Assert.IsNotNull(adventure.Hero);
            Assert.IsTrue(adventure.Challenges.Count == 3);
            Assert.IsFalse(adventure.IsCompleted);
            Assert.IsTrue(adventure.PercentageCompleted == 0);
        }
    }
}
