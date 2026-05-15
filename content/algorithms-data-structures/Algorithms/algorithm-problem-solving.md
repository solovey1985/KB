# Algorithm Problem-Solving Guide

This guide explains how to approach common interview-style algorithm tasks, how to pick the right pattern, and how to communicate your reasoning while coding.

## Goal In An Interview

Most interviewers are not checking whether you have memorized a specific LeetCode problem. They usually want to see whether you can:

- understand the problem precisely
- identify the core data shape and constraints
- start with a correct baseline solution
- improve it when a better pattern exists
- explain tradeoffs and edge cases
- write code that is correct and testable

## Standard Solving Workflow

Use this sequence on almost every problem.

### 1. Clarify The Task

Before coding, restate the problem in your own words.

Ask or confirm:

- what exactly must be returned
- whether input can be empty
- whether values can repeat
- whether the input is already sorted
- whether order matters in the output
- expected time or memory limits
- whether you may modify the input

Example:

> "We need the indices of two numbers whose sum equals the target, not the numbers themselves. The array is unsorted, values may repeat, and we want one valid pair."

### 2. Identify The Data Shape

Ask what kind of structure the problem is really about.

- array or string
- linked list
- tree or BST
- graph or grid
- stream of data
- intervals or ranges
- combinations or search space

That usually narrows the candidate patterns immediately.

### 3. Notice The Signal Words

Common phrases often point to specific patterns.

| Signal in the task | Likely pattern |
|---|---|
| pair, complement, frequency, duplicate | hash map / hash set |
| contiguous subarray, substring, window | sliding window |
| sorted array, first true, minimum feasible | binary search |
| next greater, valid parentheses | stack |
| top K, smallest/largest repeatedly | heap |
| hierarchy, parent-child, depth | tree DFS/BFS |
| islands, dependencies, shortest path | graph traversal |
| all combinations, generate all | backtracking |
| best total, minimum cost, count ways | dynamic programming |
| overlapping time ranges | sorting + intervals |

### 4. Start With The Brute-Force Idea

Give the simplest correct solution first when the optimal path is not immediate.

That shows:

- you understand the problem
- you can produce a correct answer
- you know what inefficiency you are trying to remove

Example:

- Two Sum brute force: check every pair, `O(n^2)` time, `O(1)` extra space.
- Then optimize with a hash map to `O(n)` time.

### 5. Pick The Improvement Lever

Most optimizations come from one of these levers:

- trade space for time with a hash map or set
- exploit sorted order with binary search or two pointers
- reuse previous work with dynamic programming
- avoid recomputation with memoization
- process items by priority using a heap
- model connectivity with BFS or DFS
- shrink the search space with pruning or backtracking

### 6. State Complexity Before Coding

Say what you expect before implementation.

Example:

> "I’ll use a hash map from value to index. That should give `O(n)` time and `O(n)` space."

This helps the interviewer follow your reasoning and catches weak choices early.

### 7. Code In Small Verified Steps

Write the main control flow first:

- loop or recursion skeleton
- core condition
- data structure updates
- return conditions

Do not try to type the whole perfect solution in one pass.

### 8. Dry-Run The Solution

Test with:

- one normal case
- one edge case
- one tricky case with duplicates, negatives, empty input, or single-element input

### 9. Mention Follow-Ups

After solving the base problem, show awareness of variations:

- what changes if the array is sorted
- what changes if data arrives as a stream
- what changes if memory is limited
- what changes if we need all answers instead of one

## How To Pick The Right Pattern

### Hash Map / Hash Set

Use when the problem asks about:

- lookup by value
- duplicate detection
- complements like `target - x`
- frequency counting
- grouping by key

Typical tasks:

- Two Sum
- Contains Duplicate
- Group Anagrams
- Top K Frequent Elements

Question to ask yourself:

> "Am I repeatedly searching for something I could remember in `O(1)` average time?"

### Two Pointers

Use when:

- the data is sorted
- you need a pair that satisfies a condition
- you want to compress or partition in place
- the problem naturally compares left and right ends

Typical tasks:

- Two Sum on sorted input
- remove duplicates from sorted array
- container with most water
- merge sorted arrays

Question to ask yourself:

> "Can I move one pointer to make progress without revisiting everything?"

