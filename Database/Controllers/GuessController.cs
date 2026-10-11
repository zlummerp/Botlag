using Botlag.Constants.BossInfo;
using Botlag.Database.Models;
using Botlag.Models;

namespace Botlag.Database.Controllers
{
    public class GuessController
    {
        #region Public Repository Methods

        public static void SaveNewDropCall(Guess guess)
        {
            using var db = new GuessingContext();

            try
            {
                db.Guesses.Add(guess);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Add exception handling
                throw;
            }
        }

        public static bool UpdateDropCall(Guess guess, string newItemName)
        {
            using var db = new GuessingContext();

            try
            {
                var dbGuess = db.Guesses.FirstOrDefault(g => g.Guesser == guess.Guesser && guess.ChannelId == g.ChannelId && guess.Boss == g.Boss);
                if (dbGuess == null)
                {
                    return false;
                }
                dbGuess.ItemName = newItemName;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                // TODO: Add exception handling
                throw;
            }
        }

        public static Guess? GetGuess(string username, string bossName, ulong channelId)
        {
            using var db = new GuessingContext();

            try
            {
                var guess = db.Guesses.FirstOrDefault(guess => guess.ChannelId == channelId && guess.Guesser == username && guess.Boss == bossName);

                return guess;
            }
            catch (Exception e)
            {
                // TODO: Add exception handling
                throw;
            }
        }

        public static List<string> FindCorrectGuessesForBoss(Item item, ulong channelId)
        {
            using var db = new GuessingContext();

            try
            {
                var correctGuesses = db.Guesses.Where(guess => guess.ChannelId == channelId && guess.ItemName == item.Name && guess.Boss == item.Boss);
                var guessers = correctGuesses.Select(guess => guess.Guesser).ToList();
                WipeGuessesForBoss(item.Boss, channelId, db);

                return guessers;
            }
            catch (Exception e)
            {
                // TODO: Add exception handling
                throw;
            }
        }

        public static List<string> FindCorrectGuessesForGlobal(Item item, ulong channelId)
        {
            using var db = new GuessingContext();

            try
            {
                var correctGuesses = db.Guesses.Where(guess => guess.ChannelId == channelId && guess.ItemName == item.Name && guess.Boss == BossNames.Global);
                var guessers = correctGuesses.Select(guess => guess.Guesser).ToList();
                WipeGuessesForBoss(BossNames.Global, channelId, db);

                return guessers;
            }
            catch (Exception e)
            {
                // TODO: Add exception handling
                throw;
            }
        }

        #endregion

        #region Private Helper Methods

        private static void WipeGuessesForBoss(string bossName, ulong channelId, GuessingContext db)
        {
            db.Guesses.RemoveRange(db.Guesses.Where(guess => guess.ChannelId == channelId && guess.Boss == bossName));
            db.SaveChanges();
        }

        #endregion
    }
}
