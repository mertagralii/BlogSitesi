using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSitesi.Models
{
    public class TBLBlogModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AuthorsId { get; set; }

        [Required(ErrorMessage ="Başlık Boş Bırakılamaz.")]
        [StringLength(50,ErrorMessage ="En fazla 50 karakter uzunluğunda olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Özet Boş Bırakılamaz.")]
        [StringLength(60, ErrorMessage = "En fazla 60 karakter uzunluğunda olabilir.")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Detay Boş Bırakılamaz.")]
        [StringLength(300, ErrorMessage = "En fazla 300 karakter uzunluğunda olabilir.")]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public bool IsApproved { get; set; }

        public bool IsIndex { get; set; }


    }
}
