namespace WPF.Config;

internal static class Configuration
{
	public static readonly string DatabaseConnectionString =
		@"server=(localdb)\MSSQLLocalDB; " +
		"Initial Catalog = AppDb; " +
		"Integrated Security = True;";

	public static readonly byte[] AdminPasswordHash =
		Convert.FromHexString("CA978112CA1BBDCAFAC231B39A23DC4DA786EFF8147C4E72B9807785AFEE48BB");
}
