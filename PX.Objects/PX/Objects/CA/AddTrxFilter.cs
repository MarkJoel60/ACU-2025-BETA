// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.AddTrxFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA.MultiCurrency;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CA;

/// <exclude />
[Serializable]
public class AddTrxFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CostCodeID;

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? OnlyExpense { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Doc. Date", Required = true)]
  [AddTrxFilter.AddFilter]
  public virtual DateTime? TranDate { get; set; }

  [PXDefault]
  [CashAccount]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>))]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Ref.", Required = true)]
  [PaymentRef(typeof (AddTrxFilter.cashAccountID), typeof (AddTrxFilter.paymentMethodID), null)]
  public virtual string ExtRefNbr { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>, LeftJoin<ARSetup, On<ARSetup.createdDateTime, IsNotNull>, LeftJoin<APSetup, On<APSetup.createdDateTime, IsNotNull>>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>, And2<Where<Current<AddTrxFilter.onlyExpense>, Equal<False>, Or<CAEntryType.module, Equal<BatchModule.moduleCA>>>, And2<Where<APSetup.migrationMode, NotEqual<True>, Or<CAEntryType.module, NotEqual<BatchModule.moduleAP>>>, And<Where<ARSetup.migrationMode, NotEqual<True>, Or<CAEntryType.module, NotEqual<BatchModule.moduleAR>>>>>>>>), DescriptionField = typeof (CAEntryType.descr))]
  [PXDefault(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>, LeftJoin<ARSetup, On<ARSetup.createdDateTime, IsNotNull>, LeftJoin<APSetup, On<APSetup.createdDateTime, IsNotNull>>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>, And<CashAccountETDetail.isDefault, Equal<True>, And2<Where<Current<AddTrxFilter.onlyExpense>, Equal<False>, Or<CAEntryType.module, Equal<BatchModule.moduleCA>>>, And2<Where<APSetup.migrationMode, NotEqual<True>, Or<CAEntryType.module, NotEqual<BatchModule.moduleAP>>>, And<Where<ARSetup.migrationMode, NotEqual<True>, Or<CAEntryType.module, NotEqual<BatchModule.moduleAR>>>>>>>>>))]
  [PXUIField(DisplayName = "Entry Type", Required = true)]
  public virtual string EntryTypeID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (Search<CAEntryType.module, Where<CAEntryType.entryTypeId, Equal<Current<AddTrxFilter.entryTypeID>>>>))]
  [PXUIField(DisplayName = "Module", Enabled = false)]
  public virtual string OrigModule { get; set; }

  [CAAPAROpenPeriod(typeof (AddTrxFilter.origModule), typeof (AddTrxFilter.tranDate), typeof (AddTrxFilter.branchID), null, null, null, null, true, typeof (AddTrxFilter.tranPeriodID), true, ValidatePeriod = PeriodValidation.Nothing)]
  [PXFormula(typeof (Default<AddTrxFilter.tranDate, AddTrxFilter.cashAccountID, AddTrxFilter.branchID, AddTrxFilter.entryTypeID>))]
  [PXUIField(DisplayName = "Fin. Period", Required = true)]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PXDefault("D", typeof (Search<CAEntryType.drCr, Where<CAEntryType.entryTypeId, Equal<Current<AddTrxFilter.entryTypeID>>>>))]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField(DisplayName = "Disb. / Receipt", Enabled = false)]
  public virtual string DrCr { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXDefault(typeof (Search<CashAccountETDetail.taxZoneID, Where<CashAccountETDetail.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<AddTrxFilter.entryTypeID>>>>>))]
  public virtual string TaxZoneID { get; set; }

  [PXDefault(typeof (Search<CAEntryType.descr, Where<CAEntryType.entryTypeId, Equal<Current<AddTrxFilter.entryTypeID>>>>))]
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr { get; set; }

  [PXDefault(typeof (Coalesce<Search<CashAccountETDetail.offsetAccountID, Where<CashAccountETDetail.entryTypeID, Equal<Current<AddTrxFilter.entryTypeID>>, And<CashAccountETDetail.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>>, Search<CAEntryType.accountID, Where<CAEntryType.entryTypeId, Equal<Current<AddTrxFilter.entryTypeID>>>>>))]
  [Account(typeof (AddTrxFilter.branchID), typeof (Search2<PX.Objects.GL.Account.accountID, LeftJoin<CashAccount, On<CashAccount.accountID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<CAEntryType, On<CAEntryType.entryTypeId, Equal<Current<AddTrxFilter.entryTypeID>>>>>, Where2<Where2<Where<CAEntryType.useToReclassifyPayments, Equal<False>, And<Where<PX.Objects.GL.Account.curyID, IsNull, Or<PX.Objects.GL.Account.curyID, Equal<Current<AddTrxFilter.curyID>>>>>>, Or<Where<CashAccount.cashAccountID, IsNotNull, And<CashAccount.curyID, Equal<Current<AddTrxFilter.curyID>>, And<CashAccount.cashAccountID, NotEqual<Current<AddTrxFilter.cashAccountID>>>>>>>, And<Match<Current<AccessInfo.userName>>>>>), DisplayName = "Offset Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? AccountID { get; set; }

  [SubAccount(typeof (AddTrxFilter.accountID), DisplayName = "Offset Subaccount", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault(typeof (Coalesce<Search<CashAccountETDetail.offsetSubID, Where<CashAccountETDetail.entryTypeID, Equal<Current<AddTrxFilter.entryTypeID>>, And<CashAccountETDetail.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>>, Search<CAEntryType.subID, Where<CAEntryType.entryTypeId, Equal<Current<AddTrxFilter.entryTypeID>>>>>))]
  public virtual int? SubID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<CAEntryType.referenceID, Where<CAEntryType.entryTypeId, Equal<Current<AddTrxFilter.entryTypeID>>>>))]
  [PXVendorCustomerSelector(typeof (AddTrxFilter.origModule))]
  [PXUIField]
  public virtual int? ReferenceID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Required = true)]
  [PXDBCurrency(typeof (AddTrxFilter.curyInfoID), typeof (AddTrxFilter.tranAmt))]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranAmt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Date Cleared")]
  public virtual DateTime? ClearDate { get; set; }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "CA", CuryIDField = "CuryID", SuppressDefaultBaseCury = true)]
  public virtual long? CuryInfoID { get; set; }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<AddTrxFilter.referenceID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField(DisplayName = "Location ID")]
  [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<AddTrxFilter.referenceID>>>>))]
  public virtual int? LocationID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method", Required = true)]
  [PXDefault(typeof (Coalesce<Coalesce<Search2<PX.Objects.AR.Customer.defPaymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.Customer.defPaymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>>>, Where<Current<AddTrxFilter.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.Customer.bAccountID, Equal<Current<AddTrxFilter.referenceID>>>>>, Search<PaymentMethodAccount.paymentMethodID, Where<Current<AddTrxFilter.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>>, OrderBy<Desc<PaymentMethodAccount.aRIsDefault>>>>, Coalesce<Search2<PX.Objects.CR.Location.paymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CR.Location.paymentMethodID>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>>>, Where<Current<AddTrxFilter.origModule>, Equal<BatchModule.moduleAP>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<AddTrxFilter.referenceID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<AddTrxFilter.locationID>>>>>>, Search<PaymentMethodAccount.paymentMethodID, Where<Current<AddTrxFilter.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>>, OrderBy<Desc<PaymentMethodAccount.aPIsDefault>>>>>))]
  [PXSelector(typeof (Search2<PaymentMethod.paymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<Where2<Where<PaymentMethodAccount.useForAR, Equal<True>, And<Current<AddTrxFilter.origModule>, Equal<BatchModule.moduleAR>>>, Or<Where<PaymentMethodAccount.useForAP, Equal<True>, And<Current<AddTrxFilter.origModule>, Equal<BatchModule.moduleAP>>>>>>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>))]
  public virtual string PaymentMethodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.Customer.defPMInstanceID, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<Current<AddTrxFilter.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.Customer.bAccountID, Equal<Current<AddTrxFilter.referenceID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<AddTrxFilter.paymentMethodID>>>>>>>, Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<Current<AddTrxFilter.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<AddTrxFilter.referenceID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<AddTrxFilter.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>>, OrderBy<Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>))]
  [PXSelector(typeof (Search2<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<AddTrxFilter.referenceID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<AddTrxFilter.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<boolTrue>, And<PaymentMethodAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  public virtual int? PMInstanceID { get; set; }

  [PXString(11, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false, Visible = false)]
  [BatchStatus.List]
  public virtual string Status
  {
    get
    {
      if (this.Hold.GetValueOrDefault())
        return "H";
      return this.ExcludeFromApproval.GetValueOrDefault() ? "B" : "D";
    }
    set
    {
    }
  }

  [PXDBBool]
  [PXDefault(typeof (Search<CASetup.holdEntry>))]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? Hold { get; set; }

  [PXDBBool]
  [PXDefault]
  public virtual bool? ExcludeFromApproval { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see> associated with the transaction
  /// or the <see cref="P:PX.Objects.PM.PMSetup.NonProjectCode">non-project code</see>, which indicates that the transaction is not related to any particular project.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [ProjectDefault("CA", AccountType = typeof (AddTrxFilter.accountID))]
  [CAActiveProject]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// Identifier of the particular <see cref="T:PX.Objects.PM.PMTask">task</see> associated with the transaction. The task belongs to the <see cref="P:PX.Objects.CA.AddTrxFilter.ProjectID">selected project</see>
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMTask.TaskID">PMTask.TaskID</see> field.
  /// </value>
  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<AddTrxFilter.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (AddTrxFilter.projectID), "CA", DisplayName = "Project Task")]
  public virtual int? TaskID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">Cost Code</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
  /// </value>
  [CostCode(null, typeof (AddTrxFilter.taskID), null)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <summary>
  /// The virtual flag indicating that the smart panel was opened.
  /// The field is set and controlled by the graph.
  /// </summary>
  [PXBool]
  public virtual bool? DocumentCreationContext { get; set; }

  public static void InitializeProperties(PXFilter<AddTrxFilter> addFilter)
  {
    if (((PXSelectBase<AddTrxFilter>) addFilter).Current.ProjectID.HasValue && ((PXSelectBase<AddTrxFilter>) addFilter).Current.EntryTypeID == null)
      ((PXSelectBase) addFilter).Cache.SetValueExt<AddTrxFilter.projectID>((object) ((PXSelectBase<AddTrxFilter>) addFilter).Current, (object) null);
    ((PXSelectBase) addFilter).Cache.SetValueExt<AddTrxFilter.excludeFromApproval>((object) ((PXSelectBase<AddTrxFilter>) addFilter).Current, (object) !AddTrxFilterApprovalsHelper.IsApprovalRequired(((PXSelectBase) addFilter).Cache.Graph));
  }

  public static CATran VerifyAndCreateTransaction(
    PXGraph graph,
    PXFilter<AddTrxFilter> addFilter,
    PXSelectBase<PX.Objects.CM.CurrencyInfo> currencyinfo,
    bool releaseTransaction)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AddTrxFilter.\u003C\u003Ec__DisplayClass155_0 displayClass1550 = new AddTrxFilter.\u003C\u003Ec__DisplayClass155_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1550.releaseTransaction = releaseTransaction;
    // ISSUE: reference to a compiler-generated field
    displayClass1550.parameters = ((PXSelectBase<AddTrxFilter>) addFilter).Current;
    // ISSUE: reference to a compiler-generated field
    AddTrxFilter.VerifyCommonFields(addFilter, displayClass1550.parameters);
    // ISSUE: reference to a compiler-generated field
    displayClass1550.defcurrencyinfo = currencyinfo.Current;
    // ISSUE: reference to a compiler-generated field
    if (displayClass1550.parameters.OrigModule == "AP")
    {
      // ISSUE: reference to a compiler-generated field
      AddTrxFilter.VerifyARAPFields(addFilter, displayClass1550.parameters);
      // ISSUE: reference to a compiler-generated field
      AddTrxFilter.VerifyAPFields(graph, addFilter, displayClass1550.parameters);
      // ISSUE: method pointer
      PXLongOperation.StartOperation(graph, new PXToggleAsyncDelegate((object) displayClass1550, __methodptr(\u003CVerifyAndCreateTransaction\u003Eb__0)));
      return PXLongOperation.GetCustomInfo() as CATran;
    }
    // ISSUE: reference to a compiler-generated field
    if (displayClass1550.parameters.OrigModule == "AR")
    {
      // ISSUE: reference to a compiler-generated field
      AddTrxFilter.VerifyARAPFields(addFilter, displayClass1550.parameters);
      // ISSUE: reference to a compiler-generated field
      AddTrxFilter.VerifyARFields(graph, addFilter, displayClass1550.parameters);
      // ISSUE: method pointer
      PXLongOperation.StartOperation(graph, new PXToggleAsyncDelegate((object) displayClass1550, __methodptr(\u003CVerifyAndCreateTransaction\u003Eb__1)));
      return PXLongOperation.GetCustomInfo() as CATran;
    }
    // ISSUE: reference to a compiler-generated field
    if (!(displayClass1550.parameters.OrigModule == "CA"))
      throw new PXException("Unknown module!");
    // ISSUE: reference to a compiler-generated field
    AddTrxFilter.VerifyCAFields(graph, addFilter, displayClass1550.parameters);
    // ISSUE: method pointer
    PXLongOperation.StartOperation(graph, new PXToggleAsyncDelegate((object) displayClass1550, __methodptr(\u003CVerifyAndCreateTransaction\u003Eb__2)));
    return PXLongOperation.GetCustomInfo() as CATran;
  }

  private static CATran CreateAPTransaction(
    AddTrxFilter parameters,
    PX.Objects.CM.CurrencyInfo defcurrencyinfo,
    bool releaseTransaction)
  {
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    ((PXSelectBase) instance.Document).View.Answer = (WebDialogResult) 7;
    PX.Objects.AP.APPayment apPayment = new PX.Objects.AP.APPayment();
    if (parameters.DrCr == "C")
      apPayment.DocType = "PPM";
    else
      apPayment.DocType = "REF";
    PX.Objects.AP.APPayment copy1 = PXCache<PX.Objects.AP.APPayment>.CreateCopy(((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Insert(apPayment));
    copy1.VendorID = parameters.ReferenceID;
    copy1.VendorLocationID = parameters.LocationID;
    copy1.CashAccountID = parameters.CashAccountID;
    copy1.PaymentMethodID = parameters.PaymentMethodID;
    copy1.CuryID = parameters.CuryID;
    copy1.CuryOrigDocAmt = parameters.CuryTranAmt;
    copy1.DocDesc = parameters.Descr;
    copy1.Cleared = parameters.Cleared;
    copy1.AdjDate = parameters.TranDate;
    copy1.FinPeriodID = parameters.FinPeriodID;
    copy1.AdjFinPeriodID = parameters.FinPeriodID;
    copy1.ExtRefNbr = parameters.ExtRefNbr;
    copy1.Hold = parameters.Hold;
    copy1.Status = parameters.Status;
    copy1.Cleared = parameters.Cleared;
    PXCache<PX.Objects.AP.APPayment>.CreateCopy(((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Update(copy1));
    foreach (PXResult<PX.Objects.CM.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<PX.Objects.AP.APPayment.curyInfoID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()))
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResult<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo copy2 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(defcurrencyinfo);
      copy2.CuryInfoID = currencyInfo.CuryInfoID;
      ((PXSelectBase) instance.currencyinfo).Cache.Update((object) copy2);
    }
    ((PXAction) instance.Save).Press();
    if (releaseTransaction)
      APDocumentRelease.ReleaseDoc(new List<PX.Objects.AP.APRegister>()
      {
        (PX.Objects.AP.APRegister) ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current
      }, false);
    return PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Current<PX.Objects.AP.APPayment.cATranID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
  }

  private static CATran CreateARTransaction(
    AddTrxFilter parameters,
    PX.Objects.CM.CurrencyInfo defcurrencyinfo,
    bool releaseTransaction)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    PX.Objects.AR.ARPayment arPayment = new PX.Objects.AR.ARPayment();
    if (parameters.DrCr == "C")
      arPayment.DocType = "REF";
    else
      arPayment.DocType = "PMT";
    PX.Objects.AR.ARPayment copy1 = PXCache<PX.Objects.AR.ARPayment>.CreateCopy(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Insert(arPayment));
    copy1.CustomerID = parameters.ReferenceID;
    copy1.CustomerLocationID = parameters.LocationID;
    copy1.PaymentMethodID = parameters.PaymentMethodID;
    copy1.PMInstanceID = parameters.PMInstanceID;
    copy1.CashAccountID = parameters.CashAccountID;
    copy1.CuryOrigDocAmt = parameters.CuryTranAmt;
    copy1.DocDesc = parameters.Descr;
    copy1.Cleared = parameters.Cleared;
    copy1.AdjDate = parameters.TranDate;
    copy1.AdjFinPeriodID = parameters.FinPeriodID;
    copy1.FinPeriodID = parameters.FinPeriodID;
    copy1.ExtRefNbr = parameters.ExtRefNbr;
    copy1.Hold = parameters.Hold;
    copy1.Status = parameters.Status;
    copy1.Cleared = parameters.Cleared;
    PXCache<PX.Objects.AR.ARPayment>.CreateCopy(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(copy1));
    foreach (PXResult<PX.Objects.CM.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<PX.Objects.AR.ARPayment.curyInfoID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()))
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResult<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo copy2 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(defcurrencyinfo);
      copy2.CuryInfoID = currencyInfo.CuryInfoID;
      ((PXSelectBase) instance.currencyinfo).Cache.Update((object) copy2);
    }
    ((PXAction) instance.Save).Press();
    if (releaseTransaction)
      ARDocumentRelease.ReleaseDoc(new List<ARRegister>()
      {
        (ARRegister) ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current
      }, false);
    return PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Current<PX.Objects.AR.ARPayment.cATranID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
  }

  private static CATran CreateCATransaction(
    AddTrxFilter parameters,
    PX.Objects.CM.CurrencyInfo defcurrencyinfo,
    bool releaseTransaction)
  {
    CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
    CATranEntryMultiCurrency extension = ((PXGraph) instance).GetExtension<CATranEntryMultiCurrency>();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) extension.currencyinfo).Search<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>((object) defcurrencyinfo.CuryInfoID, Array.Empty<object>()));
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2;
    if (currencyInfo1 != null)
    {
      PX.Objects.CM.Extensions.CurrencyInfo copy = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) extension.currencyinfo).Cache.CreateCopy((object) currencyInfo1);
      currencyInfo2 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) extension.currencyinfo).Insert(copy);
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo copy = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) extension.currencyinfo).Cache.CreateCopy((object) PX.Objects.CM.Extensions.CurrencyInfo.GetEX(defcurrencyinfo));
      currencyInfo2 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) extension.currencyinfo).Insert(copy);
    }
    CAAdj caAdj1 = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Insert(new CAAdj()
    {
      AdjTranType = "CAE",
      CashAccountID = parameters.CashAccountID,
      Released = new bool?(false),
      CuryID = parameters.CuryID,
      CuryInfoID = (long?) currencyInfo2?.CuryInfoID,
      DrCr = parameters.DrCr,
      TranDate = parameters.TranDate,
      EntryTypeID = parameters.EntryTypeID,
      FinPeriodID = parameters.FinPeriodID
    });
    caAdj1.ExtRefNbr = parameters.ExtRefNbr;
    caAdj1.Cleared = parameters.Cleared;
    caAdj1.TranDesc = parameters.Descr;
    caAdj1.CuryControlAmt = parameters.CuryTranAmt;
    caAdj1.CuryTranAmt = parameters.CuryTranAmt;
    caAdj1.Hold = new bool?(true);
    caAdj1.TaxZoneID = parameters.TaxZoneID;
    caAdj1.TaxCalcMode = "T";
    CAAdj caAdj2 = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Update(caAdj1);
    CASplit caSplit = ((PXSelectBase<CASplit>) instance.CASplitRecords).Insert(new CASplit()
    {
      AdjTranType = caAdj2.AdjTranType,
      AccountID = parameters.AccountID,
      CuryInfoID = (long?) currencyInfo2?.CuryInfoID,
      Qty = new Decimal?(1M),
      CuryUnitPrice = parameters.CuryTranAmt,
      CuryTranAmt = parameters.CuryTranAmt,
      SubID = parameters.SubID
    });
    caSplit.TranDesc = parameters.Descr;
    caSplit.ProjectID = parameters.ProjectID;
    caSplit.TaskID = parameters.TaskID;
    caSplit.CostCodeID = parameters.CostCodeID;
    ((PXSelectBase<CASplit>) instance.CASplitRecords).Update(caSplit);
    caAdj2.CuryTaxAmt = caAdj2.CuryTaxTotal;
    caAdj2.Hold = parameters.Hold;
    ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Update(caAdj2);
    ((PXAction) instance.Save).Press();
    CAAdj current = (CAAdj) ((PXGraph) instance).Caches[typeof (CAAdj)].Current;
    if (releaseTransaction && current.Approved.GetValueOrDefault())
      AddTrxFilter.ReleaseCATransaction(current);
    return PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CAAdj.tranID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) current.TranID
    }));
  }

  private static void ReleaseCATransaction(CAAdj adj)
  {
    CATrxRelease.GroupRelease(new List<CARegister>()
    {
      CATrxRelease.CARegister(adj)
    }, false);
  }

  private static void VerifyCAFields(
    PXGraph graph,
    PXFilter<AddTrxFilter> AddFilter,
    AddTrxFilter parameters)
  {
    if (string.IsNullOrEmpty(parameters.ExtRefNbr) && PXResultset<CASetup>.op_Implicit(PXSelectBase<CASetup, PXSelect<CASetup>.Config>.Select(graph, Array.Empty<object>())).RequireExtRefNbr.GetValueOrDefault())
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.extRefNbr));
    else
      AddTrxFilter.ThrowIfUIException<AddTrxFilter.extRefNbr>(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters);
    int? nullable = parameters.AccountID;
    if (!nullable.HasValue)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.accountID));
    else
      AddTrxFilter.ThrowIfUIException<AddTrxFilter.accountID>(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters);
    nullable = parameters.SubID;
    if (!nullable.HasValue)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.subID));
    else
      AddTrxFilter.ThrowIfUIException<AddTrxFilter.subID>(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters);
    if (!ProjectAttribute.IsPMVisible("CA"))
      return;
    nullable = parameters.ProjectID;
    if (!nullable.HasValue)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.projectID));
    else
      AddTrxFilter.ThrowIfUIException<AddTrxFilter.projectID>(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters);
    if (!ProjectDefaultAttribute.IsNonProject(parameters.ProjectID))
    {
      nullable = parameters.TaskID;
      if (!nullable.HasValue)
      {
        AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.taskID));
        goto label_17;
      }
    }
    AddTrxFilter.ThrowIfUIException<AddTrxFilter.taskID>(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters);
