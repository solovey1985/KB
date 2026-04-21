---
title: Testing Junior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the junior-level testing distinctions from the Web API interview question set.

Relevant concept maps:

- [Test Strategy Concept Map](testing-strategy.concept.md)
- [Integration and Reliability Concept Map](testing-integration-reliability.concept.md)

## Test Structure

```interview-question
What is the AAA pattern in testing?
---
answer:
AAA stands for Arrange, Act, Assert.

Arrange sets up the test data and preconditions, Act performs the operation being tested, and Assert checks the result.

It makes tests easier to read, easier to debug, and helps keep each test focused on one behavior.
hints:
- It is a three-step test structure.
- Setup happens first.
- Verification happens last.
```

Related concepts: [AAA Pattern](testing-strategy.concept.md#aaa-pattern)

```interview-choice
Which section of AAA should usually contain the main call being tested?
---
options:
- Arrange
- Act
- Assert
correct: 1
explanation:
The `Act` section should contain the main operation under test so the test reads cleanly and has one obvious behavior focus.
```

## Test Libraries

```interview-question
What testing library do you use in .NET and why? What assertion library do you prefer?
---
answer:
`xUnit` is a common default for .NET testing because it has clean conventions, strong ecosystem adoption, and good support for fixtures and parallel execution.

`FluentAssertions` is a common choice for assertions because it reads clearly and produces helpful failure messages.

The important part is not brand loyalty but picking tools that make tests readable and maintainable.
hints:
- One answer is usually the test framework.
- The other is usually the assertion library.
- Readability and failure diagnostics matter.
```

Related concepts: [xUnit and FluentAssertions](testing-strategy.concept.md#xunit-and-fluentassertions)
