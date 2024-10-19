using DotNetEnv;


namespace VocabAPP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // load environment variables from .env file
            Env.Load();
            
            var app = new Application(new VocabularyLoader());
            app.RunApp();
            
        }

        public static void Exit(string exitMessage, int exitCode)
        {
            Console.WriteLine(exitMessage);
            Environment.Exit(exitCode);
        }
    }
    
}