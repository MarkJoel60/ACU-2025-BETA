// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxDetailByGLReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select2<TaxTran, InnerJoin<TaxBucket, On<TaxBucket.vendorID, Equal<TaxTran.vendorID>, And<TaxBucket.bucketID, Equal<TaxTran.taxBucketID>>>, InnerJoin<PX.Objects.GL.Branch, On<TaxTran.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<TaxPeriodEffective, On<PX.Objects.GL.Branch.organizationID, Equal<TaxPeriodEffective.organizationID>, And<TaxPeriodEffective.vendorID, Equal<TaxTran.vendorID>, And<Where<TaxTran.taxPeriodID, IsNull, And<TaxTran.origRefNbr, Equal<Empty>, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxType, NotEqual<PX.Objects.TX.TaxType.pendingSales>, And<TaxTran.taxType, NotEqual<PX.Objects.TX.TaxType.pendingPurchase>, And<TaxTran.tranDate, Less<TaxPeriodEffective.endDate>, And<TaxPeriodEffective.status, Equal<TaxPeriodStatus.open>, Or<TaxTran.taxPeriodID, Equal<TaxPeriodEffective.taxPeriodID>>>>>>>>>>>>>>>>>))]
[PXCacheName("Tax Report Detail")]
public class TaxDetailByGLReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Module;
  protected string _TranType;
  protected string _TranTypeInvoiceDiscriminated;
  protected string _RefNbr;
  protected int? _RecordID;
  protected bool? _Released;
  protected bool? _Voided;
  protected string _TaxPeriodID;
  protected string _TaxID;
  protected int? _VendorID;
  protected int? _BranchID;
  protected string _TaxZoneID;
  protected int? _AccountID;
  protected int? _SubID;
  protected DateTime? _TranDate;
  protected string _TaxType;
  protected Decimal? _TaxRate;
  protected Decimal? _TaxableAmt;
  protected Decimal? _TaxAmt;
  protected Decimal? _TaxableAmtIO;
  protected Decimal? _TaxAmtIO;

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (TaxTran.module))]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (TaxTran.tranType))]
  [TaxAdjustmentType.List]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXString]
  [PXDBCalced(typeof (Switch<Case<Where<TaxTran.module, Equal<BatchModule.moduleAP>, And<TaxTran.tranType, Equal<APDocType.invoice>>>, TaxTranReport.tranTypeInvoiceDiscriminated.apInvoice, Case<Where<TaxTran.module, Equal<BatchModule.moduleAR>, And<TaxTran.tranType, Equal<ARDocType.invoice>>>, TaxTranReport.tranTypeInvoiceDiscriminated.arInvoice>>, TaxTran.tranType>), typeof (string))]
  [LabelList(typeof (TaxTranReport.tranTypeInvoiceDiscriminated))]
  public virtual string TranTypeInvoiceDiscriminated
  {
    get => this._TranTypeInvoiceDiscriminated;
    set => this._TranTypeInvoiceDiscriminated = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (TaxTran.refNbr))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (TaxTran.recordID))]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBBool(BqlField = typeof (TaxTran.released))]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool(BqlField = typeof (TaxTran.voided))]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (TaxPeriodEffective.taxPeriodID))]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBString(60, IsUnicode = true, IsKey = true, BqlField = typeof (TaxTran.taxID))]
  [PXSelector(typeof (Tax.taxID), DescriptionField = typeof (Tax.descr))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [Vendor(BqlField = typeof (TaxTran.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [Branch(null, null, true, true, true, BqlField = typeof (TaxTran.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (TaxTran.taxZoneID))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [Account(null, BqlField = typeof (TaxTran.accountID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (TaxTran.accountID), BqlField = typeof (TaxTran.subID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBDate(BqlField = typeof (TaxTran.tranDate))]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (TaxTran.taxType))]
  public virtual string TaxType
  {
    get => this._TaxType;
    set => this._TaxType = value;
  }

  [PXDBDecimal(6, BqlField = typeof (TaxTran.taxRate))]
  public virtual Decimal? TaxRate
  {
    get => this._TaxRate;
    set => this._TaxRate = value;
  }

  [PXDBBaseCury(BqlField = typeof (TaxTran.taxableAmt))]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  [PXDBBaseCury(BqlField = typeof (TaxTran.taxAmt))]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PXDBBaseCury(BqlField = typeof (TaxTran.taxableAmt))]
  public virtual Decimal? TaxableAmtIO
  {
    [PXDependsOnFields(new Type[] {typeof (TaxDetailByGLReport.module), typeof (TaxDetailByGLReport.tranType), typeof (TaxDetailByGLReport.taxType), typeof (TaxDetailByGLReport.taxableAmt)})] get
    {
      Decimal mult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, new short?((short) 1));
      Decimal? taxableAmt = this._TaxableAmt;
      return !taxableAmt.HasValue ? new Decimal?() : new Decimal?(mult * taxableAmt.GetValueOrDefault());
    }
    set => this._TaxableAmtIO = value;
  }

  [PXDBBaseCury(BqlField = typeof (TaxTran.taxAmt))]
  public virtual Decimal? TaxAmtIO
  {
    [PXDependsOnFields(new Type[] {typeof (TaxDetailByGLReport.module), typeof (TaxDetailByGLReport.tranType), typeof (TaxDetailByGLReport.taxType), typeof (TaxDetailByGLReport.taxAmt)})] get
    {
      Decimal mult = ReportTaxProcess.GetMult(this._Module, this._TranType, this._TaxType, new short?((short) 1));
      Decimal? taxAmt = this._TaxAmt;
      return !taxAmt.HasValue ? new Decimal?() : new Decimal?(mult * taxAmt.GetValueOrDefault());
    }
    set => this._TaxAmtIO = value;
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailByGLReport.module>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailByGLReport.tranType>
  {
  }

  public abstract class tranTypeInvoiceDiscriminated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxDetailByGLReport.tranTypeInvoiceDiscriminated>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailByGLReport.refNbr>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailByGLReport.recordID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxDetailByGLReport.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxDetailByGLReport.voided>
  {
  }

  public abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxDetailByGLReport.taxPeriodID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailByGLReport.taxID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailByGLReport.vendorID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailByGLReport.branchID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailByGLReport.taxZoneID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailByGLReport.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxDetailByGLReport.subID>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxDetailByGLReport.tranDate>
  {
  }

  public abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetailByGLReport.taxType>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxDetailByGLReport.taxRate>
  {
  }

  public abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxDetailByGLReport.taxableAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxDetailByGLReport.taxAmt>
  {
  }

  public abstract class taxableAmtIO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxDetailByGLReport.taxableAmtIO>
  {
  }

  public abstract class taxAmtIO : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxDetailByGLReport.taxAmtIO>
  {
  }
}
