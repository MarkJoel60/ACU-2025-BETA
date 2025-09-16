// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXGenericTemplatePreprocessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class PXGenericTemplatePreprocessor : PXTemplatePreprocessor<PXGenericTemplatePreprocessor>
{
  public static Token rowTemplateStart = new Token(nameof (rowTemplateStart));
  public static Token rowTemplateEnd = new Token(nameof (rowTemplateEnd));
  private static readonly TokenLexems[] GenericLocalLexems = new TokenLexems[PXTemplatePreprocessor<PXGenericTemplatePreprocessor>._localLexems.Length + 2];

  static PXGenericTemplatePreprocessor()
  {
    PXTemplatePreprocessor<PXGenericTemplatePreprocessor>._localLexems.CopyTo((Array) PXGenericTemplatePreprocessor.GenericLocalLexems, 0);
    PXGenericTemplatePreprocessor.GenericLocalLexems[PXGenericTemplatePreprocessor.GenericLocalLexems.Length - 2] = new TokenLexems(PXGenericTemplatePreprocessor.rowTemplateStart, "{$");
    PXGenericTemplatePreprocessor.GenericLocalLexems[PXGenericTemplatePreprocessor.GenericLocalLexems.Length - 1] = new TokenLexems(PXGenericTemplatePreprocessor.rowTemplateEnd, "$}");
    for (int index = 0; index < PXGenericTemplatePreprocessor.GenericLocalLexems.Length; ++index)
    {
      TokenLexems genericLocalLexem = PXGenericTemplatePreprocessor.GenericLocalLexems[index];
      if (genericLocalLexem.tkId == PXTemplatePreprocessor<PXGenericTemplatePreprocessor>.LocalTokens.templateStart)
        PXGenericTemplatePreprocessor.GenericLocalLexems[index] = new TokenLexems(genericLocalLexem.tkId, "{{$");
      else if (genericLocalLexem.tkId == PXTemplatePreprocessor<PXGenericTemplatePreprocessor>.LocalTokens.templateEnd)
        PXGenericTemplatePreprocessor.GenericLocalLexems[index] = new TokenLexems(genericLocalLexem.tkId, "$}}");
    }
  }

  public PXGenericTemplatePreprocessor()
    : base(PXGenericTemplatePreprocessor.GenericLocalLexems)
  {
  }

  protected override PXCustomTemplateDeclaration GetTemplate(
    string templateName,
    PXWikiParserContext context)
  {
    return (PXCustomTemplateDeclaration) new PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration(templateName, context);
  }

  protected class PXGenericTemplateDeclaration(string templateName, PXWikiParserContext context) : 
    PXSimpleTemplateDeclaration("GenTemplate:" + templateName, context)
  {
    private const string _TEMPLATE_NAME_PREFIX = "GenTemplate:";
    private readonly List<PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator> _multiplicators = new List<PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator>();

    protected override void InitParamDeclarations()
    {
      base.InitParamDeclarations();
      this.ParseContent();
    }

    private void ParseContent()
    {
      this._multiplicators.Clear();
      this._multiplicators.AddRange(PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.Parse(this.Value));
    }

    public override string GetContent(Dictionary<string, string> pars)
    {
      if (string.IsNullOrEmpty(this.Value))
        return this.Value;
      Dictionary<string, string> commonPars = new Dictionary<string, string>(pars.Count, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.RowParameters rowPars = new PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.RowParameters((IEqualityComparer<int>) PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.IntComparer.Asc, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (KeyValuePair<string, string> par in pars)
      {
        if (!string.IsNullOrEmpty(par.Key))
        {
          int length = par.Key.IndexOf('$');
          int result;
          if (length > -1 && length < par.Key.Length - 1 && int.TryParse(par.Key.Substring(length + 1), out result))
            rowPars.Set(result, par.Key.Substring(0, length), par.Value);
          else
            commonPars.Add(par.Key, par.Value);
        }
      }
      StringBuilder builder = new StringBuilder();
      foreach (PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator multiplicator in this._multiplicators)
        multiplicator.Multiplicate(builder, commonPars, rowPars);
      return builder.ToString();
    }

    private class IntComparer : IComparer<int>, IEqualityComparer<int>
    {
      public static readonly PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.IntComparer Asc = new PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.IntComparer(true);
      public static readonly PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.IntComparer Desc = new PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.IntComparer(false);
      private readonly bool _asc;

      private IntComparer(bool asc) => this._asc = asc;

      public int Compare(int x, int y) => !this._asc ? y - x : x - y;

      public bool Equals(int x, int y) => this.Compare(x, y) == 0;

      public int GetHashCode(int obj) => obj;
    }

    private class RowParameters
    {
      private readonly Dictionary<int, Dictionary<string, string>> _items;
      private readonly IEqualityComparer<string> _valueComparer;
      private readonly Dictionary<string, int> _mins;
      private readonly Dictionary<string, int> _maxs;

      public RowParameters(IEqualityComparer<int> comparer, IEqualityComparer<string> valueComparer)
      {
        this._items = new Dictionary<int, Dictionary<string, string>>(comparer);
        this._valueComparer = valueComparer;
        this._mins = new Dictionary<string, int>();
        this._maxs = new Dictionary<string, int>();
      }

      public Dictionary<string, string> this[int index]
      {
        get
        {
          return this._items.ContainsKey(index) ? this._items[index] : (Dictionary<string, string>) null;
        }
      }

      public int GetMin(IEnumerable<string> parameters)
      {
        int val1 = -1;
        foreach (string parameter in parameters)
        {
          if (this._mins.ContainsKey(parameter))
          {
            int min = this._mins[parameter];
            val1 = val1 == -1 ? min : System.Math.Min(val1, min);
          }
        }
        return val1 != -1 ? val1 : 0;
      }

      public int GetMax(IEnumerable<string> parameters)
      {
        int val1 = -1;
        foreach (string parameter in parameters)
        {
          if (this._maxs.ContainsKey(parameter))
          {
            int val2 = this._maxs[parameter];
            val1 = val1 == -1 ? val2 : System.Math.Max(val1, val2);
          }
        }
        return val1 != -1 ? val1 : 0;
      }

      public void Set(int index, string key, string value)
      {
        if (!this._items.ContainsKey(index))
          this._items.Add(index, new Dictionary<string, string>(this._valueComparer));
        Dictionary<string, string> dictionary = this._items[index];
        if (dictionary.ContainsKey(key))
          dictionary[key] = value;
        else
          dictionary.Add(key, value);
        if (this._mins.ContainsKey(key))
          this._mins[key] = System.Math.Min(index, this._mins[key]);
        else
          this._mins.Add(key, index);
        if (this._maxs.ContainsKey(key))
          this._maxs[key] = System.Math.Max(index, this._maxs[key]);
        else
          this._maxs.Add(key, index);
      }
    }

    private abstract class Multiplicator
    {
      private static readonly Token StartRowTemplate = new Token(nameof (StartRowTemplate));
      private static readonly Token EndRowTemplate = new Token(nameof (EndRowTemplate));
      private static readonly TokenLexems[] Lexems = new TokenLexems[2]
      {
        new TokenLexems(PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.StartRowTemplate, "{$"),
        new TokenLexems(PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.EndRowTemplate, "$}")
      };
      private readonly string _content;

      protected Multiplicator(string content) => this._content = content;

      protected string Content => this._content;

      public abstract void Multiplicate(
        StringBuilder builder,
        Dictionary<string, string> commonPars,
        PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.RowParameters rowPars);

      public static IEnumerable<PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator> Parse(
        string text)
      {
        PXBlockParser.ParseContext context = new PXBlockParser.ParseContext(text, 0, new PXWikiParserContext((ISettings) new PXSettings()));
        StringBuilder stringBuilder = new StringBuilder();
        bool flag = false;
        foreach (KeyValuePair<Token, string> token in PXBlockParser.GetTokens(context, PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.Lexems))
        {
          if (!flag && token.Key == PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.StartRowTemplate)
          {
            yield return (PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator) new PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.LabelMultiplicator(stringBuilder.ToString());
            stringBuilder = new StringBuilder();
            flag = true;
          }
          else if (flag && token.Key == PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.EndRowTemplate)
          {
            yield return (PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator) new PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.TemplateMultiplicator(stringBuilder.ToString());
            stringBuilder = new StringBuilder();
            flag = false;
          }
          else
            stringBuilder.Append(token.Value);
        }
        if (stringBuilder.Length > 0)
          yield return (PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator) new PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.LabelMultiplicator(stringBuilder.ToString());
      }

      protected static string WriteValues(
        string template,
        IEnumerable<string> parameters,
        IDictionary<string, string> values)
      {
        string str = template;
        foreach (string parameter in parameters)
        {
          if (values.ContainsKey(parameter))
            str = str.Replace($"(({parameter}))", values[parameter]);
        }
        return str;
      }
    }

    private sealed class LabelMultiplicator(string content) : 
      PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator(content)
    {
      public override void Multiplicate(
        StringBuilder builder,
        Dictionary<string, string> commonPars,
        PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.RowParameters rowPars)
      {
        builder.Append(PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.WriteValues(this.Content, (IEnumerable<string>) commonPars.Keys, (IDictionary<string, string>) commonPars));
      }
    }

    private sealed class TemplateMultiplicator : 
      PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator
    {
      private readonly string[] _parameters = new string[0];

      public TemplateMultiplicator(string content)
        : base(PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.TemplateMultiplicator.GetCorrectContent(content))
      {
        if (string.IsNullOrEmpty(content))
          return;
        int length = content.IndexOf('|');
        if (length <= 0)
          return;
        this._parameters = content.Substring(0, length).Split(new char[1]
        {
          '#'
        }, StringSplitOptions.RemoveEmptyEntries);
      }

      private static string GetCorrectContent(string content)
      {
        if (!string.IsNullOrEmpty(content))
        {
          int num = content.IndexOf('|');
          if (num > -1)
            return num >= content.Length - 1 ? string.Empty : content.Substring(num + 1);
        }
        return content;
      }

      public override void Multiplicate(
        StringBuilder builder,
        Dictionary<string, string> commonPars,
        PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.RowParameters rowPars)
      {
        string template = PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.WriteValues(this.Content, (IEnumerable<string>) this._parameters, (IDictionary<string, string>) commonPars);
        int min = rowPars.GetMin((IEnumerable<string>) this._parameters);
        int max = rowPars.GetMax((IEnumerable<string>) this._parameters);
        for (int index = 1; index <= max; ++index)
        {
          if (index < min)
            builder.Append(template);
          else
            builder.Append(PXGenericTemplatePreprocessor.PXGenericTemplateDeclaration.Multiplicator.WriteValues(template, (IEnumerable<string>) this._parameters, (IDictionary<string, string>) rowPars[index]));
        }
      }
    }
  }
}
