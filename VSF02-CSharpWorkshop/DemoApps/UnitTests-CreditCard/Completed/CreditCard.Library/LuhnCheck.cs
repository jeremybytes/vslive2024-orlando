namespace CreditCard.Library;

/// <summary>
/// Code stolen from
/// http://orb-of-knowledge.blogspot.com/2009/08/extremely-fast-luhn-function-for-c.html
/// </summary>
public static class LuhnCheck
{
    public static bool PassesLuhnCheck(string cardNumber)
    {
        try
        {
            int[] DELTAS = [0, 1, 2, 3, 4, -4, -3, -2, -1, 0];
            int checksum = 0;
            char[] chars = cardNumber.ToCharArray();
            for (int i = chars.Length - 1; i > -1; i--)
            {
                int j = chars[i] - 48;
                checksum += j;
                if (((i - chars.Length) % 2) == 0)
                    checksum += DELTAS[j];
            }

            return (checksum % 10) == 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
