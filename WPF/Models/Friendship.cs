namespace WPF.Models;

public class Friendship
{
	public int Friend1Id { get; set; }
	public int Friend2Id { get; set; }

	public required User Friend1 { get; set; }
	public required User Friend2 { get; set; }
}
