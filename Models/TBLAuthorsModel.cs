

using System.ComponentModel.DataAnnotations;

namespace BlogSitesi.Models
{
    public class TBLAuthorsModel
    {

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Yazar Adı Boş Bırakılamaz.")]
        [StringLength(50,ErrorMessage ="Yazar Adı 50 karakterden uzun olamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Yazar Soy Adı Boş Bırakılamaz.")]
        [StringLength(50,ErrorMessage ="Yazar soyismi 50 karakteden uzun olamaz.")]
        public string SurName { get; set; }

        [Required(ErrorMessage ="Yazar yaşı boş bırakılamaz.")]
        [Range(18,120,ErrorMessage ="Yazar yaşı 18'den küçük ve 120'den büyük olamaz.")]
        public int Age { get; set; }

        [Required(ErrorMessage ="Yazar doğum tarihi boş bırakılamaz.")]
        [Range(typeof(DateTime),"01/01/1920", "01/01/2020", ErrorMessage ="Yazar 1920 yılından önce ve 2020 yılından sonra doğmuş olamaz.")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage ="Yazarın doğrum yeri boş bırakılamaz.")]
        public string Birthplace { get; set; }

        [Required(ErrorMessage ="Yazarın Resmi Boş bırakılamaz.")]
        [StringLength(500,ErrorMessage ="Gönderdiğiniz resim URL'si 500 karakterden fazla olamaz.")]
        public string ImageURL { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage ="Yazarın Kendini Tanıtma Kısmı Boş bırakılamaz.")]
        [StringLength(400,ErrorMessage ="Kendinizi tanıtacağınız kısım 400 karakterden fazla olamaz.")]
        public string Description { get; set; }

        public string Summary { get; set; }
    }
}
