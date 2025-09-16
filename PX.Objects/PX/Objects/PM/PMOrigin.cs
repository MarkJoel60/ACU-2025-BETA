// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMOrigin
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMOrigin
{
  public const string Source = "S";
  public const string Change = "C";
  public const string FromAccount = "F";
  public const string None = "N";
  public const string OtherSource = "X";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("S", "Use Source"),
        PXStringListAttribute.Pair("C", "Replace")
      })
    {
    }
  }

  public class DebitAccountListAttribute : PXStringListAttribute
  {
    public DebitAccountListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("S", "Use Source"),
        PXStringListAttribute.Pair("C", "Replace"),
        PXStringListAttribute.Pair("X", "Credit Source")
      })
    {
    }
  }

  public class CreditAccountListAttribute : PXStringListAttribute
  {
    public CreditAccountListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("S", "Use Source"),
        PXStringListAttribute.Pair("C", "Replace"),
        PXStringListAttribute.Pair("X", "Debit Source")
      })
    {
    }
  }

  /// <summary>
  /// List of available Account Group sources.
  /// Account Group can be taken either from Source object, from Account or specified directly.
  /// </summary>
  public class AccountGroupListAttribute : PXStringListAttribute
  {
    public AccountGroupListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("S", "Use Source"),
        PXStringListAttribute.Pair("C", "Replace"),
        PXStringListAttribute.Pair("F", "From Account")
      })
    {
    }
  }

  public class BranchFilterListAttribute : PXStringListAttribute
  {
    public BranchFilterListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("S", "None"),
        PXStringListAttribute.Pair("C", "Specific Branch")
      })
    {
    }
  }
}
