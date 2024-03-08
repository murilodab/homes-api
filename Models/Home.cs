using System.ComponentModel.DataAnnotations;

namespace homes_api.Models
{
    public class Home
    {
        public int Id { get; set; }
        public  string? Title { get; set; }
        public  string? Description { get; set; }
        public string? City { get; set; }
        public  string? State { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedDate { get; set; }
        public  string? ImageBase64 { get; set; }
        public bool Available { get; set; }

    }
}
