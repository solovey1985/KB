# Git Intermediate

Intermediate Git work starts when local changes must be integrated with other people's history.

## Merge versus rebase

Merging preserves the branch structure by creating a merge commit when needed.

Rebasing rewrites one branch so it appears to start from a newer base commit.

```bash
git switch feature/search
git fetch origin
git rebase origin/main
```

Use merge when preserving branch integration history is useful.

Use rebase when you want a cleaner linear local history and the team allows rewritten local commits.

## Merge conflicts

A merge conflict happens when Git cannot automatically reconcile overlapping changes.

Typical conflict markers look like this:

```text
<<<<<<< HEAD
const apiUrl = '/api/v1';
=======
const apiUrl = '/api/v2';
>>>>>>> feature/new-api
```

To resolve a conflict:

1. read both sides carefully
2. decide the correct final content
3. remove the conflict markers
4. stage the resolved file
5. continue the merge or rebase flow

```bash
git add src/config.ts
git merge --continue
```

or during rebase:

```bash
git add src/config.ts
git rebase --continue
```

## Collaboration safety

Important habits:

- fetch before integrating
- keep branches short-lived
- commit small logical changes
- read diffs before resolving conflicts blindly
- do not force-push shared history casually

## Interview reminders

- explain conflict resolution as decision-making, not only marker removal
- distinguish merge conflict from rebase conflict only by context, not by content style
- mention `git add` after manual resolution
