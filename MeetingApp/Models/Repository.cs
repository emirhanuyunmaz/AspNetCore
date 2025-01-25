namespace MeetingApp.Models
{
    public static class Repository{
        private static List<UserInfo> _users = new();

        static Repository(){
            _users.Add(new UserInfo(){Id=1,Name="Ali",Email="ali@gmail.com",Phone="123456789",WillAttend=true});
            _users.Add(new UserInfo(){Id=2,Name="Ayşe",Email="ayşe@gmail.com",Phone="147852369",WillAttend=false});
            _users.Add(new UserInfo(){Id=3,Name="Fatma",Email="fatma@gmail.com",Phone="852147936",WillAttend=true});
        }
        public static List<UserInfo> Users{
            get{
                return _users;
            }
        }

        public static void CreateUser(UserInfo user){
            user.Id = Users.Count + 1;
            _users.Add(user);
        }

        public static UserInfo? GetById(int id){
            return _users.FirstOrDefault(user => user.Id == id);
        }

    }
}