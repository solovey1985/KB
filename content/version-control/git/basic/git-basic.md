# Git Basic

Git tracks changes to files so developers can create safe checkpoints, compare revisions, and collaborate without overwriting each other's work blindly.

## Core mental model

Git mainly works with three states:

- working tree
- staging area
- commit history

```bash
git status
git add src/app.ts
git commit -m "add login button"
```

## Working tree and staging area

The working tree is where you edit files.

The staging area is where you choose which changes will go into the next commit.

This separation makes commits easier to keep focused.

## Commits

A commit is a snapshot of the staged state plus metadata such as author, timestamp, and message.

Good commits are:

- small
- focused
- understandable later

## Branches

A branch is a movable pointer to a line of development.

```bash
git switch -c feature/search
```

Branches let you isolate work before integrating it back into another branch.

## Inspecting history

```bash
git log --oneline
git diff
git diff --staged
```

These commands help you understand what changed and what is about to be committed.

## Interview reminders

- explain staging as intentional commit selection
- explain branches as pointers, not as copies of the whole repo
- describe commits as snapshots, not just save points
