using System.Collections.Generic;
using System.Numerics;

namespace CSharpOddOrEven.Tests
{
    public class Numbers
    {
        public static IEnumerable<object[]> EvenNumbers =>
            new List<object[]>
            {
                new object[] { (BigInteger)2 },
                new object[] { (BigInteger)0 },
                new object[] { (BigInteger)2000000000000 },
            };
        

        public static IEnumerable<object[]> OddNumbers =>
            new List<object[]>
            {
                new object[] { (BigInteger)3 },
                new object[] { (BigInteger)1 },
                new object[] { (BigInteger)2000000000001 },
            };
    }
}
