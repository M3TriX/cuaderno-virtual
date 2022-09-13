using System.ComponentModel.DataAnnotations;

namespace Cuaderno_virtual.Models.ViewModels
{
    public partial class NoteViewModel
    {
        public int Id { get; set; }
        [Required]
        //[Display(title="Título")]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}