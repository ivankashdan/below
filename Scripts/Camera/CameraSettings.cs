//[System.Serializable]
public struct CameraSettings
{
    public float[] Heights;
    public float[] Radii;
    public float[] ScreenYs;

    public CameraSettings(float[] heights, float[] radii, float[] screenYs)
    {
        Heights = heights;
        Radii = radii;
        ScreenYs = screenYs;
    }
}
