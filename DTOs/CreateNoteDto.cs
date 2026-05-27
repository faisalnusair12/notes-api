using System.ComponentModel.DataAnnotations;
namespace Notes_API.DTOs
{
    public class CreateNoteDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Nots_Title { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Nots_Description { get; set; }
        public bool IsImportant { get; set; }
    }
}
