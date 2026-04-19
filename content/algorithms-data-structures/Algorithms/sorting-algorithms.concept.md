---
title: Sorting Algorithms Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page structures the sorting note into a compact map of algorithms and evaluation criteria.

## Core Map

```concept-card
id: sorting-algorithms
term: Sorting Algorithms
children:
- bubble-sort
- selection-sort
- insertion-sort
- merge-sort
- quick-sort
- time-complexity
- stability
- adaptivity
summary:
Sorting algorithms reorder elements into a chosen sequence, usually ascending or descending order.
details:
The algorithms in this section span simple quadratic sorts and faster divide-and-conquer approaches, and they are compared by complexity, stability, and adaptivity.
recall:
- Which algorithms in this section are simple comparison sorts?
- Which criteria are used to compare them?
```

```concept-card
id: bubble-sort
term: Bubble Sort
parents:
- sorting-algorithms
related:
- stability
- adaptivity
summary:
Bubble sort repeatedly swaps adjacent out-of-order elements until larger values bubble toward the end.
details:
It is easy to understand, uses constant extra space, and is stable, but its average and worst-case performance are quadratic.
recall:
- What local operation drives bubble sort?
- Why is bubble sort mostly educational rather than practical?
```

```concept-card
id: selection-sort
term: Selection Sort
parents:
- sorting-algorithms
related:
- time-complexity
summary:
Selection sort repeatedly finds the minimum element in the unsorted portion and swaps it into place.
details:
It uses constant extra space, but its work pattern is not adaptive because it still scans the unsorted remainder on every pass.
recall:
- Why does selection sort remain quadratic even on nearly sorted input?
```

```concept-card
id: insertion-sort
term: Insertion Sort
parents:
- sorting-algorithms
related:
- stability
- adaptivity
summary:
Insertion sort grows a sorted prefix by inserting each next element into its proper position.
details:
It is stable, in-place, and adaptive, which makes it useful on small or nearly sorted datasets.
recall:
- Why is insertion sort good on nearly sorted data?
- Which prefix property does the algorithm maintain?
```

```concept-card
id: merge-sort
term: Merge Sort
parents:
- sorting-algorithms
related:
- time-complexity
- stability
summary:
Merge sort divides the input into halves, recursively sorts them, and merges the sorted halves back together.
details:
It guarantees `O(n log n)` time but needs extra memory for merging.
recall:
- Which step gives merge sort its predictable complexity?
- What is the main space tradeoff?
```

```concept-card
id: quick-sort
term: Quick Sort
parents:
- sorting-algorithms
related:
- time-complexity
summary:
Quick sort partitions the input around a pivot and recursively sorts the resulting partitions.
details:
It is often very fast in practice and uses less extra memory than merge sort, but its worst case is quadratic.
recall:
- What role does the pivot play in quick sort?
- Why can the worst case degrade to `O(n^2)`?
```

```concept-card
id: time-complexity
term: Time Complexity
parents:
- sorting-algorithms
related:
- adaptivity
summary:
Time complexity describes how an algorithm's running time grows as input size increases.
details:
This section compares algorithms by best, average, and worst-case time, which is one of the main reasons merge sort and quick sort are preferred over quadratic sorts on larger inputs.
recall:
- Why do best, average, and worst-case analyses all matter?
```

```concept-card
id: stability
term: Stability
parents:
- sorting-algorithms
related:
- insertion-sort
- bubble-sort
summary:
Stability means equal elements preserve their original relative order after sorting.
details:
This property matters when items carry extra data and a previous ordering should be preserved among equal keys.
recall:
- Why can stability matter in multi-step data processing?
```

```concept-card
id: adaptivity
term: Adaptivity
parents:
- sorting-algorithms
related:
- insertion-sort
- bubble-sort
- selection-sort
summary:
Adaptivity means a sorting algorithm performs better when the input is already partially sorted.
details:
Insertion sort is a strong example because it does less work when elements are already close to the right order, while selection sort is not meaningfully helped by that condition.
recall:
- Which algorithms in this section benefit from nearly sorted input?
- Which one largely ignores that advantage?
```
