namespace Maths_Matrices.Tests;

public struct Quaternion
{
    #region PROPERTIES
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float w { get; set; }
    #endregion

    #region STATIC PROPERTIES
    public static Quaternion Identity => new(0f, 0f, 0f, 1f);
    #endregion

    #region CONSTRUCTORS
    public Quaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    #endregion
}