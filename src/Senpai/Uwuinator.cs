/* This code was adapted from https://github.com/senguyen1011/UwUinator
 * Copyright (C) 2019 senguyen1011
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Text.RegularExpressions;

namespace Senpai;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Uwuinator
{
    private static readonly List<string> KaomojiJoy =
        [" (* ^ ω ^)", " (o^▽^o)", " (≧◡≦)", " ☆⌒ヽ(*\"､^*)chu", " ( ˘⌣˘)♡(˘⌣˘ )", " xD"];

    private static readonly List<string> KaomojiEmbarassed =
        [" (⁄ ⁄>⁄ ▽ ⁄<⁄ ⁄)..", " (*^.^*)..,", "..,", ",,,", "... ", ".. ", " mmm..", "O.o"];

    private static readonly List<string> KaomojiConfuse = [" (o_O)?", " (°ロ°) !?", " (ーー;)?", " owo?"];
    private static readonly List<string> KaomojiSparkles = [" *:･ﾟ✧*:･ﾟ✧ ", " ☆*:・ﾟ ", "〜☆ ", " uguu.., ", "-.-"];

    private static readonly Random Random = new();

    public static string Uwuinate(string textInput)
    {
        if (string.IsNullOrWhiteSpace(textInput))
        {
            return textInput;
        }

        textInput = textInput.ToLower();

        string[] segments = Regex.Split(textInput, "(<[^>]*>)");

        var uwuWords = new List<string>();
        foreach (string segment in segments)
        {
            if (segment.StartsWith("<") && segment.EndsWith(">"))
            {
                uwuWords.Add(segment);
            }
            else
            {
                string[] words = segment.Split(' ');
                uwuWords.AddRange(words.Select(UwuWord));
            }
        }

        return string.Join(" ", uwuWords);
    }

    private static string UwuWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            return word;
        }

        string uwu = "";

        char lastChar = word.Last();
        string end = "";
        if (lastChar is '.' or '?' or '!' or ',')
        {
            word = word[..^1];
            end = lastChar.ToString();

            switch (end)
            {
                case ".":
                {
                    if (Random.Next(3) == 0)
                    {
                        end = KaomojiJoy[Random.Next(KaomojiJoy.Count)];
                    }

                    break;
                }
                case "?":
                {
                    if (Random.Next(2) == 0)
                    {
                        end = KaomojiConfuse[Random.Next(KaomojiConfuse.Count)];
                    }

                    break;
                }
                case "!":
                {
                    if (Random.Next(2) == 0)
                    {
                        end = KaomojiJoy[Random.Next(KaomojiJoy.Count)];
                    }

                    break;
                }
                case ",":
                {
                    if (Random.Next(3) == 0)
                    {
                        end = KaomojiEmbarassed[Random.Next(KaomojiEmbarassed.Count)];
                    }

                    break;
                }
            }

            if (Random.Next(4) == 0)
            {
                end = KaomojiSparkles[Random.Next(KaomojiSparkles.Count)];
            }
        }

        if (word.Contains('l'))
        {
            if (word.EndsWith("le") || word.EndsWith("ll"))
            {
                uwu += word[..^2].Replace("l", "w").Replace("r", "w")
                       + word[^2..]
                       + end;
            }
            else if (word.EndsWith("les") || word.EndsWith("lls"))
            {
                uwu += word[..^3].Replace("l", "w").Replace("r", "w")
                       + word[^3..]
                       + end;
            }
            else
            {
                uwu += word.Replace("l", "w").Replace("r", "w")
                       + end;
            }
        }
        else if (word.Contains('r'))
        {
            if (word.EndsWith("er") || word.EndsWith("re"))
            {
                uwu += word[..^2].Replace("r", "w")
                       + word[^2..]
                       + end;
            }
            else if (word.EndsWith("ers") || word.EndsWith("res"))
            {
                uwu += word[..^3].Replace("r", "w")
                       + word[^3..]
                       + end;
            }
            else
            {
                uwu += word.Replace("r", "w")
                       + end;
            }
        }
        else
        {
            uwu += word + end;
        }

        uwu = uwu.Replace("you're", "ur");
        uwu = uwu.Replace("youre", "ur");

        if (uwu.Length > 2 && char.IsLetter(uwu[0]) && Random.Next(6) == 0)
        {
            uwu = uwu[0].ToString() + '-' + uwu;
        }

        return uwu;
    }
}