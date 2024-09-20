using System.Numerics;

namespace Maths_Matrices.Tests;

public static class MatrixElementaryOperations
{
    public static void SwapLines<T>(Matrix<T> matrix, int indexLine1, int indexLine2) where T : INumber<T>
    {
        for (int columnIndex = 0; columnIndex < matrix.NbColumns; columnIndex++)
        {
            matrix[indexLine1, columnIndex] += matrix[indexLine2, columnIndex];
            matrix[indexLine2, columnIndex] = matrix[indexLine1, columnIndex] - matrix[indexLine2, columnIndex];
            matrix[indexLine1, columnIndex] -= matrix[indexLine2, columnIndex];
        }
    }

    public static void SwapColumns<T>(Matrix<T> matrix, int indexColumn1, int indexColumn2) where T : INumber<T>
    {
        for (int lineIndex = 0; lineIndex < matrix.NbLines; lineIndex++)
        {
            matrix[lineIndex, indexColumn1] += matrix[lineIndex, indexColumn2];
            matrix[lineIndex, indexColumn2] = matrix[lineIndex, indexColumn1] - matrix[lineIndex, indexColumn2];
            matrix[lineIndex, indexColumn1] -= matrix[lineIndex, indexColumn2];
        }
    }

    public static void MultiplyLine<T>(Matrix<T> matrix, int lineIndex, T factor) where T : INumber<T>
    {
        if (T.IsZero(factor))
        {
            throw new MatrixScalarZeroException();
        }
        for (int columnIndex = 0; columnIndex < matrix.NbColumns; columnIndex++)
        {
            matrix[lineIndex, columnIndex] *= factor;
        }
    }

    public static void MultiplyColumn<T>(Matrix<T> matrix, int columnIndex, T factor) where T : INumber<T>
    {
        if (T.IsZero(factor))
        {
            throw new MatrixScalarZeroException();
        }
        for (int lineIndex = 0; lineIndex < matrix.NbLines; lineIndex++)
        {
            matrix[lineIndex, columnIndex] *= factor;
        }
    }

    public static void AddLineToAnother<T>(Matrix<T> matrix, int lineToAdd, int lineToChange, T factor) where T : INumber<T>
    {
        for (int columnIndex = 0; columnIndex < matrix.NbColumns; columnIndex++)
        {
            matrix[lineToChange, columnIndex] += matrix[lineToAdd, columnIndex] * factor;
        }
    }

    public static void AddColumnToAnother<T>(Matrix<T> matrix, int columnToAdd, int columnToChange, T factor) where T : INumber<T>
    {
        for (int lineIndex = 0; lineIndex < matrix.NbColumns; lineIndex++)
        {
            matrix[lineIndex, columnToChange] += matrix[lineIndex, columnToAdd] * factor;
        }
    }
}