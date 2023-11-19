namespace UndacApp.Models {
    public class MethodologicalDocumentation : AModel
    {
        private DateTime? createdAt;
        public DateTime? CreatedAt
        {
            get => createdAt;
            set => SetField(ref createdAt, value);
        }

        private DateTime? lastModifiedAt;
        public DateTime? LastModifiedAt
        {
            get => lastModifiedAt;
            set => SetField(ref lastModifiedAt, value);
        }

        private string? author;
        public string? Author
        {
            get => author;
            set => SetField(ref author, value);
        }

        private string? contents;
        public string? Contents
        {
            get => contents;
            set => SetField(ref contents, value);
        }
    }

}
