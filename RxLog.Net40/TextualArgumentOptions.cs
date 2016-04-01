#if EXPOSE_EVERYTHING || EXPOSE_TEXTARGS
public
#endif
enum TextualArgumentOptions
{
    Default = 0,
    MixCLikeEscape = 1,
    MixCLikeEscapeOnlyQuoted = 2,
    MixCLikeEscapeOnlyFullyQuoted = 3,

    EnableEcmaScriptLike = 4,
    MixEcmaScriptLikeEscape = MixCLikeEscape | EnableEcmaScriptLike,
    MixEcmaScriptLikeEscapeOnlyQuoted = MixCLikeEscapeOnlyQuoted | EnableEcmaScriptLike,
    MixEcmaScriptLikeEscapeOnlyFullyQuoted = MixCLikeEscapeOnlyFullyQuoted | EnableEcmaScriptLike,
}

#if EXPOSE_EVERYTHING || EXPOSE_TEXTARGS
public
#endif
static class TextualArgumentOptionsEx
{
    public static bool IsValid(this TextualArgumentOptions value)
        => TextualArgumentOptions.MixEcmaScriptLikeEscapeOnlyFullyQuoted.Has(value);
}