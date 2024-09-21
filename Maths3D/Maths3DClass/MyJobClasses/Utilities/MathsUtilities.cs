namespace Maths_Matrices.Tests;

public static class MathsUtilities
{
    public const float RAD_TO_DEG = 180.0f / MathF.PI;
    public const float DEG_TO_RAD = MathF.PI / 180.0f;
    
    public static float CosWithDeg(float angleInDegree)
    {
        return MathF.Cos(angleInDegree * DEG_TO_RAD);
    }
    
    public static float SinWithDeg(float angleInDegree)
    {
        return MathF.Sin(angleInDegree * DEG_TO_RAD);
    }
}