using System.Text.RegularExpressions;

namespace Blog.Domain.ValueObjects;

public sealed class Slug : IEquatable<Slug>
{
    public const int MaxLength = 200;
    private static readonly Regex WhitespaceRegex = new(@"\s+", RegexOptions.Compiled);
    private static readonly Regex InvalidCharsRegex = new(@"[^a-z0-9\-]", RegexOptions.Compiled);
    private static readonly Regex MultipleDashesRegex = new(@"\-+", RegexOptions.Compiled);

    public string Value { get; }

    private Slug(string value)
    {
        Value = value;
    }

    public static Slug Generate(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required to generate a slug.", nameof(title));
        }

        var normalized = Normalize(title);
        return new Slug(normalized);
    }

    public static Slug FromValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Slug value cannot be empty.", nameof(value));
        }

        var normalized = Normalize(value);
        return new Slug(normalized);
    }

    public override string ToString() => Value;

    public bool Equals(Slug? other)
    {
        if (other is null)
        {
            return false;
        }

        return string.Equals(Value, other.Value, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj) => Equals(obj as Slug);

    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(Value);

    public static bool operator ==(Slug? left, Slug? right) => Equals(left, right);
    public static bool operator !=(Slug? left, Slug? right) => !Equals(left, right);

    private static string Normalize(string value)
    {
        var slug = value.Trim().ToLowerInvariant();
        slug = WhitespaceRegex.Replace(slug, "-");
        slug = InvalidCharsRegex.Replace(slug, string.Empty);
        slug = MultipleDashesRegex.Replace(slug, "-");
        slug = slug.Trim('-');
        if (slug.Length > MaxLength)
        {
            slug = slug[..MaxLength].Trim('-');
        }

        if (string.IsNullOrWhiteSpace(slug))
        {
            throw new ArgumentException("Slug cannot be empty after normalization.", nameof(value));
        }

        return slug;
    }
}
