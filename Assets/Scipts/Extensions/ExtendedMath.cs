using UnityEngine;

public static class ExtendedMath
{
    public static float generateNormalRandom(float mu, float sigma)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }

    public static float Sigmoid(float value)
    {
        return (1.0f / (1.0f + (float) Mathf.Exp(-value)));
    }

    public static float Map(float num, float fromMin, float fromMax, float toMin, float toMax)
    {
        float newNum;
        newNum = (num - fromMin) * ((toMax-toMin)/(fromMax-fromMin)) + toMin;
        return newNum;
    }

    public static double Map(double num, double fromMin, double fromMax, double toMin, double toMax)
    {
        double newNum;
        newNum = (num - fromMin) * ((toMax-toMin)/(fromMax-fromMin)) + toMin;
        return newNum;
    }
}