using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class EmployeeParameters: RequestParameters
    {
        // Filtering params
        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = int.MaxValue; 
        // Search params
        public string? SearchTerm { get; set; }

    }
}
