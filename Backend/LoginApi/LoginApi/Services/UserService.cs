using LoginApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace LoginApi.Services
{
	public class UserService
	{
		private readonly IMongoCollection<User> _users;

		public UserService(IUserDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_users = database.GetCollection<User>(settings.UsersCollectionName);
		}

		public User Get(string id) =>
			_users.Find<User>(user => user.Id == id).FirstOrDefault();

		public User Create(User user)
		{
			_users.InsertOne(user);
			return user;
		}

		public User GetByUsername(string username) =>
			_users.Find<User>(user => user.Username == username).FirstOrDefault();
	}
}
