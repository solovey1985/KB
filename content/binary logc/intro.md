
# Turing Complete — Intro & Practical Cheat-Sheet

This page is a practical “start here” guide for the game *Turing Complete*.
It focuses on the circuit-building mental model, core components, and rules of thumb you can use while solving levels.

For reference tables and symbols:

- Gate symbols + truth tables (matching the game icons): [boolean-logic.md](boolean-logic.md)
- Bigger building blocks (adders, muxes, registers): [components.md](components.md)

> Goal: build correct digital logic first, then optimize.

---

## Mental model

Think in layers:

1. **Bits and wires**: values are 0/1 (low/high).
2. **Combinational logic**: outputs depend only on current inputs (no memory).
3. **Sequential logic**: outputs depend on previous state (memory via registers/latches).
4. **Buses and structure**: group bits into multi-bit values; repeat patterns (adders, muxes, decoders).

When you get stuck, ask:

- Is this level **combinational** or does it need **state**?
- Am I mixing *bit-level* logic with *multi-bit* logic without a clear boundary?
- Can I solve a smaller slice (1 bit) and then replicate across the bus?

---

## Main logic components (what they’re for)

### Wires, constants, and probes

- **Wire**: carries a bit.
- **Constant 0/1**: tie inputs high/low.
- **Probe/Output**: verify behavior.

### Basic gates

- **NOT**: invert.
- **AND**: “both”.
- **OR**: “either”.
- **XOR**: “different”.

### Building blocks

- **Multiplexer (MUX)**: choose one of many inputs.
- **Decoder**: one-hot output based on a binary input.
- **Encoder / Priority encoder**: turn one-hot (or prioritized) inputs into a binary index.
- **Adder**: arithmetic building block for increment, sum, ALU.
- **Comparator**: equality/less-than logic (often built from XOR + reduction).
- **Register**: stores bits across clock ticks.
- **Counter**: register + adder (+1).
- **RAM/ROM (if/when present)**: store words addressed by an index.

### “Glue” patterns that solve many levels

- **Bit-slice replication**: design 1-bit logic then repeat for N bits.
- **Control + datapath split**:
  - *datapath*: buses, adders, muxes
  - *control*: selects, enables, op-codes

---

## Rules and workflow (how to reliably solve levels)

### Rules of thumb

- **Write the truth table first** for small input counts (≤4 inputs is easy).
- Prefer **XOR for parity/difference**, not a chain of OR/AND hacks.
- If you need “choose A or B”, it’s probably a **MUX**.
- If the task mentions “remember”, “previous”, “toggle”, “sequence”, “after clock”, you need **state** (register/latch).
- Always track **bit-width** (1-bit vs N-bit). Don’t feed a bus into a single-bit gate unless the game explicitly defines it.

### Debugging workflow

1. Test **extreme inputs** first: all 0s, all 1s, alternating (1010…), single-bit set.
2. If failing, isolate:
   - verify intermediate signals with probes
   - confirm selects/enables polarity (active-high vs active-low)
3. Reduce the problem:
   - solve for 1 bit (LSB) then extend
   - solve for 2 bits and look for the pattern

### Common mistakes

- Swapped bit order (LSB/MSB reversed)
- Missing carry chain in adders
- Forgetting to gate/enable writes to registers
- Building sequential behavior with combinational logic (impossible)

---

## Hints and rules for common task types (non-spoiler)

### Combinational “implement function” levels

- Make a truth table (or derive boolean expressions).
- If there are many inputs, look for structure:
  - parity → XOR
  - “any” → OR reduction
  - “all” → AND reduction
  - “exactly one” → one-hot detection (XOR/AND combinations)

### Bus / multi-bit levels

- Solve a **1-bit slice** and replicate it for each bit.
- Decide early: are you doing **unsigned** logic only, or do you need **two’s complement** behavior?
- For equality on N bits: XOR each bit, OR-reduce the differences, then NOT.
  - `eq = ¬( (a0⊕b0) ∨ (a1⊕b1) ∨ ... )`

### Decoder/encoder style levels

- Decoder is “one-hot”: only one output should be 1.
- Use AND terms for each output pattern, or build from smaller decoders hierarchically.
- For priority behavior, explicitly gate lower-priority lines with “no higher bit set”.

### Sequential / memory levels

- Identify:
  - **state** (what must be remembered?)
  - **next-state logic** (how it updates)
  - **output logic** (depends on state and/or inputs)
- For counters: next = current + 1 (unless reset/enable logic modifies it).
- For “only update sometimes”: add an **enable** that either loads new value or keeps old.

### ALU / operation-selector levels

- Build each operation (ADD, AND, XOR, SHIFT…) then use a MUX controlled by an opcode.
- Keep control wiring clean: name signals (or group them) by opcode.

---

## Quick checklist before you submit a solution

- All outputs defined for **every input combination**
- No floating inputs (every input is driven)
- Bit order (LSB/MSB) matches what the level expects
- Carry/borrow chains are correct (if arithmetic)
- Registers only update when intended (enable/reset polarity correct)

---

## Links

- Game page: [Turing Complete on Steam](https://store.steampowered.com/app/1444480/Turing_Complete/)

