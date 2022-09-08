using System;
using System.Collections.Generic;
//modelo de tabla según la base de datos
namespace Cuaderno_virtual.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateOnly Date { get; set; }
        public bool Active { get; set; }
    }
}
