namespace Maths_Matrices.Tests;

public struct Quaternion
{
    #region PROPERTIES
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float w { get; set; }

    public Matrix<float> Matrix
    {
        get => new Matrix<float>(new float[4,4]
        {
            { 1-2*y*y-2*z*z, 2*x*y-2*w*z, 2*x*z+2*w*y, 0 },
            { 2*x*y+2*w*z, 1-2*x*x-2*z*z, 2*y*z-2*w*x, 0 },
            { 2*x*z-2*w*y, 2*y*z+2*w*x, 1-2*x*x-2*y*y, 0 },
            { 0, 0, 0, 1 },
        });
    }

    // TODO
    public Vector3 EulerAngles
    {
        get
        {
            float angleY = MathF.Asin(-2 * (x * z - w * y));
            float angleX = MathF.Atan2(2 * (y * z + w * x), 1 - 2 * (y * y + z * z));
            float angleZ = MathF.Atan2(2 * (x * y + w * z), 1 - 2 * (x * x + z * z));
            return new Vector3(angleX, angleY, angleZ) * MathsUtilities.RAD_TO_DEG;
        }
    }

    #endregion

    #region STATIC PROPERTIES
    public static Quaternion Identity => new Quaternion(0f, 0f, 0f, 1f);
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

    #region STATIC METHODS
    public static Quaternion AngleAxis(float angle, Vector3 axis)
    {
        float sin = MathsUtilities.SinWithDeg(angle / 2f);
        float cos = MathsUtilities.CosWithDeg(angle / 2f);
        Vector3 axisNormalized = axis.Normalized();
        return new Quaternion(axisNormalized.x * sin, axisNormalized.y * sin, axisNormalized.z * sin, cos);
    }
    
    public static Quaternion Euler(float xAngle, float yAngle, float zAngle)
    {
        Quaternion Y = AngleAxis(yAngle, Vector3.Forward);
        Quaternion X = AngleAxis(xAngle, Vector3.Right);
        Quaternion Z = AngleAxis(zAngle, Vector3.Up);
        return Y * X * Z;
    }
    #endregion

    #region OPERATOR OVERRIDES
    public static Quaternion operator *(Quaternion q1, Quaternion q2)
    {
        return new Quaternion(q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y,
            q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x,
            q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w,
            q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z);
    }

    public static Vector3 operator *(Quaternion q, Vector3 point)
    {
        return (Vector3)(q.Matrix.SubMatrix(3,3) * point.GetMatrix());
    }
    #endregion
}