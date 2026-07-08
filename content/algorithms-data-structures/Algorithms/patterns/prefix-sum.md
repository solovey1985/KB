# Prefix Sum Pattern

Use a prefix sum when you repeatedly need the aggregate of a **range**: precompute cumulative totals once so any range query becomes an `O(1)` subtraction instead of an `O(n)` re-scan.

## When To Pick This Pattern

Reach for a prefix sum when you notice:

- **many range-sum queries** over an array that does not change (`Range Sum Query`)
- counting or finding **subarrays with a target sum** (`Subarray Sum Equals K`)
- turning a per-range recomputation into one precompute plus `O(1)` lookups
- questions about **balance** — equal counts, pivot indices, sum on each side
- 2D versions: rectangle sums inside a matrix

Ask yourself:

> "Am I recomputing overlapping range totals I could have accumulated once up front?"

### The Core Identity

`sum(i..j) = prefix[j + 1] - prefix[i]`, where `prefix[k]` is the sum of the first `k` elements and `prefix[0] = 0`.

| You need | Technique |
|---|---|
| repeated range sums, static array | precompute a `prefix` array, subtract |
| count subarrays summing to k | prefix sum + a [hash map](hash-map-set.md) of seen prefix counts |
| rectangle sums in a grid | 2D prefix sum with inclusion-exclusion |
| running answer, one pass | keep a rolling sum, no array needed |

### When NOT To Use It

- the array is frequently **updated** between queries — use a Fenwick / segment tree instead
- you need range **min/max** rather than sums (prefixes are not invertible for min/max)
- a single pass already answers it without range reuse
- values can overflow — use `long` for the prefix totals

## Algorithm

Build the cumulative array (or a rolling sum), then answer each range with a single subtraction. For subarray-count problems, walk once while storing how many times each prefix value has occurred and look up the complementary prefix.

```mermaid
flowchart TD
    A[Start: prefix = 0<br/>map: prefix 0 -> count 1] --> B{More elements?}
    B -- No --> Z[Return accumulated answer]
    B -- Yes --> C[prefix += nums at i]
    C --> D{Counting subarrays<br/>summing to k?}
    D -- Yes --> E[answer += count of<br/>prefix - k in map]
    E --> F[map: prefix++ count]
    F --> B
    D -- No --> G[Store prefix in array<br/>for later O(1) range queries]
    G --> B
```

Key discipline: **seed the map with `prefix 0 -> 1`** so subarrays that start at index 0 are counted, and update the running answer **before** inserting the current prefix.

## Code (C#)

### Range Sum Query — precompute then subtract

```csharp
// Answers sum of nums[i..j] inclusive in O(1) after an O(n) build.
public class RangeSum
{
    private readonly long[] prefix; // prefix[k] = sum of first k elements

    // Build: Time O(n), Space O(n).
    public RangeSum(int[] nums)
    {
        prefix = new long[nums.Length + 1]; // prefix[0] = 0 sentinel
        for (int i = 0; i < nums.Length; i++)
        {
            prefix[i + 1] = prefix[i] + nums[i];
        }
    }

    // Query: Time O(1). sum(i..j) = prefix[j+1] - prefix[i].
    public long Query(int i, int j) => prefix[j + 1] - prefix[i];
}
```

### Subarray Sum Equals K — prefix sum + hash map

```csharp
// Counts contiguous subarrays whose sum equals k (values may be negative).
// If prefix - k has been seen c times, c subarrays end here with sum k.
// Time:  O(n) - single pass.
// Space: O(n) - map of prefix-sum frequencies.
public static int SubarraySum(int[] nums, int k)
{
    // Maps a running prefix sum -> how many times it has occurred.
    var counts = new Dictionary<long, int> { [0] = 1 }; // empty prefix
    long prefix = 0;
    int result = 0;

    foreach (int x in nums)
    {
        prefix += x;

        // A prior prefix of (prefix - k) means the gap between sums to k.
        if (counts.TryGetValue(prefix - k, out int seen))
        {
            result += seen;
        }

        // Record this prefix AFTER counting so length-0 spans are excluded.
        counts[prefix] = counts.GetValueOrDefault(prefix) + 1;
    }

    return result;
}
```

### 2D Prefix Sum — rectangle sums by inclusion-exclusion

```csharp
// Immutable matrix region-sum queries.
// sum[r+1,c+1] = value + top + left - topLeft (inclusion-exclusion).
public class Matrix2DSum
{
    private readonly long[,] sum;

    // Build: Time O(m*n), Space O(m*n).
    public Matrix2DSum(int[,] matrix)
    {
        int m = matrix.GetLength(0), n = matrix.GetLength(1);
        sum = new long[m + 1, n + 1]; // padded row/col of zeros

        for (int r = 0; r < m; r++)
            for (int c = 0; c < n; c++)
                sum[r + 1, c + 1] = matrix[r, c]
                                  + sum[r, c + 1]
                                  + sum[r + 1, c]
                                  - sum[r, c];
    }

    // Query rectangle (r1,c1)..(r2,c2) inclusive: Time O(1).
    public long Query(int r1, int c1, int r2, int c2)
    {
        return sum[r2 + 1, c2 + 1]
             - sum[r1, c2 + 1]
             - sum[r2 + 1, c1]
             + sum[r1, c1];
    }
}
```

## Practice Tasks

Work these in rough order of difficulty:

1. **Running Sum of 1d Array** — output the prefix array itself. (rolling sum)
2. **Find Pivot Index** — index where left sum equals right sum. (total minus prefix)
3. **Range Sum Query - Immutable** — many `O(1)` range sums. (precompute prefix)
4. **Subarray Sum Equals K** — count subarrays summing to k. (prefix + [hash map](hash-map-set.md))
5. **Continuous Subarray Sum** — a subarray summing to a multiple of k. (prefix mod in a map)
6. **Contiguous Array** — longest subarray with equal 0s and 1s. (map first-seen prefix, treat 0 as -1)
7. **Subarray Sums Divisible by K** — count subarrays with sum divisible by k. (prefix remainder counts)
8. **Range Sum Query 2D - Immutable** — rectangle sums in `O(1)`. (2D prefix + inclusion-exclusion)
9. **Maximum Size Subarray Sum Equals k** — longest subarray summing to k. (map earliest prefix index)

## Related Patterns

- [Hash Map / Hash Set](hash-map-set.md) — pairs with prefix sums to count or locate target-sum subarrays.
- [Sliding Window](sliding-window.md) — the alternative for contiguous sums when all values are non-negative.
- [Two Pointers](two-pointers.md) — another way to sweep ranges without recomputation on sorted or monotonic data.
