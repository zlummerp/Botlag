namespace Botlag
{
    internal class Program
    {
        static async Task Main()
        {
            // using var db = new GuessingContext();
            await new Botlag().InitializeAsync();
        }
    }
}