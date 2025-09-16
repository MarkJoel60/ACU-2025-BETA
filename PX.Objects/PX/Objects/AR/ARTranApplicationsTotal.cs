// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranApplicationsTotal
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

[PXProjection(typeof (Select4<ARTranApplications, Aggregate<GroupBy<ARTranApplications.tranType, GroupBy<ARTranApplications.refNbr, GroupBy<ARTranApplications.lineNbr, GroupBy<ARTranApplications.curyOrigTranAmt, GroupBy<ARTranApplications.origTranAmt, Sum<ARTranApplications.curyAppBalanceSigned, Sum<ARTranApplications.appBalanceSigned>>>>>>>>>), Persistent = false)]
[PXHidden]
public class ARTranApplicationsTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true, BqlField = typeof (ARTranApplications.tranType))]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(IsKey = true, BqlField = typeof (ARTranApplications.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (ARTranApplications.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTranApplications.curyOrigTranAmt))]
  public virtual Decimal? CuryOrigTranAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTranApplications.origTranAmt))]
  public virtual Decimal? OrigTranAmt { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTranApplications.curyAppBalanceSigned))]
  public virtual Decimal? CuryAppBalanceTotal { get; set; }

  [PXDBDecimal(BqlField = typeof (ARTranApplications.appBalanceSigned))]
  public virtual Decimal? AppBalanceTotal { get; set; }

  public class PK : 
    PrimaryKeyOf<ARTranApplicationsTotal>.By<ARTranApplicationsTotal.tranType, ARTranApplicationsTotal.refNbr, ARTranApplicationsTotal.lineNbr>
  {
    public static ARTranApplicationsTotal Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ARTranApplicationsTotal) PrimaryKeyOf<ARTranApplicationsTotal>.By<ARTranApplicationsTotal.tranType, ARTranApplicationsTotal.refNbr, ARTranApplicationsTotal.lineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public abstract class tranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranApplicationsTotal.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranApplicationsTotal.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranApplicationsTotal.lineNbr>
  {
  }

  public abstract class curyOrigTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranApplicationsTotal.curyOrigTranAmt>
  {
  }

  public abstract class origTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranApplicationsTotal.origTranAmt>
  {
  }

  public abstract class curyAppBalanceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranApplicationsTotal.curyAppBalanceTotal>
  {
  }

  public abstract class appBalanceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranApplicationsTotal.appBalanceTotal>
  {
  }
}
