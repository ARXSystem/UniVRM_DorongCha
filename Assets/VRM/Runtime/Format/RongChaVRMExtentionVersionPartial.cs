﻿using System;
using System.Text.RegularExpressions;

namespace VRM
{
    public static partial class RongChaVRMExtentionVersion
    {
        /// <summary>
        /// Returns true if a passed version is newer than current RongChaVRMExtention.
        /// </summary>
        public static bool IsNewer(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                return false;
            }

            var prefix = "RongChaVRMExtention-";
            if (version.StartsWith(prefix))
            {
                version = version.Substring(prefix.Length);
            }

            return IsNewer(version, VERSION);
        }

        public static bool IsNewer(string newer, string older)
        {
            Version newerVersion;
            if (!ParseVersion(newer, out newerVersion))
            {
                return false;
            }

            Version olderVersion;
            if (!ParseVersion(older, out olderVersion))
            {
                return false;
            }

            if (newerVersion.Major > olderVersion.Major)
            {
                return true;
            }

            if (newerVersion.Minor > olderVersion.Minor)
            {
                return true;
            }

            if (newerVersion.Patch > olderVersion.Patch)
            {
                return true;
            }

            if (String.Compare(newerVersion.Pre, olderVersion.Pre) > 0)
            {
                return true;
            }

            return false;
        }

        private static readonly Regex VersionSpec =
            new Regex(@"(?<major>\d+)\.(?<minor>\d+)(\.(?<patch>\d+))?(-(?<pre>[0-9A-Za-z-]+))?");

        public static bool ParseVersion(string version, out Version v)
        {
            var match = VersionSpec.Match(version);
            if (!match.Success)
            {
                v = new Version();
                return false;
            }

            v = new Version();
            try
            {
                v.Major = int.Parse(match.Groups["major"].Value);
                v.Minor = int.Parse(match.Groups["minor"].Value);
                v.Patch = match.Groups["patch"].Success ? int.Parse(match.Groups["patch"].Value) : 0;
                v.Pre = match.Groups["pre"].Success ? match.Groups["pre"].Value : "";

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public struct Version
        {
            public int Major;
            public int Minor;
            public int Patch;
            public string Pre;
        }

        public const string RONGCHA_VRM_EXTENTION_VERSION = "RongChaVRMExtention-" + VERSION;
    }
}
