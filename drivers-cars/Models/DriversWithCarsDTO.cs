using System.Text.Json.Serialization;

namespace drivers_cars.Models
{
    public class DriversWithCarsDTO
    {
        [JsonPropertyName("fio")]
        public string FIO { get; set; }

        [JsonPropertyName("age")]
        public int? Age { get; set; }

        [JsonPropertyName("cars")]
        public List<MappedCar> Cars { get; set; }
    }

    public class MappedCar
    {
        [JsonPropertyName("brand-model")]
        public string Model { get; set; }

        [JsonPropertyName("registration_number")]
        public string RegNum { get; set; }
    }
}
