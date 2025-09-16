// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.APGLDiscrepancyEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace ReconciliationTools;

[Serializable]
public class APGLDiscrepancyEnqFilter : DiscrepancyEnqFilter
{
  [Account(null, typeof (Search5<PX.Objects.GL.Account.accountID, InnerJoin<APHistory, On<PX.Objects.GL.Account.accountID, Equal<APHistory.accountID>>>, Where<Match<Current<AccessInfo.userName>>>, Aggregate<GroupBy<PX.Objects.GL.Account.accountID>>>), DisplayName = "Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public override int? AccountID { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total AP Amount", Enabled = false)]
  public override Decimal? TotalXXAmount { get; set; }

  public new abstract class accountID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    APGLDiscrepancyEnqFilter.accountID>
  {
  }

  public new abstract class totalXXAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APGLDiscrepancyEnqFilter.totalXXAmount>
  {
  }
}
