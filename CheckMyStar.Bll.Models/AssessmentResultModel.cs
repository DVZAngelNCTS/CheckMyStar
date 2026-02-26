using System;

namespace CheckMyStar.Bll.Models
{
    public class AssessmentResultModel
    {
        public int Identifier { get; set; }
        public int AssessmentIdentifier { get; set; }
        public bool IsAccepted { get; set; }
        public int MandatoryPointsEarned { get; set; }
        public int MandatoryThreshold { get; set; }
        public int OptionalPointsEarned { get; set; }
        public int OptionalRequired { get; set; }
        public int OnceFailedCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
