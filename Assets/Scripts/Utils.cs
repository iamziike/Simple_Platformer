static public class Utils
{
    public static bool HasTimeElapsed(float time, float targetTime)
    {
        return targetTime > time;
    }

    public static bool ChangeToBoolValue(object value)
    {
        if (value is int intValue)
        {
            return intValue != 0;
        }
        else if (value is float floatValue)
        {
            return floatValue != 0;
        }
        else if (value is bool boolValue)
        {
            return boolValue;
        }
        return false;
    }
}
