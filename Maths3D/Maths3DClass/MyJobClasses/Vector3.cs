namespace Maths_Matrices.Tests;

public struct Vector3
{
    #region PROPERTIES
    public float x { get; private set; }
    public float y { get; private set; }
    public float z { get; private set; }
    
    public float Magnitude => (float)Math.Sqrt(x * x + y * y + z * z);
    #endregion
    
    #region STATIC PROPERTIES
    public static Vector3 Zero => new Vector3(0, 0, 0);
    public static Vector3 One => new Vector3(1, 1, 1);
    public static Vector3 Right => new Vector3(1, 0, 0);
    public static Vector3 Left => -Right;
    public static Vector3 Forward => new Vector3(0, 1, 0);
    public static Vector3 Backward => -Forward;
    public static Vector3 Up => new Vector3(0, 0, 1);
    public static Vector3 Down => -Up;
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

    public Vector3 Normalized()
    {
        if (Magnitude > 0)
        {
            return new Vector3(this) * (1 / Magnitude);
        }
        else
        {
            return new Vector3();
        }
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

    public static Vector3 operator *(Vector3 vec, float factor)
    {
        return new Vector3(vec.x * factor, vec.y * factor, vec.z * factor);
    }
    
    public static Vector3 operator *(float factor, Vector3 vec)
    {
        return new Vector3(vec.x * factor, vec.y * factor, vec.z * factor);
    }

    public static Vector3 operator /(Vector3 vec, float factor)
    {
        if (factor == 0)
        {
            throw new DivideByZeroException();
        }
        return new Vector3(vec.x / factor, vec.y / factor, vec.z / factor);
    }
    
    public static Vector3 operator -(Vector3 vec)
    {
        return new Vector3(-vec.x, -vec.y, -vec.z);
    }
    #endregion
}