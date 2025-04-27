namespace WPF.Models
{
	public class Invitation : Message
    {
        public Event Event { get; init; }

        public Invitation(User from, User to, string content, Event @event) : base(from, to, content)
        {
            Event = @event;
        }
    }
}
