namespace drivers_cars.Helpers
{
    public class Helpers
    {
        public static TResult MapObjects<TSource, TResult>(TSource source)
            where TSource : class, new()
            where TResult : class, new()
        {
            var destinationObject = Activator.CreateInstance<TResult>();
            if (source != null)
            {
                foreach (var sourceProperty in typeof(TSource).GetProperties())
                {
                    var destinationProperty =
                        typeof(TResult).GetProperty
                            (sourceProperty.Name);
                    if (destinationProperty != null)
                    {
                        destinationProperty.SetValue
                        (destinationObject,
                            sourceProperty.GetValue(source));
                    }
                }

                return destinationObject;
            }
            return null;
        }

        public static int GetAge(DateOnly birthDate)
        {
            DateTime n = DateTime.Now;
            int age = n.Year - birthDate.Year;

            if (n.Month < birthDate.Month || (n.Month == birthDate.Month && n.Day < birthDate.Day))
                age--;

            return age;
        }
    }
}
