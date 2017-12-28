using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace nPrimeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetConventions();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static void SetConventions()
        {
            ConventionPack cp = new ConventionPack
            {
                new SeperateWordsNamingConvention(),
                new LowerCaseElementNameConvetion()
            };
            ConventionRegistry.Register("nPrimeDev", cp, type => true);
        }

        private class LowerCaseElementNameConvetion : IMemberMapConvention
        {
            public string Name => nameof(LowerCaseElementNameConvetion);
            public void Apply(BsonMemberMap memberMap)
            {
                memberMap.SetElementName(memberMap.ElementName.ToLower());
            }
        }

        private class SeperateWordsNamingConvention : IMemberMapConvention
        {
            private static readonly Regex s_seperateWordRegex =
                new Regex(@"
                   (?<=[A-Z])(?=[A-Z][a-z]) |
                   (?<=[^A-Z])(?=[A-Z]) |
                   (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            public string Name => nameof(SeperateWordsNamingConvention);
            public void Apply(BsonMemberMap memberMap)
            {
                var replace = s_seperateWordRegex.Replace(memberMap.ElementName, "_");
                memberMap.SetElementName(replace);
            }
        }
    }
}
