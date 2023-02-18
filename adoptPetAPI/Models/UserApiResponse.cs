namespace adoptPetAPI.Models
{
    public class UserApiResponse
    {
        public List<UserResult> results { get; set; }
        public string status { get; set; }
    }

    public class UserResult
    {
        public UserName name { get; set; }
    }

    public class UserName
    {
        public string first { get; set; }
        public string last { get; set; }
    }

}
