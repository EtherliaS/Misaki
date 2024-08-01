using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.App
{
    public class AppVersion : IComparable<AppVersion>
    {
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }
        public char BuildPostfix { get; }

        public AppVersion(int major, int minor, int patch, char buildPostfix)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            BuildPostfix = buildPostfix;
        }

        public static AppVersion Parse(string version)
        {
            var parts = version.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Invalid version format. Expected format: a.b.cd");
            }

            int major = int.Parse(parts[0]);
            int minor = int.Parse(parts[1]);
            int patch = int.Parse(parts[2].Substring(0, parts[2].Length - 1));
            char buildPostfix = parts[2][parts[2].Length - 1];

            return new AppVersion(major, minor, patch, buildPostfix);
        }

        public int CompareTo(AppVersion other)
        {
            if (Major != other.Major)
            {
                return Major.CompareTo(other.Major);
            }
            if (Minor != other.Minor)
            {
                return Minor.CompareTo(other.Minor);
            }
            if (Patch != other.Patch)
            {
                return Patch.CompareTo(other.Patch);
            }
            return BuildPostfix.CompareTo(other.BuildPostfix);
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}{BuildPostfix}";
        }

        public override bool Equals(object obj)
        {
            if (obj is AppVersion other)
            {
                return Major == other.Major && Minor == other.Minor && Patch == other.Patch && BuildPostfix == other.BuildPostfix;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Major, Minor, Patch, BuildPostfix);
        }
    }
}
