namespace StoreApp.Infrastructure.Extensions
{
    public static  class StringExtension
    {
        public static string RemoveTurkishCharacters(this string input)
        {
            return input
                .Replace('ü', 'u')
                .Replace('ö', 'o')
                .Replace('ı', 'i')
                .Replace('ş', 's')
                .Replace('ç', 'c')
                .Replace('ğ', 'g');
        }
    }
}
