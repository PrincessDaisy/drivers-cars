namespace drivers_cars.DTO
{
    public class DriverDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string? MiddleName { get; set; }

        public DateOnly? BirthDate { get; set; }
    }
}
