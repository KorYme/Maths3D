namespace Maths_Matrices.Tests;

public class Transform
{
    #region PROPERTIES
    public Vector3 LocalPosition { get; set; }
    public Vector3 LocalRotation { get; set; }

    public Matrix<float> LocalTranslationMatrix
    {
        get => new Matrix<float>(new float[4, 4]
        {
            { 1f, 0f, 0f, LocalPosition.x },
            { 0f, 1f, 0f, LocalPosition.y },
            { 0f, 0f, 1f, LocalPosition.z },
            { 0f, 0f, 0f, 1f },
        });
    }

    // TO DO²
    public Matrix<float> LocalRotationXMatrix
    {
        get
        {
            float cosAngle = MathsUtilities.CosWithDeg(LocalRotation.x);
            float sinAngle = MathsUtilities.SinWithDeg(LocalRotation.x);
            return new Matrix<float>(new float[4, 4]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, cosAngle, -sinAngle, 0f },
                { 0f, sinAngle, cosAngle, 0f },
                { 0f, 0f, 0f, 1f },
            });
        } 
    }
    
    public Matrix<float> LocalRotationYMatrix
    {
        get
        {
            float cosAngle = MathsUtilities.CosWithDeg(LocalRotation.y);
            float sinAngle = MathsUtilities.SinWithDeg(LocalRotation.y);
            return new Matrix<float>(new float[4, 4]
            {
                { cosAngle, 0f, sinAngle, 0f },
                { 0f, 1f, 0f, 0f },
                { -sinAngle, 0f, cosAngle, 0f },
                { 0f, 0f, 0f, 1f },
            });
        } 
    }
    
    public Matrix<float> LocalRotationZMatrix
    {
        get
        {
            float cosAngle = MathsUtilities.CosWithDeg(LocalRotation.z);
            float sinAngle = MathsUtilities.SinWithDeg(LocalRotation.z);
            return new Matrix<float>(new float[4, 4]
            {
                { cosAngle, -sinAngle, 0f, 0f },
                { sinAngle, cosAngle, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });
        } 
    }
    
    public Matrix<float> LocalRotationMatrix
    {
        get => LocalRotationYMatrix * LocalRotationXMatrix * LocalRotationZMatrix;
    }
    #endregion
    
    #region CONSTRUCTORS
    #endregion
}