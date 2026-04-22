---
title: HTML Forms and Validation Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise HTML form and validation questions.

Relevant concept maps:

- [Concept Map](html-forms-validation.concept.md)

## Forms

```interview-question
What is the role of the `label` element in HTML forms?
---
answer:
The `label` element gives an accessible name to a form control and improves usability by making the label clickable.

It is one of the most important elements for form accessibility and clarity.
hints:
- Think accessibility first.
- It names the control.
- Click behavior is part of the benefit.
```

Related concepts: [Label Association](html-forms-validation.concept.md#label-association), [Form Semantics](html-forms-validation.concept.md#form-semantics)

```interview-question
What is the difference between `name`, `id`, `value`, and `placeholder` on a form control?
---
answer:
`id` identifies the element in the document and is commonly used to connect labels.

`name` controls the submitted key in form data.

`value` is the field's current value.

`placeholder` is temporary hint text and should not replace a visible or programmatically associated label.
hints:
- Submission and labeling are different concerns.
- Placeholder is only hint text.
- `name` matters to form submission.
```

Related concepts: [Form Control Attributes](html-forms-validation.concept.md#form-control-attributes)

```interview-question
How does native HTML validation work?
---
answer:
Native HTML validation uses input types and attributes such as `required`, `pattern`, `min`, `max`, and `maxlength` to let the browser check form constraints.

It is useful for basic validation and user feedback, but it does not replace server-side validation.
hints:
- Browser-level constraints are the key idea.
- Input types help too.
- Server-side validation still matters.
```

Related concepts: [Constraint Validation](html-forms-validation.concept.md#constraint-validation), [HTML5 Input Types](html-forms-validation.concept.md#html5-input-types)

```interview-choice
Which attribute is most directly responsible for making a field mandatory?
---
options:
- `optional`
- `required`
- `strict`
correct: 1
explanation:
The `required` attribute tells the browser that the field must have a value before the form can be submitted.
```

```interview-code
language: html
prompt: Complete the control so it is associated with the email label and uses native email validation.
starter:
<label for="email">Email</label>
<input id="email" name="email" type="" />
solution:
<label for="email">Email</label>
<input id="email" name="email" type="email" />
checks:
- includes: type="email"
```
