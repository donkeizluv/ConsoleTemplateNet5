namespace ConsoleTemplate_Net5
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// App DI root.
    /// </summary>
    public class App : IDisposable
    {
        private readonly ILogger<App> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="logger">logger implementation.</param>
        public App(ILogger<App> logger)
        {
            this.logger = logger;
        }

        public void Dispose()
        {
            this.logger.LogTrace("app disposed.");
        }

        /// <summary>
        /// Start app main routine.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task Run()
        {
            throw new InvalidOperationException("Program started with invalid states.");
            // this.logger.LogInformation("app started doing stufff");
            // throw new NotImplementedException();
        }
    }
}
