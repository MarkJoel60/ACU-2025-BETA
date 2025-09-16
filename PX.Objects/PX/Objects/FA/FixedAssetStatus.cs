// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FixedAssetStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FA;

public class FixedAssetStatus
{
  public const 
  #nullable disable
  string Active = "A";
  public const string Hold = "H";
  public const string Suspended = "S";
  public const string FullyDepreciated = "F";
  public const string Disposed = "D";
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public const string UnderConstruction = "C";
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public const string Dekitting = "K";
  public const string Reversed = "R";

  /// <summary>The list of the fixed asset statuses.</summary>
  /// <value>
  ///  The allowed values are:
  ///  <list type="bullet">
  ///  <item> <term><c>H</c></term> <description>On hold</description> </item>
  ///  <item> <term><c>A</c></term> <description>Active</description> </item>
  ///  <item> <term><c>S</c></term> <description>Suspended</description> </item>
  ///  <item> <term><c>F</c></term> <description>Fully depreciated</description> </item>
  ///  <item> <term><c>D</c></term> <description>Disposed</description> </item>
  ///  <item> <term><c>R</c></term> <description>Reversed</description> </item>
  /// </list>
  ///  </value>
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]{ "A", "H", "S", "F", "D", "R" }, new string[6]
      {
        "Active",
        "On Hold",
        "Suspended",
        "Fully Depreciated",
        "Disposed",
        "Reversed"
      })
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FixedAssetStatus.active>
  {
    public active()
      : base("A")
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FixedAssetStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class suspended : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FixedAssetStatus.suspended>
  {
    public suspended()
      : base("S")
    {
    }
  }

  public class fullyDepreciated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FixedAssetStatus.fullyDepreciated>
  {
    public fullyDepreciated()
      : base("F")
    {
    }
  }

  public class disposed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FixedAssetStatus.disposed>
  {
    public disposed()
      : base("D")
    {
    }
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public class underConstruction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FixedAssetStatus.underConstruction>
  {
    public underConstruction()
      : base("C")
    {
    }
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public class dekitting : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FixedAssetStatus.dekitting>
  {
    public dekitting()
      : base("K")
    {
    }
  }

  public class reversed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FixedAssetStatus.reversed>
  {
    public reversed()
      : base("R")
    {
    }
  }
}
