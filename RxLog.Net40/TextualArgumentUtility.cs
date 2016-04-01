using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if !NO_LINQ
using System.Linq;
#endif
using static TextualArgumentOptions;

#if EXPOSE_EVERYTHING || EXPOSE_TEXTARGS
public
#endif
static class TextualArgumentUtility
{
    const char RegularQuoteChar = '"';
    const char ESLikeAdditionalQuoteChar = '\'';

    public static string EvaluateCommandLineArgument(string argument,
        TextualArgumentOptions options = Default)
    {
        if (!options.IsValid())
            throw new ArgumentOutOfRangeException(nameof(options));

        char? last = null;
        char? quote = null;

        var useESLike = options.Has(EnableEcmaScriptLike);
        var fullyQuoted = (argument[0] == RegularQuoteChar || useESLike && argument[0] == ESLikeAdditionalQuoteChar);
        var sb = new StringBuilder();

        foreach (var c in argument)
        {
            if (last == '\\')
                last = null;
            else
            {
                switch (c)
                {
                    case ESLikeAdditionalQuoteChar:
                        if (useESLike)
                            goto case RegularQuoteChar;
                        goto default;
                    case RegularQuoteChar:
                        if (c == quote)
                            quote = null;
                        else
                            quote = c;

                        if (c == last)
                            last = null;
                        else
                        {
                            last = c;
                            continue;
                        }
                        break;
                    case '\\':
                        var shouldEscape = false;
                        switch (options)
                        {
                            case MixCLikeEscape:
                            case MixEcmaScriptLikeEscape:
                                shouldEscape = true;
                                break;
                            case MixCLikeEscapeOnlyQuoted:
                            case MixEcmaScriptLikeEscapeOnlyQuoted:
                                shouldEscape = quote.HasValue;
                                break;
                            case MixCLikeEscapeOnlyFullyQuoted:
                            case MixEcmaScriptLikeEscapeOnlyFullyQuoted:
                                shouldEscape = fullyQuoted && quote.HasValue;
                                break;
                        }

                        if (shouldEscape)
                        {
                            last = '\\';
                            continue;
                        }

                        break;
                    default:
                        last = null;
                        break;
                }
            }
            sb.Append(c);
        }
        return sb.ToString();
    }

#if NETFX
    public static string[] GetCommandLineArguments(
        TextualArgumentOptions options = Default)
        => Environment.CommandLine.ReadCommandLineArguments(options);
#endif

    public static string ReadCommandLineArgument(this TextReader reader,
        TextualArgumentOptions options = Default)
    {
        if (!options.IsValid())
            throw new ArgumentOutOfRangeException(nameof(options));

        int cp;
        char c;
        for (;;)
        {
            cp = reader.Read();
            if (cp < 0) return null;

            c = (char)cp;
            if (!char.IsWhiteSpace(c))
                break;
        }

        char? quote = null;
        var useESLike = options.Has(EnableEcmaScriptLike);
        var fullyQuoted = (c == RegularQuoteChar || useESLike && c == ESLikeAdditionalQuoteChar);
        var sb = new StringBuilder();

        while (cp >= 0)
        {
            c = (char)cp;

            switch (c)
            {
                case ESLikeAdditionalQuoteChar:
                    if (useESLike)
                        goto case RegularQuoteChar;
                    goto default;
                case RegularQuoteChar:
                    if (c == quote)
                        quote = null;
                    else
                        quote = c;
                    break;
                case '\\':
                    var shouldEscape = false;
                    switch (options)
                    {
                        case MixCLikeEscape:
                        case MixEcmaScriptLikeEscape:
                            shouldEscape = true;
                            break;
                        case MixCLikeEscapeOnlyQuoted:
                        case MixEcmaScriptLikeEscapeOnlyQuoted:
                            shouldEscape = quote.HasValue;
                            break;
                        case MixCLikeEscapeOnlyFullyQuoted:
                        case MixEcmaScriptLikeEscapeOnlyFullyQuoted:
                            shouldEscape = fullyQuoted && quote.HasValue;
                            break;
                    }

                    if (shouldEscape)
                    {
                        sb.Append('\\');

                        cp = reader.Read();
                        if (cp < 0) continue;

                        c = (char)cp;
                    }
                    break;
                default:
                    if (!quote.HasValue && char.IsWhiteSpace(c))
                    {
                        cp = -1;
                        continue;
                    }
                    break;
            }
            sb.Append(c);

            cp = reader.Read();
        }
        return sb.ToString();
    }
    public static IEnumerable<string> ReadCommandLineArguments(this TextReader reader,
        TextualArgumentOptions options = Default)
    {
        string argument;
        for (;;)
        {
            argument = ReadCommandLineArgument(reader, options);
            if (argument == null) yield break;
            yield return argument;
        }
    }
#if NO_LINQ
    public static string[] ReadCommandLineArguments(this string text,
        TextualArgumentOptions options = Default)
    {
        using (var reader = new StringReader(text))
        {
            var list = new List<string>();
            foreach(var arg in ReadCommandLineArguments(reader, options))
                list.Add(arg);
            return list.ToArray();
        }
    }
#else
    public static string[] ReadCommandLineArguments(this string text,
        TextualArgumentOptions options = Default)
    {
        using (var reader = new StringReader(text))
            return ReadCommandLineArguments(reader, options).ToArray();
    }
#endif
}
