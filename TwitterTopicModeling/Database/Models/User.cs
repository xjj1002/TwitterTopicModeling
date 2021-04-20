
//class for a system user.
//used to save and get users from the database
namespace TwitterTopicModeling.Database.Models
{
  public class User
  {
    public int Id { get; set; }

    public string userName { get; set; }

    public string password { get; set; }
  }
}