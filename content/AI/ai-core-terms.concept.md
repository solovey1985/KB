---
title: AI Core Terms Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes core AI terms and buzzwords into a study-friendly concept map.

Source inspiration: [AI Buzzwords - What They Actually Mean](https://www.youtube.com/watch?v=Nm8qkGMrXFE)

Study pages: [Section Index](index.md)

## Core AI Map

```concept-card
id: artificial-intelligence
term: Artificial Intelligence
children:
- machine-learning
- generative-ai
- model
- multimodal
summary:
Artificial intelligence is the broad field of building systems that perform tasks that normally require human-like intelligence.
details:
It includes reasoning, prediction, perception, language processing, planning, and automation. Not every AI system learns from data, but modern AI discussions often focus on machine learning systems.
example:
An AI assistant can classify text, answer questions, summarize documents, or generate code.
mnemonic:
AI is the umbrella, not one single technique.
recall:
- What does AI mean at the broadest level?
- Why is machine learning only one part of AI?
```

```concept-card
id: machine-learning
term: Machine Learning
parents:
- artificial-intelligence
children:
- supervised-learning
- unsupervised-learning
- reinforcement-learning
- deep-learning
related:
- training
- inference
summary:
Machine learning is a way to build systems that learn patterns from data instead of relying only on explicit rules.
details:
The system is trained on examples and then uses the learned pattern to make predictions, decisions, or classifications on new inputs.
example:
A spam filter learns from labeled email examples rather than from a fixed hand-written rule list.
mnemonic:
Rules from data instead of only data from rules.
recall:
- How is machine learning different from traditional hard-coded logic?
- What does a machine learning system learn from?
```

```concept-card
id: deep-learning
term: Deep Learning
parents:
- machine-learning
children:
- neural-network
- transformer
- diffusion-model
related:
- large-language-model
summary:
Deep learning is machine learning based on multi-layer neural networks.
details:
It is especially strong for language, vision, and speech tasks because deeper network layers can learn increasingly abstract features from large datasets.
example:
Modern image recognition and many language models are based on deep learning.
mnemonic:
More layers, more learned representation depth.
recall:
- Why is deep learning considered a subset of machine learning?
- Which kinds of tasks commonly use deep learning?
```

```concept-card
id: neural-network
term: Neural Network
parents:
- deep-learning
related:
- model
summary:
A neural network is a model made of connected computational units arranged in layers.
details:
During training, the network adjusts internal weights so its outputs become better aligned with the target behavior.
example:
A neural network can take pixels as input and output whether an image contains a cat.
mnemonic:
Layers transform inputs into learned outputs.
recall:
- What gets adjusted inside a neural network during training?
- Why are layers important in a neural network?
```

```concept-card
id: transformer
term: Transformer
parents:
- deep-learning
children:
- attention-mechanism
related:
- large-language-model
- token
- context-window
summary:
A transformer is a neural network architecture designed to process relationships between tokens efficiently.
details:
It became the foundation for modern language models because it handles sequence data well and can attend to different parts of the input in parallel.
example:
Most modern LLMs are built on transformer-based architectures.
mnemonic:
Transformers model token relationships at scale.
recall:
- Why are transformers central to modern LLMs?
- What kind of input structure do transformers operate over?
```

```concept-card
id: attention-mechanism
term: Attention Mechanism
parents:
- transformer
related:
- context-window
summary:
Attention is the mechanism that lets a model weigh which parts of the input matter most for the current output.
details:
Instead of treating every token equally, the model learns which earlier or nearby tokens are most relevant to the current step.
example:
When resolving a pronoun in a sentence, attention can focus on the earlier noun it refers to.
mnemonic:
Pay more attention to what matters now.
recall:
- What problem does attention help solve in sequence modeling?
- Why is attention useful for language understanding?
```

```concept-card
id: model
term: Model
parents:
- artificial-intelligence
related:
- training
- inference
summary:
A model is the learned artifact that maps input data to outputs.
details:
After training, the model contains parameters that encode patterns learned from data. It is the thing you run at inference time to produce predictions or generated output.
example:
A sentiment model can take a review as input and output positive or negative.
mnemonic:
Train the model, then run the model.
recall:
- What is a model in practical AI work?
- How is a model related to training and inference?
```

```concept-card
id: training
term: Training
parents:
- machine-learning
related:
- model
- dataset
- parameters
- fine-tuning
- reinforcement-learning-from-human-feedback
summary:
Training is the process of adjusting a model so it learns useful patterns from data.
details:
The model sees examples, produces outputs, compares them to a target or reward signal, and updates its parameters over many iterations.
example:
Training a classifier means repeatedly feeding labeled examples and updating the model to reduce mistakes.
mnemonic:
Training is practice with feedback.
recall:
- What changes during training?
- Why does training usually require many examples and iterations?
```

```concept-card
id: inference
term: Inference
parents:
- machine-learning
related:
- model
- temperature
summary:
Inference is the process of using a trained model to make a prediction or generate an output.
details:
No learning is required during normal inference. The model applies what it already learned to new input data.
example:
When a chatbot answers a prompt, it is running inference.
mnemonic:
Training learns, inference uses.
recall:
- What is the difference between training and inference?
- Why is inference the part end users usually experience directly?
```

```concept-card
id: fine-tuning
term: Fine-Tuning
parents:
- training
related:
- large-language-model
- dataset
- reinforcement-learning-from-human-feedback
summary:
Fine-tuning is additional training that adapts a pre-trained model to a narrower task, style, or domain.
details:
It starts from an already capable model and updates it further using specialized data rather than training the entire capability from scratch.
example:
A general language model can be fine-tuned for legal drafting or customer-support tone.
mnemonic:
Start broad, then specialize.
recall:
- Why is fine-tuning usually cheaper than full training from scratch?
- When is fine-tuning useful?
```

```concept-card
id: reinforcement-learning-from-human-feedback
term: RLHF
aliases:
- Reinforcement Learning from Human Feedback
parents:
- training
related:
- reinforcement-learning
- fine-tuning
- hallucination
summary:
RLHF is a training approach that uses human preferences to improve model behavior.
details:
Humans rate or compare outputs, and the model is then adjusted so it becomes more helpful, safer, or more aligned with expected behavior.
example:
Two model answers are compared by human reviewers, and the preferred style helps guide later optimization.
mnemonic:
Humans shape the model's behavior after pretraining.
recall:
- What role do humans play in RLHF?
- Why is RLHF important for assistant-like systems?
```

```concept-card
id: dataset
term: Dataset
related:
- training
- supervised-learning
- unsupervised-learning
summary:
A dataset is the collection of examples used to train, validate, or test a model.
details:
Its quality, size, balance, and representativeness strongly affect model quality. Weak or biased data often produces weak or biased outcomes.
example:
A dataset for image classification might contain thousands of labeled photos across multiple categories.
mnemonic:
Better data, better learning.
recall:
- Why does dataset quality matter so much?
- What kinds of problems can bad data introduce?
```

```concept-card
id: parameters
term: Parameters
related:
- model
- training
summary:
Parameters are the internal numeric values a model learns during training.
details:
They store the model's learned behavior. Larger models usually have more parameters, which often increases capacity but also increases compute and memory costs.
example:
Neural network weights are model parameters.
mnemonic:
Parameters are the learned knobs inside the model.
recall:
- What do parameters represent inside a model?
- Why do people talk about parameter count when comparing models?
```

```concept-card
id: supervised-learning
term: Supervised Learning
parents:
- machine-learning
related:
- dataset
- label
summary:
Supervised learning trains a model on examples paired with correct answers.
details:
The model learns to map inputs to known targets such as classes, categories, or numeric values.
example:
An email dataset labeled as spam or not spam supports supervised learning.
mnemonic:
Learn from examples with answers.
recall:
- What extra information does supervised learning require?
- Which common business problems fit supervised learning well?
```

```concept-card
id: unsupervised-learning
term: Unsupervised Learning
parents:
- machine-learning
related:
- dataset
summary:
Unsupervised learning looks for structure in data without labeled answers.
details:
It is often used for clustering, grouping, compression, and discovering patterns that are not explicitly tagged beforehand.
example:
Customer segments discovered from behavior data without preassigned segment labels.
mnemonic:
Find patterns without answer keys.
recall:
- What is missing from unsupervised learning data?
- What kinds of tasks commonly use unsupervised learning?
```

```concept-card
id: reinforcement-learning
term: Reinforcement Learning
parents:
- machine-learning
related:
- agent
summary:
Reinforcement learning trains an agent by rewarding good actions and penalizing bad ones over time.
details:
Instead of learning from explicit correct answers for every step, the system learns a policy through trial, feedback, and cumulative reward.
example:
A game-playing system learns which actions lead to higher long-term scores.
mnemonic:
Try actions, collect rewards, improve policy.
recall:
- How is reinforcement learning feedback different from supervised learning labels?
- Why is long-term reward important in reinforcement learning?
```

```concept-card
id: label
term: Label
related:
- supervised-learning
- dataset
summary:
A label is the expected target value attached to a training example in supervised learning.
details:
Labels tell the model what the correct answer should be, such as a class name, category, or numeric outcome.
example:
In fraud detection, each transaction might be labeled as fraudulent or legitimate.
mnemonic:
Label means known answer.
recall:
- Why are labels necessary in supervised learning?
- What are examples of labels in classification tasks?
```

```concept-card
id: generative-ai
term: Generative AI
parents:
- artificial-intelligence
children:
- large-language-model
- prompt
- diffusion-model
related:
- hallucination
summary:
Generative AI creates new content such as text, images, audio, or code.
details:
Unlike systems focused only on classification or scoring, generative models produce new outputs by learning patterns in large datasets.
example:
A generative model can draft an email, create an image, or summarize an article.
mnemonic:
Not just classify, create.
recall:
- What makes generative AI different from purely predictive systems?
- Which kinds of outputs can generative AI produce?
```

```concept-card
id: large-language-model
term: Large Language Model
aliases:
- LLM
parents:
- generative-ai
related:
- token
- prompt
- context-window
- transformer
- embeddings
- zero-shot-learning
- few-shot-learning
- temperature
summary:
A large language model is a generative model trained to predict and generate language-like sequences.
details:
It works with tokens rather than full ideas, using patterns learned from massive text corpora to continue, transform, summarize, or answer based on prompts.
example:
An LLM can answer a question, rewrite a paragraph, or generate code from instructions.
mnemonic:
Predict the next token at scale.
recall:
- What does an LLM fundamentally predict?
- Why is it considered part of generative AI?
```

```concept-card
id: prompt
term: Prompt
parents:
- generative-ai
children:
- prompt-engineering
related:
- large-language-model
- context-window
summary:
A prompt is the input instruction or context given to a generative model.
details:
Its wording shapes what the model pays attention to and what style, constraints, or task it tries to satisfy.
example:
`Summarize this article in five bullet points for a junior developer.`
mnemonic:
Prompt steers the generation.
recall:
- Why does prompt wording affect model output?
- What kinds of information can a good prompt include?
```

```concept-card
id: prompt-engineering
term: Prompt Engineering
parents:
- prompt
children:
- zero-shot-learning
- few-shot-learning
related:
- temperature
summary:
Prompt engineering is the practice of designing prompts so a model produces more useful, reliable, and constrained outputs.
details:
It often involves clarifying the task, defining output format, giving examples, adding constraints, and iterating on phrasing.
example:
`Extract the top 3 risks from this text and return valid JSON with keys risk and severity.`
mnemonic:
Better instructions, better responses.
recall:
- What makes a prompt engineered rather than casual?
- Why can structure and examples improve outputs?
```

```concept-card
id: zero-shot-learning
term: Zero-Shot Learning
parents:
- prompt-engineering
related:
- few-shot-learning
- large-language-model
summary:
Zero-shot learning means asking a model to perform a task without giving task-specific examples in the prompt.
details:
The model relies on what it already learned during training and any instructions in the prompt, but it is not shown worked examples for that request.
example:
`Classify this review as positive or negative.`
mnemonic:
No examples, just instructions.
recall:
- What is missing in a zero-shot prompt?
- When might zero-shot be enough?
```

```concept-card
id: few-shot-learning
term: Few-Shot Learning
parents:
- prompt-engineering
related:
- zero-shot-learning
- large-language-model
summary:
Few-shot learning means giving a model a small number of examples in the prompt to guide the task.
details:
The examples show the desired pattern, format, or reasoning style so the model can continue in the same form for the new input.
example:
You provide two input-output examples before asking the model to solve a third similar case.
mnemonic:
Teach by showing a few examples.
recall:
- How does few-shot prompting differ from zero-shot prompting?
- Why can a couple of examples improve consistency?
```

```concept-card
id: token
term: Token
parents:
- large-language-model
related:
- context-window
- embeddings
summary:
A token is a small unit of text that a language model processes.
details:
Tokens are not always words. They can be whole words, word pieces, punctuation, or other text fragments depending on the tokenizer.
example:
The sentence `AI changes software` may be split into several tokens rather than exactly three words.
mnemonic:
Models read chunks, not ideas.
recall:
- Why is a token not always the same as a word?
- Why do token counts matter when using LLMs?
```

```concept-card
id: context-window
term: Context Window
parents:
- large-language-model
related:
- token
- prompt
- retrieval-augmented-generation
summary:
The context window is the amount of tokenized input and working context a model can consider at once.
details:
If important information falls outside that limit, the model cannot directly attend to it in the current interaction.
example:
Very long documents may need chunking because the full text does not fit into one context window.
mnemonic:
If it does not fit, the model cannot see it now.
recall:
- What does the context window limit?
- Why does long input sometimes need chunking or retrieval?
```

```concept-card
id: temperature
term: Temperature
related:
- inference
- prompt-engineering
- large-language-model
summary:
Temperature is an inference setting that changes how deterministic or random the model's next-token choices are.
details:
Lower temperature usually makes outputs more predictable and conservative, while higher temperature increases variety and risk-taking in generation.
example:
Use a lower temperature for factual extraction and a higher one for brainstorming.
mnemonic:
Low is steadier, high is wilder.
recall:
- What does changing temperature affect?
- Why might different tasks need different temperature settings?
```

```concept-card
id: hallucination
term: Hallucination
related:
- large-language-model
- generative-ai
- retrieval-augmented-generation
summary:
A hallucination is a confident-sounding model output that is incorrect, unsupported, or fabricated.
details:
Generative models optimize for plausible continuation, not guaranteed truth. That is why factual verification and grounding matter in real systems.
example:
A model invents a fake API method or cites a source that does not exist.
mnemonic:
Sounds right is not the same as is right.
recall:
- Why can generative models hallucinate?
- Why is validation important when using model-generated content?
```

```concept-card
id: embeddings
term: Embeddings
related:
- token
- vector-database
- retrieval-augmented-generation
summary:
Embeddings are numeric vector representations that capture semantic similarity between pieces of data.
details:
Items with similar meaning tend to produce nearby vectors, which makes embeddings useful for search, clustering, recommendation, and retrieval.
example:
Two passages about refunds may have embeddings that sit close together even if they use different words.
mnemonic:
Meaning becomes math.
recall:
- What do embeddings represent?
- Why are embeddings useful for semantic search?
```

```concept-card
id: vector-database
term: Vector Database
parents:
- retrieval-augmented-generation
related:
- embeddings
summary:
A vector database stores embeddings and supports similarity search across them.
details:
It is commonly used to find the most semantically relevant chunks, documents, or records for a query embedding.
example:
A support bot uses a vector database to find the closest help articles before generating an answer.
mnemonic:
Store vectors, search by closeness.
recall:
- What kind of data does a vector database store and query?
- Why is it useful in modern AI applications?
```

```concept-card
id: retrieval-augmented-generation
term: RAG
aliases:
- Retrieval-Augmented Generation
parents:
- generative-ai
children:
- vector-database
related:
- embeddings
- context-window
- hallucination
- prompt
summary:
RAG combines retrieval of relevant external information with generation from a model.
details:
Instead of relying only on what the model memorized during training, the system first fetches useful context and then includes it in the prompt before generation.
example:
A documentation assistant retrieves product docs, then answers the user based on those retrieved passages.
mnemonic:
Retrieve first, generate second.
recall:
- Why does RAG help reduce hallucinations?
- What extra system step does RAG add before generation?
```

```concept-card
id: agent
term: Agent
related:
- reinforcement-learning
- large-language-model
- retrieval-augmented-generation
- multimodal
summary:
An agent is a system that perceives context, chooses actions, and pursues a goal.
details:
In classic AI and reinforcement learning, an agent interacts with an environment. In modern tooling, an agent may also combine a model with memory, tools, and multi-step decision logic.
example:
An AI coding agent can read files, choose tools, and make edits toward a user goal.
mnemonic:
Observe, decide, act.
recall:
- What makes a system an agent rather than only a predictor?
- How is the word agent used in both classic and modern AI contexts?
```

```concept-card
id: multimodal
term: Multimodal
parents:
- artificial-intelligence
related:
- large-language-model
- diffusion-model
- agent
summary:
Multimodal systems can work across more than one type of input or output, such as text, images, audio, or video.
details:
They can combine signals from multiple modalities or generate outputs in different forms, which makes them useful for richer assistant and creative workflows.
example:
A multimodal assistant can read a screenshot, answer questions about it, and generate a text explanation.
mnemonic:
More than one mode, one system.
recall:
- What does multimodal mean in AI systems?
- Why is multimodality useful for real applications?
```

```concept-card
id: diffusion-model
term: Diffusion Model
parents:
- generative-ai
- deep-learning
related:
- multimodal
summary:
A diffusion model generates data by learning how to reverse a gradual noising process.
details:
It starts from noise and iteratively denoises toward a coherent output, which is why diffusion models are widely used for image generation.
example:
Many text-to-image systems are based on diffusion models.
mnemonic:
Start noisy, refine step by step.
recall:
- Why are diffusion models associated with image generation?
- What is the core idea behind the denoising process?
```
