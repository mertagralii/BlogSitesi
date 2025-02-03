namespace BlogSitesi.Models
{
    public class TBLBlogModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int AuthorsId { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsApproved { get; set; }

        public bool IsIndex { get; set; }


    }
}
