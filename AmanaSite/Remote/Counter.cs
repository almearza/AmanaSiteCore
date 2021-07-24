using System.Collections.Generic;

namespace AmanaSite.Remote
{
    public class Item
    {
        public string projtyp_name { get; set; }
        public string bdgloc_name { get; set; }
        public int bdgyyy { get; set; }
        public int cntrno { get; set; }
        public string cntrnam { get; set; }
        public string enddate_g { get; set; }
        public int workdone_percent { get; set; }
    }

    public class ProjectLink
    {
        public string rel { get; set; }
        public string href { get; set; }
    }
    public class CounterVM
    {
        public List<Item> items { get; set; }
        public bool hasMore { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public List<ProjectLink> links { get; set; }
    }
}