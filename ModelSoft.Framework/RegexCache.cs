using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModelSoft.Framework
{
    public interface IRegexCache
    {
        Regex GetRegex(string pattern);
        Regex GetRegex(string pattern, RegexOptions options);

        void Clean();
    }

    public class RegexCache : IRegexCache
    {
        private readonly ConcurrentDictionary<RegexCacheKey, Regex> _cache;

        public RegexCache()
        {
            _cache = new ConcurrentDictionary<RegexCacheKey, Regex>();
        }

        
        private static readonly RegexCache _Default = new RegexCache();
        public static RegexCache Default
        {
            get { return _Default; }
        }

        
        private static RegexCache _Current;
        public static RegexCache Current
        {
            get { return _Current ?? _Default; }
        }


        public Regex GetRegex(string pattern)
        {
            return _cache.GetOrAdd(new RegexCacheKey(pattern), key => new Regex(key.Pattern));
        }

        public Regex GetRegex(string pattern, RegexOptions options)
        {
            return _cache.GetOrAdd(new RegexCacheKey(pattern, options), key => new Regex(key.Pattern, key.Options));
        }

        public void Clean()
        {
            _cache.Clear();
        }

        struct RegexCacheKey
        {
            public RegexCacheKey(string pattern) : this()
            {
                Pattern = pattern;
                Options = RegexOptions.None;
            }

            public RegexCacheKey(string pattern, RegexOptions options)
            {
                Pattern = pattern;
                Options = options;
            }

            public readonly string Pattern;
            public readonly RegexOptions Options;

            public bool Equals(RegexCacheKey other)
            {
                return string.Equals(Pattern, other.Pattern) && Options == other.Options;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is RegexCacheKey && Equals((RegexCacheKey) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Pattern != null ? Pattern.GetHashCode() : 0)*397) ^ (int) Options;
                }
            }

            public override string ToString()
            {
                return string.Format("Pattern: {0}, Options: {1}", Pattern, Options);
            }
        }

    }
}
