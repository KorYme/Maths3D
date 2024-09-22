namespace Maths_Matrices.Tests;

public struct Vector3
{
    #region PROPERTIES
    public float x { get; private set; }
    public float y { get; private set; }
    public float z { get; private set; }
    #endregion
    
    #region STATIC PROPERTIES
    public static Vector3 One => new Vector3(1, 1, 1);
    #endregion
    
    #region CONSTRUCTORS
    public Vector3(float x = 0f, float y = 0f, float z = 0f)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public Vector3(Vector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }
    #endregion
    
    #region METHODS
    public Matrix<float> GetMatrix()
    {
        return new Matrix<float>(new float[3, 1] { { x }, { y }, { z }, });
    }
    #endregion
    
    #region OPERATOR OVERRIDES
    public static Vector3 operator +(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }
    
    public static explicit operator Vector3(Matrix<float> matrix)
    {
        if (matrix.NbColumns != 1 || matrix.NbLines != 3)
        {
            throw new Exception("The matrix must be 4x1.");
        }
        return new Vector3(matrix[0,0], matrix[1,0], matrix[2,0]);
    }
    
    public static explicit operator Vector3(Vector4 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }
    #endregion
}