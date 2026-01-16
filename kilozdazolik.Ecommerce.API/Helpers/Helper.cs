namespace kilozdazolik.Ecommerce.API.Helpers
{
    public static class Helper
    {
        public static void ValidateName(string name, string error)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(error, nameof(name));
            }
        }
    }
}