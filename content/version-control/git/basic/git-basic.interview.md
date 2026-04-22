---
title: Git Basic Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise core Git workflow questions.

Relevant concept maps:

- [Concept Map](git-basic.concept.md)

## Core Workflow

```interview-question
What is the difference between the working tree and the staging area in Git?
---
answer:
The working tree contains the files as they currently exist in your checkout, including unstaged edits.

The staging area contains the exact changes selected for the next commit.

This separation lets developers build focused commits instead of committing every local change automatically.
hints:
- One is your live file state.
- One is the next commit candidate.
- Intentional commit shaping is the main reason.
```

Related concepts: [Working Tree](git-basic.concept.md#working-tree), [Staging Area](git-basic.concept.md#staging-area)

```interview-question
What is a commit in Git?
---
answer:
A commit is a recorded snapshot of the staged project state together with metadata such as author and message.

Commits should usually represent one coherent change so the history remains readable and useful.
hints:
- Think snapshot plus metadata.
- The staging area determines the content.
- History readability matters.
```

Related concepts: [Commit](git-basic.concept.md#commit)

```interview-question
What is a branch in Git?
---
answer:
A branch is a movable pointer to a line of development.

It allows work to progress independently until it is merged or rebased into another branch.
hints:
- It is not a whole repository copy.
- It points at commits.
- Isolation of work is the main benefit.
```

Related concepts: [Branch](git-basic.concept.md#branch)

```interview-choice
Which command stages changes for the next commit?
---
options:
- `git add`
- `git push`
- `git fetch`
correct: 0
explanation:
`git add` places changes into the staging area so they can be included in the next commit.
```

```interview-code
language: bash
prompt: Complete the command so it creates and switches to a new branch named `feature/search`.
starter:
git switch 
solution:
git switch -c feature/search
checks:
- includes: -c
- includes: feature/search
```
