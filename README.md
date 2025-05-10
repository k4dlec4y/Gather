# Gather - Social App for Events

Gather is a social application designed to help users discover, join, and organize events. Users can explore various events, filter them by category, and connect with friends. Organizers can create and manage events, while admins oversee user and event verification.

## Features

### Authentication
- **User Registration & Deletion**  
  Users can create an account or permanently delete their profile.
- **Login & Logout**  
  Secure authentication for users, organizers, and admins.

### User Roles

#### Regular User
- **Browse Events**  
  View a list of available events.
- **Filter Events by Category**  
  Narrow down events based on interests.
- **RSVP to Events**  
  Mark attendance for events.
- **Friends System**  
  - Add friends to your network.  
  - Receive event invitations from friends.  
  - See which friends are attending an event.

#### Event Organizer
- **Create Events**  
  Set up new events with details like location, date, and category.
- **Modify or Cancel Events**  
  Edit event details or remove events entirely.

#### Admin
- **User & Event Verification**  
  Approve and monitor organizers and events.
- **Admin Credentials** (for testing)  
  - Username: `admin`  
  - Password: `a`  
  - Role: Basic User

### Weather Integration
- **Automatic Weather Fetching**  
  - Weather data is fetched asynchronously from an API.  
  - Only displayed if the event occurs within **2 days or less**.  
  - Location-based weather estimation (based on organizer input).  
  - If location is unrecognized, weather is not displayed.

## Notes & Future Fixes
- A user account can only be deleted **after manually removing all friends** (current limitation).  
- Deleting a user account also removes their organizer profile (if applicable).

### Key Additions:
- **Future Security** – Password salting for better protection.  
- **Friend Discovery** – A new tab to find and connect with users.  
- **Database Improvements** – Better structure for performance.  
  and more! 