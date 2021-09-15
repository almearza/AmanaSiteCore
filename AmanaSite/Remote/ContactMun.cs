using System.ComponentModel.DataAnnotations;

namespace AmanaSite.Remote
{
    public class ContactMun
    {
        [Required]
        public string message { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string contactReason { get; set; }
    }
    public class ContactMunVM
    {
        [Required(ErrorMessage ="MsgError")]
        [Display(Prompt ="Message")]
        public string message { get; set; }
        [Display(Prompt ="Name")]
        [Required(ErrorMessage ="NameError")]
        public string name { get; set; }
        [Display(Prompt ="Phone")]
        [Required(ErrorMessage ="PhoneError")]
        public string phoneNumber { get; set; }
        [Display(Prompt ="Email")]
        [Required(ErrorMessage ="EmailError")]
        public string email { get; set; }
        public string contactReason { get; set; }
    }
}