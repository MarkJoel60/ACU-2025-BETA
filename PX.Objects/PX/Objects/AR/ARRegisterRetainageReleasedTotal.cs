// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterRetainageReleasedTotal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select4<ARRegisterRetainageReleased, Aggregate<GroupBy<ARRegisterRetainageReleased.origDocType, GroupBy<ARRegisterRetainageReleased.origRefNbr, Sum<ARRegisterRetainageReleased.curyOrigDocAmt, Sum<ARRegisterRetainageReleased.origDocAmt>>>>>>), Persistent = false)]
[PXHidden]
public class ARRegisterRetainageReleasedTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlTable = typeof (ARRegisterRetainageReleased))]
  public virtual 
  #nullable disable
  string OrigDocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARRegisterRetainageReleased))]
  public virtual string OrigRefNbr { get; set; }

  [PXDBDecimal(BqlField = typeof (ARRegisterRetainageReleased.curyOrigDocAmt))]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (ARRegisterRetainageReleased.origDocAmt))]
  public virtual Decimal? OrigDocAmt { get; set; }

  public class PK : 
    PrimaryKeyOf<ARRegisterRetainageReleasedTotal>.By<ARRegisterRetainageReleasedTotal.origDocType, ARRegisterRetainageReleasedTotal.origRefNbr>
  {
    public static ARRegisterRetainageReleasedTotal Find(
      PXGraph graph,
      string origDocType,
      string origRefNbr,
      PKFindOptions options = 0)
    {
      return (ARRegisterRetainageReleasedTotal) PrimaryKeyOf<ARRegisterRetainageReleasedTotal>.By<ARRegisterRetainageReleasedTotal.origDocType, ARRegisterRetainageReleasedTotal.origRefNbr>.FindBy(graph, (object) origDocType, (object) origRefNbr, options);
    }
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterRetainageReleasedTotal.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterRetainageReleasedTotal.origRefNbr>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterRetainageReleasedTotal.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterRetainageReleasedTotal.origDocAmt>
  {
  }
}
