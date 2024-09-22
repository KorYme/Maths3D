namespace Maths_Matrices.Tests;

public struct Vector4
{
    #region PROPERTIES
    public float x { get; private set; }
    public float y { get; private set; }
    public float z { get; private set; }
    public float w { get; private set; }
    #endregion
    
    #region CONSTRUCTORS
    public Vector4(float x = 0f, float y = 0f, float z = 0f, float w = 0f)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    
    public Vector4(Vector4 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
        this.w = v.w;
    }
    #endregion

    #region METHODS
    public Matrix<float> GetMatrix()
    {
        return new Matrix<float>(new float[4, 1] { { x }, { y }, { z }, { w } });
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

    public static explicit operator Vector4(Vector3 vector)
    {
        return new Vector4(vector.x, vector.y, vector.z);
    }
    #endregion
    
    #region STATIC METHODS
    
    #endregion
}