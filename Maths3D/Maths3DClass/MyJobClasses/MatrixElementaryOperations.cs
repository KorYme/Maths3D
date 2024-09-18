namespace Maths_Matrices.Tests;

public static class MatrixElementaryOperations
{
    public static void SwapLines(MatrixInt matrix, int indexLine1, int indexLine2)
    {
        for (int columnIndex = 0; columnIndex < matrix.NbColumns; columnIndex++)
        {
            matrix[indexLine1, columnIndex] += matrix[indexLine2, columnIndex];
            matrix[indexLine2, columnIndex] = matrix[indexLine1, columnIndex] - matrix[indexLine2, columnIndex];
            matrix[indexLine1, columnIndex] -= matrix[indexLine2, columnIndex];
        }
    }

    public static void SwapColumns(MatrixInt matrix, int indexColumn1, int indexColumn2)
    {
        for (int lineIndex = 0; lineIndex < matrix.NbLines; lineIndex++)
        {
            matrix[lineIndex, indexColumn1] += matrix[lineIndex, indexColumn2];
            matrix[lineIndex, indexColumn2] = matrix[lineIndex, indexColumn1] - matrix[lineIndex, indexColumn2];
            matrix[lineIndex, indexColumn1] -= matrix[lineIndex, indexColumn2];
        }
    }

    public static void MultiplyLine(MatrixInt matrix, int lineIndex, int factor)
    {
        if (factor == 0)
        {
            throw new MatrixScalarZeroException();
        }
        for (int columnIndex = 0; columnIndex < matrix.NbColumns; columnIndex++)
        {
            matrix[lineIndex, columnIndex] *= factor;
        }
    }

    public static void MultiplyColumn(MatrixInt matrix, int columnIndex, int factor)
    {
        if (factor == 0)
        {
            throw new MatrixScalarZeroException();
        }
        for (int lineIndex = 0; lineIndex < matrix.NbLines; lineIndex++)
        {
            matrix[lineIndex, columnIndex] *= factor;
        }
    }

    public static void AddLineToAnother(MatrixInt matrix, int lineToAdd, int lineToChange, int factor)
    {
        for (int columnIndex = 0; columnIndex < matrix.NbColumns; columnIndex++)
        {
            matrix[lineToChange, columnIndex] += matrix[lineToAdd, columnIndex] * factor;
        }
    }

    public static void AddColumnToAnother(MatrixInt matrix, int columnToAdd, int columnToChange, int factor)
    {
        for (int lineIndex = 0; lineIndex < matrix.NbColumns; lineIndex++)
        {
            matrix[lineIndex, columnToChange] += matrix[lineIndex, columnToAdd] * factor;
        }
    }
}