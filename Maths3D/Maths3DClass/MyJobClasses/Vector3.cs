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
}