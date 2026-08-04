# Algorithm Patterns

A pattern-per-page reference for solving interview-style algorithm problems. Start from the [Algorithm Problem-Solving Guide](../algorithm-problem-solving.md) to classify a problem, then open the matching pattern page for the decision heuristics, a mermaid algorithm sketch, commented C# code, and practice tasks.

## How To Use These Pages

Every page has the same shape:

1. **When To Pick This Pattern** — the signals that point here, an "ask yourself" test, and when *not* to use it.
2. **Algorithm** — the control flow as a mermaid diagram.
3. **Code (C#)** — idiomatic, commented reference implementations with complexity.
4. **Practice Tasks** — problems in increasing difficulty.
5. **Related Patterns** — links to patterns that combine or compete with this one.

## Pattern Catalogue

### Arrays, Strings & Linear Scans

- [Hash Map / Hash Set](hash-map-set.md) — `O(1)` lookups: complements, duplicates, frequencies, grouping.
- [Two Pointers](two-pointers.md) — sorted-input pairs and in-place partitioning without extra space.
- [Sliding Window](sliding-window.md) — best/longest/shortest contiguous subarray or substring.
- [Fast & Slow Pointers](fast-slow-pointers.md) — cycle detection and finding the middle of a list.
- [Prefix Sum](prefix-sum.md) — range sums and subarray-sum counting.

### Searching & Ordering

- [Binary Search](binary-search.md) — sorted data and binary search on a monotonic answer space.
- [Stack & Monotonic Stack](stack-monotonic.md) — nesting/matching and next-greater/smaller queries.
- [Heap / Priority Queue](heap-priority-queue.md) — repeated min/max access, top-k, and merging.
- [Intervals](intervals.md) — merging, inserting, and scheduling overlapping ranges.

### Trees & Graphs

- [Tree Traversal (DFS & BFS)](tree-traversal.md) — depth, order, and level traversal of trees.
- [Graph Traversal (DFS & BFS)](graph-traversal.md) — connectivity, components, and unweighted shortest paths.
- [Topological Sort](topological-sort.md) — ordering nodes of a DAG and detecting cycles.
- [Union-Find (Disjoint Set)](union-find.md) — dynamic connectivity and component counting.
- [Trie (Prefix Tree)](trie.md) — prefix queries, dictionaries, and autocomplete.

### Choice & Optimization

- [Backtracking](backtracking.md) — enumerate all valid combinations with pruning.
- [Dynamic Programming](dynamic-programming.md) — optimize/count over overlapping subproblems.
- [Greedy](greedy.md) — take the locally optimal choice when it is provably global.
- [Bit Manipulation](bit-manipulation.md) — XOR tricks, masks, and bit-level state.

## Fast Selection Checklist

Ask these in order when a problem appears:

1. Pairs, duplicates, frequencies, complements? → [Hash Map / Set](hash-map-set.md)
2. Contiguous subarray or substring? → [Sliding Window](sliding-window.md)
3. Sorted input or monotonic answer space? → [Binary Search](binary-search.md) or [Two Pointers](two-pointers.md)
4. Repeated min/max or top-k? → [Heap](heap-priority-queue.md)
5. Hierarchy, connectivity, or shortest path (unweighted)? → [Tree](tree-traversal.md) / [Graph](graph-traversal.md) traversal
6. Ordering with dependencies? → [Topological Sort](topological-sort.md)
7. Generating all valid possibilities? → [Backtracking](backtracking.md)
8. Optimizing a repeated subproblem? → [Dynamic Programming](dynamic-programming.md)
