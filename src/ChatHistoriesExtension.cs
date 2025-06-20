using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Diagnostics.Contracts;

namespace Soenneker.SemanticKernel.Extensions.ChatHistories;

/// <summary>
/// A collection of helpful extension methods for working with Semantic Kernel's <see cref="ChatHistory"/>.
/// Includes logging, transformation, and utility operations.
/// </summary>
public static class ChatHistoryLoggingExtensions
{
    /// <summary>
    /// Copies all messages from the source <see cref="ChatHistory"/> into the target <see cref="ChatHistory"/>.
    /// </summary>
    /// <param name="source">The source chat history containing the messages to copy.</param>
    /// <param name="target">The target chat history to which the messages will be copied.</param>
    public static void CopyTo(this ChatHistory source, ChatHistory target)
    {
        foreach (ChatMessageContent message in source)
        {
            target.AddMessage(message.Role, message.Content);
        }
    }

    /// <summary>
    /// Creates a new <see cref="ChatHistory"/> with the specified system message prepended to the beginning.
    /// All original messages are preserved and appended after the system message.
    /// </summary>
    /// <param name="history">The original chat history to copy.</param>
    /// <param name="systemMessage">The system message to insert at the beginning.</param>
    /// <returns>A new <see cref="ChatHistory"/> with the system message prepended.</returns>
    [Pure]
    public static ChatHistory WithPrependedSystemMessage(this ChatHistory history, string systemMessage)
    {
        var newHistory = new ChatHistory();
        newHistory.AddSystemMessage(systemMessage);
        history.CopyTo(newHistory);
        return newHistory;
    }

    /// <summary>
    /// Creates a new <see cref="ChatHistory"/> with a system message inserted
    /// after all existing system messages, but before any other message type.
    /// </summary>
    /// <param name="history">The original chat history to copy.</param>
    /// <param name="content">The system message content to insert.</param>
    /// <returns>A new <see cref="ChatHistory"/> with the inserted system message.</returns>
    [Pure]
    public static ChatHistory InsertSystemMessageAfterExistingSystemMessages(this ChatHistory history, string content)
    {
        var newHistory = new ChatHistory();
        bool inserted = false;

        for (int i = 0; i < history.Count; i++)
        {
            ChatMessageContent message = history[i];

            if (!inserted && message.Role != AuthorRole.System)
            {
                newHistory.AddSystemMessage(content);
                inserted = true;
            }

            newHistory.AddMessage(message.Role, message.Content);
        }

        if (!inserted)
        {
            newHistory.AddSystemMessage(content);
        }

        return newHistory;
    }

    /// <summary>
    /// Removes all system messages from the chat history.
    /// </summary>
    /// <param name="history">The original chat history to filter.</param>
    /// <returns>A new <see cref="ChatHistory"/> with only non-system messages.</returns>
    [Pure]
    public static ChatHistory RemoveSystemMessages(this ChatHistory history)
    {
        var filtered = new ChatHistory();

        foreach (ChatMessageContent message in history)
        {
            if (message.Role != AuthorRole.System)
                filtered.AddMessage(message.Role, message.Content);
        }

        return filtered;
    }

    /// <summary>
    /// Creates a deep copy of the chat history, preserving all messages and roles.
    /// </summary>
    /// <param name="history">The original chat history to clone.</param>
    /// <returns>A new <see cref="ChatHistory"/> that is a copy of the original.</returns>
    [Pure]
    public static ChatHistory Clone(this ChatHistory history)
    {
        var clone = new ChatHistory();

        foreach (ChatMessageContent message in history)
        {
            clone.AddMessage(message.Role, message.Content);
        }

        return clone;
    }

    /// <summary>
    /// Adds a system message to the chat history and logs the content at the specified log level.
    /// </summary>
    /// <param name="history">The chat history to modify.</param>
    /// <param name="content">The content of the system message.</param>
    /// <param name="logger">Optional logger to record the message.</param>
    /// <param name="level">The log level to use when logging the message. Defaults to Information.</param>
    /// <returns>The modified <see cref="ChatHistory"/> instance.</returns>
    public static ChatHistory AddSystemMessageLogged(this ChatHistory history, string content, ILogger? logger = null, LogLevel level = LogLevel.Information)
    {
        history.AddSystemMessage(content);
        logger?.Log(level, "System message: {Content}", content);
        return history;
    }

    /// <summary>
    /// Adds a user message to the chat history and logs the content at the specified log level.
    /// </summary>
    /// <param name="history">The chat history to modify.</param>
    /// <param name="content">The content of the user message.</param>
    /// <param name="logger">Optional logger to record the message.</param>
    /// <param name="level">The log level to use when logging the message. Defaults to Information.</param>
    /// <returns>The modified <see cref="ChatHistory"/> instance.</returns>
    public static ChatHistory AddUserMessageLogged(this ChatHistory history, string content, ILogger? logger = null, LogLevel level = LogLevel.Information)
    {
        history.AddUserMessage(content);
        logger?.Log(level, "User message: {Content}", content);
        return history;
    }

    /// <summary>
    /// Adds an assistant message to the chat history and logs the content at the specified log level.
    /// </summary>
    /// <param name="history">The chat history to modify.</param>
    /// <param name="content">The content of the assistant message.</param>
    /// <param name="logger">Optional logger to record the message.</param>
    /// <param name="level">The log level to use when logging the message. Defaults to Information.</param>
    /// <returns>The modified <see cref="ChatHistory"/> instance.</returns>
    public static ChatHistory AddAssistantMessageLogged(this ChatHistory history, string content, ILogger? logger = null, LogLevel level = LogLevel.Information)
    {
        history.AddAssistantMessage(content);
        logger?.Log(level, "Assistant message: {Content}", content);
        return history;
    }

    /// <summary>
    /// Adds a developer message to the chat history and logs the content at the specified log level.
    /// </summary>
    /// <param name="history">The chat history to modify.</param>
    /// <param name="content">The content of the developer message.</param>
    /// <param name="logger">Optional logger to record the message.</param>
    /// <param name="level">The log level to use when logging the message. Defaults to Information.</param>
    /// <returns>The modified <see cref="ChatHistory"/> instance.</returns>
    public static ChatHistory AddDeveloperMessageLogged(this ChatHistory history, string content, ILogger? logger = null, LogLevel level = LogLevel.Information)
    {
        history.AddDeveloperMessage(content);
        logger?.Log(level, "Developer message: {Content}", content);
        return history;
    }
}
