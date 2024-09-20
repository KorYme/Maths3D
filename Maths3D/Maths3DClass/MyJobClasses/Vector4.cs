namespace Maths_Matrices.Tests;

public struct Vector4
{
    private float m_x, m_y, m_z, m_w;

    #region PROPERTIES
    public float x => m_x;
    public float y => m_y;
    public float z => m_z;
    public float w => m_w;
    #endregion
    
    #region CONSTRUCTORS
    public Vector4(float x, float y, float z, float w)
    {
        m_x = x;
        m_y = y;
        m_z = z;
        m_w = w;
    }
    
    public Vector4(Vector4 v)
    {
        m_x = v.x;
        m_y = v.y;
        m_z = v.z;
        m_w = v.w;
    }
    #endregion

    #region METHODS
    public Matrix<float> GetMatrix()
    {
        return new Matrix<float>(new float[4, 1] { { m_x }, { m_y }, { m_z }, { m_w } });
    }
    #endregion
    
    #region OPERATORS OVERRIDES

    public static Vector4 operator *(Vector4 v, Matrix<float> matrix)
    {
        Matrix<float> vectorRepresentation = v.GetMatrix();
        return (Vector4)(matrix * vectorRepresentation);
    }
    
    public static Vector4 operator *(Matrix<float> matrix, Vector4 v)
    {
        return v * matrix;
    }
    
    public static explicit operator Vector4(Matrix<float> matrix)
    {
        if (matrix.NbColumns != 1 || matrix.NbLines != 4)
        {
            throw new Exception("The matrix must be 4x1.");
        }
        return new Vector4(matrix[0,0], matrix[1,0], matrix[2,0], matrix[3,0]);
    }
    #endregion
    
    #region STATIC METHODS
    
    #endregion
}