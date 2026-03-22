namespace WPF.Services.Abstractions;

internal interface IFileService
{
	bool ReadFile(string filePath, ref byte[] fileContent);
}
