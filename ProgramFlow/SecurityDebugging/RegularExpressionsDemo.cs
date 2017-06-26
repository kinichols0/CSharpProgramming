﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CSharpProgramming.SecurityDebugging
{
    public class RegularExpressionsDemo
    {
        public static void CharacterClassSyntaxDemo()
        {
            Console.WriteLine("Regex Character Class Syntax demo\n");
            string input, pattern;

            // Character class. Matches any character in the set
            pattern = "[aeiou]";
            input = "Hello my name is Bob";
            ProcessMatching(input, pattern);

            // Negated character class. Match characters not in the set
            pattern = "[^ei]";
            input = "Melvin";
            ProcessMatching(input, pattern);

            // Range character class. Match characters within range
            pattern = "[0-5]";
            input = "My daughter is 5 and I am 35.";
            ProcessMatching(input, pattern);

            // Wildcard, "." matches any character. Below patter will match 
            // any tokens starting with b and ends with d
            pattern = "b.d";
            input = " bed bad also abealgggdaa";
            ProcessMatching(input, pattern);

            // "\w" matches any one word character
            pattern = @"[Hm]\w";
            input = "Hello my name is blah";
            ProcessMatching(input, pattern);

            // "\W" matches any one none-word character
            pattern = @"\W";
            input = "Hello| my$name-is(blah!";
            ProcessMatching(input, pattern);

            // "\s" matches any white-space
            pattern = @"\s";
            input = "Hello my name is blah";
            ProcessMatching(input, pattern);

            // "\S" matches any non-white-space character
            pattern = @"\S";
            input = "Hello there";
            ProcessMatching(input, pattern);

            // "\d" matches any decimal digit
            pattern = @"\d";
            input = "124.455.566";
            ProcessMatching(input, pattern);

            // "\D" matches any character other than a decimal digit
            pattern = @"\D";
            input = "124.455.566";
            ProcessMatching(input, pattern);
        }

        /// <summary>
        /// Anchors help determin the match being a pass fail based on the
        /// current position in the string
        /// </summary>
        public static void AnchorSyntaxDemo()
        {
            Console.WriteLine("Regex Anchor Syntax demo\n");
            string input, pattern;

            // "^" the match must be at the beginning or line
            pattern = "^240";
            input = "240-555-7878";
            ProcessMatching(input, pattern);

            // "$" the match must occur at the end of string
            pattern = "7878$";
            input = "240-55-7878";
            ProcessMatching(input, pattern);

            // "\A match must occur at the start of string
            pattern = @"\A240";
            input = "240-555-7878";
            ProcessMatching(input, pattern);

            // "\Z" match must occur at the end of string or before \n at the end of the string
            pattern = @"555\Z";
            input = "240-555-555";
            ProcessMatching(input, pattern);

            // "\z" match must occur at the end of string or before \n at the end of the string
            pattern = @"-5355\z";
            input = "240-555-5355";
            ProcessMatching(input, pattern);

            // "\G" match must occur at the point the previous match ended
            pattern = @"\GHello";
            input = "HelloHello";
            ProcessMatching(input, pattern);

            // "\b" match must occur on a boundary between a "\w" (alphanumeric) 
            // and "\W" nonalphanumeric character
            pattern = @"\bthis\sis\b";
            input = "Hello|this is|Hello |this is ";
            ProcessMatching(input, pattern);

            // "\B" match must not occur on a "\b" boundary
            pattern = @"\Bcat\B";
            input = "Tomcat Certificate blackcatdown";
            ProcessMatching(input, pattern);
        }

        public static void GroupingConstructsDemo()
        {
            Console.WriteLine("Regex Grouping Syntax demo\n");
            string input, pattern;

            // "(subexpresion)" captures the match and assigns it a one-based ordinal number
            // matches a word(first captured group), a space and a repeating word (second captured group)
            pattern = @"(\w+)\s(\1)\s\w+\s(\2)\W";
            input = "Hello there there my there name is is James";
            ProcessMatching(input, pattern);

            // "(?<name>subexpression) or "(?'name'subexpression)" named captured group
            // use \k<name> to reference the named expression
            pattern = @"(?<duplicateWord>\w+)\s\k<duplicateWord>\W(?<nextWord>\w+)";
            input = "He said that that was the the correct answer";
            ProcessMatching(input, pattern);

            // "(?:subexpression)" do not assign subexpression to captured group
            pattern = @"(Write)(?:Line)";
            input = "Console.WriteLine or Console.WriteOrNot";
            ProcessMatching(input, pattern);

            // "(?i:subexpression)" turns on case insentivity group options
            // x - ignore white spacing
            // 
            pattern = @"(?i:\s*hello\s*)+";
            input = "hello HellohellO HELLO";
            ProcessMatching(input, pattern);

            // "(?=subexpression)" zero width positive lookahead, matches any
            // epxression before the group subexpression
            pattern = @"(abc(?=\W))+";
            input = "abc|fgh|abc#";
            ProcessMatching(input, pattern);

            // "(?!subexpression)" zero width negative lookahead, matches any
            // string which is not followed by the subexpression
            pattern = @"(\b(?!end)\w+\b)";
            input = "ending this is the end. this is not the end.";
            ProcessMatching(input, pattern);

            // "(?<=subexpression)" zero width positive lookbehind. Continue match
            // if previous expression is preceded by the subexpression
            pattern = @"(?<=\b\d{3})\d{3}\b";
            input = "12123 234234 334567 1234";
            ProcessMatching(input, pattern);

            // "(?<!subexpression)" zero width negative lookbehind. Continue match
            // if previous expression is not preceded by the subexpression
            pattern = @"(?<!\b\d{3})\d{3}\b";
            input = "12123 234234 334567 1234";
            ProcessMatching(input, pattern);

            // "(?>subexpression)" nonbacktracking subexpressions. Disables backtracking
            // when attempting to goback and find a match
            pattern = @"(\w)\1+.\b";
            input = "aaaa";
            ProcessMatching(input, pattern);
            pattern = @"(?>(\w)\1+).\b";
            input = "aaaa";
            ProcessMatching(input, pattern);
        }

        /// <summary>
        /// Print pattern, input, and matches to the console
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        private static void ProcessMatching(string input, string pattern)
        {
            MatchCollection matches = Regex.Matches(input, pattern);
            List<string> matchStrings = new List<string>();
            foreach (Match match in matches)
            {
                matchStrings.Add(string.Format(@"""{0}""", match.Value));
            }
            Console.WriteLine("Pattern: {0}\nInput: {1}\nMatch Count: {2}\nMatches: {3}\n",
                pattern, input, matches.Count, string.Join(",", matchStrings));
        }
    }
}
