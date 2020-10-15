using System;
using System.Linq;

namespace All.About.Objects
{
    public class TagUri
    {
        public const string Scheme = "tag";

        public TagUri(string authority, string specific, string fragment = "") : this(authority, DateTimeOffset.Now, specific, fragment) { }

        public TagUri(string authority, DateTimeOffset date, string specific, string fragment = "")
        {
            if (authority is null) throw new ArgumentNullException(nameof(authority));
            if (specific is null) throw new ArgumentNullException(nameof(specific));
            if (fragment is null) throw new ArgumentNullException(nameof(fragment));

            if (authority.All(character => char.IsWhiteSpace(character))) throw new ArgumentException("Value cannot be empty", nameof(authority));
            if (specific.All(character => char.IsWhiteSpace(character))) throw new ArgumentException("Value cannot be empty", nameof(specific));

            Authority = authority;

            var utcDate = date.UtcDateTime.Date;
            if (utcDate > DateTime.UtcNow) throw new ArgumentException("Tag specified for a future day", nameof(date));
            Date = utcDate;

            Specific = specific;
            Fragment = fragment;
        }

        public string Authority { get; }

        public DateTime Date { get; }

        public string Specific { get; }

        public string Fragment { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is TagUri other))
            {
                return false;
            }

            return Authority == other.Authority
                && Date == other.Date
                && Specific == other.Specific
                && Fragment == other.Fragment;
        }

        public override int GetHashCode() => (Authority, Date, Specific, Fragment).GetHashCode();

        public override string ToString()
        {
            return $"{Scheme}:{Authority},{Date:yyyy-MM-dd}:{Specific}{(string.IsNullOrWhiteSpace(Fragment) ? "" : $"#{Fragment}")}";
        }

        public static bool operator ==(TagUri a, TagUri b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(TagUri a, TagUri b)
        {
            return !a.Equals(b);
        }

        public static bool TryParse(string value, out TagUri result)
        {
            result = default;

            if (value == null) return false;

            var schemeAndSeperator = $"{Scheme}:";

            var correctScheme = value.StartsWith(schemeAndSeperator);
            if (!correctScheme) return false;

            var partAfterScheme = value[schemeAndSeperator.Length..];
            var authorityAndRest = partAfterScheme.Split(',', 2, StringSplitOptions.RemoveEmptyEntries);

            var authority = authorityAndRest[0];

            var partAfterAuthority = authorityAndRest[1];
            var dateAndRest = partAfterAuthority.Split(':', 2, StringSplitOptions.RemoveEmptyEntries);

            var canParseDate = TryParseDate(dateAndRest[0], out var date);
            if (!canParseDate) return false;

            var partAfterDate = dateAndRest[1];
            var specificAndRest = partAfterDate.Split('#', 2, StringSplitOptions.RemoveEmptyEntries);

            var specific = specificAndRest[0];

            var fragment = specificAndRest.Length == 2
                ? specificAndRest[1]
                : "";

            result = new TagUri(authority, date, specific, fragment);
            return true;
        }

        public static TagUri Parse(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            var schemeAndSeperator = $"{Scheme}:";

            var correctScheme = value.StartsWith(schemeAndSeperator);
            if (!correctScheme) throw new ArgumentException(nameof(value));

            var partAfterScheme = value[schemeAndSeperator.Length..];
            var authorityAndRest = partAfterScheme.Split(',', 2, StringSplitOptions.RemoveEmptyEntries);

            var authority = authorityAndRest[0];

            var partAfterAuthority = authorityAndRest[1];
            var dateAndRest = partAfterAuthority.Split(':', 2, StringSplitOptions.RemoveEmptyEntries);

            var canParseDate = TryParseDate(dateAndRest[0], out var date);
            if (!canParseDate) throw new FormatException("Incorrect date format");

            var partAfterDate = dateAndRest[1];
            var specificAndRest = partAfterDate.Split('#', 2, StringSplitOptions.RemoveEmptyEntries);

            var specific = specificAndRest[0];

            var fragment = specificAndRest.Length == 2
                ? specificAndRest[1]
                : "";

            return new TagUri(authority, date, specific, fragment);
        }

        private static bool TryParseDate(string dateString, out DateTime date)
        {
            date = default;

            var month = 1;
            var day = 1;

            var dateParts = dateString.Split('-', StringSplitOptions.RemoveEmptyEntries);
            if (dateParts.Length > 3) return false;

            var yearString = dateParts[0];
            if (yearString.Length != 4) return false;
            var canParseYear = int.TryParse(yearString, out var year);
            if (!canParseYear) return false;

            if (dateParts.Length > 1)
            {
                var monthString = dateParts[1];
                if (monthString.Length != 2) return false;
                var canParseMonth = int.TryParse(monthString, out month);
                if (!canParseMonth) return false;
            }

            if (dateParts.Length > 2)
            {
                var dayString = dateParts[2];
                if (dayString.Length != 2) return false;
                var canParseDay = int.TryParse(dayString, out day);
                if (!canParseDay) return false;
            }

            date = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            return true;
        }
    }
}