label_17:
    if (!ProjectDefaultAttribute.IsNonProject(parameters.ProjectID) && CostCodeAttribute.UseCostCode())
    {
      nullable = parameters.CostCodeID;
      if (!nullable.HasValue)
      {
        AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.costCodeID));
        return;
      }
    }
    AddTrxFilter.ThrowIfUIException<AddTrxFilter.costCodeID>(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters);
  }

  private static FinPeriod GetFinPeriod(PXGraph graph, AddTrxFilter parameters)
  {
    return graph.GetService<IFinPeriodRepository>().GetByID(parameters.FinPeriodID, PXAccess.GetParentOrganizationID(parameters.BranchID));
  }

  private static void VerifyARAPFields(PXFilter<AddTrxFilter> AddFilter, AddTrxFilter parameters)
  {
    if (!parameters.ReferenceID.HasValue)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.referenceID));
    if (!parameters.LocationID.HasValue)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.locationID));
    if (string.IsNullOrEmpty(parameters.PaymentMethodID))
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.paymentMethodID));
    if (parameters.CuryTranAmt.HasValue)
    {
      Decimal? curyTranAmt = parameters.CuryTranAmt;
      Decimal num = 0M;
      if (!(curyTranAmt.GetValueOrDefault() == num & curyTranAmt.HasValue))
        return;
    }
    AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.curyTranAmt));
  }

  private static void VerifyAPFields(
    PXGraph graph,
    PXFilter<AddTrxFilter> AddFilter,
    AddTrxFilter parameters)
  {
    APSetup apSetup = PXResultset<APSetup>.op_Implicit(PXSelectBase<APSetup, PXSelect<APSetup>.Config>.Select(graph, Array.Empty<object>()));
    PaymentMethod paymentMethod = PXResult<PaymentMethodAccount, PaymentMethod>.op_Implicit(AddTrxFilter.GetCurrentPaymentMethodInfo(graph));
    if (apSetup.RequireVendorRef.GetValueOrDefault() && string.IsNullOrEmpty(parameters.ExtRefNbr) && (paymentMethod != null ? (!paymentMethod.PrintOrExport.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.extRefNbr));
    if (PXResultset<PX.Objects.AP.VendorR>.op_Implicit(PXSelectBase<PX.Objects.AP.VendorR, PXSelect<PX.Objects.AP.VendorR, Where<PX.Objects.AP.VendorR.bAccountID, Equal<Required<PX.Objects.AP.VendorR.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) parameters.ReferenceID
    })) != null)
      return;
    AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.referenceID));
  }

  private static PXResult<PaymentMethodAccount, PaymentMethod> GetCurrentPaymentMethodInfo(
    PXGraph graph)
  {
    return (PXResult<PaymentMethodAccount, PaymentMethod>) PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelectJoin<PaymentMethodAccount, InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Current<AddTrxFilter.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<AddTrxFilter.paymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>.Config>.Select(graph, Array.Empty<object>()));
  }

  private static void VerifyARFields(
    PXGraph graph,
    PXFilter<AddTrxFilter> AddFilter,
    AddTrxFilter parameters)
  {
    if (PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select(graph, Array.Empty<object>())).RequireExtRef.GetValueOrDefault() && string.IsNullOrEmpty(parameters.ExtRefNbr))
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.extRefNbr));
    if (!parameters.PMInstanceID.HasValue)
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(parameters.PaymentMethodID))
        flag = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select(graph, new object[1]
        {
          (object) parameters.PaymentMethodID
        })).IsAccountNumberRequired.GetValueOrDefault();
      if (flag)
        AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.pMInstanceID));
    }
    if (PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) parameters.ReferenceID
    })) != null)
      return;
    AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.referenceID));
  }

  private static void VerifyCommonFields(PXFilter<AddTrxFilter> AddFilter, AddTrxFilter parameters)
  {
    if (!parameters.CashAccountID.HasValue)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.cashAccountID));
    if (string.IsNullOrEmpty(parameters.EntryTypeID))
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.entryTypeID));
    if (!parameters.TranDate.HasValue)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.tranDate));
    if (parameters.FinPeriodID == null)
      AddTrxFilter.ThrowFieldIsEmpty(((PXSelectBase) AddFilter).Cache, (IBqlTable) parameters, typeof (AddTrxFilter.finPeriodID));
    CAAPAROpenPeriodAttribute.VerifyPeriod<AddTrxFilter.finPeriodID>(((PXSelectBase) AddFilter).Cache, (object) parameters);
  }

  private static void ThrowFieldIsEmpty(PXCache cache, IBqlTable row, System.Type fieldType)
  {
    string name = fieldType.Name;
    string str = PXUIFieldAttribute.GetDisplayName(cache, name);
    if (string.IsNullOrEmpty(str))
      str = name;
    cache.RaiseExceptionHandling(name, (object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) str
    }));
    throw new PXRowPersistingException(name, (object) null, "'{0}' cannot be empty.", new object[1]
    {
      (object) str
    });
  }

  private static void ThrowIfUIException<Field>(PXCache cache, IBqlTable row) where Field : IBqlField
  {
    string errorOnly = PXUIFieldAttribute.GetErrorOnly<Field>(cache, (object) row);
    if (!string.IsNullOrWhiteSpace(errorOnly))
    {
      string name = typeof (Field).Name;
      string str = PXUIFieldAttribute.GetDisplayName(cache, name);
      if (string.IsNullOrEmpty(str))
        str = name;
      throw new PXRowPersistingException(name, (object) null, errorOnly, new object[1]
      {
        (object) str
      });
    }
  }

  public static void Clear(PXGraph graph, PXFilter<AddTrxFilter> AddFilter)
  {
    AddTrxFilter current = ((PXSelectBase<AddTrxFilter>) AddFilter).Current;
    current.AccountID = new int?();
    current.SubID = new int?();
    current.CuryTranAmt = new Decimal?(0M);
    current.Descr = (string) null;
    current.EntryTypeID = (string) null;
    current.ExtRefNbr = (string) null;
    current.OrigModule = (string) null;
    current.ReferenceID = new int?();
    current.LocationID = new int?();
    current.Cleared = new bool?();
    current.Hold = new bool?();
    current.PaymentMethodID = (string) null;
    current.PMInstanceID = new int?();
    current.PaymentMethodID = (string) null;
    current.ProjectID = new int?();
    ((PXSelectBase) AddFilter).Cache.SetDefaultExt<AddTrxFilter.tranDate>((object) current);
    ((PXSelectBase) AddFilter).Cache.SetDefaultExt<AddTrxFilter.hold>((object) current);
  }

  public abstract class onlyExpense : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AddTrxFilter.onlyExpense>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AddTrxFilter.tranDate>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.cashAccountID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.curyID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.branchID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.extRefNbr>
  {
  }

  public abstract class entryTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.entryTypeID>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.origModule>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.finPeriodID>
  {
  }

  public abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.drCr>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.taxZoneID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.descr>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.subID>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.referenceID>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AddTrxFilter.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AddTrxFilter.tranAmt>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AddTrxFilter.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  AddTrxFilter.clearDate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AddTrxFilter.curyInfoID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.locationID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddTrxFilter.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.pMInstanceID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddTrxFilter.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AddTrxFilter.hold>
  {
  }

  public abstract class excludeFromApproval : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AddTrxFilter.excludeFromApproval>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddTrxFilter.costCodeID>
  {
  }

  public abstract class documentCreationContext : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AddTrxFilter.documentCreationContext>
  {
  }

  public class AddFilterAttribute : PXEventSubscriberAttribute, IPXRowSelectedSubscriber
  {
    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
      AddTrxFilter.AddFilterAttribute addFilterAttribute1 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) addFilterAttribute1, __vmethodptr(addFilterAttribute1, AddTrxFilter_EntryTypeId_FieldUpdated));
      fieldUpdated1.AddHandler<AddTrxFilter.entryTypeID>(pxFieldUpdated1);
      PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
      AddTrxFilter.AddFilterAttribute addFilterAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) addFilterAttribute2, __vmethodptr(addFilterAttribute2, AddTrxFilter_DrCr_FieldUpdated));
      fieldUpdated2.AddHandler<AddTrxFilter.drCr>(pxFieldUpdated2);
      PXGraph.FieldUpdatedEvents fieldUpdated3 = sender.Graph.FieldUpdated;
      AddTrxFilter.AddFilterAttribute addFilterAttribute3 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) addFilterAttribute3, __vmethodptr(addFilterAttribute3, AddTrxFilter_ReferenceID_FieldUpdated));
      fieldUpdated3.AddHandler<AddTrxFilter.referenceID>(pxFieldUpdated3);
      PXGraph.FieldUpdatedEvents fieldUpdated4 = sender.Graph.FieldUpdated;
      AddTrxFilter.AddFilterAttribute addFilterAttribute4 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated4 = new PXFieldUpdated((object) addFilterAttribute4, __vmethodptr(addFilterAttribute4, AddTrxFilter_TranDate_FieldUpdated));
      fieldUpdated4.AddHandler<AddTrxFilter.tranDate>(pxFieldUpdated4);
      PXGraph.FieldUpdatedEvents fieldUpdated5 = sender.Graph.FieldUpdated;
      AddTrxFilter.AddFilterAttribute addFilterAttribute5 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated5 = new PXFieldUpdated((object) addFilterAttribute5, __vmethodptr(addFilterAttribute5, AddTrxFilter_CashAccountID_FieldUpdated));
      fieldUpdated5.AddHandler<AddTrxFilter.cashAccountID>(pxFieldUpdated5);
      PXGraph.FieldUpdatedEvents fieldUpdated6 = sender.Graph.FieldUpdated;
      AddTrxFilter.AddFilterAttribute addFilterAttribute6 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated6 = new PXFieldUpdated((object) addFilterAttribute6, __vmethodptr(addFilterAttribute6, AddTrxFilter_AccountID_FieldUpdated));
      fieldUpdated6.AddHandler<AddTrxFilter.accountID>(pxFieldUpdated6);
      PXGraph.FieldUpdatedEvents fieldUpdated7 = sender.Graph.FieldUpdated;
      AddTrxFilter.AddFilterAttribute addFilterAttribute7 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated7 = new PXFieldUpdated((object) addFilterAttribute7, __vmethodptr(addFilterAttribute7, AddTrxFilter_PaymentMethodID_FieldUpdated));
      fieldUpdated7.AddHandler<AddTrxFilter.paymentMethodID>(pxFieldUpdated7);
      PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
      AddTrxFilter.AddFilterAttribute addFilterAttribute8 = this;
      // ISSUE: virtual method pointer
      PXRowUpdated pxRowUpdated = new PXRowUpdated((object) addFilterAttribute8, __vmethodptr(addFilterAttribute8, AddTrxFilter_RowUpdated));
      rowUpdated.AddHandler<AddTrxFilter>(pxRowUpdated);
      PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
      AddTrxFilter.AddFilterAttribute addFilterAttribute9 = this;
      // ISSUE: virtual method pointer
      PXRowSelected pxRowSelected = new PXRowSelected((object) addFilterAttribute9, __vmethodptr(addFilterAttribute9, AddTrxFilter_RowSelected));
      rowSelected.AddHandler<AddTrxFilter>(pxRowSelected);
    }

    public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      AddTrxFilter row = (AddTrxFilter) e.Row;
      if (row == null)
        return;
      PXUIFieldAttribute.SetEnabled<AddTrxFilter.curyID>(sender, (object) row, false);
      bool flag1 = row.OrigModule == null || row.OrigModule == "CA";
      bool flag2 = row.OrigModule == null || row.OrigModule == "AR";
      bool flag3 = row.OrigModule == null || row.OrigModule == "AP";
      PXUIFieldAttribute.SetVisible<AddTrxFilter.accountID>(sender, (object) row, flag1);
      PXUIFieldAttribute.SetVisible<AddTrxFilter.subID>(sender, (object) row, flag1);
      PXUIFieldAttribute.SetVisible<AddTrxFilter.referenceID>(sender, (object) row, row.OrigModule == "AP" || row.OrigModule == "AR");
      PXUIFieldAttribute.SetVisible<AddTrxFilter.locationID>(sender, (object) row, row.OrigModule == "AP" || row.OrigModule == "AR");
      PXUIFieldAttribute.SetVisible<AddTrxFilter.paymentMethodID>(sender, (object) row, row.OrigModule == "AP" || row.OrigModule == "AR");
      PXUIFieldAttribute.SetVisible<AddTrxFilter.pMInstanceID>(sender, (object) row, row.OrigModule == "AR");
      PXUIFieldAttribute.SetVisible<AddTrxFilter.projectID>(sender, (object) row, (row.OrigModule == null || row.OrigModule == "CA") && ProjectAttribute.IsPMVisible("CA"));
      PXUIFieldAttribute.SetVisible<AddTrxFilter.extRefNbr>(sender, (object) row, true);
      PXUIFieldAttribute.SetVisible<AddTrxFilter.drCr>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<AddTrxFilter.cashAccountID>(sender, (object) row, false);
      PXUIFieldAttribute.SetVisible<AddTrxFilter.origModule>(sender, (object) row, false);
      PXUIFieldAttribute.SetVisible<AddTrxFilter.hold>(sender, (object) row, flag1 | flag3 | flag2);
      PXUIFieldAttribute.SetVisible<AddTrxFilter.status>(sender, (object) row, flag1 | flag3 | flag2);
      PXUIFieldAttribute.SetVisible<AddTrxFilter.taxZoneID>(sender, (object) row, flag1);
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) row.CashAccountID
      }));
      bool? nullable;
      int num1;
      if (cashAccount != null)
      {
        nullable = cashAccount.Reconcile;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      bool flag4 = num1 != 0;
      PXUIFieldAttribute.SetEnabled<AddTrxFilter.cleared>(sender, (object) row, flag4);
      PXCache pxCache1 = sender;
      AddTrxFilter addTrxFilter1 = row;
      int num2;
      if (flag4)
      {
        nullable = row.Cleared;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 0;
      PXUIFieldAttribute.SetEnabled<AddTrxFilter.clearDate>(pxCache1, (object) addTrxFilter1, num2 != 0);
      if (row.OrigModule == "AP")
      {
        PaymentMethod paymentMethod = PXResult<PaymentMethodAccount, PaymentMethod>.op_Implicit(AddTrxFilter.GetCurrentPaymentMethodInfo(sender.Graph));
        PXCache pxCache2 = sender;
        AddTrxFilter addTrxFilter2 = row;
        int num3;
        if (paymentMethod == null)
        {
          num3 = 1;
        }
        else
        {
          nullable = paymentMethod.PrintOrExport;
          num3 = !nullable.GetValueOrDefault() ? 1 : 0;
        }
        PXUIFieldAttribute.SetEnabled<AddTrxFilter.extRefNbr>(pxCache2, (object) addTrxFilter2, num3 != 0);
      }
      if (row.OrigModule == "AR")
      {
        bool flag5 = false;
        if (!string.IsNullOrEmpty(row.PaymentMethodID))
        {
          PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select(sender.Graph, new object[1]
          {
            (object) row.PaymentMethodID
          }));
          int num4;
          if (paymentMethod != null)
          {
            nullable = paymentMethod.IsAccountNumberRequired;
            num4 = nullable.GetValueOrDefault() ? 1 : 0;
          }
          else
            num4 = 0;
          flag5 = num4 != 0;
        }
        PXUIFieldAttribute.SetEnabled<AddTrxFilter.pMInstanceID>(sender, (object) row, flag5);
      }
      if (!(row.OrigModule == "CA"))
        return;
      bool flag6 = false;
      if (!string.IsNullOrEmpty(row.EntryTypeID))
      {
        if (row.AccountID.HasValue)
        {
          flag6 = PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>>>.Config>.Select(sender.Graph, new object[1]
          {
            (object) row.AccountID
          }).Count == 1;
        }
        else
        {
          CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelectReadonly<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select(sender.Graph, new object[1]
          {
            (object) row.EntryTypeID
          }));
          if (caEntryType != null)
          {
            nullable = caEntryType.UseToReclassifyPayments;
            if (nullable.GetValueOrDefault())
            {
              if (PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, NotEqual<Required<CashAccount.cashAccountID>>, And<CashAccount.curyID, Equal<Required<CashAccount.curyID>>>>>.Config>.Select(sender.Graph, new object[2]
              {
                (object) row.CashAccountID,
                (object) row.CuryID
              })) == null)
              {
                sender.RaiseExceptionHandling<AddTrxFilter.cashAccountID>((object) row, (object) null, (Exception) new PXSetPropertyKeepPreviousException("This Entry Type requires to set a Cash Account with currency {0} as an Offset Account. Currently, there is no such a Cash Account defined in the system", (PXErrorLevel) 2, new object[1]
                {
                  (object) row.CuryID
                }));
                sender.RaiseExceptionHandling<AddTrxFilter.entryTypeID>((object) row, (object) null, (Exception) new PXSetPropertyException("This Entry Type requires to set a Cash Account with currency {0} as an Offset Account. Currently, there is no such a Cash Account defined in the system", new object[2]
                {
                  (object) row.CuryID,
                  (object) (PXErrorLevel) 2
                }));
                goto label_29;
              }
              sender.RaiseExceptionHandling<AddTrxFilter.cashAccountID>((object) row, (object) null, (Exception) null);
              sender.RaiseExceptionHandling<AddTrxFilter.entryTypeID>((object) row, (object) null, (Exception) null);
              goto label_29;
            }
          }
          sender.RaiseExceptionHandling<AddTrxFilter.cashAccountID>((object) row, (object) null, (Exception) null);
          sender.RaiseExceptionHandling<AddTrxFilter.entryTypeID>((object) row, (object) null, (Exception) null);
        }
      }
