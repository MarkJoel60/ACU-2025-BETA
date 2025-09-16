// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementDetailInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.BQL;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A projection that is used in the AR Statement report. Cash sales and cash returns
/// are excluded to prevent them from appearing in the Statement Report.
/// </summary>
[PXCacheName("AR Statement Detail Info")]
[PXProjection(typeof (Select2<ARStatementDetail, LeftJoin<ARTranPostGL, On<ARTranPostGL.iD, Equal<ARStatementDetail.tranPostID>>, LeftJoin<ARRegister, On<ARTranPostGL.docType, Equal<ARRegister.docType>, And<ARTranPostGL.refNbr, Equal<ARRegister.refNbr>>>, LeftJoin<ARRegister2, On<ARTranPostGL.sourceDocType, Equal<ARRegister2.docType>, And<ARTranPostGL.sourceRefNbr, Equal<ARRegister2.refNbr>>>, LeftJoin<PX.Objects.AR.Standalone.ARInvoice, On<PX.Objects.AR.Standalone.ARInvoice.docType, Equal<ARRegister.docType>, And<PX.Objects.AR.Standalone.ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, LeftJoin<PX.Objects.AR.Standalone.ARPayment, On<PX.Objects.AR.Standalone.ARPayment.docType, Equal<ARRegister.docType>, And<PX.Objects.AR.Standalone.ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>>>>, Where2<IsNotSelfApplying<ARRegister.docType>, Or<BqlOperand<ARRegister.docType, IBqlString>.IsNull>>>), Persistent = false)]
[Serializable]
public class ARStatementDetailInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlTable = typeof (ARTranPostGL))]
  public virtual int? ID { get; set; }

  /// <summary>
  /// The date of the <see cref="T:PX.Objects.AR.ARStatement">Customer Statement</see>, to which
  /// the detail belongs. This field is part of the compound key of the statement
  /// detail, and part of the foreign key referencing the <see cref="T:PX.Objects.AR.ARStatement">
  /// Customer Statement</see> record.
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARStatement.StatementDate" /> field.
  /// </summary>
  [PXDBDate(IsKey = true, BqlTable = typeof (ARStatementDetail))]
  [PXDefault(typeof (ARStatement.statementDate))]
  [PXUIField(DisplayName = "Statement Date")]
  public virtual DateTime? StatementDate { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the document
  /// is open on the statement date.
  /// </summary>
  [PXDBBool(BqlTable = typeof (ARStatementDetail))]
  [PXDefault(true)]
  public virtual bool? IsOpen { get; set; }

  [PXDBDate(BqlField = typeof (ARRegister.statementDate))]
  public virtual DateTime? DocStatementDate { get; set; }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (ARRegister.docDesc))]
  [PXUIField]
  public virtual 
  #nullable disable
  string DocDesc { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [PXDBBool(BqlField = typeof (ARRegister.isMigratedRecord))]
  public virtual bool? IsMigratedRecord { get; set; }

  [PXString(3, IsFixed = true)]
  [ARDocType.PrintList]
  [PXUIField]
  public virtual string PrintDocType => this.DocType;

  public virtual bool? Payable
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.docType)})] get
    {
      return this.DocType == "REF" || this.DocType == "VRF" ? new bool?(true) : ARDocType.Payable(this.DocType);
    }
  }

  public virtual Decimal? BalanceSign
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.docType)})] get
    {
      return ARDocType.SignBalance(this.DocType);
    }
  }

  [PXDBLong(BqlField = typeof (ARRegister.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBGuid(false, IsKey = true, BqlTable = typeof (ARStatementDetail))]
  public virtual Guid? RefNoteID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARStatementDetailInfo.curyInfoID), typeof (ARStatementDetailInfo.origDocAmt), BqlField = typeof (ARRegister.curyOrigDocAmt))]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (ARRegister.origDocAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt { get; set; }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARStatementDetailInfo.CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARStatementDetailInfo.curyInfoID), typeof (ARStatementDetailInfo.initDocBal), BqlField = typeof (ARRegister.curyInitDocBal))]
  public virtual Decimal? CuryInitDocBal { get; set; }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury(null, null, BqlField = typeof (ARRegister.initDocBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? InitDocBal { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Standalone.ARInvoice.invoiceNbr))]
  [PXUIField]
  public virtual string InvoiceNbr { get; set; }

  [PXDBDate(BqlField = typeof (ARRegister.dueDate))]
  [PXUIField]
  public virtual DateTime? DueDate { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.extRefNbr))]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string DocExtRefNbr
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.payable), typeof (ARStatementDetailInfo.invoiceNbr), typeof (ARStatementDetailInfo.extRefNbr)})] get
    {
      bool? nullable = ARDocType.Payable(this.DocType);
      if (!nullable.HasValue)
        return string.Empty;
      return !nullable.Value ? this.ExtRefNbr : this.InvoiceNbr;
    }
  }

  [PXDecimal]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmtSigned
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.docType), typeof (ARStatementDetailInfo.balanceSign), typeof (ARStatementDetailInfo.curyOrigDocAmt)})] get
    {
      Decimal? balanceSign = this.BalanceSign;
      Decimal? curyOrigDocAmt = this.CuryOrigDocAmt;
      return !(balanceSign.HasValue & curyOrigDocAmt.HasValue) ? new Decimal?() : new Decimal?(balanceSign.GetValueOrDefault() * curyOrigDocAmt.GetValueOrDefault());
    }
  }

  [PXDecimal]
  public virtual Decimal? OrigDocAmtSigned
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.docType), typeof (ARStatementDetailInfo.balanceSign), typeof (ARStatementDetailInfo.origDocAmt)})] get
    {
      Decimal? balanceSign = this.BalanceSign;
      Decimal? origDocAmt = this.OrigDocAmt;
      return !(balanceSign.HasValue & origDocAmt.HasValue) ? new Decimal?() : new Decimal?(balanceSign.GetValueOrDefault() * origDocAmt.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARStatementDetailInfo.CuryID">currency of the document</see>.
  /// </summary>
  [PXDecimal]
  public virtual Decimal? CuryInitDocBalSigned
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.docType), typeof (ARStatementDetailInfo.balanceSign), typeof (ARStatementDetailInfo.curyInitDocBal)})] get
    {
      Decimal? balanceSign = this.BalanceSign;
      Decimal? curyInitDocBal = this.CuryInitDocBal;
      return !(balanceSign.HasValue & curyInitDocBal.HasValue) ? new Decimal?() : new Decimal?(balanceSign.GetValueOrDefault() * curyInitDocBal.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDecimal]
  public virtual Decimal? InitDocBalSigned
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.docType), typeof (ARStatementDetailInfo.balanceSign), typeof (ARStatementDetailInfo.initDocBal)})] get
    {
      Decimal? balanceSign = this.BalanceSign;
      Decimal? initDocBal = this.InitDocBal;
      return !(balanceSign.HasValue & initDocBal.HasValue) ? new Decimal?() : new Decimal?(balanceSign.GetValueOrDefault() * initDocBal.GetValueOrDefault());
    }
  }

  /// <summary>
  /// Indicates the balance, in base currency, that the document
  /// has on the statement date.
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Doc. Balance")]
  public virtual Decimal? DocBalance { get; set; }

  /// <summary>
  /// Indicates the balance, in document currency, that the document
  /// has on the statement date.
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Cury. Doc. Balance")]
  public virtual Decimal? CuryDocBalance { get; set; }

  [PXDecimal]
  [PXUIField]
  public virtual Decimal? CuryDocBalanceSigned
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.payable), typeof (ARStatementDetailInfo.curyDocBalance)})] get
    {
      if (!this.Payable.HasValue)
        return new Decimal?();
      if (this.Payable.Value)
        return this.CuryDocBalance;
      Decimal? curyDocBalance = this.CuryDocBalance;
      return !curyDocBalance.HasValue ? new Decimal?() : new Decimal?(-curyDocBalance.GetValueOrDefault());
    }
  }

  [PXDecimal]
  public virtual Decimal? DocBalanceSigned
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.payable), typeof (ARStatementDetailInfo.docBalance)})] get
    {
      if (!this.Payable.HasValue)
        return new Decimal?();
      if (this.Payable.Value)
        return this.DocBalance;
      Decimal? docBalance = this.DocBalance;
      return !docBalance.HasValue ? new Decimal?() : new Decimal?(-docBalance.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The type of the customer statement.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.StatementType" />
  /// </summary>
  [PXDBString(1, IsFixed = true, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Statement Type")]
  public virtual string StatementType { get; set; }

  /// <summary>
  /// The beginning balance of the customer statement in the base currency. Only for the Balance Brought Forvard type.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.BegBalance" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beg. Balance")]
  public virtual Decimal? BegBalance { get; set; }

  /// <summary>
  /// The beginning balance of the customer statement in the foreign currency. Only for the Balance Brought Forvard type.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.CuryBegBalance" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Beg. Balance")]
  public virtual Decimal? CuryBegBalance { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 00 in the base currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.AgeBalance00" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Age00 Balance")]
  public virtual Decimal? AgeBalance00 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 00 in the foreign currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.CuryAgeBalance00" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Cury. Age00 Balance")]
  public virtual Decimal? CuryAgeBalance00 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 01 in the base currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.AgeBalance01" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Age01 Balance")]
  public virtual Decimal? AgeBalance01 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 01 in the foreign currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.CuryAgeBalance01" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Cury. Age01 Balance")]
  public virtual Decimal? CuryAgeBalance01 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 02 in the base currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.AgeBalance02" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Age02 Balance")]
  public virtual Decimal? AgeBalance02 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 02 in the foreign currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.CuryAgeBalance02" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Cury. Age02 Balance")]
  public virtual Decimal? CuryAgeBalance02 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 03 in the base currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.AgeBalance03" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Cury. Age03 Balance")]
  public virtual Decimal? AgeBalance03 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 03 in the foreign currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.CuryAgeBalance03" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Cury. Age03 Balance")]
  public virtual Decimal? CuryAgeBalance03 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 04 in the base currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.AgeBalance04" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Age04 Balance")]
  public virtual Decimal? AgeBalance04 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 04 in the foreign currency.
  /// See <see cref="P:PX.Objects.AR.ARStatementDetail.CuryAgeBalance04" />
  /// </summary>
  [PXDBDecimal(4, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Cury. Age04 Balance")]
  public virtual Decimal? CuryAgeBalance04 { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  [ARTranPost.type.List]
  public virtual string Type { get; set; }

  [PXDBDate(BqlTable = typeof (ARRegister))]
  [PXDefault(typeof (ARRegister.docDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARStatementDetail))]
  [PXUIField(DisplayName = "Doc. Type")]
  [ARDocType.List]
  public virtual string DocType { get; set; }

  [PXDBString(IsKey = true, BqlTable = typeof (ARStatementDetail))]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXUIField(DisplayName = "Source Doc. Type", BqlTable = typeof (ARTranPostGL))]
  [ARDocType.List]
  [PXDBString(IsKey = true, BqlTable = typeof (ARTranPostGL))]
  public virtual string SourceDocType { get; set; }

  [PXUIField]
  [PXDBString(IsKey = true, BqlTable = typeof (ARTranPostGL))]
  public virtual string SourceRefNbr { get; set; }

  [Branch(null, null, true, true, true, BqlTable = typeof (ARStatementDetail))]
  public virtual int? BranchID { get; set; }

  [Customer(BqlTable = typeof (ARStatementDetail))]
  public virtual int? CustomerID { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARTranPostGL))]
  public virtual Decimal? CuryBalanceAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARTranPostGL))]
  public virtual Decimal? BalanceAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARTranPostGL))]
  public virtual Decimal? CuryTurnDiscAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARTranPostGL))]
  public virtual Decimal? TurnDiscAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARTranPostGL))]
  public virtual Decimal? CuryTurnWOAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARTranPostGL))]
  public virtual Decimal? RGOLAmt { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARTranPostGL))]
  public virtual Decimal? TurnWOAmt { get; set; }

  [PXDBString(BqlTable = typeof (ARTranPostGL))]
  public virtual string TranType { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister2.voided, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.sourceDocType, Equal<ARDocType.smallBalanceWO>>>>>.Or<BqlOperand<ARTranPostGL.sourceDocType, IBqlString>.IsEqual<ARDocType.smallCreditWO>>>>, True>, False>), typeof (bool))]
  public virtual bool? IsSelfVoidingVoidApplication { get; set; }

  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.isDocumentPresent), typeof (ARStatementDetailInfo.isSourceDocumentPresent)})]
  public virtual bool? IsOrphanApplication
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          bool? isDocumentPresent = this.IsDocumentPresent;
          return !isDocumentPresent.HasValue ? new bool?() : new bool?(!isDocumentPresent.GetValueOrDefault());
        case "D":
          bool? sourceDocumentPresent = this.IsSourceDocumentPresent;
          return !sourceDocumentPresent.HasValue ? new bool?() : new bool?(!sourceDocumentPresent.GetValueOrDefault());
        default:
          return new bool?(false);
      }
    }
  }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARStatementDetail.statementDate, IBqlDateTime>.IsEqual<ARRegister.statementDate>>, True>, False>), typeof (bool))]
  public virtual bool? IsDocumentPresent { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARStatementDetail.statementDate, IBqlDateTime>.IsEqual<ARRegister2.statementDate>>, True>, False>), typeof (bool))]
  public virtual bool? IsSourceDocumentPresent { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (ARStatementDetail))]
  public virtual string CuryID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (ARRegister2), BqlField = typeof (ARRegister2.curyID))]
  public virtual string SourceCuryID { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the parent
  /// <see cref="T:PX.Objects.AR.ARAdjust">application</see> affects documents
  /// that have different <see cref="T:PX.Objects.CM.Currency">currencies</see>.
  /// </summary>
  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.curyID), typeof (ARStatementDetailInfo.sourceCuryID)})]
  public virtual bool? IsInterCurrencyApplication => new bool?(this.CuryID != this.SourceCuryID);

  [Branch(null, null, true, true, true, BqlTable = typeof (ARRegister2), BqlField = typeof (ARRegister2.branchID))]
  public virtual int? SourceBranchID { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the parent
  /// <see cref="T:PX.Objects.AR.ARAdjust">application</see> affects documents
  /// that originate in different <see cref="T:PX.Objects.GL.Branch">branches</see>.
  /// </summary>
  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.branchID), typeof (ARStatementDetailInfo.sourceBranchID)})]
  public virtual bool? IsInterBranchApplication
  {
    get
    {
      int? branchId = this.BranchID;
      int? sourceBranchId = this.SourceBranchID;
      return new bool?(!(branchId.GetValueOrDefault() == sourceBranchId.GetValueOrDefault() & branchId.HasValue == sourceBranchId.HasValue));
    }
  }

  [Customer(BqlTable = typeof (ARRegister2), BqlField = typeof (ARRegister2.customerID))]
  public virtual int? SourceCustomerID { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the parent
  /// <see cref="T:PX.Objects.AR.ARAdjust">application</see> affects documents
  /// that belong to different <see cref="T:PX.Objects.AR.Customer">customers</see>.
  /// </summary>
  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.customerID), typeof (ARStatementDetailInfo.sourceCustomerID)})]
  public virtual bool? IsInterCustomerApplication
  {
    get
    {
      int? customerId = this.CustomerID;
      int? sourceCustomerId = this.SourceCustomerID;
      return new bool?(!(customerId.GetValueOrDefault() == sourceCustomerId.GetValueOrDefault() & customerId.HasValue == sourceCustomerId.HasValue));
    }
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the parent
  /// <see cref="T:PX.Objects.AR.ARAdjust">application</see> affects two
  /// <see cref="T:PX.Objects.AR.ARStatement">Customer Statements</see> at once.
  /// If and only if this flag is set to <c>true</c>, the ending balance
  /// of the parent statement will account for the application amount.
  /// </summary>
  [PXBool]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.isInterBranchApplication), typeof (ARStatementDetailInfo.isInterCurrencyApplication), typeof (ARStatementDetailInfo.isInterCustomerApplication)})]
  public virtual bool? IsInterStatementApplication
  {
    get
    {
      return new bool?(this.IsInterBranchApplication.GetValueOrDefault() || this.IsInterCurrencyApplication.GetValueOrDefault() || this.IsInterCustomerApplication.GetValueOrDefault());
    }
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlTable = typeof (ARAdjust))]
  [LabelList(typeof (ARDocType))]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.docType), typeof (ARStatementDetailInfo.sourceDocType)})]
  public virtual string AdjgDocType
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.DocType;
        case "D":
          return this.SourceDocType;
        default:
          return (string) null;
      }
    }
  }

  [PXString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlTable = typeof (ARAdjust))]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.docType), typeof (ARStatementDetailInfo.sourceDocType), typeof (ARStatementDetailInfo.isOrphanApplication)})]
  public virtual string AdjgRefNbr
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.RefNbr;
        case "D":
          bool? nullable = this.IsDocumentPresent;
          if (!nullable.GetValueOrDefault())
          {
            nullable = this.IsOrphanApplication;
            if (!nullable.GetValueOrDefault())
            {
              nullable = this.IsInterStatementApplication;
              if (!nullable.GetValueOrDefault())
                return (string) null;
            }
          }
          return this.SourceRefNbr;
        default:
          return (string) null;
      }
    }
  }

  [PXString(3, IsKey = true, IsFixed = true, InputMask = "", BqlTable = typeof (ARAdjust))]
  [LabelList(typeof (ARDocType))]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.docType), typeof (ARStatementDetailInfo.sourceDocType)})]
  public virtual string AdjdDocType
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.SourceDocType;
        case "D":
          return this.DocType;
        default:
          return (string) null;
      }
    }
  }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.docType), typeof (ARStatementDetailInfo.refNbr), typeof (ARStatementDetailInfo.sourceRefNbr)})]
  public virtual string AdjdRefNbr
  {
    get
    {
      if (this.DocType != null && string.IsNullOrWhiteSpace(this.DocType) && this.RefNbr != null && string.IsNullOrWhiteSpace(this.RefNbr))
        return string.Empty;
      switch (this.Type)
      {
        case "G":
          return this.SourceRefNbr;
        case "D":
          return this.RefNbr;
        default:
          return (string) null;
      }
    }
  }

  [PXDefault]
  [Customer]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.customerID), typeof (ARStatementDetailInfo.sourceCustomerID)})]
  public virtual int? AdjdCustomerID
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.SourceCustomerID;
        case "D":
          return this.CustomerID;
        default:
          return new int?();
      }
    }
  }

  [PXDefault]
  [Customer]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.customerID), typeof (ARStatementDetailInfo.sourceCustomerID)})]
  public virtual int? AdjgCustomerID
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.CustomerID;
        case "D":
          return this.SourceCustomerID;
        default:
          return new int?();
      }
    }
  }

  [Branch(null, null, true, true, true)]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.branchID), typeof (ARStatementDetailInfo.sourceBranchID)})]
  public virtual int? AdjdBranchID
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.SourceBranchID;
        case "D":
          return this.BranchID;
        default:
          return new int?();
      }
    }
  }

  [Branch(null, null, true, true, true)]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.branchID), typeof (ARStatementDetailInfo.sourceBranchID)})]
  public virtual int? AdjgBranchID
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.BranchID;
        case "D":
          return this.SourceBranchID;
        default:
          return new int?();
      }
    }
  }

  [PXString]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.curyID), typeof (ARStatementDetailInfo.sourceCuryID)})]
  public virtual string AdjdCuryID
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.SourceCuryID;
        case "D":
          return this.CuryID;
        default:
          return (string) null;
      }
    }
  }

  [PXString]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.type), typeof (ARStatementDetailInfo.curyID), typeof (ARStatementDetailInfo.sourceCuryID)})]
  public virtual string AdjgCuryID
  {
    get
    {
      switch (this.Type)
      {
        case "G":
          return this.CuryID;
        case "D":
          return this.SourceCuryID;
        default:
          return (string) null;
      }
    }
  }

  [PXDecimal]
  [PXDependsOnFields(new System.Type[] {typeof (ARStatementDetailInfo.isInterStatementApplication), typeof (ARStatementDetailInfo.adjgDocType), typeof (ARStatementDetailInfo.adjdDocType)})]
  public virtual Decimal? SignBalanceDelta
  {
    get
    {
      Decimal num = this.IsInterStatementApplication.GetValueOrDefault() ? 1M : -1M;
      return new Decimal?((this.AdjgDocType == "SMB" ? 1 : (!(this.AdjdDocType == "SMC") ? 0 : (this.AdjgDocType == "PMT" ? 1 : (this.AdjgDocType == "CRM" ? 1 : 0)))) != 0 ? -num : num);
    }
  }

  public class PK : 
    PrimaryKeyOf<ARStatementDetailInfo>.By<ARStatementDetailInfo.branchID, ARStatementDetailInfo.customerID, ARStatementDetailInfo.curyID, ARStatementDetailInfo.statementDate, ARStatementDetailInfo.docType, ARStatementDetailInfo.refNbr, ARStatementDetailInfo.refNoteID>
  {
    public static ARStatementDetailInfo Find(
      PXGraph graph,
      int? branchID,
      int? customerID,
      string curyID,
      DateTime? statementDate,
      string docType,
      string refNbr,
      Guid? refNoteID,
      PKFindOptions options = 0)
    {
      return (ARStatementDetailInfo) PrimaryKeyOf<ARStatementDetailInfo>.By<ARStatementDetailInfo.branchID, ARStatementDetailInfo.customerID, ARStatementDetailInfo.curyID, ARStatementDetailInfo.statementDate, ARStatementDetailInfo.docType, ARStatementDetailInfo.refNbr, ARStatementDetailInfo.refNoteID>.FindBy(graph, (object) branchID, (object) customerID, (object) curyID, (object) statementDate, (object) docType, (object) refNbr, (object) refNoteID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARStatementDetailInfo>.By<ARStatementDetailInfo.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARStatementDetailInfo>.By<ARStatementDetailInfo.customerID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARStatementDetailInfo>.By<ARStatementDetailInfo.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARStatementDetailInfo>.By<ARStatementDetailInfo.curyID>
    {
    }

    public class Statement : 
      PrimaryKeyOf<ARStatement>.By<ARStatement.branchID, ARStatement.customerID, ARStatement.curyID, ARStatement.statementDate>.ForeignKeyOf<ARStatementDetailInfo>.By<ARStatementDetailInfo.branchID, ARStatementDetailInfo.customerID, ARStatementDetailInfo.curyID, ARStatementDetailInfo.statementDate>
    {
    }
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementDetailInfo.iD>
  {
  }

  public abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementDetailInfo.statementDate>
  {
  }

  public abstract class isOpen : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatementDetailInfo.isOpen>
  {
  }

  public abstract class docStatementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementDetailInfo.docStatementDate>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetailInfo.docDesc>
  {
  }

  public abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isMigratedRecord>
  {
  }

  public abstract class printDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.printDocType>
  {
  }

  public abstract class payable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatementDetailInfo.payable>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.balanceSign>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARStatementDetailInfo.curyInfoID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementDetailInfo.refNoteID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.origDocAmt>
  {
  }

  public abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyInitDocBal>
  {
  }

  public abstract class initDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.initDocBal>
  {
  }

  public abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.invoiceNbr>
  {
  }

  public abstract class dueDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementDetailInfo.dueDate>
  {
  }

  public abstract class extRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.extRefNbr>
  {
  }

  public abstract class docBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.docBalance>
  {
  }

  public abstract class curyDocBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyDocBalance>
  {
  }

  public abstract class statementType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.statementType>
  {
  }

  public abstract class begBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.begBalance>
  {
  }

  public abstract class curyBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyBegBalance>
  {
  }

  public abstract class ageBalance00 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.ageBalance00>
  {
  }

  public abstract class curyAgeBalance00 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyAgeBalance00>
  {
  }

  public abstract class ageBalance01 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.ageBalance01>
  {
  }

  public abstract class curyAgeBalance01 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyAgeBalance01>
  {
  }

  public abstract class ageBalance02 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.ageBalance02>
  {
  }

  public abstract class curyAgeBalance02 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyAgeBalance02>
  {
  }

  public abstract class ageBalance03 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.ageBalance03>
  {
  }

  public abstract class curyAgeBalance03 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyAgeBalance03>
  {
  }

  public abstract class ageBalance04 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.ageBalance04>
  {
  }

  public abstract class curyAgeBalance04 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyAgeBalance04>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetailInfo.type>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementDetailInfo.docDate>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetailInfo.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetailInfo.refNbr>
  {
  }

  public abstract class sourceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.sourceRefNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementDetailInfo.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementDetailInfo.customerID>
  {
  }

  public abstract class curyBalanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyBalanceAmt>
  {
  }

  public abstract class balanceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.balanceAmt>
  {
  }

  public abstract class curyTurnDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyTurnDiscAmt>
  {
  }

  public abstract class turnDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.turnDiscAmt>
  {
  }

  public abstract class curyTurnWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.curyTurnWOAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARStatementDetailInfo.rGOLAmt>
  {
  }

  public abstract class turnWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.turnWOAmt>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetailInfo.tranType>
  {
  }

  public abstract class isSelfVoidingVoidApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isSelfVoidingVoidApplication>
  {
  }

  public abstract class isOrphanApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isOrphanApplication>
  {
  }

  public abstract class isDocumentPresent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isDocumentPresent>
  {
  }

  public abstract class isSourceDocumentPresent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isSourceDocumentPresent>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetailInfo.curyID>
  {
  }

  public abstract class sourceCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.curyID>
  {
  }

  public abstract class isInterCurrencyApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isInterCurrencyApplication>
  {
  }

  public abstract class sourceBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementDetailInfo.sourceBranchID>
  {
  }

  public abstract class isInterBranchApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isInterBranchApplication>
  {
  }

  public abstract class sourceCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementDetailInfo.sourceCustomerID>
  {
  }

  public abstract class isInterCustomerApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isInterCustomerApplication>
  {
  }

  public abstract class isInterStatementApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatementDetailInfo.isInterStatementApplication>
  {
  }

  public abstract class adjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.adjgRefNbr>
  {
  }

  public abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.adjdRefNbr>
  {
  }

  public abstract class adjdCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementDetailInfo.adjdCustomerID>
  {
  }

  public abstract class adjgCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementDetailInfo.adjgCustomerID>
  {
  }

  public abstract class adjdbranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementDetailInfo.adjdbranchID>
  {
  }

  public abstract class adjgbranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatementDetailInfo.adjgbranchID>
  {
  }

  public abstract class adjdCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.adjdCuryID>
  {
  }

  public abstract class adjgCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetailInfo.adjgCuryID>
  {
  }

  /// <summary>
  /// The sign with which the application amount affects the
  /// <see cref="T:PX.Objects.AR.ARStatement">customer statement</see> balance.
  /// </summary>
  public abstract class signBalanceDelta : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetailInfo.signBalanceDelta>
  {
  }
}
