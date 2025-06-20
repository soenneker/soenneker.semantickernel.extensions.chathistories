[![](https://img.shields.io/nuget/v/soenneker.semantickernel.extensions.chathistories.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.semantickernel.extensions.chathistories/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.semantickernel.extensions.chathistories/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.semantickernel.extensions.chathistories/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.semantickernel.extensions.chathistories.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.semantickernel.extensions.chathistories/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.SemanticKernel.Extensions.ChatHistories

### A collection of helpful Semantic Kernel `ChatHistory` extension methods

This library provides composable and production-grade extension methods for working with `Microsoft.SemanticKernel.ChatCompletion.ChatHistory`. It includes utilities for transformation, logging, filtering, restructuring, and role-aware operations—ideal for agent frameworks, prompt engineering, and chat memory management.

---

## 🛠️ Installation

```bash
dotnet add package Soenneker.SemanticKernel.Extensions.ChatHistories
````

---

## ✨ Features

* 🧩 **Modular** utilities to copy, clone, transform, and manipulate chat history
* 🔍 Logging-aware `Add*MessageLogged(...)` methods with structured `ILogger` output
* 🧠 Intelligent system prompt injection:

  * At the beginning
  * After existing system messages
* 🧼 Role-based filtering (e.g., remove all system messages)
* 🧾 Clean composition for creating structured, valid chat histories

---

## 📦 Example Usage

```csharp
var history = new ChatHistory();

history.AddUserMessage("What is the capital of France?");
history.AddAssistantMessage("Paris is the capital of France.");

// Clone the chat history
var clone = history.Clone();

// Prepend a new system message
var withSystem = history.WithPrependedSystemMessage("You are a helpful assistant.");

// Insert system message after existing system messages
var withInsertedSystem = history.InsertSystemMessageAfterExistingSystemMessages("Follow strict format rules only.");
```

Add messages with logging:

```csharp
var logger = loggerFactory.CreateLogger("Chat");
history.AddUserMessageLogged("Hello!", logger);
history.AddAssistantMessageLogged("Hi there!", logger);
```

---

## 🧩 API Highlights

### 💬 Chat History Transformations

* `Clone()`: Deep copy of a `ChatHistory`
* `CopyTo(target)`: Copy messages to another history
* `WithPrependedSystemMessage(string)`: Insert a system prompt at the beginning
* `InsertSystemMessageAfterExistingSystemMessages(string)`: Add prompt after all existing system messages
* `RemoveSystemMessages()`: Filter out system-only messages

### 🔐 Logging Helpers

* `AddSystemMessageLogged(...)`
* `AddUserMessageLogged(...)`
* `AddAssistantMessageLogged(...)`
* `AddDeveloperMessageLogged(...)`

All logging helpers support `ILogger` and `LogLevel`.

... and more