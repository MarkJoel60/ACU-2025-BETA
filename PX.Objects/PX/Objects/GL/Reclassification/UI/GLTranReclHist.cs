// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Reclassification.UI.GLTranReclHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GL.Reclassification.UI;

[PXBreakInheritance]
public class GLTranReclHist : GLTran
{
  private 
  #nullable disable
  List<GLTranReclHist> _ChildTrans;

  public GLTranReclHist()
  {
  }

  public GLTranReclHist(string module, string batchNbr, int? lineNbr)
  {
    this.Module = module;
    this.BatchNbr = batchNbr;
    this.LineNbr = lineNbr;
  }

  public GLTranReclHist(GLTran tran)
  {
    this.IncludedInReclassHistory = tran.IncludedInReclassHistory;
    this.BranchID = tran.BranchID;
    this.Module = tran.Module;
    this.BatchNbr = tran.BatchNbr;
    this.LineNbr = tran.LineNbr;
    this.LedgerID = tran.LedgerID;
    this.AccountID = tran.AccountID;
    this.SubID = tran.SubID;
    this.ProjectID = tran.ProjectID;
    this.TaskID = tran.TaskID;
    this.CostCodeID = tran.CostCodeID;
    this.RefNbr = tran.RefNbr;
    this.InventoryID = tran.InventoryID;
    this.UOM = tran.UOM;
    this.Qty = tran.Qty;
    this.DebitAmt = tran.DebitAmt;
    this.CreditAmt = tran.CreditAmt;
    this.CuryInfoID = tran.CuryInfoID;
    this.CuryDebitAmt = tran.CuryDebitAmt;
    this.CuryCreditAmt = tran.CuryCreditAmt;
    this.Released = tran.Released;
    this.Posted = tran.Posted;
    this.NonBillable = tran.NonBillable;
    this.IsInterCompany = tran.IsInterCompany;
    this.SummPost = tran.SummPost;
    this.ZeroPost = tran.ZeroPost;
    this.OrigModule = tran.OrigModule;
    this.OrigBatchNbr = tran.OrigBatchNbr;
    this.OrigLineNbr = tran.OrigLineNbr;
    this.OrigAccountID = tran.OrigAccountID;
    this.OrigSubID = tran.OrigSubID;
    this.TranID = tran.TranID;
    this.TranType = tran.TranType;
    this.TranClass = tran.TranClass;
    this.TranDesc = tran.TranDesc;
    this.TranDate = tran.TranDate;
    this.TranLineNbr = tran.TranLineNbr;
    this.ReferenceID = tran.ReferenceID;
    this.FinPeriodID = tran.FinPeriodID;
    this.TranPeriodID = tran.TranPeriodID;
    this.CATranID = tran.CATranID;
    this.PMTranID = tran.PMTranID;
    this.OrigPMTranID = tran.OrigPMTranID;
    this.LedgerBalanceType = tran.LedgerBalanceType;
    this.AccountRequireUnits = tran.AccountRequireUnits;
    this.TaxID = tran.TaxID;
    this.TaxCategoryID = tran.TaxCategoryID;
    this.NoteID = tran.NoteID;
    this.ReclassificationProhibited = tran.ReclassificationProhibited;
    this.ReclassBatchModule = tran.ReclassBatchModule;
    this.ReclassBatchNbr = tran.ReclassBatchNbr;
    this.IsReclassReverse = tran.IsReclassReverse;
    this.ReclassType = tran.ReclassType;
    this.CuryReclassRemainingAmt = tran.CuryReclassRemainingAmt;
    this.ReclassRemainingAmt = tran.ReclassRemainingAmt;
    this.Reclassified = tran.Reclassified;
    this.ReclassSourceTranModule = tran.ReclassSourceTranModule;
    this.ReclassSourceTranBatchNbr = tran.ReclassSourceTranBatchNbr;
    this.ReclassSourceTranLineNbr = tran.ReclassSourceTranLineNbr;
    this.ReclassSeqNbr = tran.ReclassSeqNbr;
    this.tstamp = tran.tstamp;
    this.CreatedByID = tran.CreatedByID;
    this.CreatedByScreenID = tran.CreatedByScreenID;
    this.CreatedDateTime = tran.CreatedDateTime;
    this.LastModifiedByID = tran.LastModifiedByID;
    this.LastModifiedByScreenID = tran.LastModifiedByScreenID;
    this.LastModifiedDateTime = tran.LastModifiedDateTime;
  }

  [PXBool]
  [PXUIField(DisplayName = "Selected", Visible = true, Enabled = true)]
  public override bool? Selected { get; set; }

  [PXUIField]
  [PXImage]
  public virtual string SplitIcon { get; set; }

  [PXString]
  [PXUIField]
  [ReclassAction.List]
  public virtual string ActionDesc { get; set; }

  [PXInt]
  [PXDefault]
  public virtual int? SortOrder { get; set; }

  [Branch(typeof (Batch.branchID), null, true, true, true, Enabled = false)]
  public override int? BranchID { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField]
  public override string BatchNbr { get; set; }

