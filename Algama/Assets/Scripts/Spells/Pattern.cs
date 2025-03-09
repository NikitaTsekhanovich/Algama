using System.Collections.Generic;
using Players;

namespace Spells
{
    public class Pattern
    {
        public int Length { get; }

        private readonly Dictionary<MagickElementSource, int> _map = new();

        public Pattern(params MagickElementSource[] elementSources)
        {
            foreach (var source in elementSources)
            {
                if (_map.ContainsKey(source))
                    _map[source] += 1;
                else
                    _map[source] = 1;
            }

            Length = elementSources.Length;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Pattern otherPattern) 
                return false;
            
            foreach (var (source, targetQuanity) in otherPattern._map)
            {   
                if (!_map.TryGetValue(source, out var quanity))
                    return false;

                if (targetQuanity > quanity)
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}