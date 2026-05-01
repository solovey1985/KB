# devinterview MCP Server — Usage Guide

The server exposes two tools over the **MCP STDIO transport**:

| Tool | Description |
|---|---|
| `get_questions` | Fetch the full question list for a topic (metadata only, no answers) |
| `get_answers` | Fetch free answer text for a topic, with optional order-number range filter |

---

## Prerequisites

```bash
pip install -r requirements.txt
```

All dependencies (`requests`, `mcp[cli]`, `pydantic`, `pydantic-settings`,
`httpx-sse`, `sse-starlette`, `jsonschema`) must be installed.

---

## Running the server manually

```bash
python mcp_server.py
```

The process communicates over **stdin / stdout** using MCP JSON-RPC.
It has no HTTP port and produces no console output of its own — all
communication is through the pipe.

---

## Connecting from Claude Desktop

Add the following block to your `claude_desktop_config.json`
(`%APPDATA%\Claude\claude_desktop_config.json` on Windows,
`~/Library/Application Support/Claude/claude_desktop_config.json` on macOS):

```json
{
  "mcpServers": {
    "devinterview": {
      "command": "python",
      "args": ["E:\\Learn\\questions-parser\\mcp_server.py"]
    }
  }
}
```

Restart Claude Desktop. The tools `get_questions` and `get_answers` will
appear automatically in the tool picker.

---

## Connecting from OpenCode

Add the server to your OpenCode config. This repository keeps the same MCP
configuration in both project-local files:

- `.opencode.json`
- `.opencode/config.jsonc`

```json
{
  "$schema": "https://opencode.ai/config.json",
  "mcp": {
    "devinterview": {
      "type": "local",
      "command": ["python", "E:\\Learn\\questions-parser\\mcp_server.py"],
      "enabled": true
    }
  },
  "permission": {
    "devinterview_*": "allow"
  }
}
```

After updating the config, restart OpenCode so the MCP tool namespace is
registered. The available tool calls are `devinterview_get_questions` and
`devinterview_get_answers`.

---

## Connecting from any MCP-compatible agent host

The server requires only a stdio pipe. Any host that supports the MCP STDIO
transport can launch it with:

```
command: python
args:    ["<absolute-path-to>/mcp_server.py"]
```

No environment variables or config files are required.

---

## Tools reference

### `get_questions`

Returns a JSON array of all questions for a topic, including premium ones
(flagged with `isPremium: true`).

**Parameters**

| Name | Type | Required | Description |
|---|---|---|---|
| `topic` | string | yes | Topic slug, e.g. `"net-core"`, `"react"`, `"python"` |

**Example agent prompt**

> List all C# interview questions grouped by difficulty.

The agent will call:
```json
{ "name": "get_questions", "arguments": { "topic": "c-sharp" } }
```

**Response shape**

```json
[
  {
    "id": "cSharp1",
    "orderNumber": 1,
    "question": "What is the difference between value types and reference types in C#?",
    "category": "C# Fundamentals",
    "topic": "C Sharp",
    "topicRef": "c-sharp",
    "isPremium": false,
    "difficulty": 1,
    "isChallenge": false,
    "codeExample": null
  },
  ...
]
```

---

### `get_answers`

Returns a JSON array of answer objects for a topic. Only free questions have
accessible answers; premium questions are silently skipped.

**Parameters**

| Name | Type | Required | Description |
|---|---|---|---|
| `topic` | string | yes | Topic slug |
| `from_order` | int | no | First question number to include (inclusive, 1-based) |
| `to_order` | int | no | Last question number to include (inclusive) |
| `max_answers` | int | no | Hard cap on returned answers after range filtering |

**Example agent prompts**

> Give me answers to the first 5 React questions.

```json
{ "name": "get_answers", "arguments": { "topic": "react", "max_answers": 5 } }
```

> What does devinterview.io say about SQL JOINs? (questions 3–8)

```json
{ "name": "get_answers", "arguments": { "topic": "sql", "from_order": 3, "to_order": 8 } }
```

**Response shape**

```json
[
  {
    "orderNumber": 1,
    "question": "What is a SQL JOIN?",
    "answer": "A JOIN clause is used to combine rows from two or more tables..."
  },
  ...
]
```

---

## Known topic slugs

```
ado-net          agile-and-scrum  android          angular
angular-js       asp-net          asp-net-mvc      asp-net-web-api
aws              azure            c-sharp          cosmos-db
css              dependency-injection  devops      django
entity-framework express          flutter          git
golang           graphql          html5            ionic
java             javascript       jquery           kotlin
laravel          linq             mongodb          net-core
next             node             objective-c      oop
php              pwa              python           react
react-native     reactive-programming  reactive-systems  redis
redux            ruby             ruby-on-rails    rust
spring           sql              swift            t-sql
testing          typescript       ux-design        vue
wcf              web-security     websocket        wpf
xamarin
```

---

## Notes

- Answers are only available for the first ~15 questions per topic (`isFree=true`
  on devinterview.io). Premium questions always return empty and are skipped.
- The `answer` field uses a markdown-like format with `_underscores_` for
  emphasis and `**double asterisks**` for bold — standard markdown renderers
  handle this correctly.
- The server makes live HTTP calls to the Firestore REST API on every tool
  invocation; there is no local cache.
