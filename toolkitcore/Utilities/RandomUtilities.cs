using UnityEngine;

namespace ToolkitCore.Utilities
{
    public static class RandomUtilities
    {
        public static string Color()
        {
            var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            return ColorUtility.ToHtmlStringRGB(color);
        }
    }
}
