namespace Maths_Matrices.Tests;

public class Transform
{
    #region PROPERTIES
    public Vector3 LocalPosition { get; set; }

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
    #endregion
    
    #region CONSTRUCTORS
    #endregion
}