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
			.Include(r => r.User)
			.ToList()
		);
	}

	public static async Task<bool> ContainsRequest(string username)
	{
		using var context = new AppDbContext();

		return await context.BecomeOrganizerRequests
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
				PasswordHash = bor.User.PasswordHash
			};
			await context.EventOrganizers.AddAsync(eo);
			context.BecomeOrganizerRequests.Remove(bor);

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
