using System.ComponentModel.DataAnnotations;
namespace Notes_API.DTOs
{
    public class UpdateNoteDto
    {
        [Required]
        public string Nots_Title { get; set; }
        [Required]
        public string Nots_Description { get; set; }
        public bool IsImportant { get; set; }
    }
}
