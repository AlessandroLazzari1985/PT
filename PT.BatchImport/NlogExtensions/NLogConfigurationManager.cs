using System.Text;
using NLog.Config;
using NLog.Targets;

namespace PT.BatchImport.NlogExtensions
{
	public static class NLogConfigurationManager
	{
		public static LoggingConfiguration BuildDatabaseConfiguration(string connectionString)
		{
			var config = new LoggingConfiguration();
			var targetName = "database";

			var databaseTarget = new DatabaseTarget(targetName)
			{
				DBProvider = "sqlserver",
				ConnectionString = connectionString,
				CommandText = @"
insert into dbo.Log (MachineName, Logged, Level, Message, Logger, Callsite, Exception) 
values (@MachineName, @Logged, @Level, @Message, @Logger, '', @Exception);",
				Parameters =
				{
					new DatabaseParameterInfo("@MachineName", "${machinename}"),
					new DatabaseParameterInfo("@Logged", "${date}"),
					new DatabaseParameterInfo("@Level", "${level}"),
					new DatabaseParameterInfo("@Message", "${message}"),
					new DatabaseParameterInfo("@Logger", "${Logger}"),
					new DatabaseParameterInfo("@Exception", "${exception:tostring}"),
				}
			};

			config.AddTarget(databaseTarget);
			config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, targetName);

			return config;
		}

		public static LoggingConfiguration BuildFileConfiguration()
		{
			var config = new LoggingConfiguration();
			var targetName = "logFile";

			var logFileTarget = new FileTarget(targetName)
			{
				DeleteOldFileOnStartup = true,
				FileName = "fam_log_${shortdate}.log",
				Layout = "${longdate} ${logger} ${message}${exception:format=ToString}",
				KeepFileOpen = true,
				Encoding = Encoding.UTF8,
			};

			config.AddTarget(logFileTarget);
			config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, targetName);

			return config;
		}
	}
}