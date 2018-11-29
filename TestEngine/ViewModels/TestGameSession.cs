using System;
using Engine.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestGameSession
    {
        [TestMethod]
        public void TestCreateGameSession()
        {
            GameSession gameSession = new GameSession();

            Assert.IsNotNull(gameSession.CurrentPlayer);
            Assert.AreEqual("Your Cell", gameSession.CurrentLocation.Name);
        }

        [TestMethod]
        public void TestPlayerIsMovedHomeAndRestoredOnDeath()
        {
            GameSession gameSession = new GameSession();

            gameSession.CurrentPlayer.TakeDamage(998);

            Assert.AreEqual("Your Cell", gameSession.CurrentLocation.Name);
            Assert.AreEqual(gameSession.CurrentPlayer.Level * 20, 
                gameSession.CurrentPlayer.CurrentHitPoints);
            Assert.AreEqual(gameSession.CurrentPlayer.Level * 9,
                gameSession.CurrentPlayer.MaxMana);
        }
    }
}
