namespace Maths_Matrices.Tests;

public struct MatrixInt
{
    private int[,] m_matrix;

    #region CONSTRUCTORS
    public MatrixInt(int[,] matrix)
    {
        m_matrix = matrix;
    }

    public MatrixInt(int NbLines, int NbColumns)
    {
        m_matrix = new int[NbLines, NbColumns];
    }

    public MatrixInt(MatrixInt copy)
    {
        m_matrix = copy.ToArray2D();
    }
    #endregion

    #region PROPERTIES
    public int NbLines => m_matrix.GetLength(0);
    public int NbColumns => m_matrix.GetLength(1);
    
    public int this[int rowIndex, int columnIndex]
    {
        get
        {
            if (rowIndex < 0 || rowIndex >= NbLines || columnIndex < 0 || columnIndex >= NbColumns)
            {
                throw new IndexOutOfRangeException();
            }
            return m_matrix[rowIndex, columnIndex];
        }
        set
        {
            if (rowIndex < 0 || rowIndex >= NbLines || columnIndex < 0 || columnIndex >= NbColumns)
            {
                throw new IndexOutOfRangeException();
            }
            m_matrix[rowIndex, columnIndex] = value;
        }
    }
    
    public int[,] ToArray2D()
    {
        return (int[,])m_matrix.Clone();
    }
    #endregion
    
    #region METHODS
    public bool IsIdentity()
    {
        if (NbLines != NbColumns)
        {
            return false;
        }

        for (int row = 0; row < NbLines; row++)
        {
            for (int column = 0; column < NbColumns; column++)
            {
                if (row == column && this[row, column] != 1 || row != column && this[row, column] != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    public MatrixInt Multiply(int factor)
    {
        for (int row = 0; row < NbLines; row++)
        {
            for (int column = 0; column < NbColumns; column++)
            {
                this[row, column] *= factor;
            }
        }
        return this;
    }

    public MatrixInt Add(MatrixInt otherMatrix)
    {
        if (NbLines != otherMatrix.NbLines || NbColumns != otherMatrix.NbColumns)
        {
            throw new MatrixSumException();
        }
        for (int row = 0; row < NbLines; row++)
        {
            for (int column = 0; column < NbColumns; column++)
            {
                this[row, column] += otherMatrix[row, column];
            }
        }
        return this;
    }

    public MatrixInt Multiply(MatrixInt otherMatrix)
    {
        return Multiply(this, otherMatrix);
    }
    
    public MatrixInt Transpose()
    {
        int[,] tmp = new int[NbColumns, NbLines];
        for (int rowIndex = 0; rowIndex < NbLines; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < NbColumns; columnIndex++)
            {
                int tmpValue = this[rowIndex, columnIndex];
                tmp[columnIndex, rowIndex] = tmpValue;
            }
        }
        return new MatrixInt(tmp);
    }
    
    public (MatrixInt m1, MatrixInt m2) Split(int maxIndex)
    {
        MatrixInt m1 = new MatrixInt(NbLines, maxIndex + 1);
        MatrixInt m2 = new MatrixInt(NbLines, NbColumns - maxIndex - 1);
        for (int lineIndex = 0; lineIndex < NbLines; lineIndex++)
        {
            for (int columnIndex = 0; columnIndex < NbColumns; columnIndex++)
            {
                if (columnIndex <= maxIndex)
                {
                    m1[lineIndex, columnIndex] = this[lineIndex, columnIndex];
                }
                else
                {
                    m2[lineIndex, columnIndex - maxIndex - 1] = this[lineIndex, columnIndex];
                }
            }
        }
        return (m1, m2);
    }
    #endregion

    #region STATIC METHODS
    public static MatrixInt Identity(int size)
    {
        MatrixInt matrix = new MatrixInt(size, size);
        for (int i = 0; i < size; i++)
        {
            matrix[i, i] = 1;
        }
        return matrix;
    }
    
    public static MatrixInt Multiply(MatrixInt matrix, int factor)
    {
        MatrixInt newMatrix = new MatrixInt(matrix);
        return newMatrix.Multiply(factor);;
    }

    public static MatrixInt Add(MatrixInt otherMatrix, MatrixInt matrix2)
    {
        MatrixInt newMatrix = new MatrixInt(otherMatrix);
        return newMatrix.Add(matrix2);
    }

    public static MatrixInt Multiply(MatrixInt matrix1, MatrixInt matrix2)
    {
        if (matrix1.NbColumns != matrix2.NbLines)
        {
            throw new MatrixMultiplyException();
        }
        MatrixInt newMatrix = new MatrixInt(matrix1.NbLines, matrix2.NbColumns);
        for (int row = 0; row < newMatrix.NbLines; row++)
        {
            for (int column = 0; column < newMatrix.NbColumns; column++)
            {
                for (int index = 0; index < matrix1.NbColumns; index++)
                {
                    newMatrix[row, column] += matrix1[row, index] * matrix2[index, column];
                }
            }
        }
        return newMatrix;
    }
    
    public static MatrixInt Transpose(MatrixInt matrix)
    {
        MatrixInt newMatrix = new MatrixInt(matrix);
        return newMatrix.Transpose();
    }
    
    public static MatrixInt GenerateAugmentedMatrix(MatrixInt m1, MatrixInt m2)
    {
        MatrixInt matrix = new MatrixInt(m1.NbLines, m1.NbColumns + 1);
        for (int lineIndex = 0; lineIndex < matrix.NbLines; lineIndex++)
        {
            for (int columnIndex = 0; columnIndex < matrix.NbColumns; columnIndex++)
            {
                if (columnIndex != m1.NbColumns)
                {
                    matrix[lineIndex, columnIndex] = m1[lineIndex, columnIndex];
                }
                else
                {
                    matrix[lineIndex, columnIndex] = m2[lineIndex, 0];
                }
            }
        }
        return matrix;
    }
    #endregion
    
    #region OPERATOR OVERRIDES
    public static MatrixInt operator*(MatrixInt matrix, int factor)
    {
        MatrixInt newMatrix = new MatrixInt(matrix);
        return newMatrix.Multiply(factor);
    }
    
    public static MatrixInt operator*(int factor, MatrixInt matrix)
    {
        MatrixInt newMatrix = new MatrixInt(matrix);
        return newMatrix.Multiply(factor);
    }

    public static MatrixInt operator-(MatrixInt matrix)
    {
        MatrixInt newMatrix = new MatrixInt(matrix);
        return newMatrix.Multiply(-1);
    }

    public static MatrixInt operator+(MatrixInt matrix1, MatrixInt matrix2)
    {
        MatrixInt newMatrix = new MatrixInt(matrix1);
        return newMatrix.Add(matrix2);
    }

    public static MatrixInt operator-(MatrixInt matrix1, MatrixInt matrix2)
    {
        MatrixInt newMatrix = new MatrixInt(matrix1);
        return newMatrix.Add(-matrix2);
    }

    public static MatrixInt operator*(MatrixInt matrix1, MatrixInt matrix2)
    {
        return Multiply(matrix1, matrix2);
    }
    #endregion
}