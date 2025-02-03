namespace BlogSitesi.Models
{
    public class TBLAuthorsModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public string Birthplace { get; set; }

        public string ImageURL { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }
    }
}
