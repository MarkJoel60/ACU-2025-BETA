// Decompiled with JetBrains decompiler
// Type: PX.Data.StringRoutines
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
internal static class StringRoutines
{
  public static string findAlias(string text, ref int start, ref int stop)
  {
    string alias = (string) null;
    if (!StringRoutines.byPassBlanks(text, ref start, stop))
      throw new Exception("No table name is specified.");
    if (start == stop)
      return (string) null;
    if (text[start] == '(')
    {
      int index = start + 1;
      int num1 = 1;
      int num2 = 0;
      for (; num1 > 0 && index <= stop; ++index)
      {
        switch (text[index])
        {
          case '\'':
            ++num2;
            break;
          case '(':
            if (num2 % 2 == 0)
            {
              ++num1;
              break;
            }
            break;
          case ')':
            if (num2 % 2 == 0)
            {
              --num1;
              break;
            }
            break;
        }
      }
      if (num1 > 0)
        throw new Exception("Invalid sub select has been found.");
      stop = index - 1;
    }
    else
    {
      int start1 = start;
      if (!StringRoutines.getNextSpace(text, ref start1, stop))
        return (string) null;
      alias = text.Substring(start, start1 - start + 1);
      int start2 = start1 + 1;
      if (!StringRoutines.byPassBlanks(text, ref start2, stop))
      {
        stop = start1;
        return alias;
      }
      if (start2 == stop)
      {
        start = start2;
        return alias;
      }
      int start3 = start2;
      if (!StringRoutines.getNextSpace(text, ref start3, stop))
      {
        start = start2;
        return alias;
      }
      switch (start3 - start2)
      {
        case 3:
          char upper1 = char.ToUpper(text[start2]);
          char upper2 = char.ToUpper(text[start2 + 1]);
          char upper3 = char.ToUpper(text[start2 + 2]);
          char upper4 = char.ToUpper(text[start2 + 3]);
          if (upper1 == 'F' && upper2 == 'U' && upper3 == 'L' && upper4 == 'L' || upper1 == 'J' && upper2 == 'O' && upper3 == 'I' && upper4 == 'N' || upper1 == 'L' && upper2 == 'E' && upper3 == 'F' && upper4 == 'T')
          {
            stop = start1;
            return alias;
          }
          break;
        case 4:
          char upper5 = char.ToUpper(text[start2]);
          char upper6 = char.ToUpper(text[start2 + 1]);
          char upper7 = char.ToUpper(text[start2 + 2]);
          char upper8 = char.ToUpper(text[start2 + 3]);
          char upper9 = char.ToUpper(text[start2 + 4]);
          if (upper5 == 'W' && upper6 == 'H' && upper7 == 'E' && upper8 == 'R' && upper9 == 'E' || upper5 == 'G' && upper6 == 'R' && upper7 == 'O' && upper8 == 'U' && upper9 == 'P' || upper5 == 'O' && upper6 == 'R' && upper7 == 'D' && upper8 == 'E' && upper9 == 'R' || upper5 == 'I' && upper6 == 'N' && upper7 == 'N' && upper8 == 'E' && upper9 == 'R' || upper5 == 'C' && upper6 == 'R' && upper7 == 'O' && upper8 == 'S' && upper9 == 'S' || upper5 == 'O' && upper6 == 'U' && upper7 == 'T' && upper8 == 'E' && upper9 == 'R' || upper5 == 'R' && upper6 == 'I' && upper7 == 'G' && upper8 == 'H' && upper9 == 'T')
          {
            stop = start1;
            return alias;
          }
          break;
      }
      start = start2;
      stop = start3;
    }
    return alias;
  }

  public static bool byPassBlanks(string text, ref int start, int stop)
  {
    while (start <= stop && (text[start] == ' ' || (int) text[start] == (int) Environment.NewLine[0] || (int) text[start] == (int) Environment.NewLine[Environment.NewLine.Length - 1]))
      ++start;
    return start <= stop;
  }

  public static bool getNextSpace(string text, ref int start, int stop)
  {
    while (start <= stop && text[start] != ' ' && (int) text[start] != (int) Environment.NewLine[0] && (int) text[start] != (int) Environment.NewLine[Environment.NewLine.Length - 1])
      ++start;
    --start;
    return start != stop;
  }

  public static bool byPassStringConstant(string text, ref int start, int stop)
  {
    int index = start;
    bool flag = false;
    for (; index <= stop; ++index)
    {
      if (!flag)
      {
        if (text[index] != '\'')
        {
          if (text[index] != ' ')
            return false;
        }
        else
          flag = true;
      }
      else if (text[index] == '\'')
      {
        if (index == stop || text[index + 1] != '\'')
        {
          start = index + 1;
          return true;
        }
        ++index;
      }
    }
    return false;
  }

  public static bool byPassParameter(string text, ref int start, int stop)
  {
    int index = start;
    bool flag = false;
    for (; index <= stop; ++index)
    {
      if (!flag)
      {
        if (text[index] != '@')
        {
          if (text[index] != ' ')
            return false;
        }
        else
          flag = true;
      }
      else if (!char.IsLetterOrDigit(text[index]))
      {
        start = index;
        return true;
      }
    }
    return false;
  }
}
