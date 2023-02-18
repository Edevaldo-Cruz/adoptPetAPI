namespace adoptPetAPI.Models
{
    public class PetApiResponse
    {
        public List<PetResult> results { get; set; }
        public string status { get; set; }
    }

    public class PetResult
    {
        public PetName name { get; set; }
    }

    public class PetName
    {
        public string first { get; set; }
      
    }

}
