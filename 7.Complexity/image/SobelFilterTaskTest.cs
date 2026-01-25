using NUnit.Framework;
using System.Collections.Generic;

namespace Recognizer
{
    [TestFixture]
    public class SobolFilter_should
    {
        static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData
                (
                    new double[,] { { 1 } },
                    new double[,] { { 2 } },
                    new double[,] { { 2.828427 } }
                );

            yield return new TestCaseData
                (
                    new double[,]
                    {
                        { 1, 1, 1 },
                        { 1, 1, 0 },
                        { 1, 0, 0 }
                    },
                    new double[,]
                    {
                        { 1 , 2 , 1  },
                        { 0 , 0 , 0  },
                        { -1, -2, -1 }
                    },
                    new double[,]
                    {
                        { 0, 0       , 0 },
                        { 0, 4.242640, 0 },
                        { 0, 0       , 0 }
                    }
                );

            yield return new TestCaseData
                (
                    new double[,]
                    {
                        { 0.9, 0.3, 0.4, 0.2, 0.8, 0.4, 0.1 },
                        { 0.6, 0.9, 0.4, 0.1, 0.3, 0.6, 0.1 },
                        { 0.3, 0.1, 0.4, 0.8, 0 ,  0.1, 0.9 },
                        { 0.8, 0.4, 0.2, 0.2, 0.7, 0.3, 0.9 },
                        { 0.4, 0.9, 0.8, 0.5, 0 ,  0.5, 0.8 },
                        { 0.3, 0.9, 0.8, 0.3, 0.2, 0.5, 0.5 },
                    },
                    new double[,]
                    {
                        { 1, 4, 6 , 4, 1 },
                        { 2, 8, 12, 8, 2 },
                        { 0, 0, 0 , 0, 0 },
                        { 2, 8, 12, 8, 2 },
                        { 1, 4, 6 , 4, 1 },
                    },
                    new double[,]
                    {
                        { 0, 0 , 0        , 0        , 0        , 0 , 0 },
                        { 0, 0 , 0        , 0        , 0        , 0 , 0 },
                        { 0, 0 , 58.409759, 48.659017, 55.169013, 0 , 0 },
                        { 0, 0 , 67.296359, 55.059422, 54.450711, 0 , 0 },
                        { 0, 0 , 0        , 0        , 0        , 0 , 0 },
                        { 0, 0 , 0        , 0        , 0        , 0 , 0 },
                    }
                );
        }

        [TestCaseSource(nameof(TestCases))]

        public void Test(double[,] original, double[,] sx, double[,] expected)
        {
            var dimOne = original.GetLength(0);
            var dimTwo = original.GetLength(1);
            var result = SobelFilterTask.SobelFilter(original, sx);
            for (int i = 0; i < dimOne; i++)
            {
                for (int j = 0; j < dimTwo; j++)
                {
                    Assert.That(result[i, j], Is.EqualTo(expected[i, j]).Within(10e-5));
                }
            }
        }
    }
}
