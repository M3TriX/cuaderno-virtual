using System.ComponentModel.DataAnnotations;

namespace Cuaderno_virtual.Models.ViewModels
{
    public partial class NoteViewModel
    {
        public int id { get; set; }
        [Required]
        //[Display(title="Título")]
        public string title { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
    }
}