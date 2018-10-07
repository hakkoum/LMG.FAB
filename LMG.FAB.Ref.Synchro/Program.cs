using LMG.Fab.Data.Entities.LmgFab;
using LMG.Fab.Data.Entities.Reflmg;
using LMG.FAB.Services;
using LMG.FAB.Services.Business;
using LMG.FAB.Services.FAB;
using LMG.FAB.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace LMG.FAB.Ref.Synchro
{
    class Program
    {
        public static IConfigurationRoot configuration;
        private static ILogger<Program> _logger;

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            loggerFactory.ConfigureNLog("nlog.config");

            _logger = loggerFactory.CreateLogger<Program>();
            var watcher = new Stopwatch();
            watcher.Start();
            _logger.LogInformation("********Debut de la synchro********");

            try
            {
                ILotManager lotManager = serviceProvider.GetService<ILotManager>();
                string dateMinString = configuration.GetValue<string>("dateMin");
                _logger.LogInformation($"Date min : {dateMinString}");
                DateTime dateMin = DateTime.ParseExact(dateMinString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string[] monthsNames = configuration.GetSection("monthsNames").GetChildren().Select(p => p.Value).ToArray();

                var result = lotManager.SyncLotFromRef(dateMin, monthsNames);

                if (!result.processOk)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var error in result.errors)
                    {
                        builder.AppendLine(error);
                    }
                    _logger.LogError($"Erreur lors de l'execution de la synchro : {builder.ToString()}");
                }
                watcher.Stop();
                _logger.LogInformation($"Fin de la synchro : temps écoulé {watcher.Elapsed.TotalMinutes} min");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'execution de la synchro");
            }
            _logger.LogInformation("********Fin de la synchro********");
            Console.ReadLine();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                 .AddJsonFile("config.json", false)
                 .Build();

            services.AddSingleton(configuration);

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Information));

            services.AddLogging();
            services.AddDbContext<ReflmgContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("ReflmgConnectionString")), ServiceLifetime.Transient);
            services.AddDbContext<LMG_FABContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("FabConnectionString")), ServiceLifetime.Transient);

            services.AddTransient<IUserService, SystemUserService>();
            services.AddTransient<ILotManager, LotManager>();
            services.AddTransient<ILockManager, LockManager>();
            services.AddTransient<ISharedDataManager, SharedDataManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IDateFormater, DateFormater>();
        }
    }
}
