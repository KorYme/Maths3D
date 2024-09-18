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

    public MatrixInt Add(MatrixInt matrix1)
    {
        if (NbLines != matrix1.NbLines || NbColumns != matrix1.NbColumns)
        {
            throw new MatrixSumException();
        }

        for (int row = 0; row < NbLines; row++)
        {
            for (int column = 0; column < NbColumns; column++)
            {
                this[row, column] += matrix1[row, column];
            }
        }
        return this;
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

    public static MatrixInt Add(MatrixInt matrix1, MatrixInt matrix2)
    {
        MatrixInt newMatrix = new MatrixInt(matrix1);
        return newMatrix.Add(matrix2);
    }
    
    public static MatrixInt Transpose(MatrixInt matrix)
    {
        MatrixInt newMatrix = new MatrixInt(matrix);
        return newMatrix.Transpose();
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
#endregion
}