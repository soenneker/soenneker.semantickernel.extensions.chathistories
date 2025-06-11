using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Soenneker.SemanticKernel.Extensions.ChatHistories;

/// <summary>
/// A collection of helpful Semantic Kernel ChatHistory extension methods
/// </summary>
public static class ChatHistoryLoggingExtensions
{
    /// <summary>
    /// Adds a system message and logs it.
    /// </summary>
    public static ChatHistory AddSystemMessageLogged(this ChatHistory history, string content, ILogger? logger = null, LogLevel level = LogLevel.Information)
    {
        history.AddSystemMessage(content);
        logger?.Log(level, "System message: {Content}", content);
        return history;
    }

    /// <summary>
    /// Adds a user message and logs it.
    /// </summary>
    public static ChatHistory AddUserMessageLogged(this ChatHistory history, string content, ILogger? logger = null, LogLevel level = LogLevel.Information)
    {
        history.AddUserMessage(content);
        logger?.Log(level, "User message: {Content}", content);
        return history;
    }

    /// <summary>
    /// Adds an assistant message and logs it.
    /// </summary>
    public static ChatHistory AddAssistantMessageLogged(this ChatHistory history, string content, ILogger? logger = null, LogLevel level = LogLevel.Information)
    {
        history.AddAssistantMessage(content);
        logger?.Log(level, "Assistant message: {Content}", content);
        return history;
    }

    /// <summary>
    /// Adds a developer message and logs it.
    /// </summary>
    public static ChatHistory AddDeveloperMessageLogged(this ChatHistory history, string content, ILogger? logger = null, LogLevel level = LogLevel.Information)
    {
        history.AddDeveloperMessage(content);
        logger?.Log(level, "Developer message: {Content}", content);
        return history;
    }
}