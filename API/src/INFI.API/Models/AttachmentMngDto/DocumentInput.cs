using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace INFI.API.Models.AttachmentMngDto
{
    public class DocumentInput
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [Required]
        public int Id { get; set; }
    }
}
