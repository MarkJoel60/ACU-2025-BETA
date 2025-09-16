// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranRetainageReleasedTotal
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

[PXProjection(typeof (Select4<ARTranRetainageReleased, Aggregate<GroupBy<ARTranRetainageReleased.origDocType, GroupBy<ARTranRetainageReleased.origRefNbr, GroupBy<ARTranRetainageReleased.origLineNbr, Sum<ARTranRetainageReleased.curyRetainageReleased, Sum<ARTranRetainageReleased.retainageReleased>>>>>>>), Persistent = false)]
[PXHidden]
public class ARTranRetainageReleasedTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlTable = typeof (ARTranRetainageReleased))]
  public virtual 
  #nullable disable
  string OrigDocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARTranRetainageReleased))]
  public virtual string OrigRefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (ARTranRetainageReleased))]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTranRetainageReleased.curyRetainageReleased))]
  public virtual Decimal? CuryRetainageReleased { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTranRetainageReleased.retainageReleased))]
  public virtual Decimal? RetainageReleased { get; set; }

  public class PK : 
    PrimaryKeyOf<ARTranRetainageReleasedTotal>.By<ARTranRetainageReleasedTotal.origDocType, ARTranRetainageReleasedTotal.origRefNbr, ARTranRetainageReleasedTotal.origLineNbr>
  {
    public static ARTranRetainageReleasedTotal Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ARTranRetainageReleasedTotal) PrimaryKeyOf<ARTranRetainageReleasedTotal>.By<ARTranRetainageReleasedTotal.origDocType, ARTranRetainageReleasedTotal.origRefNbr, ARTranRetainageReleasedTotal.origLineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranRetainageReleasedTotal.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranRetainageReleasedTotal.origRefNbr>
  {
  }

  public abstract class origLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARTranRetainageReleasedTotal.origLineNbr>
  {
  }

  public abstract class curyRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranRetainageReleasedTotal.curyRetainageReleased>
  {
  }

  public abstract class retainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranRetainageReleasedTotal.retainageReleased>
  {
  }
}
