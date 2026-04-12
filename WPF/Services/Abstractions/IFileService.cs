namespace WPF.Services.Abstractions;

public interface IFileService
{
	bool ReadFile(string filePath, ref byte[] fileContent);
}
