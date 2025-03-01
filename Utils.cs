using System.Text.RegularExpressions;

class Utils {
    public static string Clean(string s) {
        return Regex.Replace(s, "[^a-zA-Z0-9]", "").ToLower();
    }
}