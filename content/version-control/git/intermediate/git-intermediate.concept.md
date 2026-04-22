---
title: Git Intermediate Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes Git integration concepts around merging, rebasing, and conflict handling.

Study pages: [Section Index](index.md) | [Material Notes](git-intermediate.md) | [Interview Practice](git-intermediate.interview.md)

## Intermediate Map

```concept-card
id: merge
term: Merge
related:
- rebase
- merge-conflict
summary:
Merging integrates one line of history into another while preserving both histories.
details:
It may create a merge commit and is often used when branch structure and integration history should remain visible.
example:
`git merge feature/search`
mnemonic:
Merge combines histories without pretending they were always linear.
recall:
- What does merge preserve that rebase often does not?
- When is merge a better fit than rebase?
```

```concept-card
id: rebase
term: Rebase
related:
- merge
- merge-conflict
summary:
Rebase rewrites commits so they are replayed on top of a new base.
details:
It produces a cleaner linear history, but it changes commit identity and must be used carefully on shared history.
example:
`git rebase origin/main`
mnemonic:
Rebase moves your work onto a new starting point.
recall:
- Why does rebase create a linear history?
- Why should rebasing shared public history be treated carefully?
```

```concept-card
id: merge-conflict
term: Merge Conflict
children:
- conflict-markers
- conflict-resolution-workflow
summary:
A merge conflict is Git's signal that overlapping changes require human judgment to integrate correctly.
details:
Conflicts are about meaning, not only syntax. The right resolution depends on the intended final code.
example:
Two branches both changed `apiUrl` differently in the same file block.
mnemonic:
Git sees overlap, humans decide intent.
recall:
- Why does Git stop on a merge conflict?
- Why is conflict resolution not just deleting markers?
```

```concept-card
id: conflict-markers
term: Conflict Markers
parents:
- merge-conflict
summary:
Conflict markers are the special text blocks Git inserts to show competing versions of a change.
details:
They identify the current side, the incoming side, and the boundary between them.
example:
`<<<<<<<`, `=======`, and `>>>>>>>`
mnemonic:
Markers show the sides, not the final answer.
recall:
- What do the conflict markers indicate?
- Why must they be removed before the file is truly resolved?
```

```concept-card
id: conflict-resolution-workflow
term: Conflict Resolution Workflow
parents:
- merge-conflict
summary:
Conflict resolution workflow is the sequence of deciding the correct final content, editing the file, staging it, and continuing the interrupted Git operation.
details:
It is important to understand both the code intent and the Git continuation step after the edit is done.
example:
Edit the file, run `git add file.ts`, then `git rebase --continue`.
mnemonic:
Decide, edit, stage, continue.
recall:
- What steps happen after Git reports a conflict?
- Why is staging part of conflict resolution?
```
