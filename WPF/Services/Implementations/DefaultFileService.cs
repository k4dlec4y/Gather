using System.IO;
using WPF.Services.Abstractions;

namespace WPF.Services.Implementations;

internal class DefaultFileService : IFileService
{
	public bool ReadFile(string filePath, ref byte[] fileContent)
	{
		try
		{
			fileContent = File.ReadAllBytes(filePath);
			return true;
		}
		catch
		{
			return false;
		}
	}
}
