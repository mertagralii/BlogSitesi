using System.ComponentModel.DataAnnotations;

namespace BlogSitesi.Models
{
    public class TBLCategoryModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Kategori İsmi Boş Bırakılamaz.")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter uzunluğunda olabilir.")]
        public string CategoryName { get; set; }

    }
}
