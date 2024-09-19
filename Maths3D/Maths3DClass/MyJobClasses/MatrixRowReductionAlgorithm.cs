using System.Numerics;

namespace Maths_Matrices.Tests;

public static class MatrixRowReductionAlgorithm
{
    public static (Matrix<T> m1, Matrix<T> m2) Apply<T>(Matrix<T> m1, Matrix<T> m2) where T : INumber<T>
    {
        for (int step = 0; step < int.Min(m1.NbLines, m2.NbLines); step++)
        {
            // FIND MAX AND SWAP LINES
            int maxLineIndex = step;
            for (int k = step + 1; k < m1.NbLines; k++)
            {
                if (m1[k, step] > m1[step, step] && !T.IsZero(m1[k, step]))
                {
                    maxLineIndex = k;
                }
            }
            if (maxLineIndex != step)
            {
                MatrixElementaryOperations.SwapLines(m1, step, maxLineIndex);
                MatrixElementaryOperations.SwapLines(m2, step, maxLineIndex);
            }
            
            T factor = m1[step, step];
            if (!T.IsZero(factor))
            {
                MatrixElementaryOperations.MultiplyLine(m1, step, T.One / factor);
                MatrixElementaryOperations.MultiplyLine(m2, step, T.One / factor);
                for (int r = 0; r < m1.NbLines; r++)
                {
                    if (step != r)
                    {
                        factor = -m1[r, step];
                        MatrixElementaryOperations.AddLineToAnother(m1, step, r, factor);
                        MatrixElementaryOperations.AddLineToAnother(m2, step, r, factor);
                    }
                }
            }
        }
        return (m1, m2);
    }
}