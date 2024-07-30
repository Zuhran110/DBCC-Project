using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6.Models;
public class ProjectEngineer
{
    public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int EngineerId { get; set; }
        public Login Engineer { get; set; }
}
