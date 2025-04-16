using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer;
using Data_Layer;

namespace Testing_Layer
{
    [TestFixture]
    public class GameContextTest
    {
        static GamesContext gameContext;
        static GameContextTest()
        {
            gameContext = new GamesContext(TestManager.dbContext);
        }

        [Test]
        public void CreateGame()
        {
            Game game = new Game("Test001");
            int gamesBefore = TestManager.dbContext.Games.Count();

            gameContext.Create(game);

            int gamesAfter = TestManager.dbContext.Games.Count();
            Game lastGame = TestManager.dbContext.Games.Last();
            Assert.That(gamesBefore + 1 == gamesAfter && lastGame.Name == game.Name,
                "Names are not equal or genre is not created!");
        }

        [Test]
        public void ReadGame()
        {
            Game newGame = new Game("Test002");
            gameContext.Create(newGame);

            Game genre = gameContext.Read(newGame.Id);

            Assert.That(genre.Name == "Game 2", "Read() does not get Game by id!");
        }

        [Test]
        public void ReadAllGames()
        {
            int gameBefore = TestManager.dbContext.Games.Count();

            int gameAfter = ((List<Game>)gameContext.ReadAll()).Count;

            Assert.That(gameBefore == gameAfter, "ReadAll() does not return all of the Game!");
        }

        [Test]
        public void UpdateGame()
        {
            Game newGame = new Game("Test001");
            gameContext.Create(newGame);

            Game lastGame = gameContext.ReadAll().Last();
            lastGame.Name = "Updated Game";

            gameContext.Update(lastGame, false);

            Assert.That(gameContext.Read(lastGame.Id).Name == "Updated Game",
            "Update() does not change the Game's name!");
        }

        [Test]
        public void DeleteGame()
        {
            Game newGame = new Game("Test001");
            gameContext.Create(newGame);

            List<Game> games = (List<Game>)gameContext.ReadAll();
            int gameBefore = games.Count;
            Game game = games.Last();

            gameContext.Delete(game.Id);

            int gameAfter = ((List<Game>)gameContext.ReadAll()).Count;
            Assert.That(gameBefore == gameAfter + 1, "Delete() does not delete a genre!");
        }

        [Test]
        public void DeleteGame3()
        {
            Game newGame = new Game("Test003");
            gameContext.Create(newGame);

            Game game = gameContext.ReadAll().Last();
            int gameId = game.Id;

            gameContext.Delete(gameId);

            KeyNotFoundException ex = Assert.Throws<KeyNotFoundException>(() => gameContext.Read(gameId));
            Assert.That(ex.Message, Is.EqualTo("The given key was not present in the dictionary."));
        }

    }

}
