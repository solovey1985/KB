
# Turing Complete — Boolean Logic (Game Icon Symbols)

This page maps the game’s gate icons to standard boolean symbols and provides compact truth tables.

---

## Icon → name → symbol

| Game icon | Meaning | Common symbol(s) | Read as |
| --- | --- | --- | --- |
| ON | Constant 1 | `1`, `TRUE` | always high |
| OFF | Constant 0 | `0`, `FALSE` | always low |
| N | NOT | `¬A`, `!A`, `~A` | “not A” |
| AND | AND | `A ∧ B`, `A & B` | “A and B” |
| OR | OR | `A ∨ B`, `A \| B` | “A or B” |
| XOR | XOR | `A ⊕ B` | “A different from B” |
| XNOR | XNOR (equivalence) | `A ≡ B`, `A ↔ B`, `¬(A ⊕ B)` | “A same as B” |
| NAND | NAND | `A ↑ B`, `¬(A ∧ B)` | “not (A and B)” |
| NOR | NOR | `A ↓ B`, `¬(A ∨ B)` | “not (A or B)” |

Notes:

- The game names the NOT gate as **N**.
- XNOR isn’t always shown with one universal symbol; `≡` / `↔` / `¬(⊕)` are common.

---

## Truth tables

Assume inputs `A` and `B` are bits in `{0,1}`.

### NOT (N)

| A | ¬A |
| - | -- |
| 0 | 1  |
| 1 | 0  |

### 2-input gates (combined)

| A | B | AND (A∧B) | OR (A∨B) | XOR (A⊕B) | XNOR (A≡B) | NAND ¬(A∧B) | NOR ¬(A∨B) |
| - | - | --------- | -------- | --------- | ---------- | ----------- | ---------- |
| 0 | 0 | 0         | 0        | 0         | 1          | 1           | 1          |
| 0 | 1 | 0         | 1        | 1         | 0          | 1           | 0          |
| 1 | 0 | 0         | 1        | 1         | 0          | 1           | 0          |
| 1 | 1 | 1         | 1        | 0         | 1          | 0           | 0          |

---

## Quick identities (most useful in puzzles)

### XOR / XNOR

- `A ⊕ 0 = A`
- `A ⊕ 1 = ¬A`
- `A ⊕ A = 0`
- `A ≡ B = ¬(A ⊕ B)`
- `A ≡ 0 = ¬A`
- `A ≡ 1 = A`

### AND / OR

- `A ∧ 0 = 0`
- `A ∧ 1 = A`
- `A ∨ 0 = A`
- `A ∨ 1 = 1`

### De Morgan (how to “push NOT inward”)

- `¬(A ∧ B) = ¬A ∨ ¬B`  (NAND form)
- `¬(A ∨ B) = ¬A ∧ ¬B`  (NOR form)

---

## Practical “what this gate usually means”

- **XOR**: “different / toggle / parity / sum-without-carry”.
- **XNOR**: “same / equals” (1-bit equality).
- **NAND/NOR**: useful for simplifying with De Morgan; also functionally complete gate families.

---

## Suggested notation while solving

- Use `0/1` for constants (OFF/ON).
- Use `¬`, `∧`, `∨`, `⊕` for gate-level reasoning.
- When translating to implementation, remember the game’s gate names match the icons:
  - `N` = NOT, `AND`, `OR`, `XOR`, `XNOR`, `NAND`, `NOR`.
