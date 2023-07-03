using System.Diagnostics.Contracts;

namespace Algorithms.Edit_Distance; 

public class Jaro {
    //if (m == 0) 0;
    //else (m/s1.Length + m/s2.Length + (m-t)/m)/3
    // m = 
    // Max(s1.Length, s2.Length)/2 - 1
    [Pure]
    public static double Similarity(ReadOnlySpan<char> s1, ReadOnlySpan<char> s2) {
        if (s1 == s2)
            return 1;

        if (s1 == string.Empty || s2 == string.Empty)
            return 0;

        var matches = 0d;
        var transpositions = 0d;
        var maxDistance = (Math.Max(s1.Length, s2.Length) >> 1) - 1;

        for (int i = 0; i < s2.Length && i<s1.Length; i++) {
            if (Match(s2, s1[i], i, maxDistance)) {
                matches++;
                if(s1[i]==s2[i])
                    continue;
                if ((s2[Math.Max(i - 1, 0)] == s1[i] && s1[Math.Max(i - 1, 0)] == s2[i]))
                    transpositions++;
                if ((s2[Math.Min(i + 1, s2.Length - 1)] == s1[i] && s1[Math.Min(i + 1, s1.Length - 1)] == s2[i])) {
                    i++;
                    matches++;
                    transpositions++;
                }
            }
        }

        return ((matches / s1.Length) + (matches / s2.Length) + ((matches-transpositions) / matches))/3;
    }

    [Pure]
    private static bool Match(ReadOnlySpan<char> s1, char c, int index, int maxDistance) {
        // Search [index, index+maxDistance]
        for (int i = index; i <= Math.Min(index+maxDistance,s1.Length-1); i++) {
            if (s1[i].Equals(c))
                return true;
        }

        // Search [maxDistance-index, index)
        for (int i = Math.Max(index-maxDistance, 0); i < Math.Min(index, s1.Length-1); i++) {
            if (s1[i].Equals(c))
                return true;
        }

        return false;
    }
}