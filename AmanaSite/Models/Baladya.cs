using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmanaSite.Models
{
    public class Baladyat
    {
        public int Id { get; set; }
         [Required(ErrorMessage = " حقل إجباري")]
        public string Name { get; set; }
         [Required(ErrorMessage = " حقل إجباري")]
        public string PhoneNumber { get; set; }
         [Required(ErrorMessage = " حقل إجباري")]
        public string Email { get; set; }
         [Required(ErrorMessage = " حقل إجباري")]
        public string Location { get; set; }
         [Required(ErrorMessage = " حقل إجباري")]
        public string Descr { get; set; }
        public LangCode LangCode { get; set; }
        public BaladyaType BaladyaType { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string DoneBy { get; set; }
    }
   public enum BaladyaType{
       Sub=1,Provinces=2
   }
}