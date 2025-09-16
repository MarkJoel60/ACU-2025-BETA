// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.CalcAPTranGLwithLinesReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM.Extensions;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>Aggrigate AP Document Post GL with Lines.</summary>
[PXProjection(typeof (SelectFromBase<APTranPostGLwithLines, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<DateInfo>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPostGLwithLines.docDate, LessEqual<DateInfo.date>>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPostGLwithLines.closedDate, Greater<DateInfo.date>>>>>.Or<BqlOperand<APTranPostGLwithLines.closedDate, IBqlDateTime>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPostGLwithLines.released, Equal<PX.Data.True>>>>, PX.Data.Or<BqlOperand<APTranPostGLwithLines.openDoc, IBqlBool>.IsEqual<PX.Data.True>>>, PX.Data.Or<BqlOperand<APTranPostGLwithLines.prebooked, IBqlBool>.IsEqual<PX.Data.True>>>>.And<BqlOperand<APTranPostGLwithLines.voided, IBqlBool>.IsNotEqual<PX.Data.True>>>>.AggregateTo<GroupBy<APTranPostGLwithLines.projectID>, GroupBy<APTranPostGLwithLines.docType>, GroupBy<APTranPostGLwithLines.refNbr>, GroupBy<DateInfo.date>, Sum<APTranPostGLwithLines.origBalanceAmt>, Sum<APTranPostGLwithLines.balanceAmt>, Sum<APTranPostGLwithLines.origRetainageAmt>, Sum<APTranPostGLwithLines.releasedRetainageAmt>>))]
[PXCacheName("Aggrigate AP Document Post GL with Lines")]
public class CalcAPTranGLwithLinesReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The <see cref="T:PX.Objects.PM.PMProject">project</see> with which the item is associated or the non-project code if the item is not intended for any project.
  /// The field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.ProjectAccounting">Project Accounting</see> feature is enabled.
  /// </summary>
  [PXDBInt(IsKey = true, BqlField = typeof (APTranPostGLwithLines.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <summary>The type of the document.</summary>
  [PXDBString(IsKey = true, BqlTable = typeof (APTranPostGLwithLines))]
  [PXUIField(DisplayName = "Doc. Type")]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>Reference number of the document.</summary>
  [PXDBString(IsKey = true, BqlTable = typeof (APTranPostGLwithLines))]
  [PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string RefNbr { get; set; }

  /// <summary>Type of the original (source) document.</summary>
  [PXDBString(IsKey = true, BqlTable = typeof (APTranPostGLwithLines))]
  [PXUIField(DisplayName = "Orig Doc. Type")]
  public virtual string OrigDocType { get; set; }

  /// <summary>Reference number of the original (source) document.</summary>
  [PXDBString(IsKey = true, BqlTable = typeof (APTranPostGLwithLines))]
  [PXUIField(DisplayName = "Orig Ref. Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>Aging date.</summary>
  [PXDBDate(IsKey = true, BqlField = typeof (DateInfo.date))]
  public virtual System.DateTime? AgingDate { get; set; }

  /// <summary>
  /// The signed amount to be paid for the document in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury(BqlTable = typeof (APTranPostGLwithLines))]
  public virtual Decimal? OrigBalanceAmt { get; set; }

  /// <summary>Balance amtount.</summary>
  [PXDBBaseCury(BqlTable = typeof (APTranPostGLwithLines))]
  public virtual Decimal? BalanceAmt { get; set; }

  /// <summary>Original retainage amount.</summary>
  [PXDBBaseCury(BqlTable = typeof (APTranPostGLwithLines))]
  public virtual Decimal? OrigRetainageAmt { get; set; }

  /// <summary>Released retainage amount.</summary>
  [PXDBBaseCury(BqlTable = typeof (APTranPostGLwithLines))]
  public virtual Decimal? ReleasedRetainageAmt { get; set; }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.projectID>
  {
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.docType>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.refNbr>
  {
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.origRefNbr>
  {
  }

  public abstract class agingDate : 
    BqlType<IBqlDateTime, System.DateTime>.Field<CalcAPTranGLwithLinesReport.agingDate>
  {
  }

  public abstract class origBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.origBalanceAmt>
  {
  }

  public abstract class balanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.balanceAmt>
  {
  }

  public abstract class origRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.origRetainageAmt>
  {
  }

  public abstract class releasedRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CalcAPTranGLwithLinesReport.releasedRetainageAmt>
  {
  }
}
