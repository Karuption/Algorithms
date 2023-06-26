namespace Algorithms.Levenshtein_distance; 

public class Levenshtein {
    public static decimal Distance(string from,
                               string to,
                               decimal substitutionCost = 1,
                               decimal deletionCost = 0.5m,
                               decimal insertionCost = 0.5m) {
        var m = new Decimal[to.Length+1, from.Length+1];

        // init the top and left of the matrix
        for (var i = 1; i < from.Length + 1; i++)
            m[0, i] = deletionCost * i;

        for (int i = 1; i < to.Length+1; i++)
            m[i, 0] = insertionCost * i;

        for (int i = 1; i < m.GetLength(0); i++) {
            for (int j = 1; j < m.GetLength(1); j++) {
                if (from[j-1] == to[i-1]) {
                    m[i,j] = m[i-1, j-1]; //get the cost of the last operation
                    continue;
                }

                m[i,j] = Math.Min( 
                    Math.Min(
                        // insert
                        m[i, j - 1] + insertionCost,
                        // delete
                        m[i - 1, j] + deletionCost
                    ),
                    // substitute tail, tail
                    m[i - 1, j - 1] + substitutionCost
                );
            }
        }

        return m[m.GetLength(0) - 1, m.GetLength(1) - 1];
    }
}