---
title: Git Intermediate Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise Git collaboration and merge conflict questions.

Relevant concept maps:

- [Concept Map](git-intermediate.concept.md)

## Integration

```interview-question
What is the difference between `git merge` and `git rebase`?
---
answer:
`git merge` combines histories and may create a merge commit that preserves the branch structure.

`git rebase` rewrites commits so they appear on top of a new base, which creates a more linear history but changes commit identity.
hints:
- One preserves branch structure.
- One rewrites commit history.
- Linear history is the clue for rebase.
```

Related concepts: [Merge](git-intermediate.concept.md#merge), [Rebase](git-intermediate.concept.md#rebase)

## Merge Conflicts

```interview-question
What is a merge conflict in Git?
---
answer:
A merge conflict happens when Git cannot automatically decide how to combine overlapping changes from different histories.

This usually occurs when the same lines or closely related sections were changed differently in two branches.
hints:
- Automatic integration fails.
- Overlapping edits are the common cause.
- Git asks for a human decision.
```

Related concepts: [Merge Conflict](git-intermediate.concept.md#merge-conflict)

```interview-question
How do you resolve a merge conflict safely?
---
answer:
Open the conflicted file, review both sides of the conflict markers, decide the correct final content, remove the markers, and stage the resolved file.

Then continue the interrupted operation with `git merge --continue` or `git rebase --continue` depending on context.

The important part is understanding the code outcome, not just deleting markers mechanically.
hints:
- Read both sides first.
- Remove markers only after deciding the true final version.
- Staging the file is required before continuing.
```

Related concepts: [Conflict Markers](git-intermediate.concept.md#conflict-markers), [Conflict Resolution Workflow](git-intermediate.concept.md#conflict-resolution-workflow)

```interview-choice
After manually resolving a conflicted file, what is the next Git step before continuing the merge or rebase?
---
options:
- `git add <file>`
- `git push`
- `git fetch`
correct: 0
explanation:
After editing the file to its resolved form, you must stage it with `git add` so Git knows the conflict has been resolved.
```

```interview-code
language: bash
prompt: Complete the command used to continue a rebase after all conflicts have been resolved and staged.
starter:
git rebase 
solution:
git rebase --continue
checks:
- includes: --continue
```
