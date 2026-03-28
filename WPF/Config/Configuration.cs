namespace WPF.Config
{
	internal static class Configuration
	{
		public const string databaseConnectionString =
			@"server=(localdb)\MSSQLLocalDB; " +
			"Initial Catalog = AppDb; " +
			"Integrated Security = True;";
	}
}
