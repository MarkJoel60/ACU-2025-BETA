// Decompiled with JetBrains decompiler
// Type: PX.Data.Like`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>
/// Compares the preceding operand with the pattern specified in <tt>Operand</tt>.
/// Equivalent to the SQL operator LIKE.
/// </summary>
/// <typeparam name="Operand">The operand to compare to.</typeparam>
/// <remarks>
/// Operand should have a wildcard string value in which the sign "%" is used
/// to substitute missing letters. For example, "%land%" will be matched by
/// "Iceland" and "Laplandia".
/// </remarks>
/// <example><para>The code below selects and iterates over accounts whose account IDs match the provided wildcard.</para>
/// 	<code title="Example" lang="CS">
/// string acctCDWildCard = SubCDUtils.CreateSubCDWildcard(aSrc.AccountCD, AccountAttribute.DimensionName);
/// foreach (Account iAcct in PXSelect&lt;Account, Where&lt;Account.accountCD, Like&lt;Required&lt;Account.accountCD&gt;&gt;&gt;&gt;.Select(this, acctCDWildCard))
/// {
/// ...
/// }</code>
/// </example>
public class Like<Operand> : ComparisonBase<Operand> where Operand : IBqlOperand
{
  protected override bool? verifyCore(object val, object value)
  {
    return Like<Operand>.CheckLike(val as string, value as string);
  }

  protected override bool isBypass(object val) => false;

  /// <exclude />
  public Like()
    : base("LIKE", true)
  {
  }

  /// <exclude />
  public static bool? CheckLike(string val, string template)
  {
    if (val == null || template == null)
      return new bool?();
    val = val.ToLower();
    template = template.ToLower();
    int num = template.IndexOf('%');
    if (num == -1)
      return new bool?(Like<Operand>.ExactMatch(val, template));
    int startIndex1 = 0;
    string template1 = template.Substring(startIndex1, num - startIndex1);
    if (!Like<Operand>.BeginningMatch(val, template1))
      return new bool?(false);
    int startIndex2 = num + 1;
    for (int index = template.IndexOf('%', num + 1); index != -1; index = template.IndexOf('%', index + 1))
    {
      string template2 = template.Substring(startIndex2, index - startIndex2);
      if (!Like<Operand>.AnyMatch(val, template2))
        return new bool?(false);
      startIndex2 = index + 1;
    }
    string template3 = template.Substring(startIndex2);
    return new bool?(Like<Operand>.EndingMatch(val, template3));
  }

  protected static bool ExactMatch(string val, string template)
  {
    if (template == string.Empty)
      return true;
    if (val.Length != template.Length)
      return false;
    for (int index = 0; index < val.Length; ++index)
    {
      if (template[index] != '_' && (int) template[index] != (int) val[index])
        return false;
    }
    return true;
  }

  protected static bool BeginningMatch(string val, string template)
  {
    if (template == string.Empty)
      return true;
    if (val.Length < template.Length)
      return false;
    for (int index = 0; index < template.Length; ++index)
    {
      if (template[index] != '_' && (int) template[index] != (int) val[index])
        return false;
    }
    return true;
  }

  protected static bool AnyMatch(string val, string template)
  {
    if (template == string.Empty)
      return true;
    if (val.Length < template.Length)
      return false;
    for (int startIndex = 0; startIndex <= val.Length - template.Length; ++startIndex)
    {
      if (Like<Operand>.ExactMatch(val.Substring(startIndex, template.Length), template))
        return true;
    }
    return false;
  }

  protected static bool EndingMatch(string val, string template)
  {
    if (template == string.Empty)
      return true;
    if (val.Length < template.Length)
      return false;
    val = val.Substring(val.Length - template.Length);
    for (int index = template.Length - 1; index >= 0; --index)
    {
      if (template[index] != '_' && (int) template[index] != (int) val[index])
        return false;
    }
    return true;
  }

  protected override void parseNonFieldOperand(
    PXGraph graph,
    StringBuilder text,
    System.Action parseOperand)
  {
    if (graph != null && text != null)
      graph.SqlDialect.scriptLikeOperand(text, parseOperand);
    else
      parseOperand();
  }
}
