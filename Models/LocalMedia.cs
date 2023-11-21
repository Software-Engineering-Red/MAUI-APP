namespace UndacApp.Models
{
    public class LocalMedia : AModel
    {
        public string email;
        public string Email
        {
            get => email;
            set => SetField(ref email, value);
        }
        public string media;
        public string Media
        {
            get => media;
            set => SetField(ref media, value);
        }
    }
}

