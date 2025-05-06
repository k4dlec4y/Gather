using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WPF.Data;
using WPF.Models;

namespace WPF.Managers;

public static class BecomeOrganizerRequestManager
{
	public static ObservableCollection<BecomeOrganizerRequest> GetRequests()
	{
		using var context = new AppDbContext();

		return new ObservableCollection<BecomeOrganizerRequest>(
			context.BecomeOrganizerRequests
			.AsNoTracking()
			.Include(r => r.User)
			.ToList()
		);
	}

	public static async Task<bool> ContainsRequest(string username)
	{
		using var context = new AppDbContext();

		return await context.BecomeOrganizerRequests
			.AsNoTracking()
			.Include(r => r.User)
			.AnyAsync(r => r.User.Username == username);
	}

	public static async Task<bool> AddRequest(BecomeOrganizerRequest bor)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			await context.BecomeOrganizerRequests.AddAsync(bor);
			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.InnerException?.Message);
			await transaction.RollbackAsync();
			return false;
		}
	}

	public static async Task<bool> RemoveRequest(BecomeOrganizerRequest bor)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			context.BecomeOrganizerRequests.Remove(bor);
			await context.Messages
				.AddAsync(new Message
				{
					FromId = 1,
					ToId = bor.User.Id,
					Content = "You have been rejected as an event organizer.",
				});
			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			Debug.WriteLine(ex.InnerException?.Message);
			await transaction.RollbackAsync();
			return false;
		}
	}

	public static async Task<bool> AcceptRequest(BecomeOrganizerRequest bor)
	{
		using var context = new AppDbContext();
		using var transaction = await context.Database.BeginTransactionAsync();
		try
		{
			var eo = new EventOrganizer
			{
				Username = bor.User.Username,
				PasswordHash = bor.User.PasswordHash,
				Info = bor.RequestText
			};
			await context.EventOrganizers.AddAsync(eo);
			context.BecomeOrganizerRequests.Remove(bor);
			await context.Messages
				.AddAsync(new Message
				{
					FromId = 1,
					ToId = bor.User.Id,
					Content = "You have been accepted as an event organizer.",
				});

			await context.SaveChangesAsync();
			await transaction.CommitAsync();
			return true;
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
			Debug.WriteLine(ex.InnerException?.Message);
			await transaction.RollbackAsync();
			return false;
		}
	}
}
