using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using TeamDare.Bot.Resources;
using TeamDare.Contract;
using TeamDare.Core.Resources;

namespace TeamDare.Core
{
    public class GameMasterFactory : EntityBase, ICreateGame
    {
        public IGameMaster CreateGameMaster()
        {
            var gameMaster = new GameMaster();
            return gameMaster;
        }

        public IPlayer CreatePlayer(string nick)
        {
            var player = new Player(nick);
            return player;
        }

        public IPlayer InitializeGameForPlayer(IPlayer player, IGameMaster gameMaster)
        {
            //places adv
            var place1 = gameMaster.CreateChallenge(player);
            place1.Title = Adventures.IntroduceYourselfChallengeTitle + Environment.NewLine + Adventures.IntroduceYourselfChallengeDescription;
            place1.Order = 0;

            var place2 = gameMaster.CreateChallenge(player);
            place2.Title = Adventures.LunchChallengeTitle + Environment.NewLine + Adventures.LunchChallengeDescription;
            place2.Order = 1;

            var place3 = gameMaster.CreateChallenge(player);
            place3.Title = Adventures.SelfieChallangeTitle + Environment.NewLine + Adventures.SelfieChallangeDescription;
            place3.Order = 2;

            var placesChallenges = new List<IChallenge>() { place1, place2, place3 };
            var placesAdventure = gameMaster.CreateAdventure(player, placesChallenges);
            placesAdventure.Title = Adventures.PlacesAdventureTitle;
            placesAdventure.FinishedText = Adventures.PlacesAdventureFinished;
            placesAdventure.FinishedImageUrl = Images.PlacesBadge;
            placesAdventure.Order = 0;

            //food adv
            var foodCh1 = gameMaster.CreateChallenge(player);
            foodCh1.Title = Adventures.CoffieWithChallengeTitle + Environment.NewLine + Adventures.CoffieWithChallengeDescription;
            foodCh1.Order = 0;

            var foodCh2 = gameMaster.CreateChallenge(player);
            foodCh2.Title = Adventures.ShopChallengeTitle + Environment.NewLine + Adventures.ShopChallengeDescription;
            foodCh2.Order = 1;

            var foodCh3 = gameMaster.CreateChallenge(player);
            foodCh3.Title = Adventures.BeerChallengeTitle + Environment.NewLine + Adventures.BeerChallengeDescription;
            foodCh3.Order = 2;

            var adv1l = new List<IChallenge>() { foodCh1, foodCh2, foodCh3 };
            var foodAdventure = gameMaster.CreateAdventure(player, adv1l);
            foodAdventure.Title = Adventures.FoodAdventureTitle;
            foodAdventure.FinishedText = Adventures.FoodAdventureFinished;
            foodAdventure.FinishedImageUrl = Images.FoodBadge;

            foodAdventure.Order = 1;

            //people adv
            var people1 = gameMaster.CreateChallenge(player);
            people1.Title = Adventures.IntroduceYourselfChallengeTitle + Environment.NewLine + Adventures.IntroduceYourselfChallengeDescription;
            people1.Order = 0;

            var people2 = gameMaster.CreateChallenge(player);
            people2.Title = Adventures.LunchChallengeTitle + Environment.NewLine + Adventures.LunchChallengeDescription;
            people2.Order = 1;

            var people3 = gameMaster.CreateChallenge(player);
            people3.Title = Adventures.SelfieChallangeTitle + Environment.NewLine + Adventures.SelfieChallangeDescription;
            people3.Order = 2;

            var peopleChallenges = new List<IChallenge>() { people1, people2, people3 };
            var peopleAdventure = gameMaster.CreateAdventure(player, peopleChallenges);
            peopleAdventure.Title = Adventures.PeopleAdventureTitle;
            peopleAdventure.FinishedText = Adventures.PeopleAdventureFinished;
            peopleAdventure.FinishedImageUrl = Images.PeopleBadge;

            peopleAdventure.Order = 2;

            player.GamesHistory = new List<IAdventure>() { peopleAdventure, placesAdventure, foodAdventure };
            ((Player)player).GameMaster = (GameMaster)gameMaster;
            return player;
        }
    }
}
