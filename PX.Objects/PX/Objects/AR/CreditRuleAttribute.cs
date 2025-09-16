// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CreditRuleAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Fixed List Selector. Defines a name-value pair list of a possible CreditRules <br />
/// compatible with <see cref="T:PX.Objects.AR.CreditRuleTypes" /><br />
/// </summary>
public class CreditRuleAttribute : PXStringListAttribute
{
  public CreditRuleAttribute()
    : base(new string[4]{ "D", "C", "B", "N" }, new string[4]
    {
      "Days Past Due",
      "Credit Limit",
      "Limit and Days Past Due",
      "Disabled"
    })
  {
  }
}
