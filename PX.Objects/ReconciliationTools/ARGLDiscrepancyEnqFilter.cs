// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.ARGLDiscrepancyEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace ReconciliationTools;

[Serializable]
public class ARGLDiscrepancyEnqFilter : DiscrepancyEnqFilter
{
  [Account(null, typeof (Search5<PX.Objects.GL.Account.accountID, InnerJoin<ARHistory, On<PX.Objects.GL.Account.accountID, Equal<ARHistory.accountID>>>, Where<Match<Current<AccessInfo.userName>>>, Aggregate<GroupBy<PX.Objects.GL.Account.accountID>>>), DisplayName = "Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public override int? AccountID { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total AR Amount", Enabled = false)]
  public override Decimal? TotalXXAmount { get; set; }

  public new abstract class accountID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ARGLDiscrepancyEnqFilter.accountID>
  {
  }

  public new abstract class totalXXAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARGLDiscrepancyEnqFilter.totalXXAmount>
  {
  }
}
