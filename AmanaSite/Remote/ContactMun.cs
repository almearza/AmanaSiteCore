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
        [MaxLength(5000,ErrorMessage ="MaxLengthArea")]
        public string message { get; set; }
        [MaxLength(100,ErrorMessage ="MaxLength")]
        [Display(Prompt ="Name")]
        [Required(ErrorMessage ="NameError")]
        public string name { get; set; }
        [Display(Prompt ="Phone")]
        [Required(ErrorMessage ="PhoneError")]
        [MaxLength(14,ErrorMessage ="MaxLength")]
        public string phoneNumber { get; set; }
        [Display(Prompt ="Email")]
        [Required(ErrorMessage ="EmailError")]
         [MaxLength(100,ErrorMessage ="MaxLength")]
         [EmailAddress(ErrorMessage ="NotEmail")]
        public string email { get; set; }
        [MaxLength(10,ErrorMessage ="MaxLengthReason")]
        public string contactReason { get; set; }
    }
}