using System.Numerics;

namespace Maths_Matrices.Tests;

public struct Matrix<T> where T : INumber<T>
{
    private T[,] m_matrix;
    
    #region PROPERTIES
    public int NbLines => m_matrix.GetLength(0);
    public int NbColumns => m_matrix.GetLength(1);
    
    public T this[int rowIndex, int columnIndex]
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
    
    public T[,] ToArray2D()
    {
        return (T[,])m_matrix.Clone();
    }
    #endregion
    
    #region CONSTRUCTORS

    public Matrix()
    {
        m_matrix = new T[4,4];
    }
    
    public Matrix(T[,] matrix)
    {
        m_matrix = matrix;
    }

    public Matrix(int NbLines, int NbColumns)
    {
        m_matrix = new T[NbLines, NbColumns];
    }

    public Matrix(Matrix<T> copy)
    {
        m_matrix = copy.ToArray2D();
    }
    #endregion
    
    #region OPERATOR OVERRIDES
    public static Matrix<T> operator*(Matrix<T> matrix, T factor)
    {
        Matrix<T> newMatrix = new Matrix<T>(matrix);
        return newMatrix.Multiply(factor);
    }
    
    public static Matrix<T> operator*(T factor, Matrix<T> matrix)
    {
        Matrix<T> newMatrix = new Matrix<T>(matrix);
        return newMatrix.Multiply(factor);
    }

    public static Matrix<T> operator-(Matrix<T> matrix)
    {
        Matrix<T> newMatrix = new Matrix<T>(matrix);
        return newMatrix.Multiply(-T.One);
    }

    public static Matrix<T> operator+(Matrix<T> matrix1, Matrix<T> matrix2)
    {
        Matrix<T> newMatrix = new Matrix<T>(matrix1);
        return newMatrix.Add(matrix2);
    }

    public static Matrix<T> operator-(Matrix<T> matrix1, Matrix<T> matrix2)
    {
        Matrix<T> newMatrix = new Matrix<T>(matrix1);
        return newMatrix.Add(-matrix2);
    }

    public static Matrix<T> operator*(Matrix<T> matrix1, Matrix<T> matrix2)
    {
        return Multiply(matrix1, matrix2);
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
                if (row == column && !this[row, column].Equals(T.One) || row != column && !this[row, column].Equals(T.Zero))
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    public Matrix<T> Multiply(T factor)
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

    public Matrix<T> Add(Matrix<T> otherMatrix)
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

    public Matrix<T> Multiply(Matrix<T> otherMatrix)
    {
        return Multiply(this, otherMatrix);
    }
    
    public Matrix<T> Transpose()
    {
        T[,] tmp = new T[NbColumns, NbLines];
        for (int rowIndex = 0; rowIndex < NbLines; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < NbColumns; columnIndex++)
            {
                T tmpValue = this[rowIndex, columnIndex];
                tmp[columnIndex, rowIndex] = tmpValue;
            }
        }
        return new Matrix<T>(tmp);
    }
    
