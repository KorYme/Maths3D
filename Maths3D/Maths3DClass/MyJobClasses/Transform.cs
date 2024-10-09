using System.Numerics;

namespace Maths_Matrices.Tests;

public class Transform
{
    #region PROPERTIES
    public Transform Parent { get; private set; }
    public Vector3 LocalPosition { get; set; }
    public Vector3 LocalRotation { get; set; }
    public Vector3 LocalScale { get; set; } = Vector3.One;

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
    
    public Quaternion LocalRotationQuaternion
    {
        get => Quaternion.Euler(LocalRotation.x, LocalRotation.y, LocalRotation.z);
        set
        {
            LocalRotation = value.EulerAngles;
        }
    }

    public Matrix<float> LocalScaleMatrix
    {
        get => new Matrix<float>(new float[4, 4]
        {
            { LocalScale.x, 0f, 0f, 0f },
            { 0f, LocalScale.y, 0f, 0f },
            { 0f, 0f, LocalScale.z, 0f },
            { 0f, 0f, 0f, 1f },
        });
    }

    public Matrix<float> LocalToWorldMatrix
    {
        get => (Parent == null ? Matrix<float>.Identity(4) : Parent.LocalToWorldMatrix) 
               * LocalTranslationMatrix * LocalRotationMatrix * LocalScaleMatrix;
    }
    
    public Matrix<float> WorldToLocalMatrix
    {
        get => LocalToWorldMatrix.InvertByDeterminant();
    }

    public Vector3 WorldPosition
    {
        get
        {
            Matrix<float> transformMatrix = LocalToWorldMatrix;
            return new Vector3(transformMatrix[0,3], transformMatrix[1,3], transformMatrix[2,3]);
        }
        set
        {
            LocalPosition = (Vector3)(WorldToLocalMatrix.SubMatrix(3,3) 
                                      * (value.GetMatrix() - LocalToWorldMatrix.SubMatrix(3,0).GetColumn(2)));
        }
    }
    #endregion

    #region METHODS
    public void SetParent(Transform tParent)
    {
        Parent = tParent;
    }
    #endregion
}