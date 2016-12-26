using Mediachase.Commerce;

namespace EpiServerBlogs.Web.Business.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToMoney(this decimal amount, Currency currency)
        {
            var money = new Money(amount, currency);
            return money.ToString();
        }
    }
}