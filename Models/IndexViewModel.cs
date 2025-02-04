namespace BlogSitesi.Models
{
    public class IndexViewModel
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApproved { get; set; }
        public bool IsIndex { get; set; }

        // Kategori bilgisi,
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Yazar bilgisi
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
