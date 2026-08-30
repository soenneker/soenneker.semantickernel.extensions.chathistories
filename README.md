[![](https://img.shields.io/nuget/v/soenneker.semantickernel.extensions.chathistories.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.semantickernel.extensions.chathistories/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.semantickernel.extensions.chathistories/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.semantickernel.extensions.chathistories/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.semantickernel.extensions.chathistories.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.semantickernel.extensions.chathistories/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.semantickernel.extensions.chathistories/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.semantickernel.extensions.chathistories/actions/workflows/codeql.yml)

# Soenneker.SemanticKernel.Extensions.ChatHistories

Text-oriented transformation and logging extensions for Semantic Kernel `ChatHistory`.

## Installation

```bash
dotnet add package Soenneker.SemanticKernel.Extensions.ChatHistories
```

## Transforming history

```csharp
using Microsoft.SemanticKernel.ChatCompletion;
using Soenneker.SemanticKernel.Extensions.ChatHistories;

var history = new ChatHistory();
history.AddSystemMessage("Answer concisely.");
history.AddUserMessage("What is the capital of France?");

ChatHistory prepended = history.WithPrependedSystemMessage(
    "Follow the application's safety policy.");

ChatHistory inserted = history.InsertSystemMessageAfterExistingSystemMessages(
    "Return plain text only.");

ChatHistory withoutSystemPrompts = history.RemoveSystemMessages();
ChatHistory copy = history.Clone();
```

The transformation methods return a new `ChatHistory`; they do not mutate the source. `CopyTo` appends projected messages to an existing target and does not clear it first.

`InsertSystemMessageAfterExistingSystemMessages` inserts before the first non-system message. If the history is empty or contains only system messages, it appends the new system message.

## Logging while adding messages

```csharp
history
    .AddUserMessageLogged("Summarize this document.", logger, LogLevel.Debug)
    .AddAssistantMessageLogged("The document describes...", logger, LogLevel.Debug);

history.LogAllMessages(logger, LogLevel.Trace);
```

The `Add*MessageLogged` methods mutate and return the same history for chaining. Passing a null logger still adds the message without logging it.

## Content fidelity

`CopyTo`, `Clone`, `WithPrependedSystemMessage`, `InsertSystemMessageAfterExistingSystemMessages`, and `RemoveSystemMessages` copy only each message's `Role` and text `Content`. They do not preserve author name, model ID, metadata, inner content, source, encoding, function/tool calls, images, audio, or other items in `ChatMessageContent.Items`.

Use these transformations only for text-only histories. For multimodal or tool-calling histories, write an application-specific copy that preserves the required item and metadata types.

## Sensitive data

The logging helpers write complete message content as structured log values with no redaction. Prompts and model responses can contain credentials, personal data, proprietary documents, or tool output. Do not enable these helpers in production unless the chosen log level, sink access, retention, and redaction policy are appropriate for that content.
