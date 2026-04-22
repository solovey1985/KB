
# Turing Complete — Components, Truth Tables, and What They Solve

This page is a practical component reference for *Turing Complete*.
If you want the symbol/icon cheat-sheet used in the game UI, see [boolean-logic.md](boolean-logic.md).
For each component you’ll find:

- What it does
- A truth/state table (when applicable)
- Common tasks it helps solve
- Hints and logical conclusions (how to combine it with other blocks)

---

## 1-bit gates (in practice)

Truth tables and symbols for the basic gates live in [boolean-logic.md](boolean-logic.md).
This section focuses on what they *solve* inside larger circuits.

### NOT / AND / OR / XOR

- **NOT**: invert active-low signals; build NAND/NOR via De Morgan.
- **AND**: masking (`signal ∧ enable`), decoder terms, carry generation.
- **OR**: combine conditions (flags), OR-reduction (any-set) detectors.
- **XOR**: parity/difference, sum bit, equality via XOR-per-bit + reduction.

Quick conclusion:

- If you’re writing “if condition then use A else B”, you probably want a **MUX**, not gate spaghetti.

---

## Adders (core arithmetic components)

### Half adder

**Purpose**: add two 1-bit numbers.

- `Sum = A ⊕ B`
- `Carry = A ∧ B`

| A | B | Sum | Carry |
| - | - | --- | ----- |
| 0 | 0 | 0   | 0     |
| 0 | 1 | 1   | 0     |
| 1 | 0 | 1   | 0     |
| 1 | 1 | 0   | 1     |

#### Half adder — Common tasks

- LSB addition
- Building a full adder

#### Half adder — Hints / conclusions

- If a level asks for “add two bits” without carry-in, it’s a half adder.

### Full adder

**Purpose**: add A, B, plus carry-in.

- `Sum = A ⊕ B ⊕ Cin`
- `Cout = (A ∧ B) ∨ (Cin ∧ (A ⊕ B))`

| A | B | Cin | Sum | Cout |
| - | - | --- | --- | ---- |
| 0 | 0 | 0   | 0   | 0    |
| 0 | 0 | 1   | 1   | 0    |
| 0 | 1 | 0   | 1   | 0    |
| 0 | 1 | 1   | 0   | 1    |
| 1 | 0 | 0   | 1   | 0    |
| 1 | 0 | 1   | 0   | 1    |
| 1 | 1 | 0   | 0   | 1    |
| 1 | 1 | 1   | 1   | 1    |

#### Full adder — Common tasks

- N-bit addition (ripple carry)
- Incrementers (add 1)
- Two’s complement negate: invert + add 1

#### Full adder — Hints / conclusions

- If N-bit add fails: check the carry chain first.

---

## Routing / selection components

### 2:1 Multiplexer (MUX)

**Purpose**: select between A and B.

- `Y = (¬S ∧ A) ∨ (S ∧ B)`

| S | Y |
| - | - |
| 0 | A |
| 1 | B |

#### MUX — Common tasks

- “Choose X if condition else Y”
- Implement if/else logic
- Select ALU operation result
- Build wide (N-bit) selectors by repeating per bit

#### MUX — Hints / conclusions

- If you see “mode”, “sel”, “opcode”, “when … otherwise …” → think MUX.
- For N-bit: one select line usually controls all bit slices.

### Demultiplexer (DEMUX) (concept)

**Purpose**: route one input to one of multiple outputs.

If you don’t have a dedicated DEMUX component, you can build a 1-to-2 demux as:

- `Y0 = D ∧ ¬S`
- `Y1 = D ∧ S`

#### DEMUX — Common tasks

- “Send signal to exactly one destination”
- Write-enable selection (choose which register updates)

#### DEMUX — Hints / conclusions

- Demux is essentially a decoder + AND gating with the data signal.

---

## Decoders and encoders

### 1-to-2 decoder (with enable) (pattern)

**Purpose**: convert select bit(s) into one-hot outputs.

- `Y0 = E ∧ ¬S`
- `Y1 = E ∧ S`

| E | S | Y0 | Y1 |
| - | - | -- | -- |
| 0 | 0 | 0  | 0  |
| 0 | 1 | 0  | 0  |
| 1 | 0 | 1  | 0  |
| 1 | 1 | 0  | 1  |

#### Decoder — Common tasks

- One-hot control signals
- Selecting which register to write

#### Decoder — Hints / conclusions

- Larger decoders are often built hierarchically from smaller ones.

### Priority encoder (concept)

**Purpose**: output the index of the highest-priority asserted input.

#### Priority encoder — Common tasks

- “Pick first/highest request” logic
- Interrupt-style selection

#### Priority encoder — Hints / conclusions

- Implement with “no higher bit set” gating.

---

## Comparators (patterns)

### Equality (N-bit)

**Purpose**: `eq = 1` iff A equals B.

- `diff_i = A_i ⊕ B_i`
- `eq = ¬(diff_0 ∨ diff_1 ∨ ... ∨ diff_{n-1})`

#### Equality — Common tasks

- Match opcode
- Detect zero / special values
- Termination conditions in controllers

#### Equality — Hints / conclusions

- “Is zero” is equality vs a constant zero bus.

---

## Memory / sequential components

Sequential components are described with **state tables** (next state depends on current state + inputs).

### Register (D-type) (concept)

**Purpose**: store a bit (or bus) across clock ticks.

Typical behavior (with enable):

- If `EN = 1` at clock edge: `Q(next) = D`
- If `EN = 0` at clock edge: `Q(next) = Q(now)`

| EN | Q(now) | D | Q(next) |
| -- | ------ | - | ------- |
| 0  | 0      | 0 | 0       |
| 0  | 0      | 1 | 0       |
| 0  | 1      | 0 | 1       |
| 0  | 1      | 1 | 1       |
| 1  | 0      | 0 | 0       |
| 1  | 0      | 1 | 1       |
| 1  | 1      | 0 | 0       |
| 1  | 1      | 1 | 1       |

#### Register — Common tasks

- “Remember previous value”
- Build counters (register + adder)
- Pipeline stages

#### Register — Hints / conclusions

- If the level mentions “after a tick”, it’s sequential.
- Separate into:
  - *next-state logic* (compute D)
  - *state element* (register)
  - *output logic* (what you expose)

---

## General conclusions (fast pattern recognition)

- “Choose between signals” → MUX
- “Exactly one output should be 1” → decoder / one-hot
- “Add / increment / negate” → adder + carry chain
- “Compare / match / detect zero” → XOR + reduction + NOT
- “Sequence / remember / toggle / state machine” → register(s) + next-state logic

