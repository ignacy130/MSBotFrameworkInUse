using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector;
using TeamDare.Bot.Resources;
using TeamDare.Core;
using TeamDare.Core.Helpers;
using TeamDare.Core.Resources;
using TeamDare.DAL;

namespace TeamDare.Bot
{
    public class ConversationHandler
    {

        public Activity GetResponse(Activity message)
        {
            try
            {

            var registered = !this.IsRegistered(message.From);
            if (registered)
            {
                return WelcomeNewUser(message);
            }
            else
            {
                return ReactToProgress(message);
            }

            }
            catch (Exception e)
            {
                return message.CreateReply("Failed on GetResponse due to " + e.Message);
            }
        }

        private Activity GiveNewChallenge(Activity message)
        {
            var content = Adventures.NewChallengeForYou;
            Challenge challengeToGive = null;
            Activity reply = null;

            using (var context = new GameContext())
            {
                var defaultGameMaster = context.GameMasters.First();
                var player = context.Players.Single(p => p.AppId == message.From.Id);
                var adventure = player.GamesHistory.OrderBy(a => a.Order)
                                            .FirstOrDefault(a => a.IsCompleted == false);


                if (null == adventure)
                {
                    return FinishGame(message);
                }

                challengeToGive = adventure.Challenges.OrderBy(c => c.Order)
                                            .FirstOrDefault(c => c.IsCompleted == false);

                if (null == challengeToGive)
                {
                    return FinishGame(message);
                }


                challengeToGive.IsStarted = true;
                context.SaveChanges();
            }
            if (challengeToGive != null)
            {
                content += " \"" + challengeToGive.Title + "\"!";
                reply = message.CreateReply(content);
                return reply;
            }
            else
            {
                content = "Something went wrong... or you finished all challenges!";
                reply = message.CreateReply(content);
                return reply;
            }
        }

        private Activity FinishGame(Activity message)
        {
            return message.CreateReply("Congratulations! You have finished demo version!");
        }

        private Activity ReactToProgress(Activity message)
        {
            var content = "not implemented";
            Challenge challengeInProgress = null;
            Activity reply = null;

            using (var context = new GameContext())
            {
                try
                {
                    var defaultGameMaster = context.GameMasters.First();
                    var player = context.Players.Single(p => p.AppId == message.From.Id);

                    challengeInProgress = player.GamesHistory.SelectMany(a => a.Challenges)
                                            .SingleOrDefault(a => a.IsStarted && !a.IsCompleted);
                }
                catch (Exception e)
                {
                    var x = e;
                }
            }

            if (challengeInProgress != null && CheckIfReady(message.Text))
            {
                using (var context = new GameContext())
                {
                    try
                    {

                        var defaultGameMaster = context.GameMasters.First();
                        var players = context.Players.Single(p => p.AppId == message.From.Id);

                        var player = context.Players.Single(p => p.AppId == message.From.Id);
                        challengeInProgress = player.GamesHistory.SelectMany(a => a.Challenges)
                                            .SingleOrDefault(a => a.IsStarted && !a.IsCompleted);
                        challengeInProgress.IsCompleted = true;

                        if (challengeInProgress.Order == 2)
                        {
                            //pogratuluj
                        }


                        context.SaveChanges();

                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                }
                return ReactToChallengeFinished(message, challengeInProgress.Adventure.FinishedText, challengeInProgress.Adventure.FinishedImageUrl, challengeInProgress.Order);
            }
            else
            if (challengeInProgress != null && !CheckIfReady(message.Text))
            {
                content = string.Format(Adventures.HowIsChallengeGoing, challengeInProgress.Title);
                reply = message.CreateReply(content);
                return reply;
            }
            else
            {
                return GiveNewChallenge(message);
            }
        }

        private bool CheckIfReady(string message)
        {
            return message.ContainsAny("ok", "done", "finished") && !message.ContainsAny("not ok", "not done", "not finished");
        }

        private Activity ReactToChallengeFinished(Activity message, string successText, string successImageUrl, int order)
        {
            var newChallengeReply = GiveNewChallenge(message);

            var reply = message.CreateReply(String.Format("You have finished {0}/3 steps of this adventure. ",order+1));

            if (order == 2)
            {
                var content = successText + " ";
                if (!string.IsNullOrEmpty(successImageUrl))
                {
                    var a = new Attachment("image/png", successImageUrl, null, null, null);
                    reply.Attachments.Add(a);
                }
                reply.Text = content;
            }

            reply.Text += newChallengeReply.Text;

            return reply;
        }


        private Activity WelcomeNewUser(Activity message)
        {
            var reply = message.CreateReply(GeneralResponses.WelcomeText);

            using (var context = new GameContext())
            {
                var defaultGameMaster = context.GameMasters.First();
                var factory = new GameMasterFactory();

                var player = (Player)(factory.CreatePlayer(message.From.Name));
                player = (Player)factory.InitializeGameForPlayer(player, defaultGameMaster);
                player.AppId = message.From.Id;
                player.AppNick = message.From.Id;
                player.GameMaster = defaultGameMaster;

                foreach (var adv in player.GamesHistory)
                {
                    context.Adventures.Add((Adventure)adv);
                    foreach (var challenge in adv.Challenges)
                    {
                        context.Challenges.Add((Challenge)challenge);
                    }
                }
                context.Players.Add(player);
                context.SaveChanges();
            }

            return reply;
        }

        private bool IsRegistered(ChannelAccount @from)
        {
            try
            {
                using (var context = new GameContext())
                {
                    return context.Players.Any(x => x.AppId == from.Id);
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}