    public (Matrix<T> m1, Matrix<T> m2) Split(int maxIndex)
    {
        Matrix<T> m1 = new Matrix<T>(NbLines, maxIndex + 1);
        Matrix<T> m2 = new Matrix<T>(NbLines, NbColumns - maxIndex - 1);
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
    
    public Matrix<T> InvertByRowReduction()
    {
        if (NbLines != NbColumns)
        {
            throw new MatrixInvertException();
        }
        (Matrix<T> m1, Matrix<T> m2) = MatrixRowReductionAlgorithm.Apply(new Matrix<T>(this), Matrix<T>.Identity(NbLines), true);
        return m2;
    }
    
    public Matrix<T> SubMatrix(int lineRemoved, int columnRemoved)
    {
        Matrix<T> newMatrix = new Matrix<T>(NbLines - 1, NbColumns - 1);
        for (int lineIndex = 0; lineIndex < NbLines; lineIndex++)
        {
            for (int columnIndex = 0; columnIndex < NbColumns; columnIndex++)
            {
                if (lineIndex != lineRemoved && columnIndex != columnRemoved)
                {
                    newMatrix[lineIndex - (lineIndex > lineRemoved ? 1 : 0), columnIndex- (columnIndex > columnRemoved ? 1 : 0)] = this[lineIndex, columnIndex];
                }
            }
        }
        return newMatrix;
    }
    
    public Matrix<T> Adjugate()
    {
        if (T.IsZero(Determinant(this)))
        {
            throw new MatrixSumException();
        }
        Matrix<T> matrix = new Matrix<T>(NbLines, NbColumns);
        Matrix<T> transposedMatrix = Transpose();
        for (int lineIndex = 0; lineIndex < NbLines; lineIndex++)
        {
            for (int columnIndex = 0; columnIndex < NbColumns; columnIndex++)
            {
                matrix[lineIndex, columnIndex] = Determinant(transposedMatrix.SubMatrix(lineIndex, columnIndex))
                    * ((lineIndex + columnIndex) % 2 == 0 ? T.One : -T.One);
            }
        }
        return matrix;
    }
    
    public Matrix<T> InvertByDeterminant()
    {
        T determinant = Determinant(this);
        if (T.IsZero(determinant))
        {
            throw new MatrixInvertException();
        }
        return Adjugate(this) * (T.One / determinant);
    }
    #endregion

    #region STATIC METHODS
    public static Matrix<T> Identity(int size)
    {
        Matrix<T> matrix = new Matrix<T>(size, size);
        for (int i = 0; i < size; i++)
        {
            matrix[i, i] = T.One;
        }
        return matrix;
    }
    
    public static Matrix<T> Multiply(Matrix<T> matrix, T factor)
    {
        Matrix<T> newMatrix = new Matrix<T>(matrix);
        return newMatrix.Multiply(factor);
    }

    public static Matrix<T> Add(Matrix<T> otherMatrix, Matrix<T> matrix2)
    {
        Matrix<T> newMatrix = new Matrix<T>(otherMatrix);
        return newMatrix.Add(matrix2);
    }

    public static Matrix<T> Multiply(Matrix<T> matrix1, Matrix<T> matrix2)
    {
        if (matrix1.NbColumns != matrix2.NbLines)
        {
            throw new MatrixMultiplyException();
        }
        Matrix<T> newMatrix = new Matrix<T>(matrix1.NbLines, matrix2.NbColumns);
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
    
    public static Matrix<T> Transpose(Matrix<T> matrix)
    {
        Matrix<T> newMatrix = new Matrix<T>(matrix);
        return newMatrix.Transpose();
    }
    
    public static Matrix<T> GenerateAugmentedMatrix(Matrix<T> m1, Matrix<T> m2)
    {
        Matrix<T> matrix = new Matrix<T>(m1.NbLines, m1.NbColumns + 1);
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

    public static Matrix<T> InvertByRowReduction(Matrix<T> matrix)
    {
        return matrix.InvertByRowReduction();
    }
    
    public static Matrix<T> SubMatrix(Matrix<T> matrix, int lineRemoved, int columnRemoved)
    {
        return matrix.SubMatrix(lineRemoved, columnRemoved);
    }
    
    public static T Determinant(Matrix<T> matrix)
    {
        if (matrix.NbLines != matrix.NbColumns)
        {
            throw new MatrixInvertException();
        }
        switch (matrix.NbLines)
        {
            case 0:
                return T.One;
            case 1:
                return matrix[0, 0];
            case 2:
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            default:
                T determinant = T.Zero;
                for (int columnIndex = 0; columnIndex < matrix.NbColumns; columnIndex++)
                {
                    determinant += Determinant(SubMatrix(matrix, 0, columnIndex)) 
                                   * (columnIndex % 2 == 0 ? matrix[0, columnIndex] : -matrix[0, columnIndex]);
                }
                return determinant;
        }
    }

    public static Matrix<T> Adjugate(Matrix<T> matrix)
    {
        return matrix.Adjugate();
    }
    
    public static Matrix<T> InvertByDeterminant(Matrix<T> matrix)
    {
        return matrix.InvertByDeterminant();
    }
    #endregion
}
