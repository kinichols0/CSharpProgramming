using System;
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
        /// <summary>
        /// Character classes define a set of characters, any one of which can
        /// occur in an input string for a match to succeed.
        /// </summary>
        public static void CharacterClassSyntaxDemo()
        {
            Console.WriteLine("Regex Character Class Syntax demo\n");
            string input, pattern;

            // Character class. Matches any character in the set
            pattern = "[aeiou]";
            input = "Hello my name is Bob";
            ProcessMatches(input, pattern);

            // Negated character class. Match characters not in the set
            pattern = "[^ei]";
            input = "Melvin";
            ProcessMatches(input, pattern);

            // Range character class. Match characters within range
            pattern = "[0-5]";
            input = "My daughter is 5 and I am 35.";
            ProcessMatches(input, pattern);

            // Wildcard, "." matches any character. Below patter will match 
            // any tokens starting with b and ends with d
            pattern = "b.d";
            input = " bed bad also abealgggdaa";
            ProcessMatches(input, pattern);

            // "\w" matches any one word character
            pattern = @"[Hm]\w";
            input = "Hello my name is blah";
            ProcessMatches(input, pattern);

            // "\W" matches any one none-word character
            pattern = @"\W";
            input = "Hello| my$name-is(blah!";
            ProcessMatches(input, pattern);

            // "\s" matches any white-space
            pattern = @"\s";
            input = "Hello there";
            ProcessMatches(input, pattern);

            // "\S" matches any non-white-space character
            pattern = @"\S";
            input = "Hello there";
            ProcessMatches(input, pattern);

            // "\d" matches any decimal digit
            pattern = @"\d";
            input = "124.455.566";
            ProcessMatches(input, pattern);

            // "\D" matches any character other than a decimal digit
            pattern = @"\D";
            input = "124.455.566";
            ProcessMatches(input, pattern);

            // "\p{name}" matches any character part of the unicode general category or named block
            pattern = @"\p{P}hello";
            input = ".hello !hello 1hello ?hello";
            ProcessMatches(input, pattern);

            // "\P{name}" matches any character not part of the unicode general category or named block
            pattern = @"\P{P}hello";
            input = ".hello !hello 1hello ?hello";
            ProcessMatches(input, pattern);

            // "[base_group - [excludedGroup]]" character class subtraction.
            // all numbers except the even ones
            pattern = @"[0-9-[02468]]";
            input = "0123456789";
            ProcessMatches(input, pattern);
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
            ProcessMatches(input, pattern);

            // "$" the match must occur at the end of string
            pattern = "7878$";
            input = "240-55-7878";
            ProcessMatches(input, pattern);

            // "\A match must occur at the start of string
            pattern = @"\A240";
            input = "240-555-7878";
            ProcessMatches(input, pattern);

            // "\Z" match must occur at the end of string or before \n at the end of the string
            pattern = @"555\Z";
            input = "240-555-555";
            ProcessMatches(input, pattern);

            // "\z" match must occur at the end of string or before \n at the end of the string
            pattern = @"-5355\z";
            input = "240-555-5355";
            ProcessMatches(input, pattern);

            // "\G" match must occur at the point the previous match ended
            pattern = @"\GHello";
            input = "HelloHello";
            ProcessMatches(input, pattern);

            // "\b" match must occur on a boundary between a "\w" (alphanumeric) 
            // and "\W" nonalphanumeric character
            pattern = @"\bthis\sis\b";
            input = "Hello|this is|Hello |this is ";
            ProcessMatches(input, pattern);

            // "\B" match must not occur on a "\b" boundary
            pattern = @"\Bcat\B";
            input = "Tomcat Certificate blackcatdown";
            ProcessMatches(input, pattern);
        }

        /// <summary>
        /// Grouping constructs delineat the subexpressions of a regular expression and capture the
        /// substrings of an input string.
        /// </summary>
        public static void GroupingConstructsDemo()
        {
            Console.WriteLine("Regex Grouping Syntax demo\n");
            string input, pattern;

            // "(subexpresion)" captures the match and assigns it a one-based ordinal number
            // matches a word(first captured group), a space and a repeating word (second captured group)
            pattern = @"(\w+)\s(\1)\s\w+\s(\2)\W";
            input = "Hello there there my there name is is James";
            ProcessMatches(input, pattern);

            // "(?<name>subexpression) or "(?'name'subexpression)" named captured group
            // use \k<name> to reference the named expression
            pattern = @"(?<duplicateWord>\w+)\s\k<duplicateWord>\W(?<nextWord>\w+)";
            input = "He said that that was the the correct answer";
            ProcessMatches(input, pattern);

            // "(?:subexpression)" do not assign subexpression to captured group
            pattern = @"(Write)(?:Line)";
            input = "Console.WriteLine or Console.WriteOrNot";
            ProcessMatches(input, pattern);

            // "(?i:subexpression)" turns on case insentivity group options
            // x - ignore white spacing
            // 
            pattern = @"(?i:\s*hello\s*)";
            input = "hello HellohellO HELLO";
            ProcessMatches(input, pattern);

            // "(?=subexpression)" zero width positive lookahead, matches any
            // epxression before the group subexpression
            pattern = @"(abc(?=\W))+";
            input = "abc|fgh|abc#";
            ProcessMatches(input, pattern);

            // "(?!subexpression)" zero width negative lookahead, matches any
            // string which is not followed by the subexpression
            pattern = @"(\b(?!end)\w+\b)";
            input = "ending this is the end. this is not the end.";
            ProcessMatches(input, pattern);

            // "(?<=subexpression)" zero width positive lookbehind. Continue match
            // if previous expression is preceded by the subexpression
            pattern = @"(?<=\b\d{3})\d{3}\b";
            input = "12123 234234 334567 1234";
            ProcessMatches(input, pattern);

            // "(?<!subexpression)" zero width negative lookbehind. Continue match
            // if previous expression is not preceded by the subexpression
            pattern = @"(?<!\b\d{3})\d{3}\b";
            input = "12123 234234 334567 1234";
            ProcessMatches(input, pattern);

            // "(?>subexpression)" nonbacktracking subexpressions. Disables backtracking
            // when attempting to goback and find a match
            pattern = @"(\w)\1+.\b";
            input = "aaaa";
            ProcessMatches(input, pattern);
            pattern = @"(?>(\w)\1+).\b";
            input = "aaaa";
            ProcessMatches(input, pattern);

            // TODO: See microsoft docs for Balancing group definitions "(?'name1-name2' subexpression)"
        }

        /// <summary>
        /// Quantifiers show how many instances of a character, group, or character class must be pressent
        /// in the input for a match to be found.
        /// </summary>
        public static void QuantifierSyntaxDemo()
        {
            Console.WriteLine("Regex Quantifier Demo\n");

            string input, pattern;

            // "*", "*?" matches zero or more times, equivalent to {0,}
            pattern = @"\b\w+[a]*\w+\b";
            input = "bed bad baad clean";
            ProcessMatches(input, pattern);

            // "+", "+?" matches one or more times, equivalent to {1,}
            pattern = @"\b\w[a]+\w\b";
            input = "bed bad baad clean can";
            ProcessMatches(input, pattern);

            // "?", "??" matches zero or one time, equivalent to {0,1}
            pattern = @"\b\w[a]?\w\b";
            input = "bed bad baad clean can bb";
            ProcessMatches(input, pattern);

            // "{n}", "{n}?" matches exactly n times where n is any integer
            pattern = @"\b\w[a]{2}\w\b";
            input = "bed bad baad clean can bb";
            ProcessMatches(input, pattern);

            // "{n,}", "{n,}?" matches at least n times where n is any integer
            pattern = @"\b\w[a]{2,}\w\b";
            input = "bed bad baad clean can baaab";
            ProcessMatches(input, pattern);

            // "{n,m}", "{n,m}?" matches between n and m times where n and m are integers
            pattern = @"\b\w[a]{2,4}\w\b";
            input = "bed bad baad clean can baaab baaaaab";
            ProcessMatches(input, pattern);

            // Greedy vs. Lazy example:
            pattern = @"\b.*([0-9]{4})\b";
            input = "1112223333 3992991999";
            ProcessMatches(input, pattern);
            pattern = @"\b.*?([0-9]{4})\b";
            ProcessMatches(input, pattern);
        }

        /// <summary>
        /// Backreferences provide a convenient way to identify a repeated character 
        /// or substring within a string.
        /// </summary>
        public static void BackReferenceConstructsDemo()
        {
            Console.WriteLine("Regex Back Reference Constructs demo");
            string input, pattern;

            // "\number" numbered backreference where the number is the ordinal position of the captured group.
            pattern = @"(\w+)\s(\d)\s\1\2";
            input = "this is the breaks 0 breaks0 and repeated 1 repeated";
            ProcessMatches(input, pattern);

            // "\k<name>" or "\k'name'" named backreference.
            pattern = @"(?<word>\w+)\s(?<number>\d)\s\k<word>\k'number'";
            input = "this is the breaks 0 breaks0 and repeated 1 repeated";
            ProcessMatches(input, pattern);
        }

        /// <summary>
        /// Alternation constructs provide or conditional matching.
        /// </summary>
        public static void AlternationConstructDemo()
        {
            Console.WriteLine("Regex Alternation Consturcts demo\n");
            string input, pattern;

            // "|", matches any one of a series of patterns
            pattern = @"\b\w(a|e)\w\b";
            input = "bad can bed ted bob ball bedding";
            ProcessMatches(input, pattern);

            // "(?(expression)yes|no)" conditional matching with expression
            pattern = @"\b(?(a-)a-\d{3}|b-\d{3})\b";
            input = "a-123 c-123 b-123";
            ProcessMatches(input, pattern);

            // "(?(name)yes|no)" or "(?(number)yes|no)" match one of two patterns depending on whether it
            // has matcheda specified capturing group.
            pattern = @"\b(?<ahyphen>(a-))*(?(ahyphen)\d{3}|b-\d{3})\b";
            input = "a-123 c-123 b-123";
            ProcessMatches(input, pattern);
        }

        /// <summary>
        /// Print pattern, input, and matches to the console
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        private static void ProcessMatches(string input, string pattern)
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
