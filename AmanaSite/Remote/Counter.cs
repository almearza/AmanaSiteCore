using System.Collections.Generic;

namespace AmanaSite.Remote
{
     public class ProjectsPercentage
    {
        public string title { get; set; }
        public int percentage { get; set; }
        public int projects { get; set; }
    }

    public class Counters
    {
        public int amanaClients { get; set; }
        public int projectsDone { get; set; }
        public int projectsInProgress { get; set; }
        public int servicesCount { get; set; }
        public List<ProjectsPercentage> projectsPercentages { get; set; }
    }
}