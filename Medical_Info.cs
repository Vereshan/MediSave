using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medisave
{
    public class Medical_Info
    {
        public int RecordNo { get; set; }
        public int ID { get; set; }  // FK to CoreInfo.ID
        public string MedicalAid { get; set; }
        public string Occupation { get; set; }
        public string Employer { get; set; }
        public string Allergies { get; set; }
        public string SpecialFeatures { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
