using Microsoft.EntityFrameworkCore;
using WPF.Models;

namespace WPF.Data;

public class AppDbContext : DbContext
{
	private string connectionString = @"server=(localdb)\MSSQLLocalDB; Initial Catalog = AppDb; Integrated Security = True;";

	public DbSet<User> Users { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<EventOrganizer> EventOrganizers { get; set; }
	public DbSet<Message> Messages { get; set; }
	public DbSet<FriendRequest> FriendRequests { get; set; }
	public DbSet<Friendship> Friendships { get; set; }
	public DbSet<Invite> Invites { get; set; }
	public DbSet<BecomeOrganizerRequest> BecomeOrganizerRequests { get; set; }

	public AppDbContext() : base()
	{
		//Database.EnsureDeleted();
		//Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(connectionString);
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// User
		modelBuilder.Entity<User>()
			.HasKey(u => u.Id);

		modelBuilder.Entity<User>()
			.Property(u => u.Username)
			.HasMaxLength(30)
			.IsRequired();

		modelBuilder.Entity<User>()
			.Property(u => u.PasswordHash)
			.IsRequired();

		modelBuilder.Entity<User>()
			.HasIndex(u => u.Username)
			.IsUnique();

		// EventOrganizer
		modelBuilder.Entity<EventOrganizer>()
			.HasKey(eo => eo.Id);

		modelBuilder.Entity<EventOrganizer>()
			.Property(eo => eo.Username)
			.HasMaxLength(30)
			.IsRequired();

		modelBuilder.Entity<EventOrganizer>()
			.Property(eo => eo.PasswordHash)
			.IsRequired();

		modelBuilder.Entity<EventOrganizer>()
			.Property(eo => eo.Info)
			.HasMaxLength(200)
			.IsRequired();

		modelBuilder.Entity<EventOrganizer>()
			.HasIndex(eo => eo.Username)
			.IsUnique();

		// Event
		modelBuilder.Entity<Event>()
			.HasKey(e => e.Id);

		modelBuilder.Entity<Event>()
			.HasOne(e => e.Organizer)
			.WithMany(eo => eo.OrganizedEvents)
			.HasForeignKey(e => e.OrganizerId);

		// Event Participants
		modelBuilder.Entity<Event>()
		.HasMany(e => e.Participants)
		.WithMany(u => u.EventsToAttend)
		.UsingEntity<Dictionary<string, object>>(
			"EventAttendance",
			j => j.HasOne<User>()
				  .WithMany()
				  .HasForeignKey("UserId")
				  .OnDelete(DeleteBehavior.Cascade),
			j => j.HasOne<Event>()
				  .WithMany()
				  .HasForeignKey("EventId")
				  .OnDelete(DeleteBehavior.Cascade),
			j => j.HasKey("EventId", "UserId")
		);


		modelBuilder.Entity<User>()
			.HasMany(u => u.Friends)
			.WithMany()
			.UsingEntity<Friendship>
			(
				j => j.HasOne(f => f.Friend2).WithMany().HasForeignKey(f => f.Friend2Id),
				j => j.HasOne(f => f.Friend1).WithMany().HasForeignKey(f => f.Friend1Id),
				j =>
				{
					j.ToTable("Friendship");
					j.HasKey(f => new { f.Friend1Id, f.Friend2Id });
				}
			);

		// Message
		modelBuilder.Entity<Message>()
			.HasKey(m => m.Id);

		modelBuilder.Entity<Message>()
			.HasOne(m => m.From)
			.WithMany()
			.HasForeignKey(m => m.FromId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Message>()
			.HasOne(m => m.To)
			.WithMany(u => u.Inbox)
			.HasForeignKey(m => m.ToId)
			.OnDelete(DeleteBehavior.Restrict);

		// FriendRequest
		modelBuilder.Entity<FriendRequest>()
			.HasKey(f => f.Id);

		modelBuilder.Entity<FriendRequest>()
			.HasOne(f => f.From)
			.WithMany()
			.HasForeignKey(f => f.FromId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<FriendRequest>()
			.HasOne(f => f.To)
			.WithMany(u => u.FriendRequests)
			.HasForeignKey(f => f.ToId)
			.OnDelete(DeleteBehavior.Restrict);

		// Invite
		modelBuilder.Entity<Invite>()
			.HasKey(i => i.Id);

		modelBuilder.Entity<Invite>()
			.HasOne(i => i.From)
			.WithMany()
			.HasForeignKey(i => i.FromId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Invite>()
			.HasOne(i => i.To)
			.WithMany(u => u.Invites)
			.HasForeignKey(i => i.ToId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Invite>()
			.HasOne(i => i.Event)
			.WithMany()
			.HasForeignKey(i => i.EventId)
			.OnDelete(DeleteBehavior.Cascade);

		// BecomeOrganizerRequest
		modelBuilder.Entity<BecomeOrganizerRequest>()
			.HasKey(b => b.Id);

		modelBuilder.Entity<BecomeOrganizerRequest>()
			.HasOne(b => b.User)
			.WithMany()
			.HasForeignKey(b => b.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		// Data
		modelBuilder.Entity<User>().HasData(
			new User("admin", Convert.FromHexString("CA978112CA1BBDCAFAC231B39A23DC4DA786EFF8147C4E72B9807785AFEE48BB"))
			{
				Id = 1
			}
		);

		base.OnModelCreating(modelBuilder);
	}
}