using System.ComponentModel.DataAnnotations;

namespace GymAssistant.Models
{
    public class Plan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTraining { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTraining { get; set; }

    }
}
