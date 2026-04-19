---
title: Sorting Algorithms Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the tradeoffs and properties of the sorting algorithms covered in this section:

- time and space complexity
- stability and adaptivity
- simple sorts versus divide-and-conquer sorts
- implementation patterns in C#

## Foundations

```interview-question
What does it mean for a sorting algorithm to be stable?
---
answer:
A sorting algorithm is stable if equal elements keep their original relative order after sorting.

This matters when later sorting passes or attached metadata depend on preserving that order.
hints:
- Think about two equal keys with different attached records.
- Order among equal values is the key detail.
- Stability is not about speed.
```

```interview-question
Why is insertion sort often considered adaptive?
---
answer:
Insertion sort performs especially well on nearly sorted data because it shifts only small amounts of misplaced data.

That means its best case can approach linear behaviour on already or almost sorted input.
hints:
- Think nearly sorted input.
- The algorithm grows a sorted prefix.
- It does less work when elements are already close to their final positions.
```

```interview-question
How does merge sort differ from quick sort at a high level?
---
answer:
Both are divide-and-conquer algorithms, but merge sort divides first and then merges sorted halves, while quick sort partitions around a pivot and recursively sorts the partitions.

Merge sort has predictable `O(n log n)` worst-case time but uses extra memory. Quick sort is often fast in practice but has an `O(n^2)` worst case.
hints:
- One merges halves, the other partitions around a pivot.
- One has stronger worst-case guarantees.
- Their space tradeoffs differ too.
```

```interview-question
Why is selection sort usually not considered adaptive?
---
answer:
Selection sort keeps scanning the remaining unsorted portion to find the minimum on every pass, even if the data is already nearly sorted.

Because it does roughly the same amount of comparison work regardless of existing order, it is not adaptive.
hints:
- Think about whether nearly sorted input helps it much.
- It still scans the unsorted tail every time.
- Existing order does not change its comparison pattern much.
```

## Multiple Choice Questions

```interview-choice
Which algorithm in this section has `O(n log n)` time complexity in the best, average, and worst cases?
---
options:
- Quick Sort
- Merge Sort
- Insertion Sort
correct: 1
explanation:
Merge sort keeps `O(n log n)` time across all cases, unlike quick sort whose worst case is `O(n^2)`.
```

```interview-choice
Which algorithm here is stable and adaptive?
---
options:
- Selection Sort
- Insertion Sort
- Quick Sort
correct: 1
explanation:
Insertion sort is both stable and adaptive, especially on nearly sorted input.
```

```interview-choice
Which algorithm repeatedly swaps adjacent out-of-order elements?
---
options:
- Bubble Sort
- Merge Sort
- Selection Sort
correct: 0
explanation:
Bubble sort moves larger elements toward the end through repeated adjacent swaps.
```

## Code Completion Questions

```interview-code
language: cs
prompt: Complete the bubble sort inner comparison so adjacent elements are swapped when out of order.
starter:
if (array[j] > )
{
  int temp = array[j];
  array[j] = array[j + 1];
  array[j + 1] = temp;
}
solution:
if (array[j] > array[j + 1])
{
  int temp = array[j];
  array[j] = array[j + 1];
  array[j + 1] = temp;
}
checks:
- includes: array[j + 1]
- includes: temp
```

```interview-code
language: cs
prompt: Complete the insertion sort loop so larger values shift right while searching for the insertion point.
starter:
while (j >= 0 && )
{
  array[j + 1] = array[j];
  j = j - 1;
}
solution:
while (j >= 0 && array[j] > key)
{
  array[j + 1] = array[j];
  j = j - 1;
}
checks:
- includes: array[j] > key
- includes: array[j + 1] = array[j]
```

```interview-code
language: cs
prompt: Complete the merge sort recursion so both halves are sorted before merging.
starter:
if (left < right)
{
  int middle = (left + right) / 2;
  
  Merge(array, left, middle, right);
}
solution:
if (left < right)
{
  int middle = (left + right) / 2;
  MergeSort(array, left, middle);
  MergeSort(array, middle + 1, right);
  Merge(array, left, middle, right);
}
checks:
- includes: MergeSort(array, left, middle)
- includes: MergeSort(array, middle + 1, right)
- includes: Merge(array, left, middle, right)
```

## Study Notes

Use the longer explanation page for the examples and comparison table:

- [Sorting Algorithms Notes](Sorting/sort-algorithms.md)
