using UnityEngine;

// The whole palette in one place. Base hue 234 (blue-violet), triadic: 234 / 354 / 114.
public static class Pal
{
    static Color H(string hex) { ColorUtility.TryParseHtmlString(hex, out Color c); return c; }

    // dark / mid / light of the base hue
    public static readonly Color Panel   = H("#141733");   // lum 0.010
    public static readonly Color Mid     = H("#7F8BFF");   // lum 0.302  (player, buttons)
    public static readonly Color Light   = H("#EEF0FF");   // lum 0.877  (text)
    // triadic accents
    public static readonly Color Hazard  = H("#C81E3A");   // 350 deg, lum 0.135
    public static readonly Color Pickup  = H("#88F27C");   // 114 deg, lum 0.702
    // supporting
    public static readonly Color Gold    = H("#FFD166");   // score, keyboard focus
    public static readonly Color Lag     = H("#FFC4C6");   // lag bar
    public static readonly Color Track   = H("#2B3066");   // empty health track
    public static readonly Color Hover   = H("#A9B2FF");
    public static readonly Color Pressed = H("#7380F5");
    public static readonly Color Disabled= H("#2A2F5E");
    public static readonly Color DimText = H("#C9CEFF");   // text on Disabled, 8.25:1
    public static readonly Color Floor   = H("#232852");
    public static readonly Color Wall    = H("#34397A");
    public static readonly Color Dim     = new Color(0.04f, 0.05f, 0.12f, 0.72f);
    public static readonly Color Flash   = new Color(0.78f, 0.12f, 0.23f, 0.35f);
}
