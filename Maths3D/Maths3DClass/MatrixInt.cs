namespace Maths_Matrices.Tests;

public struct MatrixInt
{
    private int[,] m_matrix;
    
    public int RowsCount => m_matrix.GetLength(0);
    public int ColumnsCount => m_matrix.GetLength(1);

    public MatrixInt(int[,] matrix)
    {
        m_matrix = matrix;
    }

    public int this[int rowIndex, int columnIndex]
    {
        get
        {
            if (rowIndex < 0 || rowIndex >= RowsCount || columnIndex < 0 || columnIndex >= ColumnsCount)
            {
                throw new IndexOutOfRangeException();
            }
            return m_matrix[rowIndex, columnIndex];
        } 
    }
    
    public int[,] ToArray2D()
    {
        return m_matrix;
    }

    public MatrixInt Transpose()
    {
        int[,] tmp = new int[ColumnsCount, RowsCount];
        for (int rowIndex = 0; rowIndex < RowsCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < ColumnsCount; columnIndex++)
            {
                int tmpValue = this[rowIndex, columnIndex];
                tmp[columnIndex, rowIndex] = tmpValue;
            }
        }
        return new MatrixInt(tmp);
    }

    public static MatrixInt Transpose(MatrixInt matrix)
    {
        matrix = matrix.Transpose();
        return matrix;
    }
}