### Sliding Window

Use when the problem involves:

- a contiguous segment
- longest or shortest valid subarray or substring
- maintaining a running condition while moving through the input

Typical tasks:

- longest substring without repeating characters
- minimum window substring
- maximum sum subarray of size `k`

Question to ask yourself:

> "Is the answer always some contiguous window I can expand and shrink?"

### Binary Search

Use when:

- the input is sorted
- the answer space is ordered
- you need the first or last valid value
- feasibility changes monotonically from false to true

Typical tasks:

- first bad version
- search in sorted array
- minimum capacity to ship packages

Question to ask yourself:

> "Can I define a monotonic condition so half the candidates can be discarded each step?"

### Stack

Use when:

- the problem needs matching or nesting
- you need last-in-first-out behavior
- you need previous/next greater or smaller elements

Typical tasks:

- valid parentheses
- daily temperatures
- largest rectangle in histogram

Question to ask yourself:

> "Do I need to remember unresolved items in order and resolve them later?"

### Heap / Priority Queue

Use when:

- you repeatedly need the smallest or largest item
- you need top `k`
- you need best-first exploration

Typical tasks:

- kth largest element
- merge `k` sorted lists
- top `k` frequent elements

Question to ask yourself:

> "Do I need efficient repeated access to the current min or max?"

### DFS / BFS On Trees And Graphs

Use when:

- there is hierarchy or connectivity
- the problem asks about reachability, levels, or components
- you need shortest path in an unweighted graph or grid

Typical tasks:

- max depth of binary tree
- number of islands
- course schedule
- shortest path in a maze with equal edge cost

Question to ask yourself:

> "Is this really a traversal problem over nodes and edges?"

### Backtracking

Use when:

- you must generate all valid possibilities
- each step branches into choices
- invalid paths can be pruned early

Typical tasks:

- permutations
- subsets
- combination sum
- N-Queens

Question to ask yourself:

> "Am I exploring a decision tree of choices and undoing steps as I return?"

### Dynamic Programming

Use when:

- the problem asks for an optimal value, count, or decision
- smaller subproblems repeat
- the brute-force recursion recalculates the same states

Typical tasks:

- climbing stairs
- coin change
- house robber
- longest increasing subsequence

Question to ask yourself:

> "Does the answer depend on a small state I can define and reuse?"

## Worked Examples

## Example 1: Two Sum

Task:

> Given an unsorted integer array and a target, return indices of two numbers whose sum equals the target.

### How To Think

1. Brute force is checking every pair.
2. The repeated need is: for current number `x`, have we already seen `target - x`?
3. That is a direct hash map signal.

### Solution Choice

- pattern: hash map
- time: `O(n)`
- space: `O(n)`

```csharp
public static int[] TwoSum(int[] nums, int target)
{
    var seen = new Dictionary<int, int>();

    for (int i = 0; i < nums.Length; i++)
    {
        int complement = target - nums[i];
        if (seen.TryGetValue(complement, out int index))
        {
            return new[] { index, i };
        }

        seen[nums[i]] = i;
    }

    return Array.Empty<int>();
}
```

## Example 2: Longest Substring Without Repeating Characters

Task:

> Find the length of the longest substring without duplicate characters.

### How To Think

1. The answer is a contiguous substring.
2. We need to expand and shrink a valid region.
3. That is a sliding window signal.

### Solution Choice

- pattern: sliding window + hash set / map
- time: `O(n)`
- space: `O(k)` where `k` is the number of distinct characters in the window

```csharp
public static int LengthOfLongestSubstring(string s)
{
    var lastSeen = new Dictionary<char, int>();
    int left = 0;
    int best = 0;

    for (int right = 0; right < s.Length; right++)
    {
        char current = s[right];
        if (lastSeen.TryGetValue(current, out int previousIndex) && previousIndex >= left)
        {
            left = previousIndex + 1;
        }

        lastSeen[current] = right;
        best = Math.Max(best, right - left + 1);
    }

    return best;
}
```

## Example 3: Search In Sorted Array

Task:

> Find the target value in a sorted array.

### How To Think

1. The array is sorted.
2. We can discard half the array at each step.
3. That is the clearest binary search signal.

### Solution Choice

