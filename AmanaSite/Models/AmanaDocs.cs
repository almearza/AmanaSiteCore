using System;

namespace AmanaSite.Models
{
    public class AmanaDocs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string DoneBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public LangCode LangCode { get; set; }
    }
    
}