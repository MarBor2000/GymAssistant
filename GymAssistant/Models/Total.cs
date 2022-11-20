using System.ComponentModel.DataAnnotations;

namespace GymAssistant.Models
{
    public class Total
    {
        public int iD { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTraining { get; set; }

        public int lawa { get; set; }

        public int przysiad { get; set; }

        public int martwy { get; set; }

        public int total { get; set; }
    }
}
