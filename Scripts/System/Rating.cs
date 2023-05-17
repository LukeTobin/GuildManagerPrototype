using System.Collections.Generic;

public static class Rating
{
    private static Dictionary<int, char> valueToIntMap = new Dictionary<int, char> {
        { 100,'Z' },
        { 95, 'S' },
        { 85, 'A' },
        { 70, 'B' },
        { 55, 'C' },
        { 40, 'D' },
        { 25, 'E' },
        { 0,  'F' }
    };

    /// <summary>
    /// Get the corresponding rating based on a overall value
    /// </summary>
    /// <param name="value">The value you want a corresponding rating for</param>
    /// <returns>A rating char</returns>
    public static char Get(int value) {
        char result = 'F';
        foreach (KeyValuePair<int, char> kvp in valueToIntMap) {
            if (value >= kvp.Key && value < result) {
                return kvp.Value;
            }
        }
        return 'F';
    }
}