namespace Algorithms.Edit_Distance; 

public class Levenshtein {
    // |                            Method | Size |       Mean |      Error |     StdDev | Ratio | RatioSD |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
    // |---------------------------------- |----- |-----------:|-----------:|-----------:|------:|--------:|--------:|--------:|--------:|----------:|
    // | LevenshteinDistanceIterativeNaive |   10 |   4.213 us |  0.0439 us |  0.0410 us |  1.00 |    0.00 |  0.4654 |       - |       - |    1976 B |
    // |      LevenshteinDistanceIterative |   10 |   3.517 us |  0.0262 us |  0.0219 us |  0.83 |    0.01 |  0.1030 |       - |       - |     440 B |
    // |      LevenshteinDistanceRecursive |   10 |   8.231 us |  0.1353 us |  0.1266 us |  1.95 |    0.04 |  0.4730 |       - |       - |    2000 B |
    // |                                   |      |            |            |            |       |         |         |         |         |           |
    // | LevenshteinDistanceIterativeNaive |  100 | 382.647 us |  3.5987 us |  3.1902 us |  1.00 |    0.00 | 49.8047 | 49.8047 | 49.8047 |  163273 B |
    // |      LevenshteinDistanceIterative |  100 | 317.166 us |  3.7267 us |  3.3036 us |  0.83 |    0.01 |  0.4883 |       - |       - |    3320 B |
    // |      LevenshteinDistanceRecursive |  100 | 838.354 us | 12.7880 us | 11.3362 us |  2.19 |    0.03 | 49.8047 | 49.8047 | 49.8047 |  163297 B |
    
    public static decimal Distance(string from, string to, decimal substitutionCost = 1, decimal deletionCost = 0.5m, decimal insertionCost = 0.5m) => 
        Distance(from.AsSpan(), to.AsSpan(), substitutionCost, deletionCost, insertionCost);

    // Space complexity O(n*m)
    public static decimal DistanceIterativeNaive<T>(ReadOnlySpan<T> from,
                                   ReadOnlySpan<T> to,
                                   decimal substitutionCost = 1,
                                   decimal deletionCost = 0.5m,
                                   decimal insertionCost = 0.5m) where T : IEquatable<T> {
        var m = new Decimal[to.Length+1, from.Length+1];

        // init the top and left of the matrix
        for (var i = 1; i < from.Length + 1; i++)
            m[0, i] = deletionCost * i;

        for (int i = 1; i < to.Length+1; i++)
            m[i, 0] = insertionCost * i;

        for (int i = 1; i < m.GetLength(0); i++) {
            for (int j = 1; j < m.GetLength(1); j++) {
                if (from[j-1].Equals(to[i-1])) {
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
    
    // Optimizing the space complexity from the naive approach from 0(n*m) to O(n)
    // If you go L -> R you only need the left - left diag - up boxes
    public static decimal Distance<T>(ReadOnlySpan<T> from,
                                   ReadOnlySpan<T> to,
                                   decimal substitutionCost = 1,
                                   decimal deletionCost = 0.5m,
                                   decimal insertionCost = 0.5m) where T : IEquatable<T> {
        if (from.Length == 0)
            return to.Length * insertionCost;
        if (to.Length == 0)
            return from.Length * deletionCost;
        
        Decimal[][] m = new []{new Decimal[from.Length+1],new Decimal[from.Length+1]};

        // init the top of the matrix
        for (var i = 1; i < m[0].Length; i++)
            m[0][i] = deletionCost * i;
        
        for (int i = 1; i < to.Length+1; i++) {
            m[1][0] = insertionCost * i; // fill the left most square before we operate
            for (int j = 1; j < m[0].Length; j++) {
                if (from[j-1].Equals(to[i-1])) {
                    m[1][j] = m[0][j-1]; //get the cost of the last operation
                    continue;
                }

                m[1][j] = Math.Min( 
                    Math.Min(
                        // insert left
                        m[1][j - 1] + insertionCost,
                        // delete up
                        m[0][j] + deletionCost
                    ),
                    // substitute top left
                    m[0][j - 1] + substitutionCost
                );
            }
            
            // Take the current output and set it to the historical
            (m[0], m[1]) = (m[1], m[0]); 
        }

        // Take off the "top" because of the swap to facilitate the smaller amount of space complexity
        return m[0][^1];
    }
    
    // Memoized recursive Solution
    // Memo needs to have a global variable, so it is not able to be static like the other impls
    private decimal[,]? _memo;
    public decimal DistanceRecursive<T>(ReadOnlySpan<T> from,
                                        ReadOnlySpan<T> to,
                                        decimal substitutionCost = 1,
                                        decimal deletionCost = 0.5m,
                                        decimal insertionCost = 0.5m) where T : IEquatable<T> {
        _memo = new decimal[from.Length+1,to.Length+1];
        for (int i = 1; i < to.Length+1; i++)
            _memo[0, i] = deletionCost * i;

        for (int i = 1; i < from.Length + 1; i++) {
            _memo[i, 0] = insertionCost * i;
            for (int j = 1; j < to.Length+1; j++) {
                _memo[i, j] = -1;
            }
        }

        return DistanceRecursive_DFS(from, to, substitutionCost, deletionCost, insertionCost);
    }
    
    private decimal DistanceRecursive_DFS<T>(ReadOnlySpan<T> from,
                                      ReadOnlySpan<T> to,
                                      decimal substitutionCost = 1,
                                      decimal deletionCost = 0.5m,
                                      decimal insertionCost = 0.5m) where T : IEquatable<T> {
        if (from.Length == 0)
            return to.Length * insertionCost;
        if (to.Length == 0)
            return from.Length * deletionCost;

        if (_memo![_memo.GetLength(0) - from.Length, _memo.GetLength(1) - to.Length] > -1)
            return _memo[_memo.GetLength(0) - from.Length, _memo.GetLength(1) - to.Length];

        decimal output;
        if (from[0].Equals(to[0])) {
            output = 0 + DistanceRecursive_DFS<T>(from.Slice(1),to.Slice(1),substitutionCost,deletionCost,insertionCost); //get the cost of the last operation
        } else
            output = Math.Min(
                Math.Min(
                    (to.Length>1)? insertionCost + DistanceRecursive_DFS<T>(from,to.Slice(1),substitutionCost,deletionCost,insertionCost)
                        : insertionCost + DistanceRecursive_DFS<T>(from,Array.Empty<T>(),substitutionCost,deletionCost,insertionCost),
                    
                    (from.Length>1)? deletionCost + DistanceRecursive_DFS<T>(from.Slice(1),to,substitutionCost,deletionCost,insertionCost)
                        : deletionCost + DistanceRecursive_DFS<T>(Array.Empty<T>(),to,substitutionCost,deletionCost,insertionCost)

                ),
                // substitute
                (to.Length > 1 && from.Length > 1)? substitutionCost + DistanceRecursive_DFS<T>(from.Slice(1), to.Slice(1), substitutionCost,deletionCost,insertionCost) 
                    : substitutionCost);

        return _memo[_memo.GetLength(0) - from.Length, _memo.GetLength(1) - to.Length] = output;
    }
}