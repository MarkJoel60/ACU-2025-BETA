// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.StatementTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Fixed List Selector. Defines a name-value pair list of a possible Statement types <br />
/// compatible with <see cref="T:PX.Objects.AR.ARStatementType" />.<br />
/// </summary>
public class StatementTypeAttribute : PXStringListAttribute
{
  public StatementTypeAttribute()
    : base(new string[2]{ "O", "B" }, new string[2]
    {
      "Open Item",
      "Balance Brought Forward"
    })
  {
  }
}
