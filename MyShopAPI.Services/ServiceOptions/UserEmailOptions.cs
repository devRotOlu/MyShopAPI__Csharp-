namespace MyShopAPI.Services.ServiceOptions
{
    public class UserEmailOptions
    {
        public List<string> ToEmails { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Body { get; set; } = null!;

        public List<KeyValuePair<string, string>> PlaceHolders { get; set; } = null!;
    }
}
