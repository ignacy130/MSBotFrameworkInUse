using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamDare.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using TeamDare.Bot.Resources;

namespace TeamDare.Bot.Tests
{
    [TestClass()]
    public class ConversationHandlerTests
    {
        
        [TestMethod()]
        public void TestNewChallenge()
        {
            var handler = new ConversationHandler();
            var challengeDone = new Activity()
            {
                Text = "finished done"
            };
            var act = handler.GetResponse(challengeDone);

            var response = handler.GetResponse(act);
            Assert.AreEqual("new challenge", response.Value);
        }

        [TestMethod()]
        public void TestNewUser()
        {
            var handler = new ConversationHandler();
            var newUser = new Activity()
            {
                Text = "hello hi welcome i am i'm",
                From = new ChannelAccount((new Guid()).ToString(),"tester")
            };

            var response = handler.GetResponse(newUser);

            Assert.AreEqual(GeneralResponses.WelcomeText, response.Value);
        }

        [TestMethod()]
        public void TestInProgress()
        {
            var handler = new ConversationHandler();
            var inProgress = new Activity()
            {
                Text = "not yet still doing help"
            };

            var response = handler.GetResponse(inProgress);

            Assert.AreEqual("keep going", response.Value);
        }

    }
}