---
title: Git Basic Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core Git concepts needed for everyday local workflow.

Study pages: [Section Index](index.md) | [Material Notes](git-basic.md) | [Interview Practice](git-basic.interview.md)

## Basic Map

```concept-card
id: git-repository
term: Git Repository
children:
- working-tree
- staging-area
- commit
- branch
summary:
A Git repository is the versioned project history together with the local checkout used for development.
details:
It tracks changes over time and gives developers tools for safe snapshots, comparison, and integration.
example:
A repository contains your local files, commit graph, and branch pointers.
mnemonic:
Repo holds history and the current checkout context.
recall:
- What does a Git repository contain?
- Why is the repository more than just a folder of files?
```

```concept-card
id: working-tree
term: Working Tree
parents:
- git-repository
related:
- staging-area
summary:
The working tree is the current checked-out file state where you edit files.
details:
It can contain unstaged changes that are not yet selected for the next commit.
example:
Editing `app.ts` changes the working tree immediately.
mnemonic:
Working tree is what you are editing now.
recall:
- What does the working tree represent?
- Why is it different from the staging area?
```

```concept-card
id: staging-area
term: Staging Area
parents:
- git-repository
related:
- working-tree
summary:
The staging area holds the exact changes chosen for the next commit.
details:
It allows commit composition so one working tree can still be split into multiple focused commits.
example:
`git add src/app.ts` stages only that file's changes.
mnemonic:
Stage what you mean to commit.
recall:
- Why does Git separate staging from the working tree?
- How does staging improve commit quality?
```

```concept-card
id: commit
term: Commit
parents:
- git-repository
summary:
A commit is a recorded snapshot of the staged state plus metadata.
details:
Commits form the project history and should ideally each represent one coherent change.
example:
`git commit -m "add login validation"`
mnemonic:
Commit means one named snapshot in history.
recall:
- Why should commits be focused?
- What gives a commit meaning beyond changed files?
```

```concept-card
id: branch
term: Branch
parents:
- git-repository
summary:
A branch is a movable pointer to a line of commits.
details:
Branches isolate work and allow multiple changes to evolve independently before integration.
example:
`feature/search` can move forward without disturbing `main` until integration time.
mnemonic:
Branch points to a line of work, not a copied repo.
recall:
- What is a branch actually pointing to?
- Why are branches useful in everyday development?
```