label_29:
      PXUIFieldAttribute.SetEnabled<AddTrxFilter.subID>(sender, (object) row, !flag6);
    }

    public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      AddTrxFilter row = (AddTrxFilter) e.Row;
      sender.Current = (object) row;
      if (!(row.DrCr == "D"))
        return;
      sender.SetValueExt<AddTrxFilter.taxZoneID>((object) row, (object) null);
    }

    public virtual void AddTrxFilter_EntryTypeId_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      AddTrxFilter row = (AddTrxFilter) e.Row;
      sender.SetDefaultExt<AddTrxFilter.origModule>((object) row);
      sender.SetDefaultExt<AddTrxFilter.drCr>((object) row);
      sender.RaiseExceptionHandling<AddTrxFilter.accountID>((object) row, (object) null, (Exception) null);
      sender.RaiseExceptionHandling<AddTrxFilter.entryTypeID>((object) row, (object) null, (Exception) null);
      sender.SetDefaultExt<AddTrxFilter.descr>((object) row);
      sender.SetDefaultExt<AddTrxFilter.accountID>((object) row);
      sender.SetDefaultExt<AddTrxFilter.subID>((object) row);
      sender.SetDefaultExt<AddTrxFilter.taxZoneID>((object) row);
      sender.SetDefaultExt<AddTrxFilter.referenceID>((object) row);
      sender.SetValue<AddTrxFilter.locationID>((object) row, (object) null);
      sender.SetValue<AddTrxFilter.paymentMethodID>((object) row, (object) null);
      sender.SetValue<AddTrxFilter.pMInstanceID>((object) row, (object) null);
    }

    public virtual void AddTrxFilter_DrCr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      AddTrxFilter row = e.Row as AddTrxFilter;
      if (row.ExtRefNbr == null)
        return;
      row.ExtRefNbr = (string) null;
    }

    public virtual void AddTrxFilter_TranDate_FieldUpdating(
      PXCache sender,
      PXFieldUpdatingEventArgs e)
    {
      AddTrxFilter row = (AddTrxFilter) e.Row;
      if (row == null)
        return;
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) row.CashAccountID
      }));
      if (cashAccount == null || cashAccount.Reconcile.GetValueOrDefault())
        return;
      row.ClearDate = row.TranDate;
    }

    public virtual void AddTrxFilter_ReferenceID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      AddTrxFilter row = e.Row as AddTrxFilter;
      switch (row.OrigModule)
      {
        case "AP":
          sender.SetDefaultExt<AddTrxFilter.locationID>((object) row);
          if (row.ReferenceID.HasValue)
          {
            sender.SetDefaultExt<AddTrxFilter.paymentMethodID>((object) row);
            if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<AddTrxFilter.paymentMethodID>(sender, (object) row)))
            {
              sender.SetValue<AddTrxFilter.paymentMethodID>((object) row, (object) null);
              PXUIFieldAttribute.SetError<AddTrxFilter.paymentMethodID>(sender, (object) row, (string) null, (string) null);
            }
          }
          sender.SetValue<AddTrxFilter.pMInstanceID>((object) row, (object) null);
          break;
        case "AR":
          sender.SetDefaultExt<AddTrxFilter.locationID>((object) row);
          if (row.ReferenceID.HasValue)
          {
            sender.SetDefaultExt<AddTrxFilter.paymentMethodID>((object) row);
            if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<AddTrxFilter.paymentMethodID>(sender, (object) row)))
            {
              sender.SetValue<AddTrxFilter.paymentMethodID>((object) row, (object) null);
              PXUIFieldAttribute.SetError<AddTrxFilter.paymentMethodID>(sender, (object) row, (string) null, (string) null);
            }
          }
          sender.SetDefaultExt<AddTrxFilter.pMInstanceID>((object) row);
          if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<AddTrxFilter.pMInstanceID>(sender, (object) row)))
            break;
          sender.SetValue<AddTrxFilter.pMInstanceID>((object) row, (object) null);
          PXUIFieldAttribute.SetError<AddTrxFilter.pMInstanceID>(sender, (object) row, (string) null, (string) null);
          break;
        default:
          sender.SetValue<AddTrxFilter.locationID>((object) row, (object) null);
          sender.SetValue<AddTrxFilter.paymentMethodID>((object) row, (object) null);
          sender.SetValue<AddTrxFilter.pMInstanceID>((object) row, (object) null);
          break;
      }
    }

    public virtual void AddTrxFilter_PaymentMethodID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      AddTrxFilter row = e.Row as AddTrxFilter;
      if (row.OrigModule == "AR")
      {
        sender.SetDefaultExt<AddTrxFilter.pMInstanceID>((object) row);
        if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<AddTrxFilter.pMInstanceID>(sender, (object) row)))
          return;
        sender.SetValue<AddTrxFilter.pMInstanceID>((object) row, (object) null);
        PXUIFieldAttribute.SetError<AddTrxFilter.pMInstanceID>(sender, (object) row, (string) null, (string) null);
      }
      else
      {
        if (!(row.OrigModule == "AP"))
          return;
        PXResult<PaymentMethodAccount, PaymentMethod> paymentMethodInfo = AddTrxFilter.GetCurrentPaymentMethodInfo(sender.Graph);
        PaymentMethod paymentMethod = PXResult<PaymentMethodAccount, PaymentMethod>.op_Implicit(paymentMethodInfo);
        PaymentMethodAccount paymentMethodAccount = PXResult<PaymentMethodAccount, PaymentMethod>.op_Implicit(paymentMethodInfo);
        bool? nullable;
        int num;
        if (paymentMethod == null)
        {
          num = 1;
        }
        else
        {
          nullable = paymentMethod.PrintOrExport;
          num = !nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0 && paymentMethodAccount != null)
        {
          nullable = paymentMethodAccount.APAutoNextNbr;
          if (nullable.GetValueOrDefault())
          {
            string nextPaymentRef = PX.Objects.AP.PaymentRefAttribute.GetNextPaymentRef(sender.Graph, paymentMethodAccount.CashAccountID, paymentMethod.PaymentMethodID);
            sender.SetValueExt<AddTrxFilter.extRefNbr>((object) row, (object) nextPaymentRef);
            return;
          }
        }
        sender.SetValueExt<AddTrxFilter.extRefNbr>((object) row, (object) null);
      }
    }

    public virtual void AddTrxFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      sender.SetDefaultExt<AddTrxFilter.curyID>(e.Row);
    }

    public virtual void AddTrxFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      AddTrxFilter row = (AddTrxFilter) e.Row;
      if (row.OrigModule == null)
        return;
      if (row.OrigModule == "CA")
      {
        CASetup caSetup = PXResultset<CASetup>.op_Implicit(PXSelectBase<CASetup, PXSelect<CASetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
        PXUIFieldAttribute.SetRequired<AddTrxFilter.extRefNbr>(sender, caSetup.RequireExtRefNbr.GetValueOrDefault());
      }
      else if (row.OrigModule == "AP")
      {
        APSetup apSetup = PXResultset<APSetup>.op_Implicit(PXSelectBase<APSetup, PXSelect<APSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
        PXUIFieldAttribute.SetRequired<AddTrxFilter.extRefNbr>(sender, apSetup.RequireVendorRef.GetValueOrDefault());
      }
      else
      {
        if (!(row.OrigModule == "AR"))
          return;
        ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select(sender.Graph, Array.Empty<object>()));
        PXUIFieldAttribute.SetRequired<AddTrxFilter.extRefNbr>(sender, arSetup.RequireExtRef.GetValueOrDefault());
      }
    }

    public virtual void AddTrxFilter_TranDate_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      object row = e.Row;
      PXCache cach = sender.Graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)];
      PX.Objects.CM.CurrencyInfoAttribute.SetEffectiveDate<AddTrxFilter.tranDate>(sender, e);
    }

    public virtual void AddTrxFilter_CashAccountID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      AddTrxFilter row = e.Row as AddTrxFilter;
      CashAccount cashAccount = (CashAccount) PXSelectorAttribute.Select<AddTrxFilter.cashAccountID>(sender, (object) row);
      sender.SetDefaultExt<AddTrxFilter.curyID>((object) row);
      PXView view = sender.Graph.Views[$"_{typeof (AddTrxFilter).Name}_CurrencyInfo_"];
      PX.Objects.CM.CurrencyInfo currencyInfo = view.SelectSingle(new object[1]
      {
        (object) row.CuryInfoID
      }) as PX.Objects.CM.CurrencyInfo;
      if (cashAccount.CuryRateTypeID != null)
        view.Cache.SetValueExt<PX.Objects.CM.CurrencyInfo.curyRateTypeID>((object) currencyInfo, (object) cashAccount.CuryRateTypeID);
      else
        view.Cache.SetDefaultExt<PX.Objects.CM.CurrencyInfo.curyRateTypeID>((object) currencyInfo);
      sender.SetDefaultExt<AddTrxFilter.branchID>((object) row);
      sender.SetDefaultExt<AddTrxFilter.entryTypeID>((object) row);
      if (cashAccount == null)
        return;
      row.Cleared = new bool?(false);
      row.ClearDate = new DateTime?();
      if (cashAccount.Reconcile.GetValueOrDefault())
        return;
      row.Cleared = new bool?(true);
      row.ClearDate = row.TranDate;
    }

    public virtual void AddTrxFilter_AccountID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      AddTrxFilter row = e.Row as AddTrxFilter;
      int? nullable1;
      int? nullable2;
      if (row.AccountID.HasValue)
      {
        nullable1 = row.AccountID;
        nullable2 = (int?) e.OldValue;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          PXGraph graph = sender.Graph;
          int? accountId = row.AccountID;
          nullable2 = new int?();
          int? subID = nullable2;
          int? branchId = row.BranchID;
          CashAccount cashAccount = CATranDetailHelper.GetCashAccount(graph, accountId, subID, branchId, true);
          if (cashAccount != null)
          {
            nullable2 = cashAccount.SubID;
            if (nullable2.HasValue)
              sender.SetValue<AddTrxFilter.subID>((object) row, (object) cashAccount.SubID);
          }
        }
      }
      nullable2 = row.ProjectID;
      if (nullable2.HasValue)
      {
        nullable2 = row.ProjectID;
        nullable1 = ProjectDefaultAttribute.NonProject();
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          return;
      }
      sender.SetDefaultExt<AddTrxFilter.projectID>(e.Row);
    }
  }
}