  [Account(typeof (GLTran.branchID), LedgerID = typeof (GLTran.ledgerID), DescriptionField = typeof (PX.Objects.GL.Account.description), Enabled = false)]
  public override int? AccountID { get; set; }

  [SubAccount(typeof (GLTran.accountID), typeof (GLTran.branchID), true, Enabled = false)]
  public override int? SubID { get; set; }

  [PXDBCurrency(typeof (GLTran.curyInfoID), typeof (GLTran.debitAmt))]
  [PXUIField]
  public override Decimal? CuryDebitAmt { get; set; }

  [PXDBCurrency(typeof (GLTran.curyInfoID), typeof (GLTran.creditAmt))]
  [PXUIField]
  public override Decimal? CuryCreditAmt { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public override string TranDesc { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField(DisplayName = "Post Period", Enabled = false)]
  public override string FinPeriodID { get; set; }

  [PXBool]
  public bool? IsParent { get; set; }

  [PXBool]
  public bool? IsSplited { get; set; }

  [PXBool]
  public bool? IsCurrent { get; set; }

  public GLTranReclHist ParentTran { get; set; }

  public List<GLTranReclHist> ChildTrans
  {
    get
    {
      if (this._ChildTrans == null)
        this._ChildTrans = new List<GLTranReclHist>();
      return this._ChildTrans;
    }
    set => this._ChildTrans = value;
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.selected>
  {
  }

  public new abstract class includedInReclassHistory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranReclHist.includedInReclassHistory>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.branchID>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.module>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.batchNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.lineNbr>
  {
  }

  public new abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.ledgerID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.subID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.projectID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.taskID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.costCodeID>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.refNbr>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.inventoryID>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.uOM>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranReclHist.qty>
  {
  }

  public new abstract class debitAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranReclHist.debitAmt>
  {
  }

  public new abstract class creditAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranReclHist.creditAmt>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranReclHist.curyInfoID>
  {
  }

  public new abstract class curyDebitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranReclHist.curyDebitAmt>
  {
  }

  public new abstract class curyCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranReclHist.curyCreditAmt>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.released>
  {
  }

  public new abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.posted>
  {
  }

  public new abstract class nonBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.nonBillable>
  {
  }

  public new abstract class isInterCompany : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranReclHist.isInterCompany>
  {
  }

  public new abstract class summPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.summPost>
  {
  }

  public new abstract class zeroPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.zeroPost>
  {
  }

  public new abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.origModule>
  {
  }

  public new abstract class origBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.origBatchNbr>
  {
  }

  public new abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.origLineNbr>
  {
  }

  public new abstract class origAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.origAccountID>
  {
  }

  public new abstract class origSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.origSubID>
  {
  }

  public new abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.tranID>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.tranType>
  {
  }

  public new abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.tranClass>
  {
  }

  public new abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.tranDesc>
  {
  }

  public new abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLTranReclHist.tranDate>
  {
  }

  public new abstract class tranLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.tranLineNbr>
  {
  }

  public new abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.referenceID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.finPeriodID>
  {
  }

  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.tranPeriodID>
  {
  }

  public new abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranReclHist.cATranID>
  {
  }

  public new abstract class pMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranReclHist.pMTranID>
  {
  }

  public new abstract class origPMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLTranReclHist.origPMTranID>
  {
  }

  public new abstract class ledgerBalanceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.ledgerBalanceType>
  {
  }

  public new abstract class accountRequireUnits : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranReclHist.accountRequireUnits>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.taxID>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.taxCategoryID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTranReclHist.noteID>
  {
  }

  public new abstract class reclassificationProhibited : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranReclHist.reclassificationProhibited>
  {
  }

  public new abstract class reclassBatchModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.reclassBatchModule>
  {
  }

  public new abstract class reclassBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.reclassBatchNbr>
  {
  }

  public new abstract class isReclassReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTranReclHist.isReclassReverse>
  {
  }

  public new abstract class reclassType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.reclassType>
  {
  }

  public new abstract class curyReclassRemainingAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranReclHist.curyReclassRemainingAmt>
  {
  }

  public new abstract class reclassRemainingAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranReclHist.reclassRemainingAmt>
  {
  }

  public new abstract class reclassified : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.reclassified>
  {
  }

  public new abstract class reclassSourceTranModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.reclassSourceTranModule>
  {
  }

  public new abstract class reclassSourceTranBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranReclHist.reclassSourceTranBatchNbr>
  {
  }

  public new abstract class reclassSourceTranLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTranReclHist.reclassSourceTranLineNbr>
  {
  }

  public new abstract class reclassSeqNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.reclassSeqNbr>
  {
  }

  public abstract class splitIcon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.splitIcon>
  {
  }

  public abstract class actionDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranReclHist.actionDesc>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranReclHist.sortOrder>
  {
  }

  public abstract class isParent : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.isParent>
  {
  }

  public abstract class isSplited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.isSplited>
  {
  }

  public abstract class isCurrent : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranReclHist.isCurrent>
  {
  }
}