- pattern: binary search
- time: `O(log n)`
- space: `O(1)`

```csharp
public static int BinarySearch(int[] nums, int target)
{
    int left = 0;
    int right = nums.Length - 1;

    while (left <= right)
    {
        int middle = left + (right - left) / 2;

        if (nums[middle] == target)
        {
            return middle;
        }

        if (nums[middle] < target)
        {
            left = middle + 1;
        }
        else
        {
            right = middle - 1;
        }
    }

    return -1;
}
```

## Example 4: Number Of Islands

Task:

> Given a grid of `1`s and `0`s, count how many connected land components exist.

### How To Think

1. The grid is really a graph.
2. We need to count connected components.
3. BFS or DFS is the right pattern.

### Solution Choice

- pattern: graph traversal
- time: `O(rows * cols)`
- space: `O(rows * cols)` in the worst case

```csharp
public static int NumIslands(char[][] grid)
{
    int count = 0;

    for (int row = 0; row < grid.Length; row++)
    {
        for (int col = 0; col < grid[row].Length; col++)
        {
            if (grid[row][col] == '1')
            {
                count++;
                FloodFill(grid, row, col);
            }
        }
    }

    return count;
}

private static void FloodFill(char[][] grid, int row, int col)
{
    if (row < 0 || row >= grid.Length || col < 0 || col >= grid[row].Length || grid[row][col] != '1')
    {
        return;
    }

    grid[row][col] = '0';

    FloodFill(grid, row + 1, col);
    FloodFill(grid, row - 1, col);
    FloodFill(grid, row, col + 1);
    FloodFill(grid, row, col - 1);
}
```

## Example 5: Coin Change

Task:

> Given coin values and a target amount, return the minimum number of coins needed.

### How To Think

1. Brute force tries all combinations recursively.
2. The same remaining amounts appear again and again.
3. That repeated-state signal points to dynamic programming.

### Solution Choice

- pattern: dynamic programming
- time: `O(amount * numberOfCoins)`
- space: `O(amount)`

```csharp
public static int CoinChange(int[] coins, int amount)
{
    int[] dp = Enumerable.Repeat(amount + 1, amount + 1).ToArray();
    dp[0] = 0;

    for (int current = 1; current <= amount; current++)
    {
        foreach (int coin in coins)
        {
            if (coin <= current)
            {
                dp[current] = Math.Min(dp[current], dp[current - coin] + 1);
            }
        }
    }

    return dp[amount] > amount ? -1 : dp[amount];
}
```

## A Fast Decision Checklist

When a problem appears, ask these questions in order:

1. Is the answer about pairs, duplicates, frequencies, or complements?
   Use a hash map or set.
2. Is the answer about a contiguous subarray or substring?
   Try sliding window.
3. Is the input sorted or is the answer space monotonic?
   Try binary search or two pointers.
4. Do I need repeated min, max, or top `k`?
   Try a heap.
5. Is it hierarchy, connectivity, or shortest path in an unweighted graph?
   Try DFS or BFS.
6. Am I generating all valid possibilities?
   Try backtracking.
7. Am I optimizing a repeated subproblem?
   Try dynamic programming.

## Common Interview Mistakes

- coding before confirming what the output should be
- missing edge cases like empty input or duplicates
- choosing dynamic programming too early without defining the state clearly
- using recursion without considering stack depth
- forgetting whether the algorithm may modify the input
- giving the optimal solution without explaining how you got there
- stating the wrong space complexity for hash-based solutions

## A Good Answer Shape Out Loud

Use this structure when speaking to the interviewer:

1. Restate the task.
2. Give the brute-force solution and its complexity.
3. Explain the pattern that improves it.
4. State the final complexity.
5. Code.
6. Dry-run with an example.
7. Mention edge cases and follow-ups.

Example:

> "A brute-force solution checks every pair in `O(n^2)`. Since we only need to know whether we have already seen the complement, I can store visited values in a hash map. That reduces the time to `O(n)` with `O(n)` extra space."

## What To Practice Next

After learning this workflow, practice one problem from each pattern family:

- hashing
- sliding window
- two pointers
- binary search
- stack
- heap
- tree traversal
- graph traversal
- backtracking
- dynamic programming

That gives much better coverage than solving many variations of the same pattern.