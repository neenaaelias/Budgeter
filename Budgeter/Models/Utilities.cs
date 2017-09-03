using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    // generates the random alphanumeric code (the default is six characters
    public class Utilities
    {

        public static string GenerateSecretCode()
    {
        return GenerateSecretCode(6);
    }

    public static string GenerateSecretCode(int length)
    {
        string characterSet = "ABCDEFGHIJKLMNOPQRTUVWXYZ";
        Random random = new Random();

        //select the random characters from the set and then the array of said characters are passsed to the tring constructor to make an alphanumeric string
        string randomCode = new string(
            Enumerable.Repeat(characterSet, length)
            .Select(set => set[random.Next(set.Length)])
            .ToArray());
        return randomCode;
    }
}
}








