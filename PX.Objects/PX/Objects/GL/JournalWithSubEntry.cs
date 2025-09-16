// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalWithSubEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Bql;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL.FinPeriods;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class JournalWithSubEntry : 
  PXGraph<
  #nullable disable
  JournalWithSubEntry, GLDocBatch>,
  PXImportAttribute.IPXPrepareItems
{
  public PXWorkflowEventHandler<GLDocBatch> OnUpdateStatus;
  public PXWorkflowEventHandler<GLDocBatch> OnReleaseVoucher;
  public ToggleCurrency<GLDocBatch> CurrencyView;
  public PXSelect<GLDocBatch, Where<GLDocBatch.module, Equal<Optional<GLDocBatch.module>>>> BatchModule;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<GLDocBatch.curyInfoID>>>> currencyinfo;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<GLDocBatch.curyInfoID>>>> currencyinfoEx;
  [PXImport(typeof (GLDocBatch))]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (GLTranDoc.parentLineNbr), typeof (GLTranDoc.extRefNbr)}, FieldsToShowInSimpleImport = new System.Type[] {typeof (GLTranDoc.extRefNbr)})]
  public PXSelect<GLTranDoc, Where<GLTranDoc.module, Equal<Current<GLDocBatch.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLDocBatch.batchNbr>>>>, OrderBy<Asc<GLTranDoc.groupTranID, Asc<GLTranDoc.lineNbr>>>> GLTranModuleBatNbr;
  public PXSelectReadonly<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<GLDocBatch.finPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<GLDocBatch.branchID>>>>> finperiod;
  public PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<PX.Objects.CR.Location.locationID>>>>> Location;
  public PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>> Customer;
  public PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>> Vendor;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<GLTran, InnerJoin<Batch, On<Batch.module, Equal<GLTran.module>, And<Batch.batchNbr, Equal<GLTran.batchNbr>>>>, Where<GLTran.module, Equal<Optional<GLDocBatch.module>>, And<GLTran.batchNbr, Equal<Optional<GLDocBatch.batchNbr>>>>> GLTransactions;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.GL.GLTranCode, Where<PX.Objects.GL.GLTranCode.tranCode, Equal<Required<GLTranDoc.tranCode>>>> GLTranCode;
  public PXSelectJoin<GLTax, InnerJoin<GLTranDoc, On<GLTranDoc.module, Equal<GLTax.module>, And<GLTranDoc.batchNbr, Equal<GLTax.batchNbr>, And<GLTranDoc.lineNbr, Equal<GLTax.lineNbr>>>>>, Where<GLTranDoc.module, Equal<Current<GLDocBatch.module>>, And<GLTranDoc.batchNbr, Equal<Current<GLDocBatch.batchNbr>>, And<GLTax.detailType, Equal<GLTaxDetailType.lineTax>>>>, OrderBy<Asc<GLTax.module, Asc<GLTax.batchNbr, Asc<GLTax.taxID>>>>> Tax_Rows;
  public PXSelectJoin<GLTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<GLTaxTran.taxID>>>, Where<GLTaxTran.module, Equal<Current<GLDocBatch.module>>, And<GLTaxTran.batchNbr, Equal<Current<GLDocBatch.batchNbr>>, And<GLTax.detailType, Equal<GLTaxDetailType.docTax>>>>> Taxes;
  public PXSelectJoin<GLTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<GLTaxTran.taxID>>>, Where<GLTaxTran.module, Equal<Current<GLTranDoc.module>>, And<GLTaxTran.batchNbr, Equal<Current<GLTranDoc.batchNbr>>, And<GLTaxTran.lineNbr, Equal<Current<GLTranDoc.lineNbr>>, And<GLTax.detailType, Equal<GLTaxDetailType.docTax>>>>>> CurrentDocTaxes;
  public PXSelect<JournalWithSubEntry.GLTranDocAP, Where<JournalWithSubEntry.GLTranDocAP.module, Equal<Current<GLDocBatch.module>>, And<JournalWithSubEntry.GLTranDocAP.batchNbr, Equal<Current<GLDocBatch.batchNbr>>, And<JournalWithSubEntry.GLTranDocAP.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAP>, And<Where<JournalWithSubEntry.GLTranDocAP.tranType, Equal<APDocType.check>, Or<JournalWithSubEntry.GLTranDocAP.tranType, Equal<APDocType.prepayment>, Or<JournalWithSubEntry.GLTranDocAP.tranType, Equal<APDocType.refund>>>>>>>>, OrderBy<Asc<JournalWithSubEntry.GLTranDocAP.lineNbr>>> APPayments;
  public PXSelect<JournalWithSubEntry.GLTranDocAR, Where<JournalWithSubEntry.GLTranDocAR.module, Equal<Current<GLDocBatch.module>>, And<JournalWithSubEntry.GLTranDocAR.batchNbr, Equal<Current<GLDocBatch.batchNbr>>, And<JournalWithSubEntry.GLTranDocAR.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAR>, And<Where<JournalWithSubEntry.GLTranDocAR.tranType, Equal<ARDocType.payment>, Or<JournalWithSubEntry.GLTranDocAR.tranType, Equal<ARDocType.prepayment>, Or<JournalWithSubEntry.GLTranDocAR.tranType, Equal<ARDocType.refund>>>>>>>>, OrderBy<Asc<JournalWithSubEntry.GLTranDocAR.lineNbr>>> ARPayments;
  [PXViewName("Adjust")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.AP.APAdjust, LeftJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<PX.Objects.AP.APAdjust.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<PX.Objects.AP.APAdjust.adjdRefNbr>>>>, Where<PX.Objects.AP.APAdjust.adjgDocType, Equal<Current<JournalWithSubEntry.GLTranDocAP.tranType>>, And<PX.Objects.AP.APAdjust.adjgRefNbr, Equal<Current<JournalWithSubEntry.GLTranDocAP.refNbr>>, And<PX.Objects.AP.APAdjust.adjNbr, Equal<int0>>>>> APAdjustments;
  [PXViewName("Applications")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.AR.ARAdjust, LeftJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<PX.Objects.AR.ARAdjust.adjdDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<PX.Objects.AR.ARAdjust.adjdRefNbr>>>>, Where<PX.Objects.AR.ARAdjust.adjgDocType, Equal<Current<JournalWithSubEntry.GLTranDocAR.tranType>>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, Equal<Current<JournalWithSubEntry.GLTranDocAR.refNbr>>, And<PX.Objects.AR.ARAdjust.adjNbr, Equal<int0>>>>> ARAdjustments;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>> apRegister;
  public PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>> arRegister;
  public PXSelect<CAAdj, Where<CAAdj.adjTranType, Equal<Required<CAAdj.adjTranType>>, And<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>>>> caAdj;
  public PXSelect<Batch, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>> glBatch;
  public PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>> cashAccount;
  public PXSelectJoin<PX.Objects.CA.CashAccount, LeftJoin<CashAccountETDetail, On<CashAccountETDetail.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<CashAccountETDetail.entryTypeID, Equal<Required<CAEntryType.entryTypeId>>>>>, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And2<Where<PX.Objects.CA.CashAccount.branchID, Equal<Required<PX.Objects.CA.CashAccount.branchID>>, Or<PX.Objects.CA.CashAccount.restrictVisibilityWithBranch, NotEqual<True>>>, And<Where<Required<CAEntryType.entryTypeId>, IsNull, Or<CashAccountETDetail.cashAccountID, IsNotNull>>>>>> cashAccountByAccountID;
  public PXSelectReadonly<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.vendorID, Equal<Required<PX.Objects.AP.APInvoice.vendorID>>, And<PX.Objects.AP.APInvoice.docType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>> APInvoice_VendorID_DocType_RefNbr;
  public PXSelect<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.vendorID, Equal<Required<PX.Objects.AP.APPayment.vendorID>>, And<PX.Objects.AP.APPayment.docType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>> APPayment_VendorID_DocType_RefNbr;
  public PXSelectReadonly<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.customerID, Equal<Required<PX.Objects.AR.ARInvoice.customerID>>, And<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>> ARInvoice_CustomerID_DocType_RefNbr;
  public PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.customerID, Equal<Required<PX.Objects.AR.ARPayment.customerID>>, And<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>> ARPayment_CustomerID_DocType_RefNbr;
  public PXFilter<JournalWithSubEntry.RefDocKey> deletedKeys;
  protected Ledger _Ledger;
  public PXSetup<GLSetup> glsetup;
  public APSetupNoMigrationMode apsetup;
  public ARSetupNoMigrationMode arsetup;
  public PXSetup<CASetup> casetup;
  public CMSetupSelect CMSetup;
  public PXInitializeState<GLDocBatch> initializeState;
  public PXAction<GLDocBatch> putOnHold;
  public PXAction<GLDocBatch> releaseFromHold;
  public PXAction<GLDocBatch> release;
  public PXAction<GLDocBatch> viewDocument;
  public PXAction<GLDocBatch> showTaxes;
  protected bool _ExceptionHandling;
  private const int UnlockRefNbrThresholdHours = 72;
  private System.Type prevTable;
  private List<PXDataField> prevParams;
  protected bool _skipExtensionTables;
  private bool _importing;
  private bool _skipDefaulting;
  private static bool _UseControlTotalEntry = true;
  private bool _isPMDefaulting;
  private bool _isMassDelete;
  private bool _isCacheSync;
  private GLTranDoc _parent;
  private GLTranDoc _previousTran;
  private PX.Objects.CA.CashAccount _cashAccountDebit;
  private PX.Objects.CA.CashAccount _cashAccountCredit;
  public bool TakeDiscAlways;
  private bool internalCall;
  private bool internalCallAR;
  public bool TakeDiscAlwaysAR;
  private bool _AutoPaymentApp;
  protected Dictionary<Pair<string, string>, int> _ImportedDocs;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Module", Visible = true)]
  [PX.Objects.GL.BatchModule.List]
  protected virtual void GLTran_Module_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<GLDocBatch.batchNbr, Where<GLDocBatch.module, Equal<Current<GLTran.module>>>, OrderBy<Desc<GLDocBatch.batchNbr>>>), Filterable = true)]
  [PXUIField(DisplayName = "Batch Number", Visible = true)]
  protected virtual void GLTran_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [APDocType.List]
  [PXUIField]
  protected virtual void APRegister_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Ref. Nbr.")]
  [JournalWithSubEntry.APDocNumbering]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.tranType, Equal<Current<PX.Objects.AP.APRegister.docType>>, And<GLTranDoc.refNbr, Equal<Current<PX.Objects.AP.APRegister.refNbr>>, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAP>, And<GLTranDoc.parentLineNbr, IsNull>>>>>))]
  protected virtual void APRegister_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("GL")]
  [PXUIField]
  [PX.Objects.GL.BatchModule.FullList]
  protected virtual void APRegister_OrigModule_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  protected virtual void APRegister_CuryID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(0)]
  [PXDBInt]
  protected virtual void APRegister_APAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(0)]
  [PXDBInt]
  protected virtual void APRegister_APSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXDBBool]
  protected virtual void APRegister_Hold_CacheAttached(PXCache sender)
  {
  }

  [PXDBTimestamp]
  protected virtual void APRegister_tstamp_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [ARDocType.List]
  [PXUIField]
  protected virtual void ARRegister_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Ref. Nbr.")]
  [JournalWithSubEntry.ARDocNumbering]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.tranType, Equal<Current<PX.Objects.AR.ARRegister.docType>>, And<GLTranDoc.refNbr, Equal<Current<PX.Objects.AR.ARRegister.refNbr>>, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAR>, And<GLTranDoc.parentLineNbr, IsNull>>>>>))]
  protected virtual void ARRegister_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("GL")]
  [PXUIField]
  [PX.Objects.GL.BatchModule.FullList]
  protected virtual void ARRegister_OrigModule_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  protected virtual void ARRegister_CuryID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(0)]
  [PXDBInt]
  protected virtual void ARRegister_ARAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(0)]
  [PXDBInt]
  protected virtual void ARRegister_ARSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXDBBool]
  protected virtual void ARRegister_Hold_CacheAttached(PXCache sender)
  {
  }

  [PXDBTimestamp]
  protected virtual void ARRegister_tstamp_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CAAdj.adjRefNbr>))]
  [AutoNumber(typeof (CASetup.registerNumberingID), typeof (CAAdj.tranDate))]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.refNbr, Equal<Current<CAAdj.adjRefNbr>>, And<GLTranDoc.tranType, Equal<Current<CAAdj.adjTranType>>, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleCA>, And<GLTranDoc.parentLineNbr, IsNull>>>>>))]
  protected virtual void CAAdj_AdjRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  protected virtual void CAAdj_TranID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXDBBool]
  protected virtual void CAAdj_Draft_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXDBBool]
  protected virtual void CAAdj_Hold_CacheAttached(PXCache sender)
  {
  }

  [PXDBTimestamp]
  protected virtual void CAAdj_tstamp_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<Current<Batch.module>>>, OrderBy<Desc<Batch.batchNbr>>>), Filterable = true)]
  [PX.Objects.GL.BatchModule.Numbering]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.refNbr, Equal<Current<Batch.batchNbr>>, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleGL>, And<GLTranDoc.parentLineNbr, IsNull>>>>))]
  protected virtual void Batch_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXDBBool]
  protected virtual void Batch_Draft_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true)]
  [PXDBBool]
  protected virtual void Batch_Hold_CacheAttached(PXCache sender)
  {
  }

  [PXDBTimestamp]
  protected virtual void Batch_tstamp_CacheAttached(PXCache sender)
  {
  }

  [PX.Objects.AP.Vendor]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAP.bAccountID))]
  protected virtual void APAdjust_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAP.tranType))]
  [PXUIField]
  protected virtual void APAdjust_AdjgDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAP.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<JournalWithSubEntry.GLTranDocAP, Where<JournalWithSubEntry.GLTranDocAP.tranType, Equal<Current<PX.Objects.AP.APAdjust.adjgDocType>>, And<JournalWithSubEntry.GLTranDocAP.refNbr, Equal<Current<PX.Objects.AP.APAdjust.adjgRefNbr>>, And<JournalWithSubEntry.GLTranDocAP.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAP>>>>>))]
  protected virtual void APAdjust_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDefault(0)]
  protected virtual void APAdjust_AdjNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [JournalWithSubEntry.APAdjdRefNbr2(typeof (Search2<PX.Objects.AP.APAdjust.APInvoice.refNbr, LeftJoin<PX.Objects.AP.APAdjust, On<PX.Objects.AP.APAdjust.adjdDocType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<PX.Objects.AP.APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<PX.Objects.AP.APAdjust.released, Equal<False>, And<Where<PX.Objects.AP.APAdjust.adjgDocType, NotEqual<Current<JournalWithSubEntry.GLTranDocAP.tranType>>, Or<PX.Objects.AP.APAdjust.adjgRefNbr, NotEqual<Current<JournalWithSubEntry.GLTranDocAP.refNbr>>>>>>>>, LeftJoin<PX.Objects.AP.APPayment, On<PX.Objects.AP.APPayment.docType, Equal<PX.Objects.AP.APAdjust.APInvoice.docType>, And<PX.Objects.AP.APPayment.refNbr, Equal<PX.Objects.AP.APAdjust.APInvoice.refNbr>, And<Where<PX.Objects.AP.APPayment.docType, Equal<APDocType.prepayment>, Or<PX.Objects.AP.APPayment.docType, Equal<APDocType.debitAdj>>>>>>>>, Where<PX.Objects.AP.APAdjust.APInvoice.vendorID, Equal<Current<JournalWithSubEntry.GLTranDocAP.bAccountID>>, And<PX.Objects.AP.APAdjust.APInvoice.docType, Equal<Optional<PX.Objects.AP.APAdjust.adjdDocType>>, And<PX.Objects.AP.APAdjust.APInvoice.released, Equal<True>, And<PX.Objects.AP.APAdjust.APInvoice.openDoc, Equal<True>, And<PX.Objects.AP.APAdjust.adjgRefNbr, IsNull, And2<Where<PX.Objects.AP.APPayment.refNbr, IsNull, And<Current<JournalWithSubEntry.GLTranDocAP.tranType>, NotEqual<APDocType.refund>, Or<PX.Objects.AP.APPayment.refNbr, IsNotNull, And<Current<JournalWithSubEntry.GLTranDocAP.tranType>, Equal<APDocType.refund>, Or<PX.Objects.AP.APPayment.docType, Equal<APDocType.debitAdj>, And<Current<JournalWithSubEntry.GLTranDocAP.tranType>, Equal<APDocType.check>, Or<PX.Objects.AP.APPayment.docType, Equal<APDocType.debitAdj>, And<Current<JournalWithSubEntry.GLTranDocAP.tranType>, Equal<APDocType.voidCheck>>>>>>>>>, And<Where<PX.Objects.AP.APAdjust.APInvoice.docDate, LessEqual<Current<JournalWithSubEntry.GLTranDocAP.tranDate>>, And<PX.Objects.AP.APAdjust.APInvoice.finPeriodID, LessEqual<Current<JournalWithSubEntry.GLTranDocAP.finPeriodID>>, Or<Current<APSetup.earlyChecks>, Equal<True>, And<Where<Current<JournalWithSubEntry.GLTranDocAP.tranType>, Equal<APDocType.check>, Or<Current<JournalWithSubEntry.GLTranDocAP.tranType>, Equal<APDocType.voidCheck>, Or<Current<JournalWithSubEntry.GLTranDocAP.tranType>, Equal<APDocType.prepayment>>>>>>>>>>>>>>>>), typeof (Search2<GLTranDoc.refNbr, LeftJoin<JournalWithSubEntry.APAdjust3, On<JournalWithSubEntry.APAdjust3.adjdDocType, Equal<GLTranDoc.tranType>, And<JournalWithSubEntry.APAdjust3.adjdRefNbr, Equal<GLTranDoc.refNbr>, And<JournalWithSubEntry.APAdjust3.released, Equal<False>, And<Where<JournalWithSubEntry.APAdjust3.adjgDocType, NotEqual<Current2<JournalWithSubEntry.GLTranDocAP.tranType>>, Or<JournalWithSubEntry.APAdjust3.adjgRefNbr, NotEqual<Current2<JournalWithSubEntry.GLTranDocAP.refNbr>>>>>>>>>, Where<GLTranDoc.batchNbr, Equal<Current2<JournalWithSubEntry.GLTranDocAP.batchNbr>>, And<GLTranDoc.parentLineNbr, IsNull, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAP>, And<GLTranDoc.bAccountID, Equal<Current2<JournalWithSubEntry.GLTranDocAP.bAccountID>>, And<GLTranDoc.docCreated, Equal<False>, And<GLTranDoc.tranType, Equal<Optional<PX.Objects.AP.APAdjust.adjdDocType>>, And<JournalWithSubEntry.APAdjust3.adjgRefNbr, IsNull>>>>>>>>))]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAP>, And<GLTranDoc.tranType, Equal<Current<PX.Objects.AP.APAdjust.adjdDocType>>, And<GLTranDoc.refNbr, Equal<Current<PX.Objects.AP.APAdjust.adjdRefNbr>>, And<GLTranDoc.parentLineNbr, IsNull>>>>>))]
  protected virtual void APAdjust_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [Branch(typeof (JournalWithSubEntry.GLTranDocAP.branchID), null, true, true, true)]
  protected virtual void APAdjust_AdjgBranchID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (JournalWithSubEntry.GLTranDocAP.curyInfoID), CuryIDField = "AdjgCuryID")]
  protected virtual void APAdjust_AdjgCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAP.tranDate))]
  protected virtual void APAdjust_AdjgDocDate_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAP.finPeriodID))]
  [PXUIField(DisplayName = "Application Period", Enabled = false)]
  protected virtual void APAdjust_AdjgFinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAP.tranPeriodID))]
  protected virtual void APAdjust_AdjgTranPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (PX.Objects.AP.APAdjust.adjgCuryInfoID), typeof (PX.Objects.AP.APAdjust.adjAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Mult<PX.Objects.AP.APAdjust.adjgBalSign, PX.Objects.AP.APAdjust.curyAdjgAmt>), typeof (SumCalc<JournalWithSubEntry.GLTranDocAP.curyApplAmt>))]
  protected virtual void APAdjust_CuryAdjgAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (PX.Objects.AP.APAdjust.adjdCuryInfoID), typeof (PX.Objects.AP.APAdjust.adjDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (Mult<PX.Objects.AP.APAdjust.adjdBalSign, PX.Objects.AP.APAdjust.curyAdjdDiscAmt>), typeof (SumCalc<GLTranDoc.curyDiscTaken>))]
  protected virtual void APAdjust_CuryAdjdDiscAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (PX.Objects.AP.APAdjust.adjdCuryInfoID), typeof (PX.Objects.AP.APAdjust.adjWhTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (Mult<PX.Objects.AP.APAdjust.adjdBalSign, PX.Objects.AP.APAdjust.curyAdjdWhTaxAmt>), typeof (SumCalc<GLTranDoc.curyTaxWheld>))]
  protected virtual void APAdjust_CuryAdjdWhTaxAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (Mult<PX.Objects.AP.APAdjust.adjdBalSign, PX.Objects.AP.APAdjust.curyAdjdAmt>), typeof (SumCalc<GLTranDoc.curyApplAmt>))]
  [PXFormula(null, typeof (CountCalc<GLTranDoc.applCount>))]
  protected virtual void APAdjust_CuryAdjdAmt_CacheAttached(PXCache sender)
  {
  }

  [PX.Objects.AR.Customer]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAR.bAccountID))]
  protected virtual void ARAdjust_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAR.tranType))]
  [PXUIField]
  protected virtual void ARAdjust_AdjgDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAR.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<JournalWithSubEntry.GLTranDocAR, Where<JournalWithSubEntry.GLTranDocAR.tranType, Equal<Current<PX.Objects.AR.ARAdjust.adjgDocType>>, And<JournalWithSubEntry.GLTranDocAR.refNbr, Equal<Current<PX.Objects.AR.ARAdjust.adjgRefNbr>>, And<JournalWithSubEntry.GLTranDocAR.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAR>>>>>))]
  protected virtual void ARAdjust_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDefault(0)]
  protected virtual void ARAdjust_AdjNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [JournalWithSubEntry.ARAdjdRefNbr2(typeof (Search2<PX.Objects.AR.ARAdjust.ARInvoice.refNbr, LeftJoin<PX.Objects.AR.ARAdjust, On<PX.Objects.AR.ARAdjust.adjdDocType, Equal<PX.Objects.AR.ARAdjust.ARInvoice.docType>, And<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<PX.Objects.AR.ARAdjust.ARInvoice.refNbr>, And<PX.Objects.AR.ARAdjust.released, Equal<False>, And<PX.Objects.AR.ARAdjust.voided, Equal<False>, And<Where<PX.Objects.AR.ARAdjust.adjgDocType, NotEqual<Current<JournalWithSubEntry.GLTranDocAR.tranType>>, Or<PX.Objects.AR.ARAdjust.adjgRefNbr, NotEqual<Current<JournalWithSubEntry.GLTranDocAR.refNbr>>>>>>>>>>, Where<PX.Objects.AR.ARAdjust.ARInvoice.customerID, Equal<Current<JournalWithSubEntry.GLTranDocAR.bAccountID>>, And<PX.Objects.AR.ARAdjust.ARInvoice.docType, Equal<Optional<PX.Objects.AR.ARAdjust.adjdDocType>>, And<PX.Objects.AR.ARAdjust.ARInvoice.released, Equal<True>, And<PX.Objects.AR.ARAdjust.ARInvoice.openDoc, Equal<True>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, IsNull>>>>>>), typeof (Search2<GLTranDoc.refNbr, LeftJoin<JournalWithSubEntry.ARAdjust3, On<JournalWithSubEntry.ARAdjust3.adjdDocType, Equal<GLTranDoc.tranType>, And<JournalWithSubEntry.ARAdjust3.adjdRefNbr, Equal<GLTranDoc.refNbr>, And<JournalWithSubEntry.ARAdjust3.released, Equal<False>, And<Where<JournalWithSubEntry.ARAdjust3.adjgDocType, NotEqual<Current2<JournalWithSubEntry.GLTranDocAR.tranType>>, Or<JournalWithSubEntry.ARAdjust3.adjgRefNbr, NotEqual<Current2<JournalWithSubEntry.GLTranDocAR.refNbr>>>>>>>>>, Where<GLTranDoc.batchNbr, Equal<Current2<JournalWithSubEntry.GLTranDocAR.batchNbr>>, And<GLTranDoc.parentLineNbr, IsNull, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAR>, And<GLTranDoc.bAccountID, Equal<Current2<JournalWithSubEntry.GLTranDocAR.bAccountID>>, And<GLTranDoc.docCreated, Equal<False>, And<GLTranDoc.tranType, Equal<Optional<PX.Objects.AR.ARAdjust.adjdDocType>>, And<JournalWithSubEntry.ARAdjust3.adjgRefNbr, IsNull>>>>>>>>), Filterable = true)]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAR>, And<GLTranDoc.tranType, Equal<Current<PX.Objects.AR.ARAdjust.adjdDocType>>, And<GLTranDoc.refNbr, Equal<Current<PX.Objects.AR.ARAdjust.adjdRefNbr>>, And<GLTranDoc.parentLineNbr, IsNull>>>>>))]
  protected virtual void ARAdjust_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [Branch(typeof (JournalWithSubEntry.GLTranDocAR.branchID), null, true, true, true)]
  protected virtual void ARAdjust_AdjgBranchID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (JournalWithSubEntry.GLTranDocAR.curyInfoID), CuryIDField = "AdjgCuryID")]
  protected virtual void ARAdjust_AdjgCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAR.tranDate))]
  protected virtual void ARAdjust_AdjgDocDate_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAR.finPeriodID))]
  [PXUIField(DisplayName = "Application Period", Enabled = false)]
  protected virtual void ARAdjust_AdjgFinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault(typeof (JournalWithSubEntry.GLTranDocAR.tranPeriodID))]
  protected virtual void ARAdjust_AdjgTranPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (PX.Objects.AR.ARAdjust.adjgCuryInfoID), typeof (PX.Objects.AR.ARAdjust.adjAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Mult<PX.Objects.AR.ARAdjust.adjgBalSign, PX.Objects.AR.ARAdjust.curyAdjgAmt>), typeof (SumCalc<JournalWithSubEntry.GLTranDocAR.curyApplAmt>))]
  protected virtual void ARAdjust_CuryAdjgAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (PX.Objects.AR.ARAdjust.adjdCuryInfoID), typeof (PX.Objects.AR.ARAdjust.adjDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (Mult<PX.Objects.AR.ARAdjust.adjdBalSign, PX.Objects.AR.ARAdjust.curyAdjdDiscAmt>), typeof (SumCalc<GLTranDoc.curyDiscTaken>))]
  protected virtual void ARAdjust_CuryAdjdDiscAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (PX.Objects.AR.ARAdjust.adjdCuryInfoID), typeof (PX.Objects.AR.ARAdjust.adjWOAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (Mult<PX.Objects.AR.ARAdjust.adjdBalSign, PX.Objects.AR.ARAdjust.curyAdjdWOAmt>), typeof (SumCalc<GLTranDoc.curyTaxWheld>))]
  protected virtual void ARAdjust_CuryAdjdWOAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (Mult<PX.Objects.AR.ARAdjust.adjdBalSign, PX.Objects.AR.ARAdjust.curyAdjdAmt>), typeof (SumCalc<GLTranDoc.curyApplAmt>))]
  [PXFormula(null, typeof (CountCalc<GLTranDoc.applCount>))]
  protected virtual void ARAdjust_CuryAdjdAmt_CacheAttached(PXCache sender)
  {
  }

  public JournalWithSubEntry()
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (CAAdj)];
    GLSetup current1 = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    APSetup current2 = ((PXSelectBase<APSetup>) this.apsetup).Current;
    ARSetup current3 = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    CASetup current4 = ((PXSelectBase<CASetup>) this.casetup).Current;
    OpenPeriodAttribute.SetValidatePeriod<GLDocBatch.finPeriodID>(((PXSelectBase) this.BatchModule).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXUIFieldAttribute.SetVisible<GLTranDoc.projectID>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, ProjectAttribute.IsPMVisible("GL"));
    PXUIFieldAttribute.SetVisible<GLTranDoc.taskID>(((PXSelectBase) this.GLTranModuleBatNbr).Cache, (object) null, ProjectAttribute.IsPMVisible("GL"));
    ((PXSelectBase) this.GLTransactions).Cache.AllowDelete = false;
    ((PXSelectBase) this.GLTransactions).Cache.AllowUpdate = false;
    ((PXSelectBase) this.GLTransactions).Cache.AllowInsert = false;
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(new PXFieldDefaulting((object) this, __methodptr(\u003C\u002Ector\u003Eb__72_0)));
  }

  public virtual IEnumerable gltransactions()
  {
    JournalWithSubEntry journalWithSubEntry = this;
    Dictionary<int, GLTran> result = new Dictionary<int, GLTran>();
    PXSelectBase pxSelectBase = (PXSelectBase) new PXSelectJoin<GLTran, InnerJoin<Batch, On<Batch.module, Equal<GLTran.module>, And<Batch.batchNbr, Equal<GLTran.batchNbr>>>, InnerJoin<PX.Objects.AP.APRegister, On<PX.Objects.AP.APRegister.batchNbr, Equal<GLTran.batchNbr>, And<GLTran.module, Equal<PX.Objects.GL.BatchModule.moduleAP>>>, InnerJoin<GLTranDoc, On<PX.Objects.AP.APRegister.docType, Equal<GLTranDoc.tranType>, And<GLTranDoc.refNbr, Equal<PX.Objects.AP.APRegister.refNbr>, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAP>, And<GLTranDoc.parentLineNbr, IsNull>>>>>>>, Where<GLTranDoc.module, Equal<Optional<GLDocBatch.module>>, And<GLTranDoc.batchNbr, Equal<Optional<GLDocBatch.batchNbr>>>>, OrderBy<Asc<GLTranDoc.lineNbr, Asc<GLTran.lineNbr>>>>((PXGraph) journalWithSubEntry);
    PXSelectBase selectAR = (PXSelectBase) new PXSelectJoin<GLTran, InnerJoin<Batch, On<Batch.module, Equal<GLTran.module>, And<Batch.batchNbr, Equal<GLTran.batchNbr>>>, InnerJoin<PX.Objects.AR.ARRegister, On<PX.Objects.AR.ARRegister.batchNbr, Equal<GLTran.batchNbr>, And<GLTran.module, Equal<PX.Objects.GL.BatchModule.moduleAR>>>, InnerJoin<GLTranDoc, On<PX.Objects.AR.ARRegister.docType, Equal<GLTranDoc.tranType>, And<GLTranDoc.refNbr, Equal<PX.Objects.AR.ARRegister.refNbr>, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAR>, And<GLTranDoc.parentLineNbr, IsNull>>>>>>>, Where<GLTranDoc.module, Equal<Optional<GLDocBatch.module>>, And<GLTranDoc.batchNbr, Equal<Optional<GLDocBatch.batchNbr>>>>, OrderBy<Asc<GLTranDoc.lineNbr, Asc<GLTran.lineNbr>>>>((PXGraph) journalWithSubEntry);
    PXSelectBase selectCA = (PXSelectBase) new PXSelectJoin<GLTran, InnerJoin<Batch, On<Batch.module, Equal<GLTran.module>, And<Batch.batchNbr, Equal<GLTran.batchNbr>>>, InnerJoin<CATran, On<Batch.module, Equal<PX.Objects.GL.BatchModule.moduleCA>, And<Batch.batchNbr, Equal<CATran.batchNbr>>>, InnerJoin<CAAdj, On<CAAdj.tranID, Equal<CATran.tranID>>, InnerJoin<GLTranDoc, On<GLTranDoc.refNbr, Equal<CAAdj.adjRefNbr>, And<GLTranDoc.tranType, Equal<CAAdj.adjTranType>, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleCA>>>>>>>>, Where<GLTranDoc.module, Equal<Optional<GLDocBatch.module>>, And<GLTranDoc.batchNbr, Equal<Optional<GLDocBatch.batchNbr>>, And<GLTranDoc.tranType, Equal<CATranType.cAAdjustment>, And<GLTranDoc.parentLineNbr, IsNull>>>>, OrderBy<Asc<GLTranDoc.lineNbr, Asc<GLTran.lineNbr>>>>((PXGraph) journalWithSubEntry);
    PXSelectBase selectGL = (PXSelectBase) new PXSelectJoin<GLTran, InnerJoin<Batch, On<Batch.module, Equal<GLTran.module>, And<Batch.batchNbr, Equal<GLTran.batchNbr>>>, InnerJoin<GLTranDoc, On<GLTranDoc.refNbr, Equal<Batch.batchNbr>, And<GLTranDoc.tranModule, Equal<Batch.module>>>>>, Where<GLTranDoc.module, Equal<Optional<GLDocBatch.module>>, And<GLTranDoc.batchNbr, Equal<Optional<GLDocBatch.batchNbr>>, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleGL>, And<GLTranDoc.parentLineNbr, IsNull>>>>, OrderBy<Asc<GLTranDoc.lineNbr, Asc<GLTran.lineNbr>>>>((PXGraph) journalWithSubEntry);
    foreach (PXResult<GLTran, Batch, PX.Objects.AP.APRegister, GLTranDoc> pxResult in pxSelectBase.View.SelectMulti(Array.Empty<object>()))
    {
      GLTran glTran = PXResult<GLTran, Batch, PX.Objects.AP.APRegister, GLTranDoc>.op_Implicit(pxResult);
      Batch batch = PXResult<GLTran, Batch, PX.Objects.AP.APRegister, GLTranDoc>.op_Implicit(pxResult);
      PXResult<GLTran, Batch, PX.Objects.AP.APRegister, GLTranDoc>.op_Implicit(pxResult);
      PXResult<GLTran, Batch, PX.Objects.AP.APRegister, GLTranDoc>.op_Implicit(pxResult);
      if (!result.ContainsKey(glTran.TranID.Value))
      {
        result[glTran.TranID.Value] = glTran;
        yield return (object) new PXResult<GLTran, Batch>(glTran, batch);
      }
    }
    foreach (PXResult<GLTran, Batch, PX.Objects.AR.ARRegister, GLTranDoc> pxResult in selectAR.View.SelectMulti(Array.Empty<object>()))
    {
      GLTran glTran = PXResult<GLTran, Batch, PX.Objects.AR.ARRegister, GLTranDoc>.op_Implicit(pxResult);
      Batch batch = PXResult<GLTran, Batch, PX.Objects.AR.ARRegister, GLTranDoc>.op_Implicit(pxResult);
      PXResult<GLTran, Batch, PX.Objects.AR.ARRegister, GLTranDoc>.op_Implicit(pxResult);
      PXResult<GLTran, Batch, PX.Objects.AR.ARRegister, GLTranDoc>.op_Implicit(pxResult);
      if (!result.ContainsKey(glTran.TranID.Value))
      {
        result[glTran.TranID.Value] = glTran;
        yield return (object) new PXResult<GLTran, Batch>(glTran, batch);
      }
    }
    foreach (PXResult<GLTran, Batch, CATran, CAAdj, GLTranDoc> pxResult in selectCA.View.SelectMulti(Array.Empty<object>()))
    {
      GLTran glTran = PXResult<GLTran, Batch, CATran, CAAdj, GLTranDoc>.op_Implicit(pxResult);
      Batch batch = PXResult<GLTran, Batch, CATran, CAAdj, GLTranDoc>.op_Implicit(pxResult);
      PXResult<GLTran, Batch, CATran, CAAdj, GLTranDoc>.op_Implicit(pxResult);
      PXResult<GLTran, Batch, CATran, CAAdj, GLTranDoc>.op_Implicit(pxResult);
      if (!result.ContainsKey(glTran.TranID.Value))
      {
        result[glTran.TranID.Value] = glTran;
        yield return (object) new PXResult<GLTran, Batch>(glTran, batch);
      }
    }
    foreach (PXResult<GLTran, Batch, GLTranDoc> pxResult in selectGL.View.SelectMulti(Array.Empty<object>()))
    {
      GLTran glTran = PXResult<GLTran, Batch, GLTranDoc>.op_Implicit(pxResult);
      Batch batch = PXResult<GLTran, Batch, GLTranDoc>.op_Implicit(pxResult);
      PXResult<GLTran, Batch, GLTranDoc>.op_Implicit(pxResult);
      if (!result.ContainsKey(glTran.TranID.Value))
      {
        result[glTran.TranID.Value] = glTran;
        yield return (object) new PXResult<GLTran, Batch>(glTran, batch);
      }
    }
  }

  public virtual IEnumerable currentdoctaxes()
  {
    GLTranDoc current = ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Current;
    if (current == null)
      return (IEnumerable) null;
    GLTranDoc glTranDoc = current;
    if (current.IsChildTran)
      glTranDoc = PXResultset<GLTranDoc>.op_Implicit(PXSelectBase<GLTranDoc, PXSelect<GLTranDoc, Where<GLTranDoc.module, Equal<Required<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Required<GLTranDoc.batchNbr>>, And<GLTranDoc.lineNbr, Equal<Required<GLTranDoc.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) current.Module,
        (object) current.BatchNbr,
        (object) current.ParentLineNbr
      }));
    return (IEnumerable) PXSelectBase<GLTaxTran, PXSelectJoin<GLTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<GLTaxTran.taxID>>>, Where<GLTaxTran.module, Equal<Required<GLTranDoc.module>>, And<GLTaxTran.batchNbr, Equal<Required<GLTranDoc.batchNbr>>, And<GLTaxTran.lineNbr, Equal<Required<GLTranDoc.lineNbr>>, And<GLTax.detailType, Equal<GLTaxDetailType.docTax>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) glTranDoc.Module,
      (object) glTranDoc.BatchNbr,
      (object) glTranDoc.LineNbr
    });
  }

  public PX.Objects.CM.CurrencyInfo currencyInfo
  {
    get
    {
      return PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
    }
  }

  public OrganizationFinPeriod FINPERIOD
  {
    get
    {
      return PXResultset<OrganizationFinPeriod>.op_Implicit(((PXSelectBase<OrganizationFinPeriod>) this.finperiod).Select(Array.Empty<object>()));
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    JournalWithSubEntry.\u003C\u003Ec__DisplayClass121_0 displayClass1210 = new JournalWithSubEntry.\u003C\u003Ec__DisplayClass121_0();
    PXCache cach = ((PXGraph) this).Caches[typeof (GLDocBatch)];
    // ISSUE: reference to a compiler-generated field
    displayClass1210.list = new List<GLDocBatch>();
    foreach (GLDocBatch glDocBatch in adapter.Get())
    {
      if (glDocBatch.Status == "B")
      {
        cach.Update((object) glDocBatch);
        // ISSUE: reference to a compiler-generated field
        displayClass1210.list.Add(glDocBatch);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (displayClass1210.list.Count == 0)
      throw new PXException("Batch Status invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    if (displayClass1210.list.Count > 0)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1210, __methodptr(\u003CRelease\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1210.list;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Current == null)
      return adapter.Get();
    GLTranDoc current = ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Current;
    bool? docCreated = current.DocCreated;
    bool flag = false;
    if (docCreated.GetValueOrDefault() == flag & docCreated.HasValue)
      throw new PXException("A Document for this row is not created yet");
    IDocGraphCreator docGraphCreator = (IDocGraphCreator) null;
    string tranModule = current.TranModule;
    if (tranModule != null && tranModule.Length == 2)
    {
      switch (tranModule[0])
      {
        case 'A':
          switch (tranModule)
          {
            case "AP":
              docGraphCreator = (IDocGraphCreator) new APDocGraphCreator();
              break;
            case "AR":
              docGraphCreator = (IDocGraphCreator) new ARDocGraphCreator();
              break;
          }
          break;
        case 'C':
          if (tranModule == "CA")
          {
            docGraphCreator = (IDocGraphCreator) new CADocGraphCreator();
            break;
          }
          break;
        case 'D':
          if (tranModule == "DR")
          {
            docGraphCreator = (IDocGraphCreator) new DRDocGraphCreator();
            break;
          }
          break;
        case 'G':
          if (tranModule == "GL")
          {
            docGraphCreator = (IDocGraphCreator) new GLDocGraphCreator();
            break;
          }
          break;
        case 'I':
          if (tranModule == "IN")
          {
            docGraphCreator = (IDocGraphCreator) new INDocGraphCreator();
            break;
          }
          break;
        case 'P':
          if (tranModule == "PM")
          {
            docGraphCreator = (IDocGraphCreator) new PMDocGraphCreator();
            break;
          }
          break;
      }
    }
    if (docGraphCreator != null)
    {
      PXGraph pxGraph = docGraphCreator.Create(current.TranType, current.RefNbr, current.BAccountID);
      if (pxGraph != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException(pxGraph, true, nameof (ViewDocument));
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    throw new PXException("The selected reference number is assigned to a journal entry. Click the link in the Batch Number column to open the batch that includes the entry.");
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ShowTaxes(PXAdapter adapter)
  {
    ((PXSelectBase<GLTaxTran>) this.CurrentDocTaxes).AskExt(true);
    return adapter.Get();
  }

  public static void ReleaseBatch(List<GLDocBatch> list)
  {
    GLBatchDocRelease instance = PXGraph.CreateInstance<GLBatchDocRelease>();
    for (int index = 0; index < list.Count; ++index)
    {
      ((PXGraph) instance).Clear((PXClearOption) 0);
      GLDocBatch aBatch = list[index];
      instance.ReleaseBatchProc(aBatch, true);
    }
  }

  private void SetTransactionsChanged()
  {
    foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (GLTranDoc)], (object) PXResult<GLTranDoc>.op_Implicit(pxResult));
  }

  private void SetTransactionsChanged<Field>() where Field : class, IBqlField
  {
    foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
    {
      GLTranDoc glTranDoc = PXResult<GLTranDoc>.op_Implicit(pxResult);
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.SetDefaultExt<Field>((object) glTranDoc);
      GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (GLTranDoc)], (object) glTranDoc);
    }
  }

  private string GetNumberingID(GLTranDoc doc)
  {
    PXCache pxCache = (PXCache) null;
    System.Type type1 = (System.Type) null;
    switch (doc.TranModule)
    {
      case "AP":
        System.Type type2 = APInvoiceType.NumberingAttribute.GetNumberingIDField(doc.TranType);
        if ((object) type2 == null)
          type2 = APPaymentType.NumberingAttribute.GetNumberingIDField(doc.TranType) ?? APQuickCheckType.NumberingAttribute.GetNumberingIDField(doc.TranType);
        type1 = type2;
        pxCache = ((PXSelectBase) this.apsetup).Cache;
        break;
      case "AR":
        System.Type type3 = ARInvoiceType.NumberingAttribute.GetNumberingIDField(doc.TranType);
        if ((object) type3 == null)
          type3 = ARPaymentType.NumberingAttribute.GetNumberingIDField(doc.TranType) ?? ARCashSaleType.NumberingAttribute.GetNumberingIDField(doc.TranType);
        type1 = type3;
        pxCache = ((PXSelectBase) this.arsetup).Cache;
        break;
      case "CA":
        type1 = typeof (CASetup.registerNumberingID);
        pxCache = ((PXSelectBase) this.casetup).Cache;
        break;
      case "GL":
        type1 = PX.Objects.GL.BatchModule.NumberingAttribute.GetNumberingIDField("GL");
        pxCache = ((PXSelectBase) this.glsetup).Cache;
        break;
    }
    return type1 != (System.Type) null ? (string) pxCache.GetValue(pxCache.Current, type1.Name) : (string) null;
  }

  protected virtual NumberingSequence GetNumberingSequence(GLTranDoc doc)
  {
    string numberingId = this.GetNumberingID(doc);
    if (string.IsNullOrEmpty(numberingId))
      return (NumberingSequence) null;
    Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) numberingId
    }));
    if ((numbering != null ? (numbering.UserNumbering.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new JournalWithSubEntry.PXManualNumberingException();
    return AutoNumberAttribute.GetNumberingSequence(numberingId, doc.BranchID, doc.TranDate);
  }

  protected bool ShouldReuseRefNbrs
  {
    get
    {
      return ((PXSelectBase<GLSetup>) this.glsetup).Current.ReuseRefNbrsInVouchers.GetValueOrDefault();
    }
  }

  protected virtual void AttemptReuseRecentlyDeletedKey(
    GLTranDoc source,
    NumberingSequence sequence)
  {
    if (!this.ShouldReuseRefNbrs)
      return;
    JournalWithSubEntry.RefDocKey chosenKey = (JournalWithSubEntry.RefDocKey) null;
    if (sequence == null)
    {
      chosenKey = source.BranchID.HasValue ? PXResultset<JournalWithSubEntry.RefDocKey>.op_Implicit(((PXSelectBase<JournalWithSubEntry.RefDocKey>) this.deletedKeys).Search<JournalWithSubEntry.RefDocKey.tranModule, JournalWithSubEntry.RefDocKey.tranType>((object) source.TranModule, (object) source.TranType, Array.Empty<object>())) : PXResultset<JournalWithSubEntry.RefDocKey>.op_Implicit(((PXSelectBase<JournalWithSubEntry.RefDocKey>) this.deletedKeys).Search<JournalWithSubEntry.RefDocKey.branchID, JournalWithSubEntry.RefDocKey.tranModule, JournalWithSubEntry.RefDocKey.tranType>((object) source.BranchID, (object) source.TranModule, (object) source.TranType, Array.Empty<object>()));
    }
    else
    {
      foreach (PXResult<JournalWithSubEntry.RefDocKey> pxResult in ((PXSelectBase<JournalWithSubEntry.RefDocKey>) this.deletedKeys).Select(Array.Empty<object>()))
      {
        JournalWithSubEntry.RefDocKey refDocKey = PXResult<JournalWithSubEntry.RefDocKey>.op_Implicit(pxResult);
        if (refDocKey.TranModule == source.TranModule && refDocKey.TranType == source.TranType && !string.IsNullOrEmpty(refDocKey.RefNbr))
        {
          int? nullable = sequence.NBranchID;
          if (nullable.HasValue)
          {
            nullable = refDocKey.BranchID;
            int? branchId = source.BranchID;
            if (!(nullable.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable.HasValue == branchId.HasValue))
              continue;
          }
          if ((string.IsNullOrEmpty(sequence.StartNbr) || string.Compare(refDocKey.RefNbr, sequence.StartNbr) >= 0) && (string.IsNullOrEmpty(sequence.EndNbr) || string.Compare(refDocKey.RefNbr, sequence.EndNbr) <= 0))
          {
            chosenKey = refDocKey;
            break;
          }
        }
      }
    }
    if (chosenKey == null || string.IsNullOrEmpty(chosenKey.RefNbr))
      return;
    source.RefNbr = chosenKey.RefNbr;
    ((PXSelectBase<JournalWithSubEntry.RefDocKey>) this.deletedKeys).Delete(chosenKey);
    this.RemoveDeletedRecordFromCache(chosenKey);
  }

  private void RemoveDeletedRecordFromCache(JournalWithSubEntry.RefDocKey chosenKey)
  {
    switch (chosenKey.TranModule)
    {
      case "AP":
        PX.Objects.AP.APRegister apRegister = new PX.Objects.AP.APRegister()
        {
          DocType = chosenKey.TranType,
          RefNbr = chosenKey.RefNbr
        };
        if (((PXGraph) this).Caches[typeof (PX.Objects.AP.APRegister)].GetStatus((object) apRegister) != 3)
          break;
        ((PXGraph) this).Caches[typeof (PX.Objects.AP.APRegister)].SetStatus((object) apRegister, (PXEntryStatus) 0);
        break;
      case "AR":
        PX.Objects.AR.ARRegister arRegister = new PX.Objects.AR.ARRegister()
        {
          DocType = chosenKey.TranType,
          RefNbr = chosenKey.RefNbr
        };
        if (((PXGraph) this).Caches[typeof (PX.Objects.AR.ARRegister)].GetStatus((object) arRegister) != 3)
          break;
        ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARRegister)].SetStatus((object) arRegister, (PXEntryStatus) 0);
        break;
      case "GL":
        Batch batch = new Batch()
        {
          Module = chosenKey.TranModule,
          BatchNbr = chosenKey.RefNbr
        };
        if (((PXGraph) this).Caches[typeof (Batch)].GetStatus((object) batch) != 3)
          break;
        ((PXGraph) this).Caches[typeof (Batch)].SetStatus((object) batch, (PXEntryStatus) 0);
        break;
      case "CA":
        CAAdj caAdj = new CAAdj()
        {
          DocType = chosenKey.TranType,
          RefNbr = chosenKey.RefNbr
        };
        if (((PXGraph) this).Caches[typeof (CAAdj)].GetStatus((object) caAdj) != 3)
          break;
        ((PXGraph) this).Caches[typeof (CAAdj)].SetStatus((object) caAdj, (PXEntryStatus) 0);
        break;
    }
  }

  protected virtual void CreateRefNbr(GLTranDoc source)
  {
    if (string.IsNullOrEmpty(source.TranType) || !source.TranDate.HasValue || source.IsChildTran || !string.IsNullOrEmpty(source.RefNbr))
      return;
    int? nullable;
    if (source.TranModule == "AP")
    {
      nullable = source.BAccountID;
      if (nullable.HasValue)
      {
        nullable = source.LocationID;
        if (nullable.HasValue)
        {
          NumberingSequence numberingSequence = this.GetNumberingSequence(source);
          this.AttemptReuseRecentlyDeletedKey(source, numberingSequence);
          if (string.IsNullOrEmpty(source.RefNbr))
          {
            PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.AP.APRegister)];
            string deletedRefNbr = this.ShouldReuseRefNbrs ? this.FindDeletedRefNbr(typeof (PX.Objects.AP.APRegister), typeof (PX.Objects.AP.APRegister.refNbr).Name, typeof (PX.Objects.AP.APRegister.docType).Name, source.TranType, typeof (PX.Objects.AP.APRegister.branchID).Name, numberingSequence) : (string) null;
            PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.currencyinfo).Cache.Insert((object) new PX.Objects.CM.CurrencyInfo()
            {
              CuryID = source.CuryID
            });
            ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfoEx).Insert(PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info));
            PX.Objects.AP.APRegister apRegister = (PX.Objects.AP.APRegister) cach.Insert((object) new PX.Objects.AP.APRegister()
            {
              DocType = source.TranType,
              DocDate = source.TranDate,
              VendorID = source.BAccountID,
              VendorLocationID = source.LocationID,
              BranchID = source.BranchID,
              FinPeriodID = source.FinPeriodID,
              CuryID = source.CuryID,
              CuryInfoID = info.CuryInfoID
            });
            bool flag = !string.IsNullOrEmpty(deletedRefNbr);
            if (flag)
            {
              apRegister.RefNbr = deletedRefNbr;
              cach.Normalize();
              cach.SetStatus((object) apRegister, (PXEntryStatus) 1);
            }
            using (PXTransactionScope transactionScope = new PXTransactionScope())
            {
              try
              {
                this._skipExtensionTables = true;
                if (!flag)
                {
                  this.PersistWithExceptionHandling(source, cach, (PXDBOperation) 2);
                  PXDatabase.Delete<PX.Objects.AP.APRegister>(new PXDataFieldRestrict[2]
                  {
                    (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AP.APRegister.docType>((object) apRegister.DocType),
                    (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AP.APRegister.refNbr>((object) apRegister.RefNbr)
                  });
                }
                else
                {
                  this.PersistWithExceptionHandling(source, cach, (PXDBOperation) 1);
                  PXDatabase.Delete<PX.Objects.AP.APRegister>(new PXDataFieldRestrict[2]
                  {
                    (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AP.APRegister.docType>((object) apRegister.DocType),
                    (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AP.APRegister.refNbr>((object) apRegister.RefNbr)
                  });
                }
              }
              finally
              {
                this._skipExtensionTables = false;
              }
              foreach (System.Type type in cach.GetExtensionTables() ?? new List<System.Type>())
              {
                PXDataFieldRestrict[] dataFieldRestrictArray = new PXDataFieldRestrict[2]
                {
                  new PXDataFieldRestrict("DocType", (object) apRegister.DocType),
                  new PXDataFieldRestrict("RefNbr", (object) apRegister.RefNbr)
                };
                PXDatabase.ForceDelete(type, dataFieldRestrictArray);
              }
              transactionScope.Complete();
              apRegister.tstamp = PXDatabase.SelectTimeStamp();
            }
            cach.Persisted(false);
            source.RefNbr = apRegister.RefNbr;
          }
        }
      }
    }
    if (source.TranModule == "AR")
    {
      nullable = source.BAccountID;
      if (nullable.HasValue)
      {
        nullable = source.LocationID;
        if (nullable.HasValue)
        {
          NumberingSequence numberingSequence = this.GetNumberingSequence(source);
          this.AttemptReuseRecentlyDeletedKey(source, numberingSequence);
          if (string.IsNullOrEmpty(source.RefNbr))
          {
            PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARRegister)];
            string deletedRefNbr = this.ShouldReuseRefNbrs ? this.FindDeletedRefNbr(typeof (PX.Objects.AR.ARRegister), typeof (PX.Objects.AR.ARRegister.refNbr).Name, typeof (PX.Objects.AR.ARRegister.docType).Name, source.TranType, typeof (PX.Objects.AR.ARRegister.branchID).Name, numberingSequence) : (string) null;
            PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.currencyinfo).Cache.Insert((object) new PX.Objects.CM.CurrencyInfo()
            {
              CuryID = source.CuryID
            });
            ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfoEx).Insert(PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info));
            PX.Objects.AR.ARRegister arRegister = (PX.Objects.AR.ARRegister) cach.Insert((object) new PX.Objects.AR.ARRegister()
            {
              DocType = source.TranType,
              DocDate = source.TranDate,
              CustomerID = source.BAccountID,
              CustomerLocationID = source.LocationID,
              BranchID = source.BranchID,
              FinPeriodID = source.FinPeriodID,
              CuryID = source.CuryID,
              CuryInfoID = info.CuryInfoID
            });
            bool flag = !string.IsNullOrEmpty(deletedRefNbr);
            if (flag)
            {
              arRegister.RefNbr = deletedRefNbr;
              cach.Normalize();
              cach.SetStatus((object) arRegister, (PXEntryStatus) 1);
            }
            using (PXTransactionScope transactionScope = new PXTransactionScope())
            {
              try
              {
                this._skipExtensionTables = true;
                if (!flag)
                {
                  this.PersistWithExceptionHandling(source, cach, (PXDBOperation) 2);
                  PXDatabase.Delete<PX.Objects.AR.ARRegister>(new PXDataFieldRestrict[2]
                  {
                    (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AR.ARRegister.docType>((object) arRegister.DocType),
                    (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AR.ARRegister.refNbr>((object) arRegister.RefNbr)
                  });
                }
                else
                {
                  this.PersistWithExceptionHandling(source, cach, (PXDBOperation) 1);
                  PXDatabase.Delete<PX.Objects.AR.ARRegister>(new PXDataFieldRestrict[2]
                  {
                    (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AR.ARRegister.docType>((object) arRegister.DocType),
                    (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.AR.ARRegister.refNbr>((object) arRegister.RefNbr)
                  });
                }
              }
              finally
              {
                this._skipExtensionTables = false;
              }
              foreach (System.Type type in cach.GetExtensionTables() ?? new List<System.Type>())
              {
                PXDataFieldRestrict[] dataFieldRestrictArray = new PXDataFieldRestrict[2]
                {
                  new PXDataFieldRestrict("DocType", (object) arRegister.DocType),
                  new PXDataFieldRestrict("RefNbr", (object) arRegister.RefNbr)
                };
                PXDatabase.ForceDelete(type, dataFieldRestrictArray);
              }
              transactionScope.Complete();
              arRegister.tstamp = PXDatabase.SelectTimeStamp();
            }
            cach.Persisted(false);
            source.RefNbr = arRegister.RefNbr;
          }
        }
      }
    }
    if (source.TranModule == "CA" && !string.IsNullOrEmpty(source.EntryTypeID))
    {
      nullable = source.CashAccountID;
      if (nullable.HasValue)
      {
        NumberingSequence numberingSequence = this.GetNumberingSequence(source);
        this.AttemptReuseRecentlyDeletedKey(source, numberingSequence);
        if (string.IsNullOrEmpty(source.RefNbr))
        {
          PXCache cach = ((PXGraph) this).Caches[typeof (CAAdj)];
          string deletedRefNbr = this.ShouldReuseRefNbrs ? this.FindDeletedRefNbr(typeof (CAAdj), typeof (CAAdj.adjRefNbr).Name, typeof (CAAdj.adjTranType).Name, source.TranType, typeof (CAAdj.branchID).Name, numberingSequence) : (string) null;
          ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfoEx).Insert(PX.Objects.CM.Extensions.CurrencyInfo.GetEX((PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.currencyinfo).Cache.Insert((object) new PX.Objects.CM.CurrencyInfo()
          {
            CuryID = source.CuryID
          })));
          CAAdj caAdj = (CAAdj) cach.Insert((object) new CAAdj()
          {
            AdjTranType = source.TranType,
            TranDate = source.TranDate,
            FinPeriodID = source.FinPeriodID,
            CashAccountID = source.CashAccountID,
            EntryTypeID = source.EntryTypeID,
            ExtRefNbr = "",
            BranchID = source.BranchID,
            CuryID = source.CuryID
          });
          bool flag = !string.IsNullOrEmpty(deletedRefNbr);
          if (flag)
          {
            caAdj.AdjRefNbr = deletedRefNbr;
            cach.Normalize();
            cach.SetStatus((object) caAdj, (PXEntryStatus) 1);
          }
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            try
            {
              this._skipExtensionTables = true;
              if (!flag)
              {
                this.PersistWithExceptionHandling(source, cach, (PXDBOperation) 2);
                PXDatabase.Delete<CAAdj>(new PXDataFieldRestrict[2]
                {
                  (PXDataFieldRestrict) new PXDataFieldRestrict<CAAdj.adjTranType>((object) caAdj.AdjTranType),
                  (PXDataFieldRestrict) new PXDataFieldRestrict<CAAdj.adjRefNbr>((object) caAdj.AdjRefNbr)
                });
              }
              else
              {
                this.PersistWithExceptionHandling(source, cach, (PXDBOperation) 1);
                PXDatabase.Delete<CAAdj>(new PXDataFieldRestrict[2]
                {
                  (PXDataFieldRestrict) new PXDataFieldRestrict<CAAdj.adjTranType>((object) caAdj.AdjTranType),
                  (PXDataFieldRestrict) new PXDataFieldRestrict<CAAdj.adjRefNbr>((object) caAdj.AdjRefNbr)
                });
              }
            }
            finally
            {
              this._skipExtensionTables = false;
            }
            foreach (System.Type type in cach.GetExtensionTables() ?? new List<System.Type>())
            {
              PXDataFieldRestrict[] dataFieldRestrictArray = new PXDataFieldRestrict[2]
              {
                new PXDataFieldRestrict("AdjTranType", (object) caAdj.AdjTranType),
                new PXDataFieldRestrict("AdjRefNbr", (object) caAdj.AdjRefNbr)
              };
              PXDatabase.ForceDelete(type, dataFieldRestrictArray);
            }
            transactionScope.Complete();
            caAdj.tstamp = PXDatabase.SelectTimeStamp();
          }
          cach.Persisted(false);
          source.RefNbr = caAdj.RefNbr;
        }
      }
    }
    if (!(source.TranModule == "GL"))
      return;
    nullable = source.DebitAccountID;
    if (!nullable.HasValue)
    {
      nullable = source.CreditAccountID;
      if (!nullable.HasValue)
        return;
    }
    NumberingSequence numberingSequence1 = this.GetNumberingSequence(source);
    this.AttemptReuseRecentlyDeletedKey(source, numberingSequence1);
    if (!string.IsNullOrEmpty(source.RefNbr))
      return;
    PXCache cach1 = ((PXGraph) this).Caches[typeof (Batch)];
    string deletedRefNbr1 = this.ShouldReuseRefNbrs ? this.FindDeletedRefNbr(typeof (Batch), typeof (Batch.batchNbr).Name, typeof (Batch.module).Name, source.TranModule, typeof (Batch.branchID).Name, numberingSequence1) : (string) null;
    Batch batch = (Batch) cach1.Insert((object) new Batch()
    {
      Module = source.TranModule,
      DateEntered = source.TranDate,
      BranchID = source.BranchID,
      FinPeriodID = source.FinPeriodID,
      CuryID = source.CuryID,
      Status = "H"
    });
    bool flag1 = !string.IsNullOrEmpty(deletedRefNbr1);
    if (flag1)
    {
      batch.BatchNbr = deletedRefNbr1;
      cach1.Normalize();
      cach1.SetStatus((object) batch, (PXEntryStatus) 1);
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        this._skipExtensionTables = true;
        if (!flag1)
        {
          this.PersistWithExceptionHandling(source, cach1, (PXDBOperation) 2);
          PXDatabase.Delete<Batch>(new PXDataFieldRestrict[2]
          {
            (PXDataFieldRestrict) new PXDataFieldRestrict<Batch.module>((object) batch.Module),
            (PXDataFieldRestrict) new PXDataFieldRestrict<Batch.batchNbr>((object) batch.BatchNbr)
          });
        }
        else
        {
          this.PersistWithExceptionHandling(source, cach1, (PXDBOperation) 1);
          PXDatabase.Delete<Batch>(new PXDataFieldRestrict[2]
          {
            (PXDataFieldRestrict) new PXDataFieldRestrict<Batch.module>((object) batch.Module),
            (PXDataFieldRestrict) new PXDataFieldRestrict<Batch.batchNbr>((object) batch.BatchNbr)
          });
        }
      }
      finally
      {
        this._skipExtensionTables = false;
      }
      foreach (System.Type type in cach1.GetExtensionTables() ?? new List<System.Type>())
      {
        PXDataFieldRestrict[] dataFieldRestrictArray = new PXDataFieldRestrict[2]
        {
          new PXDataFieldRestrict("Module", (object) batch.Module),
          new PXDataFieldRestrict("BatchNbr", (object) batch.BatchNbr)
        };
        PXDatabase.ForceDelete(type, dataFieldRestrictArray);
      }
      transactionScope.Complete();
      batch.tstamp = PXDatabase.SelectTimeStamp();
    }
    cach1.Persisted(false);
    source.RefNbr = batch.BatchNbr;
  }

  private void PersistWithExceptionHandling(
    GLTranDoc source,
    PXCache cache,
    PXDBOperation operation)
  {
    try
    {
      cache.Persist(operation);
    }
    catch (PXOuterException ex)
    {
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.RaiseExceptionHandling<GLTranDoc.tranDate>((object) source, (object) source.TranDate, (Exception) new PXSetPropertyException<GLTranDoc.tranDate>(string.Join(". ", ex.InnerMessages), (PXErrorLevel) 4));
    }
  }

  [Obsolete("Will be removed in Acumatica 2019R1")]
  protected virtual string FindDeletedRefNbr(
    System.Type aTable,
    string aRefNbrField,
    string aTranTypeField,
    string aTranType,
    NumberingSequence sequence)
  {
    return this.FindDeletedRefNbr(aTable, aRefNbrField, aTranTypeField, aTranType, (string) null, sequence);
  }

  protected virtual string FindDeletedRefNbr(
    System.Type aTable,
    string aRefNbrField,
    string aTranTypeField,
    string aTranType,
    string aBranchIDField,
    NumberingSequence sequence)
  {
    string deletedRefNbr = (string) null;
    List<PXDataField> pxDataFieldList = new List<PXDataField>(8);
    pxDataFieldList.Add(new PXDataField(aRefNbrField));
    pxDataFieldList.Add(new PXDataField("CreatedByID"));
    pxDataFieldList.Add(new PXDataField("LastModifiedByID"));
    pxDataFieldList.Add(new PXDataField("CreatedDateTime"));
    pxDataFieldList.Add(new PXDataField("LastModifiedDateTime"));
    if (!string.IsNullOrEmpty(aTranTypeField))
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue(aTranTypeField, (object) aTranType));
    pxDataFieldList.Add((PXDataField) new PXDataFieldValue("DeletedDatabaseRecord", (PXDbType) 2, new int?(1), (object) 1));
    pxDataFieldList.Add((PXDataField) new PXDataFieldOrder("LastModifiedDateTime"));
    if (sequence != null && !string.IsNullOrEmpty(sequence.StartNbr))
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue(aRefNbrField, (PXDbType) 12, new int?(sequence.StartNbr.Length), (object) sequence.StartNbr, (PXComp) 3));
    if (sequence != null && !string.IsNullOrEmpty(sequence.EndNbr))
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue(aRefNbrField, (PXDbType) 12, new int?(sequence.EndNbr.Length), (object) sequence.EndNbr, (PXComp) 5));
    if (sequence != null && sequence.NBranchID.HasValue)
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue(aBranchIDField, (PXDbType) 8, new int?(4), (object) sequence.NBranchID, (PXComp) 0));
    using (new PXReadDeletedScope(false))
    {
      DateTime dateTime1;
      DateTime dateTime2;
      PXDatabase.SelectDate(ref dateTime1, ref dateTime2);
      if (this._importing && this.prevParams != null && this.prevTable != (System.Type) null && this.prevTable == aTable && this.prevParams.Count == pxDataFieldList.Count)
      {
        bool flag = true;
        for (int index = 0; index < pxDataFieldList.Count; ++index)
        {
          if (!this.prevParams[index].Expression.Equals(pxDataFieldList[index].Expression))
          {
            flag = false;
            break;
          }
        }
        if (flag)
          return (string) null;
      }
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(aTable, pxDataFieldList.ToArray()))
      {
        string str = pxDataRecord.GetString(0);
        Guid? guid1 = pxDataRecord.GetGuid(1);
        Guid? guid2 = pxDataRecord.GetGuid(2);
        DateTime? dateTime3 = pxDataRecord.GetDateTime(3);
        DateTime? dateTime4 = pxDataRecord.GetDateTime(4);
        if (!string.IsNullOrEmpty(str))
        {
          DateTime? nullable1 = dateTime4;
          DateTime? nullable2 = dateTime3;
          Guid? nullable3 = (nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? guid2 : guid1;
          DateTime? nullable4 = dateTime4;
          nullable1 = dateTime3;
          DateTime? nullable5 = (nullable4.HasValue & nullable1.HasValue ? (nullable4.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? dateTime4 : dateTime3;
          if ((dateTime2 - nullable5.GetValueOrDefault()).TotalHours > 72.0)
          {
            deletedRefNbr = str;
            break;
          }
          GLTranDoc glTranDoc = PXResultset<GLTranDoc>.op_Implicit(PXSelectBase<GLTranDoc, PXSelectReadonly<GLTranDoc, Where<GLTranDoc.createdByID, Equal<Required<GLTranDoc.createdByID>>, And<GLTranDoc.createdDateTime, GreaterEqual<Required<GLTranDoc.createdDateTime>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) nullable3,
            (object) nullable5
          }));
          if (glTranDoc != null && !string.IsNullOrEmpty(glTranDoc.RefNbr))
          {
            deletedRefNbr = str;
            break;
          }
        }
      }
    }
    if (deletedRefNbr == null && this._importing)
    {
      this.prevTable = aTable;
      this.prevParams = pxDataFieldList;
    }
    return deletedRefNbr;
  }

  protected virtual void RestoreRefNbr(GLTranDoc row)
  {
    if (string.IsNullOrEmpty(row.TranType) || row.IsChildTran || string.IsNullOrEmpty(row.RefNbr))
      return;
    if (row.TranModule == "GL")
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (Batch)];
      Batch batch1 = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelectReadonly<Batch, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.TranModule,
        (object) row.RefNbr
      }));
      if (batch1 == null)
      {
        using (new PXReadDeletedScope(false))
          batch1 = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelectReadonly<Batch, Where<Batch.module, Equal<Required<Batch.module>>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) row.TranModule,
            (object) row.RefNbr
          }));
        if (batch1 != null)
        {
          PXDatabase.Update<Batch>(new PXDataFieldParam[4]
          {
            (PXDataFieldParam) new PXDataFieldAssign("DeletedDatabaseRecord", (PXDbType) 2, (object) false),
            (PXDataFieldParam) new PXDataFieldRestrict<Batch.module>((object) batch1.Module),
            (PXDataFieldParam) new PXDataFieldRestrict<Batch.batchNbr>((object) batch1.BatchNbr),
            (PXDataFieldParam) new PXDataFieldRestrict("DeletedDatabaseRecord", (PXDbType) 2, (object) true)
          });
          foreach (Batch batch2 in cach.Cached)
          {
            if (batch2.Module == batch1.Module && batch2.BatchNbr == batch1.BatchNbr)
            {
              batch2.tstamp = PXDatabase.SelectTimeStamp();
              break;
            }
          }
        }
      }
    }
    if (row.TranModule == "AP")
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.AP.APRegister)];
      PX.Objects.AP.APRegister apRegister1 = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelectReadonly<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.TranType,
        (object) row.RefNbr
      }));
      if (apRegister1 == null)
      {
        using (new PXReadDeletedScope(false))
          apRegister1 = PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelectReadonly<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) row.TranType,
            (object) row.RefNbr
          }));
        if (apRegister1 != null)
        {
          PXDatabase.Update<PX.Objects.AP.APRegister>(new PXDataFieldParam[4]
          {
            (PXDataFieldParam) new PXDataFieldAssign("DeletedDatabaseRecord", (PXDbType) 2, (object) false),
            (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AP.APRegister.docType>((object) apRegister1.DocType),
            (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AP.APRegister.refNbr>((object) apRegister1.RefNbr),
            (PXDataFieldParam) new PXDataFieldRestrict("DeletedDatabaseRecord", (PXDbType) 2, (object) true)
          });
          foreach (PX.Objects.AP.APRegister apRegister2 in cach.Cached)
          {
            if (apRegister2.DocType == apRegister1.DocType && apRegister2.RefNbr == apRegister1.RefNbr)
            {
              apRegister2.tstamp = PXDatabase.SelectTimeStamp();
              break;
            }
          }
        }
      }
    }
    if (row.TranModule == "AR")
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARRegister)];
      PX.Objects.AR.ARRegister arRegister1 = PXResultset<PX.Objects.AR.ARRegister>.op_Implicit(PXSelectBase<PX.Objects.AR.ARRegister, PXSelectReadonly<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.TranType,
        (object) row.RefNbr
      }));
      if (arRegister1 == null)
      {
        using (new PXReadDeletedScope(false))
          arRegister1 = PXResultset<PX.Objects.AR.ARRegister>.op_Implicit(PXSelectBase<PX.Objects.AR.ARRegister, PXSelectReadonly<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) row.TranType,
            (object) row.RefNbr
          }));
        if (arRegister1 != null)
        {
          PXDatabase.Update<PX.Objects.AR.ARRegister>(new PXDataFieldParam[4]
          {
            (PXDataFieldParam) new PXDataFieldAssign("DeletedDatabaseRecord", (PXDbType) 2, (object) false),
            (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AR.ARRegister.docType>((object) arRegister1.DocType),
            (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.AR.ARRegister.refNbr>((object) arRegister1.RefNbr),
            (PXDataFieldParam) new PXDataFieldRestrict("DeletedDatabaseRecord", (PXDbType) 2, (object) true)
          });
          foreach (PX.Objects.AR.ARRegister arRegister2 in cach.Cached)
          {
            if (arRegister2.DocType == arRegister1.DocType && arRegister2.RefNbr == arRegister1.RefNbr)
            {
              arRegister2.tstamp = PXDatabase.SelectTimeStamp();
              break;
            }
          }
        }
      }
    }
    if (!(row.TranModule == "CA"))
      return;
    PXCache cach1 = ((PXGraph) this).Caches[typeof (CAAdj)];
    CAAdj caAdj1 = PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelectReadonly<CAAdj, Where<CAAdj.adjTranType, Equal<Required<CAAdj.adjTranType>>, And<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.TranType,
      (object) row.RefNbr
    }));
    if (caAdj1 != null)
      return;
    using (new PXReadDeletedScope(false))
      caAdj1 = PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelectReadonly<CAAdj, Where<CAAdj.adjTranType, Equal<Required<CAAdj.adjTranType>>, And<CAAdj.adjRefNbr, Equal<Required<CAAdj.adjRefNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.TranType,
        (object) row.RefNbr
      }));
    if (caAdj1 == null)
      return;
    PXDatabase.Update<CAAdj>(new PXDataFieldParam[4]
    {
      (PXDataFieldParam) new PXDataFieldAssign("DeletedDatabaseRecord", (PXDbType) 2, (object) false),
      (PXDataFieldParam) new PXDataFieldRestrict<CAAdj.adjTranType>((object) caAdj1.AdjTranType),
      (PXDataFieldParam) new PXDataFieldRestrict<CAAdj.adjRefNbr>((object) caAdj1.AdjRefNbr),
      (PXDataFieldParam) new PXDataFieldRestrict("DeletedDatabaseRecord", (PXDbType) 2, (object) true)
    });
    foreach (CAAdj caAdj2 in cach1.Cached)
    {
      if (caAdj2.AdjTranType == caAdj1.AdjTranType && caAdj2.AdjRefNbr == caAdj1.AdjRefNbr)
      {
        caAdj2.tstamp = PXDatabase.SelectTimeStamp();
        break;
      }
    }
  }

  public virtual bool ProviderInsert(System.Type table, params PXDataFieldAssign[] pars)
  {
    if (this._skipExtensionTables && typeof (PXCacheExtension).IsAssignableFrom(table) && table.BaseType.IsGenericType)
    {
      System.Type genericArgument = table.BaseType.GetGenericArguments()[table.BaseType.GetGenericArguments().Length - 1];
      if ((typeof (PX.Objects.AP.APRegister).IsAssignableFrom(genericArgument) || typeof (PX.Objects.AR.ARRegister).IsAssignableFrom(genericArgument) || typeof (Batch).IsAssignableFrom(genericArgument) ? 1 : (typeof (CAAdj).IsAssignableFrom(genericArgument) ? 1 : 0)) != 0)
        return true;
    }
    return ((PXGraph) this).ProviderInsert(table, pars);
  }

  public virtual bool ProviderUpdate(System.Type table, params PXDataFieldParam[] pars)
  {
    if (this._skipExtensionTables && typeof (PXCacheExtension).IsAssignableFrom(table) && table.BaseType.IsGenericType)
    {
      System.Type genericArgument = table.BaseType.GetGenericArguments()[table.BaseType.GetGenericArguments().Length - 1];
      if ((typeof (PX.Objects.AP.APRegister).IsAssignableFrom(genericArgument) || typeof (PX.Objects.AR.ARRegister).IsAssignableFrom(genericArgument) || typeof (Batch).IsAssignableFrom(genericArgument) ? 1 : (typeof (CAAdj).IsAssignableFrom(genericArgument) ? 1 : 0)) != 0)
        return true;
    }
    return ((PXGraph) this).ProviderUpdate(table, pars);
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._Ledger == null)
      return;
    e.NewValue = (object) this._Ledger.BaseCuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_BaseCuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._Ledger == null)
      return;
    e.NewValue = (object) this._Ledger.BaseCuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current.GLRateTypeDflt;
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((PX.Objects.CM.CurrencyInfo) e.Row == null || ((PXSelectBase<GLDocBatch>) this.BatchModule).Current == null || !((PXSelectBase<GLDocBatch>) this.BatchModule).Current.DateEntered.HasValue)
      return;
    e.NewValue = (object) ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.DateEntered;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    PX.Objects.CM.CurrencyInfo row = (PX.Objects.CM.CurrencyInfo) e.Row;
    object curyId = (object) row.CuryID;
    object curyRateTypeId = (object) row.CuryRateTypeID;
    object curyMultDiv = (object) row.CuryMultDiv;
    object curyRate = (object) row.CuryRate;
    if (((PXSelectBase<GLDocBatch>) this.BatchModule).Current != null && !(((PXSelectBase<GLDocBatch>) this.BatchModule).Current.Module != "GL"))
      return;
    BqlCommand bqlCommand = (BqlCommand) new Select<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyID>>, And<PX.Objects.CM.CurrencyInfo.curyRateTypeID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyRateTypeID>>, And<PX.Objects.CM.CurrencyInfo.curyMultDiv, Equal<Required<PX.Objects.CM.CurrencyInfo.curyMultDiv>>, And<PX.Objects.CM.CurrencyInfo.curyRate, Equal<Required<PX.Objects.CM.CurrencyInfo.curyRate>>>>>>>();
    foreach (PX.Objects.CM.CurrencyInfo currencyInfo in sender.Cached)
    {
      if (sender.GetStatus((object) currencyInfo) != 3 && sender.GetStatus((object) currencyInfo) != 4)
      {
        if (bqlCommand.Meet(sender, (object) currencyInfo, new object[4]
        {
          curyId,
          curyRateTypeId,
          curyMultDiv,
          curyRate
        }))
        {
          sender.SetValue(e.Row, "CuryInfoID", (object) currencyInfo.CuryInfoID);
          sender.Delete((object) currencyInfo);
          break;
        }
      }
    }
  }

  protected virtual void GLDocBatch_LedgerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    GLDocBatch row = (GLDocBatch) e.Row;
    this._Ledger = (Ledger) PXSelectorAttribute.Select<GLDocBatch.ledgerID>(((PXSelectBase) this.BatchModule).Cache, (object) row);
    ((PXSelectBase) this.currencyinfo).Cache.SetDefaultExt<PX.Objects.CM.CurrencyInfo.baseCuryID>((object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current);
    ((PXSelectBase) this.currencyinfo).Cache.SetDefaultExt<PX.Objects.CM.CurrencyInfo.curyID>((object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current);
    sender.SetDefaultExt<GLDocBatch.curyID>((object) row);
    this._Ledger = (Ledger) null;
    foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
    {
      GLTranDoc copy = PXCache<GLTranDoc>.CreateCopy(PXResult<GLTranDoc>.op_Implicit(pxResult));
      copy.LedgerID = row.LedgerID;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.Update((object) copy);
    }
  }

  protected virtual void GLDocBatch_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._Ledger == null)
      return;
    e.NewValue = (object) this._Ledger.BaseCuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLDocBatch_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    GLDocBatch row = (GLDocBatch) e.Row;
    Decimal? nullable;
    if (!((PXSelectBase<GLSetup>) this.glsetup).Current.RequireControlTotal.Value || row.Status == "R")
    {
      nullable = row.CuryCreditTotal;
      if (nullable.HasValue)
      {
        nullable = row.CuryCreditTotal;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          cache.SetValue<GLDocBatch.curyControlTotal>((object) row, (object) row.CuryCreditTotal);
          goto label_8;
        }
      }
      nullable = row.CuryDebitTotal;
      if (nullable.HasValue)
      {
        nullable = row.CuryDebitTotal;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          cache.SetValue<GLDocBatch.curyControlTotal>((object) row, (object) row.CuryDebitTotal);
          goto label_8;
        }
      }
      cache.SetValue<GLDocBatch.curyControlTotal>((object) row, (object) 0M);
label_8:
      nullable = row.CreditTotal;
      if (nullable.HasValue)
      {
        nullable = row.CreditTotal;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          cache.SetValue<GLDocBatch.controlTotal>((object) row, (object) row.CreditTotal);
          goto label_15;
        }
      }
      nullable = row.DebitTotal;
      if (nullable.HasValue)
      {
        nullable = row.DebitTotal;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        {
          cache.SetValue<GLDocBatch.controlTotal>((object) row, (object) row.DebitTotal);
          goto label_15;
        }
      }
      cache.SetValue<GLDocBatch.controlTotal>((object) row, (object) 0M);
    }
label_15:
    bool flag = false;
    if (row.Status == "B")
    {
      nullable = row.CuryDebitTotal;
      Decimal? curyCreditTotal = row.CuryCreditTotal;
      if (!(nullable.GetValueOrDefault() == curyCreditTotal.GetValueOrDefault() & nullable.HasValue == curyCreditTotal.HasValue))
        flag = true;
      if (((PXSelectBase<GLSetup>) this.glsetup).Current.RequireControlTotal.Value)
      {
        curyCreditTotal = row.CuryCreditTotal;
        nullable = row.CuryControlTotal;
        if (!(curyCreditTotal.GetValueOrDefault() == nullable.GetValueOrDefault() & curyCreditTotal.HasValue == nullable.HasValue))
          cache.RaiseExceptionHandling<GLDocBatch.curyControlTotal>((object) row, (object) row.CuryControlTotal, (Exception) new PXSetPropertyException("The batch is not balanced. Review the debit and credit amounts."));
        else
          cache.RaiseExceptionHandling<GLDocBatch.curyControlTotal>((object) row, (object) row.CuryControlTotal, (Exception) null);
      }
    }
    if (flag)
      cache.RaiseExceptionHandling<GLDocBatch.curyDebitTotal>((object) row, (object) row.CuryDebitTotal, (Exception) new PXSetPropertyException("The batch is not balanced. Review the debit and credit amounts."));
    else
      cache.RaiseExceptionHandling<GLDocBatch.curyDebitTotal>((object) row, (object) row.CuryDebitTotal, (Exception) null);
  }

  protected virtual void GLDocBatch_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    bool flag = false;
    foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
    {
      if (PXResult<GLTranDoc>.op_Implicit(pxResult).DocCreated.GetValueOrDefault())
      {
        flag = true;
        break;
      }
    }
    if (flag)
      throw new PXException("This batch contains rows referencing created documents. It may not be deleted");
  }

  protected virtual void GLDocBatch_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is GLDocBatch row))
      return;
    if (((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current != null && !object.Equals((object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current.CuryInfoID, (object) row.CuryInfoID))
      ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current = (PX.Objects.CM.CurrencyInfo) null;
    if (((PXSelectBase<OrganizationFinPeriod>) this.finperiod).Current != null && !object.Equals((object) ((PXSelectBase<OrganizationFinPeriod>) this.finperiod).Current.FinPeriodID, (object) row.FinPeriodID))
      ((PXSelectBase<OrganizationFinPeriod>) this.finperiod).Current = (OrganizationFinPeriod) null;
    bool? nullable1 = row.Released;
    bool isBaseCalc = !nullable1.GetValueOrDefault();
    nullable1 = row.Posted;
    nullable1.GetValueOrDefault();
    nullable1 = row.Voided;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    int num1 = row.Module == "GL" ? 1 : 0;
    bool flag1 = row.Module == "CM";
    bool flag2 = cache.GetStatus(e.Row) == 2;
    ((PXAction) this.viewDocument).SetEnabled(true);
    PXUIFieldAttribute.SetVisible<GLDocBatch.curyID>(cache, (object) row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PX.Objects.CM.PXDBCurrencyAttribute.SetBaseCalc<GLDocBatch.curyCreditTotal>(cache, (object) row, isBaseCalc);
    PX.Objects.CM.PXDBCurrencyAttribute.SetBaseCalc<GLDocBatch.curyDebitTotal>(cache, (object) row, isBaseCalc);
    PX.Objects.CM.PXDBCurrencyAttribute.SetBaseCalc<GLDocBatch.curyControlTotal>(cache, (object) row, isBaseCalc);
    if (((num1 != 0 ? 0 : (!flag1 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      cache.AllowUpdate = false;
      cache.AllowDelete = false;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowDelete = false;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowUpdate = false;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowInsert = false;
    }
    else if (valueOrDefault || !isBaseCalc)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      cache.AllowDelete = false;
      cache.AllowUpdate = false;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowDelete = false;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowUpdate = false;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowInsert = false;
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<GLDocBatch.status>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLDocBatch.curyCreditTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLDocBatch.curyDebitTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLDocBatch.origBatchNbr>(cache, (object) row, false);
      cache.AllowDelete = true;
      cache.AllowUpdate = true;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowDelete = true;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowUpdate = true;
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.AllowInsert = true;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
      {
        GLTranDoc glTranDoc = PXResult<GLTranDoc>.op_Implicit(pxResult);
        flag4 = true;
        if (!flag3)
        {
          int? nullable2 = glTranDoc.BAccountID;
          if (!nullable2.HasValue)
          {
            nullable2 = glTranDoc.DebitAccountID;
            if (!nullable2.HasValue)
            {
              nullable2 = glTranDoc.CreditAccountID;
              if (!nullable2.HasValue)
                goto label_16;
            }
          }
          flag3 = true;
        }
label_16:
        nullable1 = glTranDoc.DocCreated;
        if (nullable1.GetValueOrDefault())
          flag5 = true;
        if (flag3 & flag5)
          break;
      }
      PXUIFieldAttribute.SetEnabled<GLDocBatch.curyID>(cache, (object) row, !flag3);
      PXUIFieldAttribute.SetEnabled<GLDocBatch.branchID>(cache, (object) row, !flag4);
      ((PXSelectBase) this.currencyinfo).Cache.AllowUpdate = !flag5;
    }
    PXUIFieldAttribute.SetEnabled<GLDocBatch.module>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<GLDocBatch.batchNbr>(cache, (object) row);
    PXCache pxCache = cache;
    GLDocBatch glDocBatch = row;
    nullable1 = ((PXSelectBase<GLSetup>) this.glsetup).Current.RequireControlTotal;
    int num2 = nullable1.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<GLDocBatch.curyControlTotal>(pxCache, (object) glDocBatch, num2 != 0);
    PXUIFieldAttribute.SetEnabled<GLDocBatch.ledgerID>(cache, (object) row, false);
  }

  protected virtual void GLDocBatch_FinPeriodID_ExceptionHandling(
    PXCache sender,
    PXExceptionHandlingEventArgs e)
  {
    if (!this._ExceptionHandling)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLDocBatch_DateEntered_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLDocBatch row = (GLDocBatch) e.Row;
    this.SetTransactionsChanged<GLTranDoc.tranDate>();
    PX.Objects.CM.CurrencyInfoAttribute.SetEffectiveDate<GLDocBatch.dateEntered>(cache, e);
  }

  protected virtual void GLDocBatch_FinPeriodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.SetTransactionsChanged();
  }

  protected virtual void GLDocBatch_Module_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "GL";
  }

  protected virtual void GLDocBatch_BranchID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<GLDocBatch.ledgerID>(e.Row);
  }

  protected virtual void GLTranDoc_TranCode_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null)
      return;
    if (row.IsChildTran)
    {
      GLTranDoc parent = this.FindParent(row);
      e.NewValue = sender.GetValue((object) parent, "TranCode");
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      GLTranDoc prevMasterTran = this.FindPrevMasterTran(row);
      if (prevMasterTran == null)
        return;
      e.NewValue = (object) prevMasterTran.TranCode;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void GLTranDoc_BranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = (object) parent.BranchID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_TranModule_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = sender.GetValue((object) parent, "TranModule");
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_TranType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = sender.GetValue((object) parent, "TranType");
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_TranDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = sender.GetValue((object) parent, "TranDate");
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = sender.GetValue((object) parent, "CuryID");
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_CuryInfoID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = sender.GetValue((object) parent, "CuryInfoID");
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_BAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    e.GetType();
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = (object) parent.BAccountID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = (object) parent.LocationID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_RefNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    e.GetType();
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = (object) parent.RefNbr;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_EntryTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = (object) parent.EntryTypeID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_ProjectID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = (object) parent.ProjectID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_DebitAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || string.IsNullOrEmpty(row.TranType))
      return;
    if (JournalWithSubEntry.IsDrCrAcctRequired(row, false))
    {
      e.NewValue = (object) this.FindDefaultAccount(row, false);
      row.DebitCashAccountID = this._cashAccountDebit != null ? this._cashAccountDebit.CashAccountID : new int?();
    }
    else
    {
      e.NewValue = (object) null;
      row.DebitCashAccountID = new int?();
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_DebitSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || string.IsNullOrEmpty(row.TranType))
      return;
    int? debitAccountId = row.DebitAccountID;
    e.NewValue = !debitAccountId.HasValue ? (object) null : (object) this.FindDefaultSubAccount(row, false);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_CreditAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || string.IsNullOrEmpty(row.TranType))
      return;
    if (JournalWithSubEntry.IsDrCrAcctRequired(row, true))
    {
      e.NewValue = (object) this.FindDefaultAccount(row, true);
      row.CreditCashAccountID = this._cashAccountCredit != null ? this._cashAccountCredit.CashAccountID : new int?();
    }
    else
    {
      e.NewValue = (object) null;
      row.CreditCashAccountID = new int?();
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_CreditSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    e.NewValue = (object) null;
    if (row == null || string.IsNullOrEmpty(row.TranType))
      return;
    int? creditAccountId = row.CreditAccountID;
    e.NewValue = !creditAccountId.HasValue ? (object) null : (object) this.FindDefaultSubAccount(row, true);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_TermsID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    e.NewValue = (object) null;
    if (row == null || string.IsNullOrEmpty(row.TranType))
      return;
    if (!row.IsChildTran)
    {
      int? baccountId;
      if (JournalWithSubEntry.IsAPInvoice(row))
      {
        baccountId = row.BAccountID;
        if (baccountId.HasValue)
        {
          PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Select(new object[1]
          {
            (object) row.BAccountID
          }));
          e.NewValue = (object) vendor.TermsID;
          ((CancelEventArgs) e).Cancel = true;
        }
      }
      if (!JournalWithSubEntry.IsARInvoice(row))
        return;
      baccountId = row.BAccountID;
      if (!baccountId.HasValue)
        return;
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) row.BAccountID
      }));
      e.NewValue = (object) customer.TermsID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
      ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<GLTranDoc, GLTranDoc.termsID> e)
  {
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<PX.Objects.AP.APInvoice.termsID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<GLTranDoc, GLTranDoc.termsID>>) e).Cache, (object) e.Row);
    GLTranDoc row = e.Row;
    if (!(row.DocType == "ADR") && !JournalWithSubEntry.IsARCreditMemo(row) || terms != null)
      return;
    row.CuryDiscAmt = new Decimal?(0M);
    row.DiscDate = new DateTime?();
    row.DueDate = new DateTime?();
  }

  protected virtual void GLTranDoc_ParentLineNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this._skipDefaulting)
      return;
    GLTranDoc row = (GLTranDoc) e.Row;
    if (!this._importing | sender.Graph.IsImport && row != null)
    {
      e.NewValue = (object) null;
      Dictionary<int, List<GLTranDoc>> dictionary = new Dictionary<int, List<GLTranDoc>>();
      GLTranDoc glTranDoc1 = (GLTranDoc) null;
      try
      {
        this._skipDefaulting = true;
        foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Select(Array.Empty<object>()))
        {
          GLTranDoc glTranDoc2 = PXResult<GLTranDoc>.op_Implicit(pxResult);
          if (glTranDoc2 != row)
          {
            int? nullable;
            if (!glTranDoc2.IsChildTran)
            {
              if (glTranDoc1 != null)
              {
                int? lineNbr = glTranDoc1.LineNbr;
                nullable = glTranDoc2.LineNbr;
                if (!(lineNbr.GetValueOrDefault() < nullable.GetValueOrDefault() & lineNbr.HasValue & nullable.HasValue))
                  goto label_10;
              }
              glTranDoc1 = glTranDoc2;
            }
label_10:
            if (!glTranDoc2.IsBalanced)
            {
              bool? split = glTranDoc2.Split;
              bool flag = false;
              if (!(split.GetValueOrDefault() == flag & split.HasValue))
              {
                int num;
                if (!glTranDoc2.IsChildTran)
                {
                  nullable = glTranDoc2.LineNbr;
                  num = nullable.Value;
                }
                else
                {
                  nullable = glTranDoc2.ParentLineNbr;
                  num = nullable.Value;
                }
                int key = num;
                if (!dictionary.ContainsKey(key))
                  dictionary[key] = new List<GLTranDoc>();
                dictionary[key].Add(glTranDoc2);
              }
            }
          }
        }
      }
      finally
      {
        this._skipDefaulting = false;
      }
      List<KeyValuePair<GLTranDoc, Decimal>> keyValuePairList = new List<KeyValuePair<GLTranDoc, Decimal>>();
      GLTranDoc glTranDoc3 = (GLTranDoc) null;
      foreach (List<GLTranDoc> glTranDocList in dictionary.Values)
      {
        Decimal num1 = 0M;
        GLTranDoc key = (GLTranDoc) null;
        int? nullable1;
        foreach (GLTranDoc glTranDoc4 in glTranDocList)
        {
          Decimal num2;
          if (JournalWithSubEntry._UseControlTotalEntry)
          {
            if (!glTranDoc4.IsChildTran)
            {
              Decimal? nullable2 = glTranDoc4.CuryTranTotal;
              Decimal num3 = nullable2.Value;
              nullable2 = glTranDoc4.CuryTaxTotal;
              Decimal valueOrDefault = nullable2.GetValueOrDefault();
              num2 = num3 - valueOrDefault;
            }
            else
              num2 = glTranDoc4.CuryTranAmt.Value;
          }
          else
            num2 = glTranDoc4.CuryTranAmt.Value;
          Decimal num4 = num2;
          nullable1 = glTranDoc4.DebitAccountID;
          if (nullable1.HasValue)
            num1 += num4;
          nullable1 = glTranDoc4.CreditAccountID;
          if (nullable1.HasValue)
            num1 -= num4;
          if (!glTranDoc4.IsChildTran && key == null)
            key = glTranDoc4;
        }
        if (key != null && num1 != 0M)
          keyValuePairList.Add(new KeyValuePair<GLTranDoc, Decimal>(key, num1));
        else if (key != null && key.TranModule == "GL" && (key.TranModule == row.TranModule || string.IsNullOrEmpty(row.TranModule)))
        {
          if (glTranDoc3 != null)
          {
            nullable1 = glTranDoc3.LineNbr;
            int? lineNbr = key.LineNbr;
            if (!(nullable1.GetValueOrDefault() < lineNbr.GetValueOrDefault() & nullable1.HasValue & lineNbr.HasValue))
              continue;
          }
          glTranDoc3 = key;
        }
      }
      if (glTranDoc3 != null)
      {
        int? lineNbr1 = glTranDoc3.LineNbr;
        int? lineNbr2 = glTranDoc1.LineNbr;
        if (lineNbr1.GetValueOrDefault() < lineNbr2.GetValueOrDefault() & lineNbr1.HasValue & lineNbr2.HasValue)
          glTranDoc3 = (GLTranDoc) null;
      }
      KeyValuePair<GLTranDoc, Decimal> keyValuePair;
      GLTranDoc glTranDoc5;
      if (keyValuePairList.Count <= 0)
      {
        glTranDoc5 = glTranDoc3;
      }
      else
      {
        keyValuePair = keyValuePairList[0];
        glTranDoc5 = keyValuePair.Key;
      }
      GLTranDoc aRow = glTranDoc5;
      Decimal num5;
      if (keyValuePairList.Count <= 0)
      {
        num5 = 0M;
      }
      else
      {
        keyValuePair = keyValuePairList[0];
        num5 = -keyValuePair.Value;
      }
      Decimal num6 = num5;
      if (aRow != null)
      {
        bool? nullable3 = JournalWithSubEntry.IsDebitTran(aRow);
        GLTranDoc glTranDoc6 = row;
        Decimal num7;
        if (nullable3.HasValue)
        {
          bool? nullable4 = nullable3;
          bool flag = false;
          num7 = nullable4.GetValueOrDefault() == flag & nullable4.HasValue ? num6 : -num6;
        }
        else
          num7 = 0M;
        Decimal? nullable5 = new Decimal?(num7);
        glTranDoc6.CuryBalanceAmt = nullable5;
        e.NewValue = (object) aRow.LineNbr;
      }
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_CuryTranAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!JournalWithSubEntry._UseControlTotalEntry)
    {
      if (this._skipDefaulting)
        return;
      GLTranDoc row = (GLTranDoc) e.Row;
      if (row != null && row.IsChildTran)
      {
        PXFieldDefaultingEventArgs defaultingEventArgs = e;
        Decimal? curyBalanceAmt = row.CuryBalanceAmt;
        Decimal num;
        if (!curyBalanceAmt.HasValue)
        {
          num = 0M;
        }
        else
        {
          curyBalanceAmt = row.CuryBalanceAmt;
          num = curyBalanceAmt.Value;
        }
        // ISSUE: variable of a boxed type
        __Boxed<Decimal> local = (ValueType) num;
        defaultingEventArgs.NewValue = (object) local;
        ((CancelEventArgs) e).Cancel = true;
      }
      else
        e.NewValue = (object) 0M;
    }
    else
      e.NewValue = (object) 0M;
  }

  protected virtual void GLTranDoc_CuryTranTotal_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
  }

  protected virtual void GLTranDoc_CuryTaxableAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran)
      return;
    e.NewValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_TaxableAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran)
      return;
    e.NewValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_CuryTaxAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran)
      return;
    e.NewValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_TaxAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran)
      return;
    e.NewValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_CuryInclTaxAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran)
      return;
    e.NewValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_InclTaxAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran)
      return;
    e.NewValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_CuryDiscAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran)
      return;
    e.NewValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_DiscAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran)
      return;
    e.NewValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_ExtRefNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !row.IsChildTran)
      return;
    GLTranDoc parent = this.FindParent(row);
    e.NewValue = (object) parent.ExtRefNbr;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_Split_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    e.NewValue = (object) false;
    if (row != null && row.IsChildTran)
      e.NewValue = (object) true;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_PaymentMethodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    this._isPMDefaulting = false;
    e.NewValue = (object) null;
    if (row != null && !row.IsChildTran)
    {
      if (JournalWithSubEntry.IsAPPayment(row.TranModule, row.TranType) || JournalWithSubEntry.IsAPInvoice(row.TranModule, row.TranType))
      {
        int? nullable = row.BAccountID;
        if (nullable.HasValue)
        {
          nullable = row.LocationID;
          if (nullable.HasValue)
          {
            PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) row.BAccountID,
              (object) row.LocationID
            }));
            e.NewValue = (object) location?.VPaymentMethodID;
          }
        }
      }
      else if (JournalWithSubEntry.IsARPayment(row.TranModule, row.TranType) || JournalWithSubEntry.IsARInvoice(row.TranModule, row.TranType))
      {
        PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<GLTranDoc.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.BAccountID
        }));
        e.NewValue = (object) customer?.DefPaymentMethodID;
      }
    }
    if (e.NewValue != null)
      this._isPMDefaulting = true;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_PMInstanceID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    e.NewValue = (object) null;
    if (row != null && !row.IsChildTran && (JournalWithSubEntry.IsARPayment(row) || JournalWithSubEntry.IsARInvoice(row)))
    {
      int? nullable1 = row.BAccountID;
      if (nullable1.HasValue && !string.IsNullOrEmpty(row.PaymentMethodID))
      {
        PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = (PX.Objects.AR.CustomerPaymentMethod) null;
        PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.PaymentMethodID
        }));
        if (paymentMethod != null)
        {
          bool? nullable2 = paymentMethod.IsActive;
          if (nullable2.GetValueOrDefault())
          {
            nullable2 = paymentMethod.ARIsProcessingRequired;
            if (!nullable2.GetValueOrDefault() || !JournalWithSubEntry.IsARPayment(row))
            {
              customerPaymentMethod = PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelectJoin<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.Customer.defPMInstanceID, Equal<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>>.Config>.Select((PXGraph) this, new object[2]
              {
                (object) row.BAccountID,
                (object) row.PaymentMethodID
              }));
              if (customerPaymentMethod == null)
                customerPaymentMethod = PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelect<PX.Objects.AR.CustomerPaymentMethod, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>, OrderBy<Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select((PXGraph) this, new object[2]
                {
                  (object) row.BAccountID,
                  (object) row.PaymentMethodID
                }));
            }
          }
        }
        PXFieldDefaultingEventArgs defaultingEventArgs = e;
        int? nullable3;
        if (customerPaymentMethod == null)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = customerPaymentMethod.PMInstanceID;
        // ISSUE: variable of a boxed type
        __Boxed<int?> local = (ValueType) nullable3;
        defaultingEventArgs.NewValue = (object) local;
      }
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    e.NewValue = (object) null;
    int? nullable;
    if (row != null && !row.IsChildTran)
    {
      nullable = row.BAccountID;
      if (nullable.HasValue)
      {
        nullable = row.LocationID;
        if (nullable.HasValue)
        {
          PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.Location).Select(new object[2]
          {
            (object) row.BAccountID,
            (object) row.LocationID
          }));
          if (JournalWithSubEntry.IsARInvoice(row))
          {
            e.NewValue = (object) location.CTaxZoneID;
            ((CancelEventArgs) e).Cancel = true;
          }
          if (JournalWithSubEntry.IsAPInvoice(row))
          {
            e.NewValue = (object) location.VTaxZoneID;
            ((CancelEventArgs) e).Cancel = true;
          }
        }
      }
    }
    if (row != null && !row.IsChildTran && row.TranModule == "CA" && !string.IsNullOrEmpty(row.EntryTypeID))
    {
      nullable = row.CashAccountID;
      if (nullable.HasValue)
      {
        CashAccountETDetail cashAccountEtDetail = this.GetCashAccountETDetail(row.EntryTypeID, row.CashAccountID);
        if (cashAccountEtDetail != null)
          e.NewValue = (object) cashAccountEtDetail.TaxZoneID;
      }
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  private CashAccountETDetail GetCashAccountETDetail(string entryTypeID, int? cashAccountID)
  {
    return PXResultset<CashAccountETDetail>.op_Implicit(PXSelectBase<CashAccountETDetail, PXSelect<CashAccountETDetail, Where<CashAccountETDetail.entryTypeID, Equal<Required<CashAccountETDetail.entryTypeID>>, And<CashAccountETDetail.cashAccountID, Equal<Required<CashAccountETDetail.cashAccountID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) entryTypeID,
      (object) cashAccountID
    }));
  }

  protected virtual void GLTranDoc_TaxCategoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    this._importing = sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null;
    if (!row.Split.GetValueOrDefault() || row.IsChildTran || !this._importing || row.TaxCategoryID == null || !(row.TaxCategoryID != (string) e.OldValue))
      return;
    sender.SetValueExt<GLTranDoc.taxCategoryID>((object) row, (object) null);
  }

  protected virtual void GLTranDoc_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    e.NewValue = (object) null;
    if (row != null)
    {
      bool? split1 = row.Split;
      bool flag1 = false;
      if (split1.GetValueOrDefault() == flag1 & split1.HasValue || row.IsChildTran)
      {
        int? nullable = row.BAccountID;
        if (nullable.HasValue)
        {
          nullable = row.LocationID;
          if (nullable.HasValue)
          {
            if (row.IsChildTran)
            {
              GLTranDoc prevSibling = this.FindPrevSibling(row);
              if (prevSibling != null)
              {
                e.NewValue = (object) prevSibling.TaxCategoryID;
                ((CancelEventArgs) e).Cancel = true;
              }
            }
            if (!((CancelEventArgs) e).Cancel)
            {
              PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.Location).Select(new object[2]
              {
                (object) row.BAccountID,
                (object) row.LocationID
              }));
              if (JournalWithSubEntry.IsARInvoice(row) || JournalWithSubEntry.IsAPInvoice(row))
              {
                bool? split2 = row.Split;
                bool flag2 = false;
                PX.Objects.TX.TaxZone taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXSelect<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Required<PX.Objects.TX.TaxZone.taxZoneID>>>>.Config>.Select((PXGraph) this, new object[1]
                {
                  (object) (split2.GetValueOrDefault() == flag2 & split2.HasValue ? row : this.FindParent(row)).TaxZoneID
                }));
                if (taxZone != null)
                  e.NewValue = (object) taxZone.DfltTaxCategoryID;
              }
            }
          }
        }
      }
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_DebitAccountID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    this._importing = sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null;
  }

  protected virtual void GLTranDoc_CreditAccountID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null)
      return;
    this._importing = sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null;
  }

  protected virtual void GLTranDoc_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (!e.ExternalCall || row == null)
      return;
    e.NewValue = (object) row.RefNbr;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDoc_RefNbr_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !(PXLongOperation.GetCustomInfoPersistent(((PXGraph) this).UID, true) is Dictionary<long, CAMessage> customInfoPersistent))
      return;
    CAMessage caMessage = (CAMessage) null;
    Dictionary<long, CAMessage> dictionary1 = customInfoPersistent;
    int? nullable1 = row.LineNbr;
    long key1 = (long) nullable1.Value;
    if (dictionary1.ContainsKey(key1))
    {
      Dictionary<long, CAMessage> dictionary2 = customInfoPersistent;
      nullable1 = row.LineNbr;
      long key2 = (long) nullable1.Value;
      caMessage = dictionary2[key2];
    }
    if (caMessage == null)
      return;
    string name = typeof (GLTranDoc.refNbr).Name;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    System.Type type = typeof (string);
    bool? nullable2 = new bool?(false);
    bool? nullable3 = new bool?();
    nullable1 = new int?();
    int? nullable4 = nullable1;
    nullable1 = new int?();
    int? nullable5 = nullable1;
    nullable1 = new int?();
    int? nullable6 = nullable1;
    string str = name;
    string message = caMessage.Message;
    PXErrorLevel errorLevel = caMessage.ErrorLevel;
    bool? nullable7 = new bool?();
    bool? nullable8 = new bool?();
    bool? nullable9 = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance(returnState, type, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, str, (string) null, (string) null, message, errorLevel, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    selectingEventArgs.ReturnState = (object) instance;
    e.IsAltered = true;
  }

  protected virtual void GLTranDoc_TranCode_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    GLDocBatch current = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current;
    if (row != null && !string.IsNullOrEmpty(row.RefNbr))
    {
      if (row.TranModule == "GL" && current != null)
      {
        int? lineNbr = row.LineNbr;
        int? lineCntr = current.LineCntr;
        if (lineNbr.GetValueOrDefault() == lineCntr.GetValueOrDefault() & lineNbr.HasValue == lineCntr.HasValue)
          return;
      }
      e.NewValue = (object) row.TranCode;
      throw new PXSetPropertyException("The reference number has been assigned - it's not possible to change Transaction Code.");
    }
  }

  protected virtual void GLTranDoc_DebitAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyAccount<GLTranDoc.debitAccountID>(sender, e, true);
  }

  protected virtual void GLTranDoc_CreditAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyAccount<GLTranDoc.creditAccountID>(sender, e, false);
  }

  private void VerifyAccountIDToBeNoControl<T>(
    PXCache cache,
    EventArgs e,
    GLTranDoc tran,
    bool debit)
    where T : IBqlField
  {
    PX.Objects.GL.GLTranCode glTranCode = PXResultset<PX.Objects.GL.GLTranCode>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTranCode, PXSelect<PX.Objects.GL.GLTranCode>.Config>.Search<PX.Objects.GL.GLTranCode.tranCode>((PXGraph) this, (object) tran.TranCode, Array.Empty<object>()));
    string module = glTranCode?.Module;
    string tranType = glTranCode?.TranType;
    bool flag = !debit;
    if ((!(module == "AR") || !(tranType == "CSL")) && (!(module == "AP") || !(tranType == "QCK")) && (!(module == "CA") || !(tranType == "CAE")) && (!(module == "GL") || !(tranType == "GLE")) && (!debit || (!(module == "AR") || !(tranType == "CRM")) && (!(module == "AP") || !(tranType == "INV") && !(tranType == "ACR"))) && (!flag || (!(module == "AR") || !(tranType == "INV") && !(tranType == "DRM")) && (!(module == "AP") || !(tranType == "ADR"))))
      return;
    AccountAttribute.VerifyAccountIsNotControl<T>(cache, e);
  }

  private void VerifyDebitAccountToBeControl(PXCache cache, EventArgs e, GLTranDoc tran)
  {
    PX.Objects.GL.GLTranCode glTranCode = PXResultset<PX.Objects.GL.GLTranCode>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTranCode, PXSelect<PX.Objects.GL.GLTranCode>.Config>.Search<PX.Objects.GL.GLTranCode.tranCode>((PXGraph) this, (object) tran.TranCode, Array.Empty<object>()));
    if (glTranCode == null || glTranCode.Module == null || glTranCode == null || glTranCode.TranType == null)
      return;
    string module = glTranCode.Module;
    string tranType = glTranCode.TranType;
    if ((!(module == "AR") || !(tranType == "INV") && !(tranType == "DRM")) && (!(module == "AP") || !(tranType == "ADR") && !(tranType == "CHK") && !(tranType == "PPM")))
      return;
    PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<Account.accountID>((PXGraph) this, (object) tran.DebitAccountID, Array.Empty<object>()));
    AccountAttribute.VerifyControlAccount<GLTranDoc.debitAccountID>(cache, e, module);
  }

  private void VerifyCreditAccountToBeControl(PXCache cache, EventArgs e, GLTranDoc tran)
  {
    PX.Objects.GL.GLTranCode glTranCode = PXResultset<PX.Objects.GL.GLTranCode>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTranCode, PXSelect<PX.Objects.GL.GLTranCode>.Config>.Search<PX.Objects.GL.GLTranCode.tranCode>((PXGraph) this, (object) tran.TranCode, Array.Empty<object>()));
    if (glTranCode == null || glTranCode.Module == null || glTranCode == null || glTranCode.TranType == null)
      return;
    string module = glTranCode.Module;
    string tranType = glTranCode.TranType;
    if ((!(module == "AR") || !(tranType == "CRM") && !(tranType == "PMT") && !(tranType == "PPM")) && (!(module == "AP") || !(tranType == "ACR") && !(tranType == "INV")))
      return;
    PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<Account.accountID>((PXGraph) this, (object) tran.CreditAccountID, Array.Empty<object>()));
    AccountAttribute.VerifyControlAccount<GLTranDoc.creditAccountID>(cache, e, module);
  }

  protected virtual void VerifyCreditSubAccount(PXCache cache, GLTranDoc tran)
  {
    if (tran == null || !tran.CreditSubID.HasValue || !tran.CreditAccountID.HasValue)
      return;
    bool cashExistForAccount = false;
    int? creditSubId = tran.CreditSubID;
    bool flag;
    if (this.IsMatching(this._cashAccountCredit, tran.BranchID, tran.CreditAccountID, creditSubId))
    {
      cashExistForAccount = true;
      flag = true;
    }
    else
    {
      string entryTypeId = string.IsNullOrEmpty(tran.EntryTypeID) || !tran.NeedsCreditCashAccount.GetValueOrDefault() ? (string) null : tran.EntryTypeID;
      this._cashAccountCredit = this.GetCashAccount(tran.BranchID.Value, tran.CreditAccountID.Value, creditSubId.Value, entryTypeId, out cashExistForAccount);
      flag = this._cashAccountCredit != null;
    }
    if (!cashExistForAccount || flag)
      return;
    Branch branch = (Branch) PXSelectorAttribute.Select<GLTranDoc.branchID>(cache, (object) tran);
    Account account = (Account) PXSelectorAttribute.Select<GLTranDoc.creditAccountID>(cache, (object) tran);
    Sub sub = PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) creditSubId
    }));
    cache.RaiseExceptionHandling<GLTranDoc.creditSubID>((object) tran, (object) sub.SubCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "Cash account doesn't exist for this branch, account and sub account ({0}, {1}, {2})", new object[3]
    {
      (object) branch.BranchCD,
      (object) account.AccountCD,
      (object) sub.SubCD
    }));
  }

  protected virtual void VerifyDeditSubAccount(PXCache cache, GLTranDoc tran)
  {
    if (tran == null || !tran.DebitSubID.HasValue || !tran.DebitAccountID.HasValue)
      return;
    bool cashExistForAccount = false;
    int? debitSubId = tran.DebitSubID;
    bool flag;
    if (this.IsMatching(this._cashAccountDebit, tran.BranchID, tran.DebitAccountID, debitSubId))
    {
      cashExistForAccount = true;
      flag = true;
    }
    else
    {
      string entryTypeId = string.IsNullOrEmpty(tran.EntryTypeID) || !tran.NeedsDebitCashAccount.GetValueOrDefault() ? (string) null : tran.EntryTypeID;
      this._cashAccountDebit = this.GetCashAccount(tran.BranchID.Value, tran.DebitAccountID.Value, debitSubId.Value, entryTypeId, out cashExistForAccount);
      flag = this._cashAccountDebit != null;
    }
    if (!cashExistForAccount || flag)
      return;
    Branch branch = (Branch) PXSelectorAttribute.Select<GLTranDoc.branchID>(cache, (object) tran);
    Account account = (Account) PXSelectorAttribute.Select<GLTranDoc.debitAccountID>(cache, (object) tran);
    Sub sub = PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) debitSubId
    }));
    cache.RaiseExceptionHandling<GLTranDoc.debitSubID>((object) tran, (object) sub.SubCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "Cash account doesn't exist for this branch, account and sub account ({0}, {1}, {2})", new object[3]
    {
      (object) branch.BranchCD,
      (object) account.AccountCD,
      (object) sub.SubCD
    }));
  }

  protected virtual void VerifyFinPeriod(PXCache cache, GLTranDoc row)
  {
    try
    {
      object finPeriodId = (object) row.FinPeriodID;
      cache.RaiseFieldVerifying<GLTranDoc.finPeriodID>((object) row, ref finPeriodId);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.RaiseExceptionHandling<GLTranDoc.tranDate>((object) row, (object) row.TranDate, (Exception) new PXSetPropertyException<GLTranDoc.tranDate>(((Exception) ex).Message));
    }
  }

  private void VerifyExtRefNumberLength(PXCache cache, GLTranDoc tran, object newValue)
  {
    PX.Objects.GL.GLTranCode glTranCode = ((PXSelectBase<PX.Objects.GL.GLTranCode>) this.GLTranCode).SelectSingle(new object[1]
    {
      (object) tran.TranCode
    });
    if (!(glTranCode?.Module == "GL") || !(glTranCode?.TranType == "GLE") || ((newValue ?? (object) string.Empty) as string).Length <= 15)
      return;
    cache.RaiseExceptionHandling<GLTranDoc.extRefNbr>((object) tran, newValue, (Exception) new PXSetPropertyException("The length of Ext. Ref. Number cannot exceed 15 characters for a GL transaction."));
  }

  protected virtual void GLTranDoc_PaymentMethodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    GLTranDoc row = e.Row as GLTranDoc;
    bool isImport = ((PXGraph) this).IsImport;
    try
    {
      if (row == null || string.IsNullOrEmpty(row.TranModule))
        return;
      string newValue = (string) e.NewValue;
      if (string.IsNullOrEmpty(newValue) || !JournalWithSubEntry.IsARPayment(row) && !JournalWithSubEntry.IsAPPayment(row))
        return;
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) newValue
      }));
      if (paymentMethod == null)
        return;
      if (!(this._isPMDefaulting & isImport))
      {
        bool? nullable;
        if (row.TranModule == "AP")
        {
          nullable = paymentMethod.APPrintChecks;
          if (!nullable.GetValueOrDefault())
          {
            nullable = paymentMethod.APCreateBatchPayment;
            if (!nullable.GetValueOrDefault())
              goto label_9;
          }
          throw new PXSetPropertyException("Payment Methods which have 'Print Check' or 'Create Batch Payment' setting set may not be used in this interface");
        }
label_9:
        if (!(row.TranModule == "AR"))
          return;
        nullable = paymentMethod.ARIsProcessingRequired;
        if (nullable.GetValueOrDefault())
          throw new PXSetPropertyException("Payment Methods which have 'Integrated Processing' setting set may not be used in this interface");
      }
      else
        ((CancelEventArgs) e).Cancel = true;
    }
    finally
    {
      this._isPMDefaulting = false;
    }
  }

  protected virtual void GLTranDoc_Split_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || row.IsChildTran || !JournalWithSubEntry.HasDocumentRow(row) || !(bool) e.NewValue)
      return;
    Decimal? curyTranTotal = row.CuryTranTotal;
    Decimal num = 0M;
    if (!(curyTranTotal.GetValueOrDefault() == num & curyTranTotal.HasValue))
      return;
    sender.RaiseExceptionHandling<GLTranDoc.split>(e.Row, (object) false, (Exception) new PXSetPropertyException("You must enter a document's Total Amount before you may add splits"));
  }

  protected virtual void GLTranDoc_CuryTranTotal_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row != null && JournalWithSubEntry.HasDocumentRow(row) && !row.IsChildTran && row.Split.GetValueOrDefault())
    {
      Decimal? newValue = (Decimal?) e.NewValue;
      if (newValue.HasValue)
      {
        Decimal? nullable = newValue;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() <= num & nullable.HasValue))
          return;
      }
      throw new PXSetPropertyException("You must enter a document's Total Amount before you may add splits");
    }
  }

  protected virtual void GLTranDoc_DebitAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null)
      return;
    try
    {
      this.VerifyDebitAccountToBeControl(sender, (EventArgs) e, row);
      this.VerifyAccountIDToBeNoControl<GLTranDoc.debitAccountID>(sender, (EventArgs) e, row, true);
    }
    catch (PXSetPropertyException ex)
    {
    }
    int? nullable1 = row.DebitAccountID;
    if (nullable1.HasValue)
    {
      if (this._cashAccountDebit != null)
      {
        nullable1 = this._cashAccountDebit.AccountID;
        int? debitAccountId = row.DebitAccountID;
        if (nullable1.GetValueOrDefault() == debitAccountId.GetValueOrDefault() & nullable1.HasValue == debitAccountId.HasValue)
          goto label_8;
      }
      string entryTypeId = !(row.TranModule == "CA") || !row.NeedsDebitCashAccount.GetValueOrDefault() ? (string) null : row.EntryTypeID;
      int? nullable2 = row.BranchID;
      int branch = nullable2.Value;
      nullable2 = row.DebitAccountID;
      int account = nullable2.Value;
      string entryTypeID = entryTypeId;
      bool flag;
      ref bool local = ref flag;
      this._cashAccountDebit = this.GetCashAccount(branch, account, entryTypeID, out local);
      row.DebitCashAccountID = this._cashAccountDebit != null ? this._cashAccountDebit.CashAccountID : new int?();
    }
    else
      row.DebitCashAccountID = new int?();
label_8:
    sender.SetDefaultExt<GLTranDoc.debitSubID>(e.Row);
    if (!(row.TranModule == "CA") || !(row.CADrCr == "D"))
      return;
    sender.SetDefaultExt<GLTranDoc.creditAccountID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.taxZoneID>(e.Row);
  }

  protected virtual void GLTranDoc_CreditAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null)
      return;
    this.VerifyCreditAccountToBeControl(sender, (EventArgs) e, row);
    this.VerifyAccountIDToBeNoControl<GLTranDoc.creditAccountID>(sender, (EventArgs) e, row, false);
    if (row.CreditAccountID.HasValue)
    {
      if (this._cashAccountCredit != null)
      {
        int? accountId = this._cashAccountCredit.AccountID;
        int? creditAccountId = row.CreditAccountID;
        if (accountId.GetValueOrDefault() == creditAccountId.GetValueOrDefault() & accountId.HasValue == creditAccountId.HasValue)
          goto label_5;
      }
      string entryTypeId = !(row.TranModule == "CA") || !row.NeedsCreditCashAccount.GetValueOrDefault() ? (string) null : row.EntryTypeID;
      int? nullable = row.BranchID;
      int branch = nullable.Value;
      nullable = row.CreditAccountID;
      int account = nullable.Value;
      string entryTypeID = entryTypeId;
      bool flag;
      ref bool local = ref flag;
      this._cashAccountCredit = this.GetCashAccount(branch, account, entryTypeID, out local);
label_5:
      row.CreditCashAccountID = this._cashAccountCredit != null ? this._cashAccountCredit.CashAccountID : new int?();
    }
    else
      row.CreditCashAccountID = new int?();
    sender.SetDefaultExt<GLTranDoc.creditSubID>(e.Row);
    if (!(row.TranModule == "CA") || !(row.CADrCr == "C"))
      return;
    sender.SetDefaultExt<GLTranDoc.debitAccountID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.taxZoneID>(e.Row);
  }

  protected virtual void GLTranDoc_BAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row != null)
      this._importing = sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null;
    // ISSUE: explicit non-virtual call
    int num1 = (e.Row is GLTranDoc row ? __nonvirtual (row.DocType) : (string) null) == "ADR" ? 1 : 0;
    APSetup current1 = ((PXSelectBase<APSetup>) this.apsetup).Current;
    bool? nullable;
    int num2;
    if (current1 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = current1.TermsInDebitAdjustments;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag1 = num2 != 0;
    ARSetup current2 = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    int num3;
    if (current2 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable = current2.TermsInCreditMemos;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag2 = num3 != 0;
    if ((num1 == 0 || flag1 ? (!JournalWithSubEntry.IsARCreditMemo(row) ? 1 : (flag2 ? 1 : 0)) : 0) != 0)
      sender.SetDefaultExt<GLTranDoc.termsID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.projectID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.locationID>(e.Row);
  }

  protected virtual void GLTranDoc_LocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row != null)
      this._importing = sender.GetValuePending(e.Row, PXImportAttribute.ImportFlag) != null;
    sender.SetDefaultExt<GLTranDoc.paymentMethodID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.debitAccountID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.debitSubID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.creditAccountID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.creditSubID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.taxZoneID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.taxCategoryID>(e.Row);
  }

  protected virtual void GLTranDoc_EntryTypeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    sender.SetDefaultExt<GLTranDoc.cADrCr>(e.Row);
    sender.SetDefaultExt<GLTranDoc.debitAccountID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.creditAccountID>(e.Row);
  }

  protected virtual void GLTranDoc_Split_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (e.Row == null || row.IsChildTran)
      return;
    bool flag;
    if ((JournalWithSubEntry.IsMixedType(row) ? 1 : 0) != 0)
    {
      flag = (JournalWithSubEntry.IsARInvoice(row) ? ARInvoiceType.DrCr(row.TranType) : APInvoiceType.DrCr(row.TranType)) == "D";
    }
    else
    {
      bool? nullable1 = JournalWithSubEntry.IsDebitType(row, true);
      if (nullable1.HasValue)
      {
        flag = nullable1.Value;
      }
      else
      {
        int? nullable2 = row.CreditAccountID;
        if (nullable2.HasValue)
        {
          nullable2 = row.DebitAccountID;
          if (nullable2.HasValue)
          {
            flag = false;
            goto label_9;
          }
        }
        nullable2 = row.CreditAccountID;
        flag = nullable2.HasValue;
      }
    }
label_9:
    if (row.Split.GetValueOrDefault())
    {
      if (flag)
        sender.SetValueExt<GLTranDoc.debitAccountID>((object) row, (object) null);
      else
        sender.SetValueExt<GLTranDoc.creditAccountID>((object) row, (object) null);
      if (JournalWithSubEntry.HasDocumentRow(row))
        sender.SetValueExt<GLTranDoc.curyTranAmt>((object) row, (object) 0M);
      sender.SetDefaultExt<GLTranDoc.taskID>((object) row);
    }
    else if (flag)
      sender.SetDefaultExt<GLTranDoc.debitAccountID>((object) row);
    else
      sender.SetDefaultExt<GLTranDoc.creditAccountID>((object) row);
    sender.SetDefaultExt<GLTranDoc.taxCategoryID>((object) row);
  }

  protected virtual void GLTranDoc_TranCode_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (e.ExternalCall && row.IsChildTran && (string) e.OldValue != row.TranCode)
    {
      row.ParentLineNbr = new int?();
      row.Split = new bool?(false);
      row.RefNbr = (string) null;
    }
    if (row == null)
      return;
    sender.SetDefaultExt<GLTranDoc.tranModule>(e.Row);
    sender.SetDefaultExt<GLTranDoc.tranType>(e.Row);
  }

  protected virtual void GLTranDoc_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row != null && (JournalWithSubEntry.IsARPayment(row) || JournalWithSubEntry.IsARInvoice(row)))
    {
      if (JournalWithSubEntry.IsARPayment(row))
      {
        sender.SetDefaultExt<GLTranDoc.creditAccountID>(e.Row);
        sender.SetDefaultExt<GLTranDoc.debitAccountID>(e.Row);
      }
      sender.SetDefaultExt<GLTranDoc.pMInstanceID>(e.Row);
    }
    if (row == null || !JournalWithSubEntry.IsAPPayment(row))
      return;
    sender.SetDefaultExt<GLTranDoc.creditAccountID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.debitAccountID>(e.Row);
  }

  protected virtual void GLTranDoc_PMInstanceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null || !JournalWithSubEntry.IsARPayment(row))
      return;
    sender.SetDefaultExt<GLTranDoc.creditAccountID>(e.Row);
    sender.SetDefaultExt<GLTranDoc.debitAccountID>(e.Row);
  }

  protected virtual void GLTranDoc_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if ((GLTranDoc) e.Row == null)
      return;
    sender.SetDefaultExt<GLTranDoc.taskID>(e.Row);
  }

  protected virtual void GLTranDoc_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row != null)
    {
      bool? nullable1;
      int num1;
      if (!string.IsNullOrEmpty(row.RefNbr))
      {
        nullable1 = row.DocCreated;
        num1 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      bool flag1 = row.TranModule == "AP" || row.TranModule == "GL";
      if (num1 != 0)
      {
        PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
      }
      else
      {
        bool flag2 = row.TranModule == "GL";
        bool flag3 = row.TranModule == "AP" || row.TranModule == "AR";
        bool flag4 = false;
        if (row.IsChildTran)
        {
          int? nullable2;
          if (flag3)
          {
            nullable2 = this.FindParent(row).ApplCount;
            flag4 = nullable2.GetValueOrDefault() > 0;
          }
          if (flag4)
          {
            PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
          }
          else
          {
            bool flag5 = row.TranModule == "GL";
            PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.curyTranAmt>(sender, (object) row, false);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.curyTranTotal>(sender, (object) row, true);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.tranDesc>(sender, (object) row, true);
            int? nullable3;
            int num2;
            if (sender.GetStatus((object) row) == 2)
            {
              nullable2 = row.LineNbr;
              nullable3 = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.LineCntr;
              num2 = nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue ? 1 : 0;
            }
            else
              num2 = 0;
            bool flag6 = num2 != 0;
            PXUIFieldAttribute.SetEnabled<GLTranDoc.tranCode>(sender, (object) row, flag2 & flag6 && row.CuryBalanceAmt.GetValueOrDefault() == 0M);
            nullable3 = row.DebitAccountID;
            bool flag7 = nullable3.HasValue;
            nullable3 = row.CreditAccountID;
            bool flag8 = nullable3.HasValue;
            nullable3 = row.DebitAccountID;
            int num3;
            if (nullable3.HasValue)
            {
              nullable1 = row.NeedsDebitCashAccount;
              num3 = nullable1.GetValueOrDefault() ? 1 : 0;
            }
            else
              num3 = 0;
            bool flag9 = num3 != 0;
            nullable3 = row.CreditAccountID;
            int num4;
            if (nullable3.HasValue)
            {
              nullable1 = row.NeedsCreditCashAccount;
              num4 = nullable1.GetValueOrDefault() ? 1 : 0;
            }
            else
              num4 = 0;
            bool flag10 = num4 != 0;
            bool flag11 = false;
            bool flag12 = false;
            if (row.TranModule == "GL" || row.TranModule == "CA")
            {
              nullable3 = row.DebitAccountID;
              if (nullable3.HasValue)
              {
                string str1;
                if (row.EntryTypeID != null)
                {
                  nullable1 = row.NeedsDebitCashAccount;
                  if (nullable1.GetValueOrDefault())
                  {
                    str1 = row.EntryTypeID;
                    goto label_26;
                  }
                }
                str1 = (string) null;
label_26:
                string str2 = str1;
                nullable3 = row.BranchID;
                int branch = nullable3.Value;
                nullable3 = row.DebitAccountID;
                int account = nullable3.Value;
                string entryTypeID = str2;
                ref bool local = ref flag11;
                flag9 = this.HasCashAccount(branch, account, entryTypeID, out local);
              }
              nullable3 = row.CreditAccountID;
              if (nullable3.HasValue)
              {
                string str3;
                if (row.EntryTypeID != null)
                {
                  nullable1 = row.NeedsCreditCashAccount;
                  if (nullable1.GetValueOrDefault())
                  {
                    str3 = row.EntryTypeID;
                    goto label_32;
                  }
                }
                str3 = (string) null;
label_32:
                string str4 = str3;
                nullable3 = row.BranchID;
                int branch = nullable3.Value;
                nullable3 = row.CreditAccountID;
                int account = nullable3.Value;
                string entryTypeID = str4;
                ref bool local = ref flag12;
                flag10 = this.HasCashAccount(branch, account, entryTypeID, out local);
              }
            }
            else
            {
              if (flag9)
              {
                nullable3 = row.BranchID;
                int branch = nullable3.Value;
                nullable3 = row.DebitAccountID;
                int account = nullable3.Value;
                ref bool local = ref flag11;
                this.HasCashAccount(branch, account, (string) null, out local);
              }
              if (flag10)
              {
                nullable3 = row.BranchID;
                int branch = nullable3.Value;
                nullable3 = row.CreditAccountID;
                int account = nullable3.Value;
                ref bool local = ref flag12;
                this.HasCashAccount(branch, account, (string) null, out local);
              }
            }
            bool flag13 = row.TranModule == "CA";
            bool flag14 = ((JournalWithSubEntry.IsARInvoice(row) ? 1 : (JournalWithSubEntry.IsAPInvoice(row) ? 1 : 0)) | (flag13 ? 1 : 0)) != 0;
            if (!flag8 && !flag7)
            {
              GLTranDoc parent = this.FindParent(row);
              int num5;
              if (parent != null)
              {
                nullable3 = parent.DebitAccountID;
                num5 = nullable3.HasValue ? 1 : 0;
              }
              else
                num5 = 0;
              flag8 = num5 != 0;
              int num6;
              if (parent != null)
              {
                nullable3 = parent.CreditAccountID;
                num6 = nullable3.HasValue ? 1 : 0;
              }
              else
                num6 = 0;
              flag7 = num6 != 0;
            }
            bool flag15 = !flag8 | flag5;
            bool flag16 = !flag7 | flag5;
            PXUIFieldAttribute.SetEnabled<GLTranDoc.debitAccountID>(sender, (object) row, flag15);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.debitSubID>(sender, (object) row, flag7 & flag15 && !(flag9 & flag11));
            PXUIFieldAttribute.SetEnabled<GLTranDoc.creditAccountID>(sender, (object) row, flag16);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.creditSubID>(sender, (object) row, flag8 & flag16 && !(flag10 & flag12));
            PXUIFieldAttribute.SetEnabled<GLTranDoc.taxZoneID>(sender, (object) row, false);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.taxCategoryID>(sender, (object) row, flag14);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.entryTypeID>(sender, (object) row, false);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.projectID>(sender, (object) row, flag1);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.taskID>(sender, (object) row, true);
          }
        }
        else
        {
          int? nullable4 = row.ApplCount;
          if (nullable4.GetValueOrDefault() > 0)
            PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
          else if (string.IsNullOrEmpty(row.TranType))
          {
            PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.tranCode>(sender, (object) row, true);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.tranType>(sender, (object) row, false);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.tranDate>(sender, (object) row, true);
          }
          else
          {
            nullable1 = row.Split;
            bool flag17 = nullable1.Value;
            bool flag18 = JournalWithSubEntry.IsAPPayment(row);
            bool flag19 = JournalWithSubEntry.IsARPayment(row);
            bool flag20 = JournalWithSubEntry.IsAPPrePayment(row);
            bool flag21 = JournalWithSubEntry.IsARInvoice(row);
            bool flag22 = JournalWithSubEntry.IsAPInvoice(row);
            bool flag23 = JournalWithSubEntry.IsARCreditMemo(row);
            bool flag24 = row.TranModule == "GL";
            bool flag25 = row.TranModule == "CA";
            PXUIFieldAttribute.SetEnabled(sender, (object) row, true);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.paymentMethodID>(sender, (object) row, flag19 | flag18 | flag22 | flag21);
            bool flag26 = false;
            if (flag19 | flag21 && !string.IsNullOrEmpty(row.PaymentMethodID))
            {
              nullable1 = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) row.PaymentMethodID
              })).IsAccountNumberRequired;
              flag26 = nullable1.GetValueOrDefault();
            }
            PXUIFieldAttribute.SetEnabled<GLTranDoc.pMInstanceID>(sender, (object) row, (flag19 | flag21) & flag26);
            nullable4 = row.CreditAccountID;
            bool hasValue1 = nullable4.HasValue;
            nullable4 = row.DebitAccountID;
            bool hasValue2 = nullable4.HasValue;
            nullable4 = row.DebitAccountID;
            int num7;
            if (nullable4.HasValue)
            {
              nullable1 = row.NeedsDebitCashAccount;
              num7 = nullable1.GetValueOrDefault() ? 1 : 0;
            }
            else
              num7 = 0;
            bool flag27 = num7 != 0;
            nullable4 = row.CreditAccountID;
            int num8;
            if (nullable4.HasValue)
            {
              nullable1 = row.NeedsCreditCashAccount;
              num8 = nullable1.GetValueOrDefault() ? 1 : 0;
            }
            else
              num8 = 0;
            bool flag28 = num8 != 0;
            bool flag29 = true;
            bool flag30 = true;
            bool flag31 = !string.IsNullOrEmpty(row.EntryTypeID);
            bool flag32 = !string.IsNullOrEmpty(row.RefNbr);
            if (row.TranModule == "GL" || row.TranModule == "CA")
            {
              nullable4 = row.DebitAccountID;
              if (nullable4.HasValue)
              {
                string str5;
                if (row.EntryTypeID != null)
                {
                  nullable1 = row.NeedsDebitCashAccount;
                  if (nullable1.GetValueOrDefault())
                  {
                    str5 = row.EntryTypeID;
                    goto label_64;
                  }
                }
                str5 = (string) null;
label_64:
                string str6 = str5;
                nullable4 = row.BranchID;
                int branch = nullable4.Value;
                nullable4 = row.DebitAccountID;
                int account = nullable4.Value;
                string entryTypeID = str6;
                ref bool local = ref flag29;
                flag27 = this.HasCashAccount(branch, account, entryTypeID, out local);
              }
              nullable4 = row.CreditAccountID;
              if (nullable4.HasValue)
              {
                string str7;
                if (row.EntryTypeID != null)
                {
                  nullable1 = row.NeedsCreditCashAccount;
                  if (nullable1.GetValueOrDefault())
                  {
                    str7 = row.EntryTypeID;
                    goto label_70;
                  }
                }
                str7 = (string) null;
label_70:
                string str8 = str7;
                nullable4 = row.BranchID;
                int branch = nullable4.Value;
                nullable4 = row.CreditAccountID;
                int account = nullable4.Value;
                string entryTypeID = str8;
                ref bool local = ref flag30;
                flag28 = this.HasCashAccount(branch, account, entryTypeID, out local);
              }
            }
            else
            {
              if (flag27)
              {
                nullable4 = row.BranchID;
                int branch = nullable4.Value;
                nullable4 = row.DebitAccountID;
                int account = nullable4.Value;
                ref bool local = ref flag29;
                this.HasCashAccount(branch, account, (string) null, out local);
              }
              if (flag28)
              {
                nullable4 = row.BranchID;
                int branch = nullable4.Value;
                nullable4 = row.CreditAccountID;
                int account = nullable4.Value;
                ref bool local = ref flag30;
                this.HasCashAccount(branch, account, (string) null, out local);
              }
            }
            PXUIFieldAttribute.SetEnabled<GLTranDoc.creditAccountID>(sender, (object) row, !flag17 | hasValue1 && !flag25 | flag31);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.creditSubID>(sender, (object) row, hasValue1 && !flag17 | hasValue1 && !(flag28 & flag30) && !flag25 | flag31);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.debitAccountID>(sender, (object) row, !flag17 | hasValue2 && !flag25 | flag31);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.debitSubID>(sender, (object) row, hasValue2 && !flag17 | hasValue2 && !(flag27 & flag29) && !flag25 | flag31);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.tranCode>(sender, (object) row, !flag17 && !flag32);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.tranType>(sender, (object) row, false);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.tranDate>(sender, (object) row, true);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.termsID>(sender, (object) row, flag21 | flag22 && !flag23);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.dueDate>(sender, (object) row, flag21 | flag22 && !flag23);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.discDate>(sender, (object) row, flag21 | flag22 && !flag23);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.curyDiscAmt>(sender, (object) row, flag21 | flag22 && !flag23);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.entryTypeID>(sender, (object) row, flag25);
            if (flag25)
              this.AdjustCAAccountsFields(sender, row);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.split>(sender, (object) row, flag21 | flag22 | flag25 | flag24);
            bool flag33 = row.TranModule == "AP" || row.TranModule == "AR";
            bool flag34 = ((JournalWithSubEntry.IsARInvoice(row) ? 1 : (JournalWithSubEntry.IsAPInvoice(row) ? 1 : 0)) | (flag25 ? 1 : 0)) != 0;
            PXUIFieldAttribute.SetEnabled<GLTranDoc.taxZoneID>(sender, (object) row, flag34);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.taxCategoryID>(sender, (object) row, flag34 && !flag17);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.bAccountID>(sender, (object) row, flag33);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.locationID>(sender, (object) row, flag33);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.refNbr>(sender, (object) row, false);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.projectID>(sender, (object) row, !flag17 || !flag1);
            PXUIFieldAttribute.SetEnabled<GLTranDoc.taskID>(sender, (object) row, true);
            nullable1 = row.Released;
            bool flag35 = false;
            if (nullable1.GetValueOrDefault() == flag35 & nullable1.HasValue)
            {
              int num9;
              if (((PXSelectBase<GLDocBatch>) this.BatchModule).Current != null)
              {
                nullable1 = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.Hold;
                bool flag36 = false;
                num9 = nullable1.GetValueOrDefault() == flag36 & nullable1.HasValue ? 1 : 0;
              }
              else
                num9 = 0;
              bool flag37 = num9 != 0;
              if (((!flag18 || flag20 ? 0 : (!flag22 ? 1 : 0)) & (flag37 ? 1 : 0)) != 0)
              {
                Decimal? curyUnappliedBal = row.CuryUnappliedBal;
                Decimal num10 = 0M;
                if (!(curyUnappliedBal.GetValueOrDefault() == num10 & curyUnappliedBal.HasValue) && row.TranType != "PPM")
                {
                  sender.RaiseExceptionHandling<GLTranDoc.curyUnappliedBal>((object) row, (object) row.CuryUnappliedBal, (Exception) new PXSetPropertyException("You have to apply full amount of this document before it may be released", (PXErrorLevel) 4));
                  goto label_85;
                }
              }
              sender.RaiseExceptionHandling<GLTranDoc.curyUnappliedBal>((object) row, (object) row.CuryUnappliedBal, (Exception) null);
label_85:
              if (flag20 | flag18)
              {
                PaymentMethodAccount paymentMethodAccount = PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
                {
                  (object) row.PaymentMethodID
                }));
                int num11;
                if (paymentMethodAccount == null)
                {
                  num11 = 0;
                }
                else
                {
                  nullable1 = paymentMethodAccount.APAutoNextNbr;
                  num11 = nullable1.GetValueOrDefault() ? 1 : 0;
                }
                if (num11 != 0 && !string.IsNullOrEmpty(row.ExtRefNbr))
                  sender.RaiseExceptionHandling<GLTranDoc.extRefNbr>((object) row, (object) row.ExtRefNbr, (Exception) new PXSetPropertyException((IBqlTable) row, "The system will update the AP Last Reference Number column on the Cash Accounts (CA202000) form with the following Ext. Ref. Number value: {0}.", (PXErrorLevel) 2, new object[1]
                  {
                    (object) row.ExtRefNbr
                  }));
              }
            }
          }
        }
      }
      PXUIFieldAttribute.SetEnabled<GLTranDoc.released>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLTranDoc.docCreated>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLTranDoc.curyTaxableAmt>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLTranDoc.curyTaxAmt>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLTranDoc.curyTaxTotal>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLTranDoc.curyDocTotal>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<GLTranDoc.curyInclTaxAmt>(sender, (object) row, false);
      if (JournalWithSubEntry._UseControlTotalEntry)
        PXUIFieldAttribute.SetEnabled<GLTranDoc.curyTranAmt>(sender, (object) row, false);
      int? parentOrganizationId = PXAccess.GetParentOrganizationID(row.BranchID);
      nullable1 = row.Released;
      DateTime? tranDate1;
      if (!nullable1.GetValueOrDefault())
      {
        IFinPeriodRepository periodRepository = this.FinPeriodRepository;
        string finPeriodId = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.FinPeriodID;
        tranDate1 = row.TranDate;
        DateTime date = tranDate1.Value;
        int? organizationID = parentOrganizationId;
        if (!periodRepository.IsDateWithinPeriod(finPeriodId, date, organizationID))
        {
          nullable1 = row.Released;
          if (!nullable1.GetValueOrDefault() && ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.FinPeriodID == null)
          {
            PXCache pxCache = sender;
            GLTranDoc glTranDoc = row;
            // ISSUE: variable of a boxed type
            __Boxed<DateTime?> tranDate2 = (ValueType) row.TranDate;
            object[] objArray = new object[1];
            tranDate1 = row.TranDate;
            ref DateTime? local = ref tranDate1;
            objArray[0] = (object) (local.HasValue ? local.GetValueOrDefault().ToShortDateString() : (string) null);
            PXSetPropertyException propertyException = new PXSetPropertyException(PXLocalizer.LocalizeFormat("The financial period for the {0} date is not defined in the system. To proceed, generate the necessary periods on the Financial Periods (GL201000) form.", objArray), (PXErrorLevel) 4);
            pxCache.RaiseExceptionHandling<GLTranDoc.tranDate>((object) glTranDoc, (object) tranDate2, (Exception) propertyException);
          }
        }
      }
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault() && ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.FinPeriodID != null)
      {
        IFinPeriodRepository periodRepository = this.FinPeriodRepository;
        string finPeriodId = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.FinPeriodID;
        tranDate1 = row.TranDate;
        DateTime date = tranDate1.Value;
        int? organizationID = parentOrganizationId;
        if (!periodRepository.IsDateWithinPeriod(finPeriodId, date, organizationID) && string.IsNullOrEmpty(PXUIFieldAttribute.GetError<GLTranDoc.tranDate>(sender, (object) row)))
          sender.RaiseExceptionHandling<GLTranDoc.tranDate>((object) row, (object) row.TranDate, (Exception) new PXSetPropertyException("The date is outside the range of the selected financial period.", (PXErrorLevel) 2));
      }
      bool flag38 = row.DocType == "ADR";
      if (flag38 || JournalWithSubEntry.IsARCreditMemo(row))
      {
        nullable1 = ((PXSelectBase<APSetup>) this.apsetup).Current.TermsInDebitAdjustments;
        bool valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable1 = ((PXSelectBase<ARSetup>) this.arsetup).Current.TermsInCreditMemos;
        bool valueOrDefault2 = nullable1.GetValueOrDefault();
        PXUIFieldAttribute.SetRequired<GLTranDoc.dueDate>(sender, (valueOrDefault1 & flag38 || JournalWithSubEntry.IsARCreditMemo(row) & valueOrDefault2) && row.TermsID != null);
        PXUIFieldAttribute.SetRequired<GLTranDoc.discDate>(sender, (valueOrDefault1 & flag38 || JournalWithSubEntry.IsARCreditMemo(row) & valueOrDefault2) && row.TermsID != null);
        PXCache pxCache1 = sender;
        GLTranDoc glTranDoc1 = row;
        int num12;
        if (valueOrDefault1 & flag38 || JournalWithSubEntry.IsARCreditMemo(row) & valueOrDefault2)
        {
          nullable1 = row.Released;
          bool flag39 = false;
          num12 = nullable1.GetValueOrDefault() == flag39 & nullable1.HasValue ? 1 : 0;
        }
        else
          num12 = 0;
        PXUIFieldAttribute.SetEnabled<GLTranDoc.termsID>(pxCache1, (object) glTranDoc1, num12 != 0);
        PXUIFieldAttribute.SetEnabled<GLTranDoc.dueDate>(sender, (object) row, row.TermsID != null);
        PXUIFieldAttribute.SetEnabled<GLTranDoc.discDate>(sender, (object) row, row.TermsID != null);
        bool flag40 = ((PX.Objects.CS.Terms) PXSelectorAttribute.Select<GLTranDoc.termsID>(sender, (object) row))?.InstallmentType == "M";
        PXCache pxCache2 = sender;
        GLTranDoc glTranDoc2 = row;
        int num13;
        if (!flag40 && row.TermsID != null)
        {
          nullable1 = row.Released;
          bool flag41 = false;
          num13 = nullable1.GetValueOrDefault() == flag41 & nullable1.HasValue ? 1 : 0;
        }
        else
          num13 = 0;
        PXUIFieldAttribute.SetEnabled<GLTranDoc.curyDiscAmt>(pxCache2, (object) glTranDoc2, num13 != 0);
      }
    }
    PXUIFieldAttribute.SetVisible<GLTranDoc.curyDocTotal>(sender, (object) null, false);
    PXUIFieldAttribute.SetVisible<GLTranDoc.curyInclTaxAmt>(sender, (object) null, false);
  }

  private void AdjustCAAccountsFields(PXCache cache, GLTranDoc transaction)
  {
    if (transaction.EntryTypeID == null)
      return;
    int num = !(transaction.CADrCr == "D") ? 0 : (!transaction.DebitAccountID.HasValue ? 1 : 0);
    if ((!(transaction.CADrCr == "C") ? 0 : (!transaction.CreditAccountID.HasValue ? 1 : 0)) != 0)
    {
      PXUIFieldAttribute.SetEnabled<GLTranDoc.debitAccountID>(cache, (object) transaction, false);
      PXUIFieldAttribute.SetEnabled<GLTranDoc.debitSubID>(cache, (object) transaction, false);
    }
    if (num == 0)
      return;
    PXUIFieldAttribute.SetEnabled<GLTranDoc.creditAccountID>(cache, (object) transaction, false);
    PXUIFieldAttribute.SetEnabled<GLTranDoc.creditSubID>(cache, (object) transaction, false);
  }

  protected virtual void GLTranDoc_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    GLTranDoc oldRow = (GLTranDoc) e.OldRow;
    this.VerifyExtRefNumberLength(sender, row, (object) row.ExtRefNbr);
    Decimal? nullable1;
    if ((!sender.ObjectsEqual<GLTranDoc.curyTranTotal>(e.Row, e.OldRow) || !sender.ObjectsEqual<GLTranDoc.curyTaxTotal>(e.Row, e.OldRow)) && row != null)
    {
      nullable1 = row.CuryTranTotal;
      if (nullable1.HasValue)
        sender.RaiseFieldUpdated<GLTranDoc.curyTranTotal>((object) row, (object) row.CuryTranTotal);
    }
    bool isImport = sender.Graph.IsImport;
    int? nullable2;
    if (e.ExternalCall && (e.Row == null || !this._importing) && sender.GetStatus((object) row) == 2 && !((GLTranDoc) e.OldRow).DebitAccountID.HasValue)
    {
      nullable2 = row.DebitAccountID;
      if (nullable2.HasValue)
      {
        GLTranDoc copy = PXCache<GLTranDoc>.CreateCopy(row);
        sender.RaiseRowUpdated((object) row, (object) copy);
      }
    }
    bool flag1 = false;
    bool? split;
    if (!row.IsChildTran)
    {
      split = row.Split;
      bool flag2 = false;
      if (split.GetValueOrDefault() == flag2 & split.HasValue)
      {
        split = oldRow.Split;
        if (split.GetValueOrDefault() && !this._isMassDelete)
          this.DeleteChildren(row);
      }
    }
    if (!row.IsChildTran)
    {
      split = row.Split;
      if (split.GetValueOrDefault())
      {
        List<int> intList = new List<int>();
        string[] strArray = new string[5]
        {
          "ExtRefNbr",
          "TranDate",
          "BAccountID",
          "LocationID",
          "ProjectID"
        };
        bool flag3 = row.TranModule == "AP" || row.TranModule == "GL";
        foreach (string str in strArray)
        {
          int fieldOrdinal = sender.GetFieldOrdinal(str);
          if (!(str == "ProjectID" & flag3) && !object.Equals(sender.GetValue((object) row, fieldOrdinal), sender.GetValue((object) oldRow, fieldOrdinal)))
            intList.Add(fieldOrdinal);
        }
        if (intList.Count > 0)
        {
          foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).SearchAll<Asc<GLTranDoc.parentLineNbr>>(new object[1]
          {
            (object) row.LineNbr
          }, Array.Empty<object>()))
          {
            GLTranDoc glTranDoc = PXResult<GLTranDoc>.op_Implicit(pxResult);
            bool flag4 = false;
            if (glTranDoc != row)
            {
              nullable2 = row.LineNbr;
              int? parentLineNbr = glTranDoc.ParentLineNbr;
              if (nullable2.GetValueOrDefault() == parentLineNbr.GetValueOrDefault() & nullable2.HasValue == parentLineNbr.HasValue)
              {
                GLTranDoc copy = (GLTranDoc) sender.CreateCopy((object) glTranDoc);
                foreach (int num in intList)
                {
                  object obj = sender.GetValue((object) row, num);
                  sender.SetValue((object) copy, num, obj);
                  flag4 = true;
                }
                if (flag4)
                {
                  object current = sender.Current;
                  sender.Update((object) copy);
                  sender.Current = current;
                }
              }
            }
          }
          flag1 = true;
        }
      }
    }
    Decimal? nullable3;
    if (!this._importing | isImport && row.IsChildTran && JournalWithSubEntry.HasDocumentRow(row))
    {
      nullable1 = row.CuryTranAmt;
      nullable3 = oldRow.CuryTranAmt;
      if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
      {
        GLTranDoc parent = this.FindParent(row);
        if (parent != null)
        {
          nullable3 = parent.CuryTranAmt;
          Decimal? curyTranAmt1 = row.CuryTranAmt;
          Decimal? curyTranAmt2 = oldRow.CuryTranAmt;
          nullable1 = curyTranAmt1.HasValue & curyTranAmt2.HasValue ? new Decimal?(curyTranAmt1.GetValueOrDefault() - curyTranAmt2.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable4 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          GLTranDoc copy = (GLTranDoc) sender.CreateCopy((object) parent);
          copy.CuryTranAmt = nullable4;
          object current = sender.Current;
          sender.Update((object) copy);
          sender.Current = current;
        }
      }
    }
    if (!this._importing | isImport && !JournalWithSubEntry.HasDocumentRow(row))
    {
      nullable1 = row.CuryTranTotal;
      nullable3 = oldRow.CuryTranTotal;
      if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
      {
        row.CuryTranAmt = row.CuryTranTotal;
        row.TranAmt = row.TranTotal;
      }
    }
    if (!flag1)
    {
      split = row.Split;
      if (split.GetValueOrDefault() && row.IsChildTran)
      {
        string[] strArray = new string[2]
        {
          "CuryTranAmt",
          "TaxCategoryID"
        };
        foreach (string str in strArray)
        {
          int fieldOrdinal = sender.GetFieldOrdinal(str);
          if (!object.Equals(sender.GetValue((object) row, fieldOrdinal), sender.GetValue((object) oldRow, fieldOrdinal)))
          {
            flag1 = true;
            break;
          }
        }
      }
    }
    if (!row.IsChildTran)
    {
      if (string.IsNullOrEmpty(row.RefNbr))
      {
        try
        {
          this.CreateRefNbr(row);
        }
        catch (JournalWithSubEntry.PXManualNumberingException ex)
        {
          sender.RaiseExceptionHandling<GLTranDoc.refNbr>((object) row, (object) row.RefNbr, (Exception) new PXSetPropertyException(ex.MessageNoPrefix, (PXErrorLevel) 5));
        }
        catch (PXOuterException ex)
        {
          sender.RaiseExceptionHandling<GLTranDoc.tranDate>((object) row, (object) row.TranDate, (Exception) new PXSetPropertyException(string.Join(". ", ex.InnerMessages), (PXErrorLevel) 4));
        }
        flag1 = true;
      }
    }
    if (flag1)
      ((PXSelectBase) this.GLTranModuleBatNbr).View.RequestRefresh();
    if (!row.IsChildTran)
    {
      nullable3 = row.CuryTranTotal;
      nullable1 = row.CuryDocTotal;
      if (!(nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue))
      {
        nullable1 = row.CuryTranTotal;
        Decimal num1 = nullable1.Value;
        nullable1 = row.CuryDocTotal;
        Decimal num2 = nullable1.Value;
        Decimal num3 = num1 - num2;
        sender.RaiseExceptionHandling<GLTranDoc.curyTranTotal>((object) row, (object) row.CuryTranTotal, (Exception) new PXSetPropertyException("This document is not balanced. The difference is {0}.", (PXErrorLevel) 2, new object[1]
        {
          (object) num3
        }));
        goto label_60;
      }
    }
    sender.RaiseExceptionHandling<GLTranDoc.curyTranTotal>((object) row, (object) row.CuryTranTotal, (Exception) null);
label_60:
    if (!this._isCacheSync && !row.IsChildTran)
    {
      if (row.TranModule == "AP")
      {
        JournalWithSubEntry.GLTranDocAP glTranDocAp = PXResultset<JournalWithSubEntry.GLTranDocAP>.op_Implicit(((PXSelectBase<JournalWithSubEntry.GLTranDocAP>) this.APPayments).Search<JournalWithSubEntry.GLTranDocAP.module, JournalWithSubEntry.GLTranDocAP.batchNbr, JournalWithSubEntry.GLTranDocAP.lineNbr>((object) row.Module, (object) row.BatchNbr, (object) row.LineNbr, Array.Empty<object>()));
        if (glTranDocAp != null)
        {
          sender.RestoreCopy((object) glTranDocAp, (object) row);
          ((PXSelectBase<JournalWithSubEntry.GLTranDocAP>) this.APPayments).Update(glTranDocAp);
        }
      }
      if (row.TranModule == "AR")
      {
        JournalWithSubEntry.GLTranDocAR glTranDocAr = PXResultset<JournalWithSubEntry.GLTranDocAR>.op_Implicit(((PXSelectBase<JournalWithSubEntry.GLTranDocAR>) this.ARPayments).Search<JournalWithSubEntry.GLTranDocAR.module, JournalWithSubEntry.GLTranDocAR.batchNbr, JournalWithSubEntry.GLTranDocAR.lineNbr>((object) row.Module, (object) row.BatchNbr, (object) row.LineNbr, Array.Empty<object>()));
        if (glTranDocAr != null)
        {
          sender.RestoreCopy((object) glTranDocAr, (object) row);
          ((PXSelectBase<JournalWithSubEntry.GLTranDocAR>) this.ARPayments).Update(glTranDocAr);
        }
      }
    }
    if (this._cashAccountDebit != null && this.IsMatching(this._cashAccountDebit, row.BranchID, row.DebitAccountID, row.DebitSubID))
      row.DebitCashAccountID = this._cashAccountDebit.CashAccountID;
    if (this._cashAccountCredit != null && this.IsMatching(this._cashAccountCredit, row.BranchID, row.CreditAccountID, row.CreditSubID))
      row.CreditCashAccountID = this._cashAccountCredit.CashAccountID;
    this.VerifyCreditSubAccount(sender, row);
    this.VerifyDeditSubAccount(sender, row);
  }

  protected virtual void GLTranDoc_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    bool flag = false;
    if (row.IsChildTran && JournalWithSubEntry.HasDocumentRow(row) && !((PXGraph) this).IsCopyPasteContext)
    {
      sender.RaiseFieldUpdated<GLTranDoc.curyTranAmt>((object) row, (object) 0M);
      GLTranDoc parent = this.FindParent(row);
      if (parent != null)
      {
        Decimal? curyTranAmt1 = parent.CuryTranAmt;
        Decimal? curyTranAmt2 = row.CuryTranAmt;
        Decimal? nullable = curyTranAmt1.HasValue & curyTranAmt2.HasValue ? new Decimal?(curyTranAmt1.GetValueOrDefault() + curyTranAmt2.GetValueOrDefault()) : new Decimal?();
        GLTranDoc copy = (GLTranDoc) sender.CreateCopy((object) parent);
        copy.CuryTranAmt = nullable;
        object current = sender.Current;
        sender.Update((object) copy);
        sender.Current = current;
      }
      flag = true;
    }
    if (!row.IsChildTran)
    {
      if (string.IsNullOrEmpty(row.RefNbr))
      {
        try
        {
          this.CreateRefNbr(row);
        }
        catch (JournalWithSubEntry.PXManualNumberingException ex)
        {
          sender.RaiseExceptionHandling<GLTranDoc.refNbr>((object) row, (object) row.RefNbr, (Exception) new PXSetPropertyException(ex.MessageNoPrefix, (PXErrorLevel) 5));
        }
        flag = true;
      }
    }
    if (flag)
      ((PXSelectBase) this.GLTranModuleBatNbr).View.RequestRefresh();
    if (this._cashAccountDebit != null && this.IsMatching(this._cashAccountDebit, row.BranchID, row.DebitAccountID, row.DebitSubID))
      row.DebitCashAccountID = this._cashAccountDebit.CashAccountID;
    if (this._cashAccountCredit != null && this.IsMatching(this._cashAccountCredit, row.BranchID, row.CreditAccountID, row.CreditSubID))
      row.CreditCashAccountID = this._cashAccountCredit.CashAccountID;
    this.VerifyCreditSubAccount(sender, row);
    this.VerifyDeditSubAccount(sender, row);
  }

  protected virtual void GLTranDoc_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row == null)
      return;
    if (row.IsChildTran && JournalWithSubEntry.HasDocumentRow(row))
    {
      if (!this._isMassDelete)
      {
        GLTranDoc parent = this.FindParent(row);
        if (parent != null)
        {
          Decimal? curyTranAmt1 = parent.CuryTranAmt;
          Decimal? curyTranAmt2 = row.CuryTranAmt;
          Decimal? nullable = curyTranAmt1.HasValue & curyTranAmt2.HasValue ? new Decimal?(curyTranAmt1.GetValueOrDefault() - curyTranAmt2.GetValueOrDefault()) : new Decimal?();
          GLTranDoc copy = (GLTranDoc) sender.CreateCopy((object) parent);
          copy.CuryTranAmt = nullable;
          sender.Update((object) copy);
        }
      }
      ((PXSelectBase) this.GLTranModuleBatNbr).View.RequestRefresh();
    }
    else
    {
      if (row.IsChildTran)
        return;
      this.DeleteChildren(row);
      this.DeleteDocRef(row);
      if (this._isCacheSync)
        return;
      if (row.TranModule == "AP")
      {
        JournalWithSubEntry.GLTranDocAP glTranDocAp = PXResultset<JournalWithSubEntry.GLTranDocAP>.op_Implicit(((PXSelectBase<JournalWithSubEntry.GLTranDocAP>) this.APPayments).Search<JournalWithSubEntry.GLTranDocAP.module, JournalWithSubEntry.GLTranDocAP.batchNbr, JournalWithSubEntry.GLTranDocAP.lineNbr>((object) row.Module, (object) row.BatchNbr, (object) row.LineNbr, Array.Empty<object>()));
        if (glTranDocAp != null)
          ((PXSelectBase<JournalWithSubEntry.GLTranDocAP>) this.APPayments).Delete(glTranDocAp);
      }
      if (!(row.TranModule == "AR"))
        return;
      JournalWithSubEntry.GLTranDocAR glTranDocAr = PXResultset<JournalWithSubEntry.GLTranDocAR>.op_Implicit(((PXSelectBase<JournalWithSubEntry.GLTranDocAR>) this.ARPayments).Search<JournalWithSubEntry.GLTranDocAR.module, JournalWithSubEntry.GLTranDocAR.batchNbr, JournalWithSubEntry.GLTranDocAR.lineNbr>((object) row.Module, (object) row.BatchNbr, (object) row.LineNbr, Array.Empty<object>()));
      if (glTranDocAr == null)
        return;
      ((PXSelectBase<JournalWithSubEntry.GLTranDocAR>) this.ARPayments).Delete(glTranDocAr);
    }
  }

  protected virtual void GLTranDoc_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    if (row != null && !string.IsNullOrEmpty(row.RefNbr) && row.DocCreated.GetValueOrDefault())
      throw new PXException("This row may not be deleted - a it refers an existing document");
  }

  protected virtual void GLTranDoc_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    GLDocBatch current = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current;
    if (e.Operation != 2 && e.Operation != 1)
      return;
    this.VerifyDebitAccountToBeControl(sender, (EventArgs) e, row);
    this.VerifyAccountIDToBeNoControl<GLTranDoc.debitAccountID>(sender, (EventArgs) e, row, true);
    this.VerifyCreditAccountToBeControl(sender, (EventArgs) e, row);
    this.VerifyAccountIDToBeNoControl<GLTranDoc.creditAccountID>(sender, (EventArgs) e, row, false);
    this.VerifyExtRefNumberLength(sender, row, (object) row.ExtRefNbr);
    if (!row.IsChildTran)
    {
      foreach (PXResult<GLTranDoc> pxResult in PXSelectBase<GLTranDoc, PXSelect<GLTranDoc, Where<GLTranDoc.tranModule, Equal<Required<GLTranDoc.tranModule>>, And<GLTranDoc.tranType, Equal<Required<GLTranDoc.tranType>>, And<GLTranDoc.refNbr, Equal<Required<GLTranDoc.refNbr>>, And<GLTranDoc.parentLineNbr, IsNull>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.TranModule,
        (object) row.TranType,
        (object) row.RefNbr
      }))
      {
        GLTranDoc glTranDoc = PXResult<GLTranDoc>.op_Implicit(pxResult);
        if (!(glTranDoc.Module != row.Module) && !(glTranDoc.BatchNbr != row.BatchNbr))
        {
          int? lineNbr1 = glTranDoc.LineNbr;
          int? lineNbr2 = row.LineNbr;
          if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
            continue;
        }
        PXException pxException = new PXException("Reference number generated for this document is invalid. Try to re-enter the line. If this error appears again, please contact support team.");
        sender.RaiseExceptionHandling<GLTranDoc.refNbr>((object) row, (object) null, (Exception) pxException);
        throw pxException;
      }
    }
    int? nullable1;
    bool? nullable2;
    if (!string.IsNullOrEmpty(row.TranType))
    {
      bool? nullable3 = JournalWithSubEntry.IsDebitType(row, true);
      bool flag1;
      bool flag2;
      if (row.IsChildTran)
      {
        if (nullable3.HasValue)
        {
          flag1 = nullable3.Value;
          flag2 = !nullable3.Value;
        }
        else
        {
          nullable1 = row.DebitAccountID;
          if (!nullable1.HasValue)
          {
            nullable1 = row.CreditAccountID;
            if (!nullable1.HasValue)
            {
              GLTranDoc parent = this.FindParent(row);
              nullable1 = parent.CreditAccountID;
              flag1 = nullable1.HasValue;
              nullable1 = parent.DebitAccountID;
              flag2 = nullable1.HasValue;
              goto label_24;
            }
          }
          nullable1 = row.DebitAccountID;
          flag1 = nullable1.HasValue;
          nullable1 = row.CreditAccountID;
          flag2 = nullable1.HasValue;
        }
      }
      else
      {
        nullable2 = row.Split;
        bool flag3 = nullable2.Value;
        if (nullable3.HasValue)
        {
          nullable2 = nullable3;
          bool flag4 = false;
          flag1 = nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue || nullable3.Value && !flag3;
          flag2 = nullable3.Value || !nullable3.Value && !flag3;
        }
        else if (!flag3)
        {
          flag1 = flag2 = true;
        }
        else
        {
          nullable1 = row.CreditAccountID;
          flag1 = !nullable1.HasValue;
          nullable1 = row.DebitAccountID;
          flag2 = !nullable1.HasValue;
        }
      }
label_24:
      PXDefaultAttribute.SetPersistingCheck<GLTranDoc.debitAccountID>(sender, (object) row, flag1 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<GLTranDoc.debitSubID>(sender, (object) row, flag1 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<GLTranDoc.creditAccountID>(sender, (object) row, flag2 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<GLTranDoc.creditSubID>(sender, (object) row, flag2 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    }
    bool flag5 = !row.IsChildTran && (JournalWithSubEntry.IsARPayment(row) || JournalWithSubEntry.IsAPPayment(row));
    bool flag6 = !row.IsChildTran && (row.TranModule == "AP" || row.TranModule == "AR");
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.paymentMethodID>(sender, e.Row, flag5 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    bool flag7 = false;
    if (!row.IsChildTran && JournalWithSubEntry.IsARPayment(row) && !string.IsNullOrEmpty(row.PaymentMethodID))
    {
      nullable2 = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.PaymentMethodID
      })).IsAccountNumberRequired;
      flag7 = nullable2.GetValueOrDefault();
    }
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.pMInstanceID>(sender, e.Row, flag7 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.bAccountID>(sender, e.Row, flag6 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.locationID>(sender, e.Row, flag6 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.entryTypeID>(sender, e.Row, row.TranModule == "CA" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    bool flag8 = false;
    bool flag9 = false;
    if (!row.IsChildTran)
    {
      switch (row.TranModule)
      {
        case "AP":
          nullable2 = ((PXSelectBase<APSetup>) this.apsetup).Current.RequireVendorRef;
          flag8 = nullable2.GetValueOrDefault();
          break;
        case "AR":
          flag8 = JournalWithSubEntry.IsARPayment(row);
          break;
        case "CA":
          flag8 = true;
          break;
      }
      if (row.TranModule != "GL")
      {
        Decimal? curyTranTotal = row.CuryTranTotal;
        Decimal? curyDocTotal = row.CuryDocTotal;
        if (!(curyTranTotal.GetValueOrDefault() == curyDocTotal.GetValueOrDefault() & curyTranTotal.HasValue == curyDocTotal.HasValue))
        {
          Decimal? nullable4 = row.CuryTranTotal;
          Decimal num1 = nullable4.Value;
          nullable4 = row.CuryDocTotal;
          Decimal num2 = nullable4.Value;
          Decimal num3 = num1 - num2;
          sender.RaiseExceptionHandling<GLTranDoc.curyTranTotal>((object) row, (object) row.CuryTranTotal, (Exception) new PXSetPropertyException("This document is not balanced. The difference is {0}.", (PXErrorLevel) 4, new object[1]
          {
            (object) num3
          }));
        }
      }
      else
      {
        nullable2 = row.Split;
        if (nullable2.GetValueOrDefault())
        {
          Decimal signedAmount = JournalWithSubEntry.GetSignedAmount(row);
          List<GLTranDoc> glTranDocList = new List<GLTranDoc>();
          foreach (PXResult<GLTranDoc> pxResult in PXSelectBase<GLTranDoc, PXSelect<GLTranDoc, Where<GLTranDoc.module, Equal<Required<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Required<GLTranDoc.batchNbr>>, And<GLTranDoc.parentLineNbr, Equal<Required<GLTranDoc.parentLineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) row.Module,
            (object) row.BatchNbr,
            (object) row.LineNbr
          }))
          {
            GLTranDoc aRow = PXResult<GLTranDoc>.op_Implicit(pxResult);
            nullable1 = aRow.ParentLineNbr;
            int? lineNbr = row.LineNbr;
            if (nullable1.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable1.HasValue == lineNbr.HasValue)
              signedAmount += JournalWithSubEntry.GetSignedAmount(aRow);
          }
          if (signedAmount != 0M)
            sender.RaiseExceptionHandling<GLTranDoc.curyTranTotal>((object) row, (object) row.CuryTranTotal, (Exception) new PXSetPropertyException("This document is not balanced. The difference is {0}.", (PXErrorLevel) 4, new object[1]
            {
              (object) signedAmount
            }));
        }
      }
      flag9 = (JournalWithSubEntry.IsAPInvoice(row) || JournalWithSubEntry.IsARInvoice(row)) && !JournalWithSubEntry.IsARCreditMemo(row) && row.DocType != "ADR";
    }
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.extRefNbr>(sender, e.Row, flag8 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.termsID>(sender, e.Row, flag9 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.dueDate>(sender, e.Row, flag9 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.discDate>(sender, e.Row, flag9 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<GLTranDoc.curyDiscAmt>(sender, e.Row, flag9 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (current != null && row != null)
    {
      int? nullable5 = row.ProjectID;
      if (nullable5.HasValue && !ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      {
        nullable5 = row.TaskID;
        if (nullable5.HasValue)
        {
          int num4 = row.TranModule == "GL" ? 1 : 0;
          int num5 = row.TranModule == "CA" ? 1 : 0;
          PXErrorLevel pxErrorLevel = (PXErrorLevel) 4;
          nullable5 = row.CreditAccountID;
          Account account;
          if (nullable5.HasValue && !this.IsAccountValidForProject(row, row.CreditAccountID, false, out account))
            sender.RaiseExceptionHandling<GLTranDoc.creditAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", pxErrorLevel, new object[1]
            {
              (object) account.AccountCD
            }));
          nullable5 = row.DebitAccountID;
          if (nullable5.HasValue && !this.IsAccountValidForProject(row, row.DebitAccountID, true, out account))
            sender.RaiseExceptionHandling<GLTranDoc.debitAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", pxErrorLevel, new object[1]
            {
              (object) account.AccountCD
            }));
        }
      }
    }
    this.VerifyCreditSubAccount(sender, row);
    this.VerifyDeditSubAccount(sender, row);
    this.VerifyFinPeriod(sender, row);
  }

  protected virtual void GLTranDoc_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    GLDocBatch current = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current;
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1 || e.TranStatus != null || (e.Operation & 3) != 2 || row.IsChildTran)
      return;
    this.RestoreRefNbr(row);
  }

  protected virtual Account GetAccount(int? accountID)
  {
    if (!accountID.HasValue)
      return (Account) null;
    return PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) accountID
    }));
  }

  protected bool IsAccountValidForProject(
    GLTranDoc row,
    int? accountID,
    bool isDebitAccount,
    out Account account)
  {
    bool flag1 = row.TranModule == "GL";
    bool flag2 = row.TranModule == "CA";
    account = (Account) null;
    if (!accountID.HasValue)
      return true;
    bool? nullable;
    if (((JournalWithSubEntry.IsAPInvoice(row) ? 1 : (JournalWithSubEntry.IsARInvoice(row) ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
    {
      nullable = JournalWithSubEntry.IsDebitType(row);
      bool flag3 = isDebitAccount;
      if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        goto label_8;
    }
    if (!JournalWithSubEntry.IsAPPayment(row))
    {
      if (JournalWithSubEntry.IsARPayment(row))
      {
        nullable = JournalWithSubEntry.IsDebitType(row);
        bool flag4 = !isDebitAccount;
        if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
          goto label_8;
      }
      if (!flag1)
        goto label_10;
    }
label_8:
    account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) accountID
    }));
    if (account != null && !account.AccountGroupID.HasValue)
      return false;
label_10:
    return true;
  }

  protected virtual bool IsAccountValidForProject(
    GLTranDoc row,
    Account account,
    bool isDebitAccount)
  {
    bool flag1 = row.TranModule == "GL";
    bool flag2 = row.TranModule == "CA";
    if (account == null)
      return true;
    bool? nullable;
    if (((JournalWithSubEntry.IsAPInvoice(row) ? 1 : (JournalWithSubEntry.IsARInvoice(row) ? 1 : 0)) | (flag2 ? 1 : 0)) != 0)
    {
      nullable = JournalWithSubEntry.IsDebitType(row);
      bool flag3 = isDebitAccount;
      if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        goto label_8;
    }
    if (!JournalWithSubEntry.IsAPPayment(row))
    {
      if (JournalWithSubEntry.IsARPayment(row))
      {
        nullable = JournalWithSubEntry.IsDebitType(row);
        bool flag4 = !isDebitAccount;
        if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
          goto label_8;
      }
      if (!flag1)
        goto label_10;
    }
label_8:
    if (!account.AccountGroupID.HasValue)
      return false;
label_10:
    return true;
  }

  protected virtual bool IsActiveAccount(Account account)
  {
    return ((bool?) account?.Active).GetValueOrDefault();
  }

  private bool IsPendingValue<TField>(PXCache sender, PXFieldVerifyingEventArgs e) where TField : IBqlField
  {
    GLTranDoc row = e.Row as GLTranDoc;
    object valuePending = sender.GetValuePending<TField>((object) row);
    return string.IsNullOrEmpty(valuePending as string) || valuePending == e.NewValue;
  }

  protected virtual void VerifyAccount<TField>(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    bool isDebitAccount)
    where TField : IBqlField
  {
    GLTranDoc row = e.Row as GLTranDoc;
    if (((PXSelectBase<GLDocBatch>) this.BatchModule).Current != null && row != null)
    {
      Account account = this.GetAccount((int?) e.NewValue);
      int? nullable = row.ProjectID;
      if (nullable.HasValue && !ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      {
        nullable = row.TaskID;
        if (nullable.HasValue && !this.IsAccountValidForProject(row, account, isDebitAccount))
        {
          if (((PXGraph) this).IsImport && this.IsPendingValue<TField>(sender, e))
            throw new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", new object[1]
            {
              (object) account.AccountCD
            });
          PXErrorLevel pxErrorLevel = (PXErrorLevel) 4;
          sender.RaiseExceptionHandling<TField>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", pxErrorLevel, new object[1]
          {
            (object) account.AccountCD
          }));
        }
      }
      if (account != null && !this.IsActiveAccount(account))
      {
        if (((PXGraph) this).IsImport && this.IsPendingValue<TField>(sender, e))
          throw new PXSetPropertyException("Account is inactive.");
        PXErrorLevel pxErrorLevel = (PXErrorLevel) 4;
        sender.RaiseExceptionHandling<TField>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Account is inactive.", pxErrorLevel));
      }
    }
    ((CancelEventArgs) e).Cancel = ((CancelEventArgs) e).Cancel || ((PXGraph) this).IsImport && !string.IsNullOrEmpty(sender.GetValuePending<TField>((object) row) as string);
  }

  protected void DeleteChildren(GLTranDoc aRow)
  {
    if (aRow == null)
      return;
    if (aRow.IsChildTran)
      return;
    try
    {
      this._isMassDelete = true;
      foreach (PXResult<GLTranDoc> pxResult in ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).SearchAll<Asc<GLTranDoc.parentLineNbr>>(new object[1]
      {
        (object) aRow.LineNbr
      }, Array.Empty<object>()))
      {
        GLTranDoc glTranDoc = PXResult<GLTranDoc>.op_Implicit(pxResult);
        if (glTranDoc != aRow)
        {
          int? lineNbr = aRow.LineNbr;
          int? parentLineNbr = glTranDoc.ParentLineNbr;
          if (lineNbr.GetValueOrDefault() == parentLineNbr.GetValueOrDefault() & lineNbr.HasValue == parentLineNbr.HasValue)
            ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Delete(glTranDoc);
        }
      }
    }
    finally
    {
      this._isMassDelete = false;
    }
  }

  protected void DeleteDocRef(GLTranDoc aRow)
  {
    if (aRow == null || string.IsNullOrEmpty(aRow.RefNbr))
      return;
    JournalWithSubEntry.RefDocKey refDocKey = new JournalWithSubEntry.RefDocKey();
    refDocKey.Copy(aRow);
    ((PXSelectBase<JournalWithSubEntry.RefDocKey>) this.deletedKeys).Insert(refDocKey);
  }

  public GLTranDoc FindParent(GLTranDoc aTran)
  {
    if (!aTran.ParentLineNbr.HasValue)
      return (GLTranDoc) null;
    if (this._parent != null)
    {
      int? lineNbr = this._parent.LineNbr;
      int? parentLineNbr = aTran.ParentLineNbr;
      if (lineNbr.GetValueOrDefault() == parentLineNbr.GetValueOrDefault() & lineNbr.HasValue == parentLineNbr.HasValue)
        goto label_5;
    }
    this._parent = PXResultset<GLTranDoc>.op_Implicit(((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Search<GLTranDoc.lineNbr>((object) aTran.ParentLineNbr, Array.Empty<object>()));
label_5:
    return this._parent;
  }

  protected GLTranDoc FindPrevMasterTran(GLTranDoc aTran)
  {
    if (!aTran.LineNbr.HasValue || aTran.ParentLineNbr.HasValue)
      return (GLTranDoc) null;
    if (this._previousTran == null || this._previousTran.LineNbr.Value >= aTran.LineNbr.Value)
      this._previousTran = PXResultset<GLTranDoc>.op_Implicit(PXSelectBase<GLTranDoc, PXSelect<GLTranDoc, Where<GLTranDoc.module, Equal<Required<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Required<GLTranDoc.batchNbr>>, And<GLTranDoc.parentLineNbr, IsNull, And<GLTranDoc.lineNbr, Less<Required<GLTranDoc.lineNbr>>>>>>, OrderBy<Desc<GLTranDoc.lineNbr>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) aTran.Module,
        (object) aTran.BatchNbr,
        (object) aTran.LineNbr
      }));
    return this._previousTran;
  }

  protected GLTranDoc FindPrevSibling(GLTranDoc aTran)
  {
    if (!aTran.LineNbr.HasValue || !aTran.ParentLineNbr.HasValue)
      return (GLTranDoc) null;
    return PXResultset<GLTranDoc>.op_Implicit(PXSelectBase<GLTranDoc, PXSelect<GLTranDoc, Where<GLTranDoc.module, Equal<Required<GLTranDoc.module>>, And<GLTranDoc.batchNbr, Equal<Required<GLTranDoc.batchNbr>>, And<GLTranDoc.parentLineNbr, Equal<Required<GLTranDoc.parentLineNbr>>, And<GLTranDoc.lineNbr, Less<Required<GLTranDoc.lineNbr>>>>>>, OrderBy<Desc<GLTranDoc.lineNbr>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) aTran.Module,
      (object) aTran.BatchNbr,
      (object) aTran.ParentLineNbr,
      (object) aTran.LineNbr
    }));
  }

  protected virtual int? FindDefaultAccount(GLTranDoc aRow, bool isCredit)
  {
    int? nullable = new int?();
    this.SetCashAccount((PX.Objects.CA.CashAccount) null, isCredit);
    if (string.IsNullOrEmpty(aRow.TranModule) || string.IsNullOrEmpty(aRow.TranType))
      return new int?();
    if (aRow.TranModule == "AP")
    {
      if (!aRow.BAccountID.HasValue || !aRow.LocationID.HasValue)
        return new int?();
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.Location).Select(new object[2]
      {
        (object) aRow.BAccountID,
        (object) aRow.LocationID
      }));
      return this.FindDefaultAPAccount(aRow, isCredit, location);
    }
    if (aRow.TranModule == "AR")
    {
      if (!aRow.BAccountID.HasValue || !aRow.LocationID.HasValue)
        return new int?();
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.Location).Select(new object[2]
      {
        (object) aRow.BAccountID,
        (object) aRow.LocationID
      }));
      return this.FindDefaultARAccount(aRow, isCredit, location);
    }
    return aRow.TranModule == "CA" ? this.FindDefaultCAAccount(aRow, isCredit) : nullable;
  }

  private void SetCashAccount(PX.Objects.CA.CashAccount cashAccount, bool isCredit)
  {
    if (isCredit)
      this._cashAccountCredit = cashAccount;
    else
      this._cashAccountDebit = cashAccount;
  }

  protected bool HasCashAccount(int branch, int account, string entryTypeID, out bool single)
  {
    return this.GetCashAccount(branch, account, entryTypeID, out single) != null;
  }

  protected PX.Objects.CA.CashAccount GetCashAccount(
    int branch,
    int account,
    string entryTypeID,
    out bool single)
  {
    int? nullable1 = new int?();
    single = true;
    PX.Objects.CA.CashAccount cashAccount1 = (PX.Objects.CA.CashAccount) null;
    foreach (PXResult<PX.Objects.CA.CashAccount> pxResult in ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashAccountByAccountID).Select(new object[4]
    {
      (object) entryTypeID,
      (object) account,
      (object) branch,
      (object) entryTypeID
    }))
    {
      PX.Objects.CA.CashAccount cashAccount2 = PXResult<PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
      if (cashAccount2 != null)
      {
        int? nullable2 = cashAccount2.CashAccountID;
        if (nullable2.HasValue)
        {
          if (cashAccount1 == null)
            cashAccount1 = cashAccount2;
          if (!nullable1.HasValue)
            nullable1 = cashAccount2.SubID;
          nullable2 = nullable1;
          int? subId = cashAccount2.SubID;
          if (!(nullable2.GetValueOrDefault() == subId.GetValueOrDefault() & nullable2.HasValue == subId.HasValue))
          {
            single = false;
            break;
          }
        }
      }
    }
    return cashAccount1;
  }

  protected virtual PX.Objects.CA.CashAccount GetCashAccount(
    int aBranchID,
    int aAccountID,
    int aSubID,
    string entryTypeID,
    out bool cashExistForAccount)
  {
    cashExistForAccount = false;
    PX.Objects.CA.CashAccount cashAccount1 = (PX.Objects.CA.CashAccount) null;
    foreach (PXResult<PX.Objects.CA.CashAccount> pxResult in PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.branchID, Equal<Required<PX.Objects.CA.CashAccount.branchID>>, And<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aBranchID,
      (object) aAccountID
    }))
    {
      PX.Objects.CA.CashAccount cashAccount2 = PXResult<PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
      cashExistForAccount = true;
      int? subId = cashAccount2.SubID;
      int num = aSubID;
      if (subId.GetValueOrDefault() == num & subId.HasValue)
      {
        cashAccount1 = cashAccount2;
        break;
      }
    }
    return cashAccount1;
  }

  protected virtual bool IsMatching(
    PX.Objects.CA.CashAccount aCashAccount,
    int? branchID,
    int? accountID,
    int? subID)
  {
    if (aCashAccount == null || !branchID.HasValue || !accountID.HasValue || !subID.HasValue)
      return false;
    int? nullable1 = aCashAccount.BranchID;
    int? nullable2 = branchID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      nullable2 = aCashAccount.AccountID;
      nullable1 = accountID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        nullable1 = aCashAccount.SubID;
        nullable2 = subID;
        return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue;
      }
    }
    return false;
  }

  protected virtual int? FindDefaultAPAccount(GLTranDoc aRow, bool isCredit, PX.Objects.CR.Location location)
  {
    string tranType = aRow.TranType;
    if (tranType != null && tranType.Length == 3)
    {
      switch (tranType[0])
      {
        case 'A':
          switch (tranType)
          {
            case "ACR":
              break;
            case "ADR":
              return isCredit ? this.GetDefaultAPExpenceAccount(aRow, location) : this.GetDefaultAPPayableAccount(aRow, location);
            default:
              goto label_35;
          }
          break;
        case 'C':
          if (tranType == "CHK")
            goto label_25;
          goto label_35;
        case 'I':
          if (tranType == "INV")
            break;
          goto label_35;
        case 'P':
          if (tranType == "PPM")
            goto label_25;
          goto label_35;
        case 'Q':
          if (tranType == "QCK")
          {
            if (!isCredit)
              return this.GetDefaultAPExpenceAccount(aRow, location);
            PX.Objects.CA.CashAccount defaultApCashAccount = this.FindDefaultAPCashAccount(aRow.PaymentMethodID, location, aRow.BranchID);
            if (defaultApCashAccount == null)
              return new int?();
            this.SetCashAccount(defaultApCashAccount, isCredit);
            return defaultApCashAccount.AccountID;
          }
          goto label_35;
        case 'R':
          if (tranType == "REF")
          {
            if (isCredit)
              return this.GetDefaultAPPayableAccount(aRow, location);
            PX.Objects.CA.CashAccount defaultApCashAccount = this.FindDefaultAPCashAccount(aRow.PaymentMethodID, location, aRow.BranchID);
            if (defaultApCashAccount == null)
              return new int?();
            this.SetCashAccount(defaultApCashAccount, isCredit);
            return defaultApCashAccount.AccountID;
          }
          goto label_35;
        case 'V':
          if (tranType == "VQC")
          {
            if (isCredit)
              return this.GetDefaultAPExpenceAccount(aRow, location);
            PX.Objects.CA.CashAccount defaultApCashAccount = this.FindDefaultAPCashAccount(aRow.PaymentMethodID, location, aRow.BranchID);
            if (defaultApCashAccount == null)
              return new int?();
            this.SetCashAccount(defaultApCashAccount, isCredit);
            return defaultApCashAccount.AccountID;
          }
          goto label_35;
        default:
          goto label_35;
      }
      return !isCredit ? this.GetDefaultAPExpenceAccount(aRow, location) : this.GetDefaultAPPayableAccount(aRow, location);
label_25:
      if (!isCredit)
        return this.GetDefaultAPPayableAccount(aRow, location);
      PX.Objects.CA.CashAccount defaultApCashAccount1 = this.FindDefaultAPCashAccount(aRow.PaymentMethodID, location, aRow.BranchID, aRow.CuryID);
      if (defaultApCashAccount1 == null)
        return new int?();
      this.SetCashAccount(defaultApCashAccount1, isCredit);
      return defaultApCashAccount1.AccountID;
    }
label_35:
    return new int?();
  }

  protected virtual void TryToSetDefaultCashAccountForAP(GLTranDoc aRow, bool isCredit)
  {
    switch (aRow.TranType)
    {
      case "QCK":
      case "PPM":
      case "CHK":
        if (!isCredit)
          break;
        PX.Objects.CA.CashAccount defaultApCashAccount1 = this.FindDefaultAPCashAccount(aRow.PaymentMethodID, PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.Location).Select(new object[2]
        {
          (object) aRow.BAccountID,
          (object) aRow.LocationID
        })), aRow.BranchID);
        if (defaultApCashAccount1 == null)
          break;
        this.SetCashAccount(defaultApCashAccount1, isCredit);
        break;
      case "VQC":
      case "REF":
        if (isCredit)
          break;
        PX.Objects.CA.CashAccount defaultApCashAccount2 = this.FindDefaultAPCashAccount(aRow.PaymentMethodID, PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.Location).Select(new object[2]
        {
          (object) aRow.BAccountID,
          (object) aRow.LocationID
        })), aRow.BranchID);
        if (defaultApCashAccount2 == null)
          break;
        this.SetCashAccount(defaultApCashAccount2, isCredit);
        break;
    }
  }

  protected virtual int? GetDefaultAPPayableAccount(GLTranDoc aRow, PX.Objects.CR.Location location)
  {
    if (aRow.TranType == "PPM")
    {
      PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Select(new object[1]
      {
        (object) aRow.BAccountID
      }));
      if (vendor != null && vendor.PrepaymentAcctID.HasValue)
        return vendor.PrepaymentAcctID;
    }
    if (object.Equals((object) location.LocationID, (object) location.VAPAccountLocationID))
      return location.VAPAccountID;
    return PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) location.BAccountID
    })).VAPAccountID;
  }

  protected virtual int? GetDefaultAPExpenceAccount(GLTranDoc aRow, PX.Objects.CR.Location location)
  {
    return location?.VExpenseAcctID;
  }

  protected virtual PX.Objects.CA.CashAccount FindDefaultAPCashAccount(
    string aPaymentMethodID,
    PX.Objects.CR.Location aLocation,
    int? aBranchID)
  {
    if (aLocation != null && aLocation.VPaymentMethodID == aPaymentMethodID && aLocation.VCashAccountID.HasValue)
      return PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) aLocation.VCashAccountID
      }));
    if (string.IsNullOrEmpty(aPaymentMethodID))
      return (PX.Objects.CA.CashAccount) null;
    return PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectJoin<PX.Objects.CA.CashAccount, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAP, Equal<True>>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.aPIsDefault, Equal<True>, And2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Required<GLTranDoc.branchID>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aPaymentMethodID,
      (object) aBranchID
    }));
  }

  protected virtual PX.Objects.CA.CashAccount FindDefaultAPCashAccount(
    string aPaymentMethodID,
    PX.Objects.CR.Location aLocation,
    int? aBranchID,
    string aCuryID)
  {
    if (aLocation != null && aLocation.VPaymentMethodID == aPaymentMethodID && aLocation.VCashAccountID.HasValue)
      return PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>, And<PX.Objects.CA.CashAccount.curyID, Equal<Required<PX.Objects.CA.CashAccount.curyID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) aLocation.VCashAccountID,
        (object) aCuryID
      }));
    if (string.IsNullOrEmpty(aPaymentMethodID))
      return (PX.Objects.CA.CashAccount) null;
    return PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectJoin<PX.Objects.CA.CashAccount, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAP, Equal<True>>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.aPIsDefault, Equal<True>, And2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Required<GLTranDoc.branchID>>, And<PX.Objects.CA.CashAccount.curyID, Equal<Required<PX.Objects.CA.CashAccount.curyID>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) aPaymentMethodID,
      (object) aBranchID,
      (object) aCuryID
    }));
  }

  protected virtual int? FindDefaultARAccount(GLTranDoc aRow, bool isCredit, PX.Objects.CR.Location location)
  {
    string tranType = aRow.TranType;
    if (tranType != null && tranType.Length == 3)
    {
      switch (tranType[1])
      {
        case 'C':
          if (tranType == "RCS")
          {
            if (!isCredit)
              return this.GetDefaultARSalesAccount(aRow, location);
            PX.Objects.CA.CashAccount defaultArCashAccount = this.FindDefaultARCashAccount(aRow.PaymentMethodID, aRow.PMInstanceID, aRow.CuryID, aRow.BranchID);
            if (defaultArCashAccount == null)
              return new int?();
            this.SetCashAccount(defaultArCashAccount, isCredit);
            return defaultArCashAccount.AccountID;
          }
          goto label_35;
        case 'E':
          if (tranType == "REF")
          {
            if (!isCredit)
              return this.GetDefaultARReceivableAccount(aRow, location);
            PX.Objects.CA.CashAccount defaultArCashAccount = this.FindDefaultARCashAccount(aRow.PaymentMethodID, aRow.PMInstanceID, aRow.CuryID, aRow.BranchID);
            if (defaultArCashAccount == null)
              return new int?();
            this.SetCashAccount(defaultArCashAccount, isCredit);
            return defaultArCashAccount.AccountID;
          }
          goto label_35;
        case 'M':
          if (tranType == "PMT")
            goto label_25;
          goto label_35;
        case 'N':
          if (tranType == "INV")
            break;
          goto label_35;
        case 'P':
          if (tranType == "PPM")
            goto label_25;
          goto label_35;
        case 'R':
          switch (tranType)
          {
            case "DRM":
              break;
            case "CRM":
              return !isCredit ? this.GetDefaultARSalesAccount(aRow, location) : this.GetDefaultARReceivableAccount(aRow, location);
            default:
              goto label_35;
          }
          break;
        case 'S':
          if (tranType == "CSL")
          {
            if (isCredit)
              return this.GetDefaultARSalesAccount(aRow, location);
            PX.Objects.CA.CashAccount defaultArCashAccount = this.FindDefaultARCashAccount(aRow.PaymentMethodID, aRow.PMInstanceID, aRow.CuryID, aRow.BranchID);
            if (defaultArCashAccount == null)
              return new int?();
            this.SetCashAccount(defaultArCashAccount, isCredit);
            return defaultArCashAccount.AccountID;
          }
          goto label_35;
        default:
          goto label_35;
      }
      return isCredit ? this.GetDefaultARSalesAccount(aRow, location) : this.GetDefaultARReceivableAccount(aRow, location);
label_25:
      if (isCredit)
        return this.GetDefaultARReceivableAccount(aRow, location);
      PX.Objects.CA.CashAccount defaultArCashAccount1 = this.FindDefaultARCashAccount(aRow.PaymentMethodID, aRow.PMInstanceID, aRow.CuryID, aRow.BranchID);
      if (defaultArCashAccount1 == null)
        return new int?();
      this.SetCashAccount(defaultArCashAccount1, isCredit);
      return defaultArCashAccount1.AccountID;
    }
label_35:
    return new int?();
  }

  protected virtual void TryToSetDefaultCashAccountForAR(GLTranDoc aRow, bool isCredit)
  {
    string tranType = aRow.TranType;
    if (tranType == null || tranType.Length != 3)
      return;
    switch (tranType[1])
    {
      case 'C':
        if (!(tranType == "RCS"))
          return;
        goto label_21;
      case 'E':
        if (!(tranType == "REF"))
          return;
        goto label_21;
      case 'M':
        if (!(tranType == "PMT"))
          return;
        break;
      case 'N':
        if (!(tranType == "INV"))
          return;
        break;
      case 'O':
        return;
      case 'P':
        if (!(tranType == "PPM"))
          return;
        break;
      case 'Q':
        return;
      case 'R':
        if (!(tranType == "DRM") && !(tranType == "CRM"))
          return;
        break;
      case 'S':
        if (!(tranType == "CSL"))
          return;
        break;
      default:
        return;
    }
    if (isCredit)
      return;
    PX.Objects.CA.CashAccount defaultArCashAccount1 = this.FindDefaultARCashAccount(aRow.PaymentMethodID, aRow.PMInstanceID, aRow.CuryID, aRow.BranchID);
    if (defaultArCashAccount1 == null)
      return;
    this.SetCashAccount(defaultArCashAccount1, isCredit);
    return;
label_21:
    if (!isCredit)
      return;
    PX.Objects.CA.CashAccount defaultArCashAccount2 = this.FindDefaultARCashAccount(aRow.PaymentMethodID, aRow.PMInstanceID, aRow.CuryID, aRow.BranchID);
    if (defaultArCashAccount2 == null)
      return;
    this.SetCashAccount(defaultArCashAccount2, isCredit);
  }

  protected virtual int? GetDefaultARReceivableAccount(GLTranDoc aRow, PX.Objects.CR.Location location)
  {
    if (aRow.TranType == "PPM")
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) aRow.BAccountID
      }));
      if (customer != null && customer.PrepaymentAcctID.HasValue)
        return customer.PrepaymentAcctID;
    }
    if (object.Equals((object) location.LocationID, (object) location.CARAccountLocationID))
      return location.CARAccountID;
    return PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) location.BAccountID
    })).CARAccountID;
  }

  protected virtual int? GetDefaultARSalesAccount(GLTranDoc aRow, PX.Objects.CR.Location location)
  {
    return location?.CSalesAcctID;
  }

  [Obsolete]
  protected virtual PX.Objects.CA.CashAccount FindDefaultARCashAccount(
    string aPaymentMethodID,
    int? aPMInstanceID)
  {
    return this.FindDefaultARCashAccount(aPaymentMethodID, aPMInstanceID, ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.CuryID, new int?());
  }

  [Obsolete]
  protected virtual PX.Objects.CA.CashAccount FindDefaultARCashAccount(
    string aPaymentMethodID,
    int? aPMInstanceID,
    string curyID)
  {
    return this.FindDefaultARCashAccount(aPaymentMethodID, aPMInstanceID, curyID, new int?());
  }

  protected virtual PX.Objects.CA.CashAccount FindDefaultARCashAccount(
    string aPaymentMethodID,
    int? aPMInstanceID,
    string curyID,
    int? branchID)
  {
    if (string.IsNullOrEmpty(aPaymentMethodID))
      return (PX.Objects.CA.CashAccount) null;
    PX.Objects.CA.CashAccount defaultArCashAccount = (PX.Objects.CA.CashAccount) null;
    if (aPMInstanceID.HasValue)
      defaultArCashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectJoin<PX.Objects.CA.CashAccount, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<Match<Current<AccessInfo.userName>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) aPMInstanceID.Value
      }));
    if (defaultArCashAccount == null)
      defaultArCashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectJoin<PX.Objects.CA.CashAccount, InnerJoin<PaymentMethodAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<PaymentMethodAccount.paymentMethodID>>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>, And<PX.Objects.CA.CashAccount.curyID, Equal<Required<PX.Objects.CM.Currency.curyID>>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Required<GLTranDoc.branchID>>>>>>>>, OrderBy<Desc<PaymentMethodAccount.aRIsDefault>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) aPaymentMethodID,
        (object) curyID,
        (object) branchID
      }));
    return defaultArCashAccount;
  }

  protected virtual int? FindDefaultCAAccount(GLTranDoc aRow, bool isCredit)
  {
    if (!string.IsNullOrEmpty(aRow.EntryTypeID))
    {
      CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) aRow.EntryTypeID
      }));
      bool flag = caEntryType.DrCr == "D";
      int? nullable = new int?();
      int? cashAccountID;
      if (!aRow.IsChildTran)
      {
        cashAccountID = flag ? aRow.DebitCashAccountID : aRow.CreditCashAccountID;
      }
      else
      {
        GLTranDoc parent = this.FindParent(aRow);
        cashAccountID = flag ? parent.DebitCashAccountID : parent.CreditCashAccountID;
      }
      if (flag == isCredit)
      {
        if (cashAccountID.HasValue)
        {
          CashAccountETDetail cashAccountEtDetail = this.GetCashAccountETDetail(caEntryType.EntryTypeId, cashAccountID);
          if (cashAccountEtDetail != null && cashAccountEtDetail.OffsetAccountID.HasValue)
            return cashAccountEtDetail.OffsetAccountID;
        }
        return caEntryType.AccountID;
      }
    }
    return new int?();
  }

  protected virtual int? FindDefaultSubAccount(GLTranDoc aRow, bool isCredit)
  {
    int? nullable1 = new int?();
    if (string.IsNullOrEmpty(aRow.TranModule) || string.IsNullOrEmpty(aRow.TranType))
      return new int?();
    int? keyAccount = isCredit ? aRow.CreditAccountID : aRow.DebitAccountID;
    PX.Objects.CA.CashAccount cashAccount = isCredit ? this._cashAccountCredit : this._cashAccountDebit;
    if (!keyAccount.HasValue)
      return new int?();
    if (cashAccount == null && keyAccount.HasValue && this.IsPaymentType(aRow))
    {
      this.TryToSetDefaultCashAccount(aRow, isCredit);
      cashAccount = isCredit ? this._cashAccountCredit : this._cashAccountDebit;
    }
    int? defaultSubAccount;
    if (cashAccount != null)
    {
      defaultSubAccount = keyAccount;
      int? accountId = cashAccount.AccountID;
      if (!(defaultSubAccount.GetValueOrDefault() == accountId.GetValueOrDefault() & defaultSubAccount.HasValue == accountId.HasValue))
        return new int?();
    }
    int num;
    if (keyAccount.HasValue && cashAccount != null)
    {
      int? nullable2 = keyAccount;
      defaultSubAccount = cashAccount.AccountID;
      num = nullable2.GetValueOrDefault() == defaultSubAccount.GetValueOrDefault() & nullable2.HasValue == defaultSubAccount.HasValue ? 1 : 0;
    }
    else
      num = 0;
    if (num != 0)
    {
      if (cashAccount != null)
        return cashAccount.SubID;
      defaultSubAccount = new int?();
      return defaultSubAccount;
    }
    if (aRow.TranModule == "AP")
    {
      defaultSubAccount = aRow.BAccountID;
      if (defaultSubAccount.HasValue)
      {
        defaultSubAccount = aRow.LocationID;
        if (defaultSubAccount.HasValue)
        {
          PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.Location).Select(new object[2]
          {
            (object) aRow.BAccountID,
            (object) aRow.LocationID
          }));
          return this.FindDefaultAPSubID(aRow, isCredit, location);
        }
      }
      defaultSubAccount = new int?();
      return defaultSubAccount;
    }
    if (aRow.TranModule == "AR")
    {
      defaultSubAccount = aRow.BAccountID;
      if (defaultSubAccount.HasValue)
      {
        defaultSubAccount = aRow.LocationID;
        if (defaultSubAccount.HasValue)
        {
          PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.Location).Select(new object[2]
          {
            (object) aRow.BAccountID,
            (object) aRow.LocationID
          }));
          return this.FindDefaultARSubID(aRow, isCredit, location);
        }
      }
      defaultSubAccount = new int?();
      return defaultSubAccount;
    }
    if (aRow.TranModule == "CA")
      return this.FindDefaultCASubID(aRow, isCredit);
    return aRow.TranModule == "GL" ? this.FindDefaultGLSubID(aRow, isCredit, keyAccount) : nullable1;
  }

  private void TryToSetDefaultCashAccount(GLTranDoc aRow, bool isCredit)
  {
    if (aRow.TranModule == "AP")
      this.TryToSetDefaultCashAccountForAP(aRow, isCredit);
    if (!(aRow.TranModule == "AR"))
      return;
    this.TryToSetDefaultCashAccountForAR(aRow, isCredit);
  }

  private bool IsPaymentType(GLTranDoc aRow)
  {
    if (aRow.TranModule == "AP")
      return aRow.TranType == "QCK" || aRow.TranType == "PPM" || aRow.TranType == "CHK" || aRow.TranType == "VQC" || aRow.TranType == "REF";
    if (!(aRow.TranModule == "AR"))
      return false;
    return aRow.TranType == "CRM" || aRow.TranType == "CSL" || aRow.TranType == "PPM" || aRow.TranType == "PMT" || aRow.TranType == "RCS" || aRow.TranType == "REF";
  }

  protected virtual int? FindDefaultAPSubID(GLTranDoc aRow, bool isCredit, PX.Objects.CR.Location location)
  {
    string tranType = aRow.TranType;
    if (tranType != null && tranType.Length == 3)
    {
      switch (tranType[0])
      {
        case 'A':
          switch (tranType)
          {
            case "ACR":
              break;
            case "ADR":
              return isCredit ? this.GetDefaultAPExpenceSubID(aRow, location) : this.GetDefaultAPPayableSubID(aRow, location);
            default:
              goto label_27;
          }
          break;
        case 'C':
          if (tranType == "CHK")
            goto label_21;
          goto label_27;
        case 'I':
          if (tranType == "INV")
            break;
          goto label_27;
        case 'P':
          if (tranType == "PPM")
            goto label_21;
          goto label_27;
        case 'Q':
          if (tranType == "QCK")
            return !isCredit ? this.GetDefaultAPExpenceSubID(aRow, location) : new int?();
          goto label_27;
        case 'R':
          if (tranType == "REF")
            return isCredit ? this.GetDefaultAPPayableSubID(aRow, location) : new int?();
          goto label_27;
        case 'V':
          if (tranType == "VQC")
            return isCredit ? this.GetDefaultAPExpenceSubID(aRow, location) : new int?();
          goto label_27;
        default:
          goto label_27;
      }
      return !isCredit ? this.GetDefaultAPExpenceSubID(aRow, location) : this.GetDefaultAPPayableSubID(aRow, location);
label_21:
      return !isCredit ? this.GetDefaultAPPayableSubID(aRow, location) : new int?();
    }
label_27:
    return new int?();
  }

  protected virtual int? GetDefaultAPPayableSubID(GLTranDoc aRow, PX.Objects.CR.Location location)
  {
    if (aRow.TranType == "PPM")
    {
      PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Select(new object[1]
      {
        (object) aRow.BAccountID
      }));
      if (vendor != null && vendor.PrepaymentSubID.HasValue)
        return vendor.PrepaymentSubID;
    }
    if (object.Equals((object) location.LocationID, (object) location.VAPAccountLocationID))
      return location.VAPSubID;
    return PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) location.BAccountID
    })).VAPSubID;
  }

  protected virtual int? GetDefaultAPExpenceSubID(GLTranDoc aRow, PX.Objects.CR.Location location)
  {
    return location?.VExpenseSubID;
  }

  protected virtual int? FindDefaultARSubID(GLTranDoc aRow, bool isCredit, PX.Objects.CR.Location location)
  {
    string tranType = aRow.TranType;
    if (tranType != null && tranType.Length == 3)
    {
      switch (tranType[1])
      {
        case 'C':
          if (tranType == "RCS")
            return !isCredit ? this.GetDefaultARSalesSubID(aRow, location) : new int?();
          goto label_27;
        case 'E':
          if (tranType == "REF")
            return !isCredit ? this.GetDefaultARReceivableSubID(aRow, location) : new int?();
          goto label_27;
        case 'M':
          if (tranType == "PMT")
            goto label_21;
          goto label_27;
        case 'N':
          if (tranType == "INV")
            break;
          goto label_27;
        case 'P':
          if (tranType == "PPM")
            goto label_21;
          goto label_27;
        case 'R':
          switch (tranType)
          {
            case "DRM":
              break;
            case "CRM":
              return !isCredit ? this.GetDefaultARSalesSubID(aRow, location) : this.GetDefaultARReceivableSubID(aRow, location);
            default:
              goto label_27;
          }
          break;
        case 'S':
          if (tranType == "CSL")
            return isCredit ? this.GetDefaultARSalesSubID(aRow, location) : new int?();
          goto label_27;
        default:
          goto label_27;
      }
      return isCredit ? this.GetDefaultARSalesSubID(aRow, location) : this.GetDefaultARReceivableSubID(aRow, location);
label_21:
      return isCredit ? this.GetDefaultARReceivableSubID(aRow, location) : new int?();
    }
label_27:
    return new int?();
  }

  protected virtual int? GetDefaultARReceivableSubID(GLTranDoc aRow, PX.Objects.CR.Location location)
  {
    if (aRow.TranType == "PPM")
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
      {
        (object) aRow.BAccountID
      }));
      if (customer != null && customer.PrepaymentSubID.HasValue)
        return customer.PrepaymentSubID;
    }
    if (object.Equals((object) location.LocationID, (object) location.CARAccountLocationID))
      return location.CARSubID;
    return PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) location.BAccountID
    })).CARSubID;
  }

  protected virtual int? GetDefaultARSalesSubID(GLTranDoc aRow, PX.Objects.CR.Location location)
  {
    return location?.CSalesSubID;
  }

  protected virtual int? FindDefaultCASubID(GLTranDoc aRow, bool isCredit)
  {
    if (!string.IsNullOrEmpty(aRow.EntryTypeID))
    {
      CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) aRow.EntryTypeID
      }));
      bool flag = caEntryType.DrCr == "D";
      int? nullable = new int?();
      int? cashAccountID;
      if (!aRow.IsChildTran)
      {
        cashAccountID = flag ? aRow.DebitAccountID : aRow.CreditAccountID;
      }
      else
      {
        GLTranDoc parent = this.FindParent(aRow);
        cashAccountID = flag ? parent.DebitAccountID : parent.CreditAccountID;
      }
      if (flag == isCredit)
      {
        if (cashAccountID.HasValue)
        {
          CashAccountETDetail cashAccountEtDetail = this.GetCashAccountETDetail(caEntryType.EntryTypeId, cashAccountID);
          if (cashAccountEtDetail != null && cashAccountEtDetail.OffsetSubID.HasValue)
            return cashAccountEtDetail.OffsetSubID;
        }
        return caEntryType.SubID;
      }
    }
    return new int?();
  }

  protected virtual int? FindDefaultGLSubID(GLTranDoc aRow, bool isCredit, int? keyAccount)
  {
    int? nullable1 = new int?();
    PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashAccount).Select(new object[1]
    {
      (object) keyAccount
    }));
    if (cashAccount != null)
      return cashAccount.SubID;
    int? nullable2 = new int?();
    GLTranDoc glTranDoc = aRow;
    if (aRow.IsChildTran)
      glTranDoc = this.FindParent(aRow);
    if (glTranDoc != null)
      nullable2 = isCredit ? glTranDoc.DebitSubID : glTranDoc.CreditSubID;
    int? nullable3 = nullable2;
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) keyAccount
    }));
    return account != null && account.NoSubDetail.Value && ((PXSelectBase<GLSetup>) this.glsetup).Current.DefaultSubID.HasValue ? ((PXSelectBase<GLSetup>) this.glsetup).Current.DefaultSubID : nullable3;
  }

  protected virtual void GLTranDocAP_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    GLTranDoc oldRow = (GLTranDoc) e.OldRow;
    Decimal? curyApplAmt1 = row.CuryApplAmt;
    Decimal? curyApplAmt2 = oldRow.CuryApplAmt;
    if (curyApplAmt1.GetValueOrDefault() == curyApplAmt2.GetValueOrDefault() & curyApplAmt1.HasValue == curyApplAmt2.HasValue)
      return;
    GLTranDoc glTranDoc = PXResultset<GLTranDoc>.op_Implicit(((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Search<GLTranDoc.module, GLTranDoc.batchNbr, GLTranDoc.lineNbr>((object) row.Module, (object) row.BatchNbr, (object) row.LineNbr, Array.Empty<object>()));
    if (glTranDoc == null)
      return;
    GLTranDoc copy = (GLTranDoc) ((PXSelectBase) this.GLTranModuleBatNbr).Cache.CreateCopy((object) glTranDoc);
    copy.CuryApplAmt = row.CuryApplAmt;
    copy.ApplAmt = row.ApplAmt;
    try
    {
      this._isCacheSync = true;
      ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Update(copy);
    }
    finally
    {
      this._isCacheSync = false;
    }
  }

  protected virtual void GLTranDocAP_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDocAP_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    JournalWithSubEntry.GLTranDocAP row = (JournalWithSubEntry.GLTranDocAP) e.Row;
    bool? nullable;
    if (row != null)
    {
      nullable = row.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        int num1;
        if (((PXSelectBase<GLDocBatch>) this.BatchModule).Current != null)
        {
          nullable = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.Hold;
          bool flag2 = false;
          num1 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        if (num1 != 0)
        {
          Decimal? curyUnappliedBal = row.CuryUnappliedBal;
          Decimal num2 = 0M;
          if (!(curyUnappliedBal.GetValueOrDefault() == num2 & curyUnappliedBal.HasValue) && row.TranType != "PPM")
          {
            sender.RaiseExceptionHandling<JournalWithSubEntry.GLTranDocAP.curyUnappliedBal>((object) row, (object) row.CuryUnappliedBal, (Exception) new PXSetPropertyException("You have to apply full amount of this document before it may be released", (PXErrorLevel) 4));
            goto label_9;
          }
        }
        sender.RaiseExceptionHandling<JournalWithSubEntry.GLTranDocAP.curyUnappliedBal>((object) row, (object) row.CuryUnappliedBal, (Exception) null);
      }
    }
label_9:
    int num;
    if (row != null)
    {
      nullable = row.Released;
      bool flag = false;
      num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag3 = num != 0;
    ((PXSelectBase) this.APAdjustments).Cache.AllowInsert = flag3;
    ((PXSelectBase) this.APAdjustments).Cache.AllowUpdate = flag3;
    ((PXSelectBase) this.APAdjustments).Cache.AllowDelete = flag3;
    if (row == null)
      return;
    APPaymentEntry.SetDocTypeList(((PXSelectBase) this.APAdjustments).Cache, row.TranType);
  }

  protected virtual void GLTranDocAR_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    GLTranDoc row = (GLTranDoc) e.Row;
    GLTranDoc oldRow = (GLTranDoc) e.OldRow;
    Decimal? curyApplAmt1 = row.CuryApplAmt;
    Decimal? curyApplAmt2 = oldRow.CuryApplAmt;
    if (curyApplAmt1.GetValueOrDefault() == curyApplAmt2.GetValueOrDefault() & curyApplAmt1.HasValue == curyApplAmt2.HasValue)
      return;
    GLTranDoc glTranDoc = PXResultset<GLTranDoc>.op_Implicit(((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Search<GLTranDoc.module, GLTranDoc.batchNbr, GLTranDoc.lineNbr>((object) row.Module, (object) row.BatchNbr, (object) row.LineNbr, Array.Empty<object>()));
    if (glTranDoc == null)
      return;
    GLTranDoc copy = (GLTranDoc) ((PXSelectBase) this.GLTranModuleBatNbr).Cache.CreateCopy((object) glTranDoc);
    copy.CuryApplAmt = row.CuryApplAmt;
    copy.ApplAmt = row.ApplAmt;
    try
    {
      this._isCacheSync = true;
      ((PXSelectBase<GLTranDoc>) this.GLTranModuleBatNbr).Update(copy);
    }
    finally
    {
      this._isCacheSync = false;
    }
  }

  protected virtual void GLTranDocAR_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLTranDocAR_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    JournalWithSubEntry.GLTranDocAR row = (JournalWithSubEntry.GLTranDocAR) e.Row;
    bool? nullable;
    if (row != null)
    {
      nullable = row.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        int num1;
        if (((PXSelectBase<GLDocBatch>) this.BatchModule).Current != null)
        {
          nullable = ((PXSelectBase<GLDocBatch>) this.BatchModule).Current.Hold;
          bool flag2 = false;
          num1 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        if (num1 != 0)
        {
          Decimal? curyUnappliedBal = row.CuryUnappliedBal;
          Decimal num2 = 0M;
          if (!(curyUnappliedBal.GetValueOrDefault() == num2 & curyUnappliedBal.HasValue))
          {
            sender.RaiseExceptionHandling<JournalWithSubEntry.GLTranDocAR.curyUnappliedBal>((object) row, (object) row.CuryUnappliedBal, (Exception) new PXSetPropertyException("This document is not fully applied", (PXErrorLevel) 2));
            goto label_9;
          }
        }
        sender.RaiseExceptionHandling<JournalWithSubEntry.GLTranDocAR.curyUnappliedBal>((object) row, (object) row.CuryUnappliedBal, (Exception) null);
      }
    }
label_9:
    int num;
    if (row != null)
    {
      nullable = row.Released;
      bool flag = false;
      num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag3 = num != 0;
    ((PXSelectBase) this.ARAdjustments).Cache.AllowInsert = flag3;
    ((PXSelectBase) this.ARAdjustments).Cache.AllowUpdate = flag3;
    ((PXSelectBase) this.ARAdjustments).Cache.AllowDelete = flag3;
    if (row == null)
      return;
    ARPaymentEntry.SetDocTypeList(((PXSelectBase) this.ARAdjustments).Cache, row.TranType);
  }

  protected virtual void APAdjust_AdjdDocType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    sender.SetDefaultExt<PX.Objects.AP.APAdjust.adjdRefNbr>(e.Row);
  }

  protected virtual void APAdjust_AdjdRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    JournalWithSubEntry.GLTranDocAP current = ((PXSelectBase<JournalWithSubEntry.GLTranDocAP>) this.APPayments).Current;
    ((CancelEventArgs) e).Cancel = current != null && current.TranType == "QCK";
  }

  protected virtual void APAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AP.APAdjust row))
      return;
    try
    {
      using (IEnumerator<PXResult<PX.Objects.AP.APInvoice>> enumerator = PXSelectBase<PX.Objects.AP.APInvoice, PXSelectJoin<PX.Objects.AP.APInvoice, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APInvoice.curyInfoID>>>, Where<PX.Objects.AP.APInvoice.vendorID, Equal<Current<JournalWithSubEntry.GLTranDocAP.bAccountID>>, And<PX.Objects.AP.APInvoice.docType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.AdjdDocType,
        (object) row.AdjdRefNbr
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo> current = (PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo>) enumerator.Current;
          this.APAdjust_AdjdRefNbr_FieldUpdated<PX.Objects.AP.APInvoice>((JournalWithSubEntry.RegisterAdapter<PX.Objects.AP.APInvoice>) new JournalWithSubEntry.APRegisterAdapter<PX.Objects.AP.APInvoice>(PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo>.op_Implicit(current)), PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.CurrencyInfo>.op_Implicit(current), row);
          return;
        }
      }
      using (IEnumerator<PXResult<GLTranDoc>> enumerator = PXSelectBase<GLTranDoc, PXSelectJoin<GLTranDoc, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<GLTranDoc.curyInfoID>>>, Where<GLTranDoc.batchNbr, Equal<Current<GLDocBatch.batchNbr>>, And<GLTranDoc.parentLineNbr, IsNull, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAP>, And<GLTranDoc.bAccountID, Equal<Required<GLTranDoc.bAccountID>>, And<GLTranDoc.tranType, Equal<Required<PX.Objects.AP.APAdjust.adjdDocType>>, And<GLTranDoc.refNbr, Equal<Required<GLTranDoc.refNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.VendorID,
        (object) row.AdjdDocType,
        (object) row.AdjdRefNbr
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<GLTranDoc, PX.Objects.CM.CurrencyInfo> current = (PXResult<GLTranDoc, PX.Objects.CM.CurrencyInfo>) enumerator.Current;
          this.APAdjust_AdjdRefNbr_FieldUpdated<GLTranDoc>((JournalWithSubEntry.RegisterAdapter<GLTranDoc>) new JournalWithSubEntry.GlRegisterAdapter<GLTranDoc>(PXResult<GLTranDoc, PX.Objects.CM.CurrencyInfo>.op_Implicit(current)), PXResult<GLTranDoc, PX.Objects.CM.CurrencyInfo>.op_Implicit(current), row);
          return;
        }
      }
      foreach (PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.AP.APPayment, PXSelectJoin<PX.Objects.AP.APPayment, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.APPayment.curyInfoID>>>, Where<PX.Objects.AP.APPayment.vendorID, Equal<Current<JournalWithSubEntry.GLTranDocAP.bAccountID>>, And<PX.Objects.AP.APPayment.docType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.AdjdDocType,
        (object) row.AdjdRefNbr
      }))
        this.APAdjust_AdjdRefNbr_FieldUpdated<PX.Objects.AP.APPayment>((JournalWithSubEntry.RegisterAdapter<PX.Objects.AP.APPayment>) new JournalWithSubEntry.APRegisterAdapter<PX.Objects.AP.APPayment>(PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult)), PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult), row);
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
  }

  private void APAdjust_AdjdRefNbr_FieldUpdated<T>(
    JournalWithSubEntry.RegisterAdapter<T> invoice,
    PX.Objects.CM.CurrencyInfo info,
    PX.Objects.AP.APAdjust adj)
    where T : class, IBqlTable, IInvoice, new()
  {
    JournalWithSubEntry.GLTranDocAP current = ((PXSelectBase<JournalWithSubEntry.GLTranDocAP>) this.APPayments).Current;
    DateTime? tranDate1 = current.TranDate;
    DateTime? tranDate2 = current.TranDate;
    PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(info);
    copy.CuryInfoID = new long?();
    PX.Objects.CM.CurrencyInfo info1 = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.currencyinfo).Cache.Insert((object) copy);
    info1.SetCuryEffDate(((PXSelectBase) this.currencyinfo).Cache, (object) tranDate2);
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfoEx).Insert(PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info1));
    adj.VendorID = invoice.BAccountID;
    adj.AdjgDocDate = tranDate1;
    adj.AdjgCuryInfoID = current.CuryInfoID;
    adj.AdjdCuryInfoID = info1.CuryInfoID;
    adj.AdjdOrigCuryInfoID = info.CuryInfoID;
    adj.AdjdBranchID = invoice.BranchID;
    adj.AdjdAPAcct = invoice.AccountID;
    adj.AdjdAPSub = invoice.SubID;
    adj.AdjdDocDate = invoice.DocDate;
    adj.AdjdFinPeriodID = invoice.FinPeriodID;
    adj.Released = new bool?(false);
    APPaymentBalanceCalculator balanceCalculator = new APPaymentBalanceCalculator((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo_CuryInfoID);
    balanceCalculator.CalcBalances<T>(adj, invoice.Entity, false, true, (APTran) null);
    Decimal? curyWhTaxBal1 = adj.CuryWhTaxBal;
    Decimal num1 = 0M;
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    if (curyWhTaxBal1.GetValueOrDefault() >= num1 & curyWhTaxBal1.HasValue)
    {
      nullable1 = adj.CuryDiscBal;
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() >= num2 & nullable1.HasValue)
      {
        nullable2 = adj.CuryDocBal;
        Decimal? curyWhTaxBal2 = adj.CuryWhTaxBal;
        Decimal? nullable4 = nullable2.HasValue & curyWhTaxBal2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - curyWhTaxBal2.GetValueOrDefault()) : new Decimal?();
        nullable3 = adj.CuryDiscBal;
        nullable1 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
        Decimal num3 = 0M;
        if (nullable1.GetValueOrDefault() <= num3 & nullable1.HasValue)
          return;
      }
    }
    Decimal? nullable5 = adj.AdjgDocType == "ADR" ? new Decimal?(0M) : adj.CuryDiscBal;
    Decimal? curyDocBal = adj.CuryDocBal;
    Decimal? nullable6 = adj.CuryWhTaxBal;
    Decimal? nullable7;
    if (!(curyDocBal.HasValue & nullable6.HasValue))
    {
      nullable2 = new Decimal?();
      nullable7 = nullable2;
    }
    else
      nullable7 = new Decimal?(curyDocBal.GetValueOrDefault() - nullable6.GetValueOrDefault());
    nullable1 = nullable7;
    nullable3 = nullable5;
    Decimal? nullable8;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable6 = new Decimal?();
      nullable8 = nullable6;
    }
    else
      nullable8 = new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault());
    Decimal? nullable9 = nullable8;
    Decimal? curyUnappliedBal = current.CuryUnappliedBal;
    if (current != null)
    {
      nullable3 = adj.AdjgBalSign;
      Decimal num4 = 0M;
      if (nullable3.GetValueOrDefault() < num4 & nullable3.HasValue)
      {
        nullable3 = curyUnappliedBal;
        Decimal num5 = 0M;
        if (nullable3.GetValueOrDefault() < num5 & nullable3.HasValue)
        {
          nullable9 = new Decimal?(Math.Min(nullable9.Value, Math.Abs(curyUnappliedBal.Value)));
          goto label_26;
        }
        goto label_26;
      }
    }
    if (current != null)
    {
      nullable3 = curyUnappliedBal;
      Decimal num6 = 0M;
      if (nullable3.GetValueOrDefault() > num6 & nullable3.HasValue)
      {
        nullable3 = adj.AdjgBalSign;
        Decimal num7 = 0M;
        if (nullable3.GetValueOrDefault() > num7 & nullable3.HasValue)
        {
          nullable3 = curyUnappliedBal;
          nullable1 = nullable5;
          if (nullable3.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable3.HasValue & nullable1.HasValue)
          {
            nullable9 = curyUnappliedBal;
            nullable5 = new Decimal?(0M);
            goto label_26;
          }
        }
      }
    }
    if (current != null)
    {
      nullable1 = curyUnappliedBal;
      Decimal num8 = 0M;
      if (nullable1.GetValueOrDefault() > num8 & nullable1.HasValue)
      {
        nullable1 = adj.AdjgBalSign;
        Decimal num9 = 0M;
        if (nullable1.GetValueOrDefault() > num9 & nullable1.HasValue)
        {
          nullable9 = new Decimal?(Math.Min(nullable9.Value, curyUnappliedBal.Value));
          goto label_26;
        }
      }
    }
    if (current != null)
    {
      nullable1 = curyUnappliedBal;
      Decimal num10 = 0M;
      if (nullable1.GetValueOrDefault() <= num10 & nullable1.HasValue)
      {
        nullable1 = current.CuryTranAmt;
        Decimal num11 = 0M;
        if (nullable1.GetValueOrDefault() > num11 & nullable1.HasValue)
          nullable9 = new Decimal?(0M);
      }
    }
label_26:
    adj.CuryAdjgAmt = nullable9;
    adj.CuryAdjgDiscAmt = nullable5;
    adj.CuryAdjgWhTaxAmt = adj.CuryWhTaxBal;
    balanceCalculator.CalcBalances<T>(adj, invoice.Entity, true, true, (APTran) null);
  }

  protected virtual void APAdjust_CuryDocBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    if (!this.internalCall)
    {
      if (e.Row != null && row.AdjdCuryInfoID.HasValue && !row.CuryDocBal.HasValue && sender.GetStatus(e.Row) != 3)
        this.CalcBalances(row, false);
      if (e.Row != null)
        e.NewValue = (object) row.CuryDocBal;
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void APAdjust_CuryDiscBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    if (!this.internalCall)
    {
      if (e.Row != null && row.AdjdCuryInfoID.HasValue && !row.CuryDiscBal.HasValue && sender.GetStatus(e.Row) != 3)
        this.CalcBalances(row, false);
      if (e.Row != null)
        e.NewValue = (object) row.CuryDiscBal;
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void APAdjust_CuryWhTaxBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    if (!this.internalCall)
    {
      if (e.Row != null && row.AdjdCuryInfoID.HasValue && !row.CuryWhTaxBal.HasValue && sender.GetStatus(e.Row) != 3)
        this.CalcBalances(row, false);
      if (e.Row != null)
        e.NewValue = (object) row.CuryWhTaxBal;
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void APAdjust_AdjdCuryRate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((Decimal) e.NewValue <= 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  protected virtual void APAdjust_CuryAdjgAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    Decimal? nullable;
    if (row.CuryDocBal.HasValue)
    {
      nullable = row.CuryDiscBal;
      if (nullable.HasValue)
      {
        nullable = row.CuryWhTaxBal;
        if (nullable.HasValue)
          goto label_4;
      }
    }
    this.CalcBalances(row, false);
label_4:
    nullable = row.CuryDocBal;
    if (!nullable.HasValue)
      throw new PXSetPropertyException<PX.Objects.AP.APAdjust.adjdRefNbr>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AP.APAdjust.adjdRefNbr>(sender)
      });
    int? voidAdjNbr = row.VoidAdjNbr;
    if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    voidAdjNbr = row.VoidAdjNbr;
    if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    nullable = row.CuryDocBal;
    Decimal num1 = nullable.Value;
    nullable = row.CuryAdjgAmt;
    Decimal num2 = nullable.Value;
    if (num1 + num2 - (Decimal) e.NewValue < 0M)
    {
      object[] objArray = new object[1];
      nullable = row.CuryDocBal;
      Decimal num3 = nullable.Value;
      nullable = row.CuryAdjgAmt;
      Decimal num4 = nullable.Value;
      objArray[0] = (object) (num3 + num4).ToString();
      throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
    }
  }

  protected virtual void APAdjust_CuryAdjgAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    if (e.OldValue != null)
    {
      Decimal? nullable = row.CuryDocBal;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      {
        nullable = row.CuryAdjgAmt;
        Decimal oldValue = (Decimal) e.OldValue;
        if (nullable.GetValueOrDefault() < oldValue & nullable.HasValue)
          row.CuryAdjgDiscAmt = new Decimal?(0M);
      }
    }
    this.CalcBalances(row, true);
  }

  protected virtual void APAdjust_CuryAdjgDiscAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    Decimal? nullable1;
    if (row.CuryDocBal.HasValue)
    {
      nullable1 = row.CuryDiscBal;
      if (nullable1.HasValue)
      {
        nullable1 = row.CuryWhTaxBal;
        if (nullable1.HasValue)
          goto label_4;
      }
    }
    this.CalcBalances(row, false);
label_4:
    nullable1 = row.CuryDocBal;
    if (nullable1.HasValue)
    {
      nullable1 = row.CuryDiscBal;
      if (nullable1.HasValue)
      {
        int? voidAdjNbr = row.VoidAdjNbr;
        if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        voidAdjNbr = row.VoidAdjNbr;
        if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        nullable1 = row.CuryDiscBal;
        Decimal num1 = nullable1.Value;
        nullable1 = row.CuryAdjgDiscAmt;
        Decimal num2 = nullable1.Value;
        if (num1 + num2 - (Decimal) e.NewValue < 0M)
        {
          object[] objArray = new object[1];
          nullable1 = row.CuryDiscBal;
          Decimal num3 = nullable1.Value;
          nullable1 = row.CuryAdjgDiscAmt;
          Decimal num4 = nullable1.Value;
          objArray[0] = (object) (num3 + num4).ToString();
          throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
        }
        nullable1 = row.CuryAdjgAmt;
        if (!nullable1.HasValue)
          return;
        if (sender.GetValuePending<PX.Objects.AP.APAdjust.curyAdjgAmt>(e.Row) != PXCache.NotSetValue)
        {
          nullable1 = (Decimal?) sender.GetValuePending<PX.Objects.AP.APAdjust.curyAdjgAmt>(e.Row);
          Decimal? curyAdjgAmt = row.CuryAdjgAmt;
          if (!(nullable1.GetValueOrDefault() == curyAdjgAmt.GetValueOrDefault() & nullable1.HasValue == curyAdjgAmt.HasValue))
            return;
        }
        Decimal? nullable2 = row.CuryDocBal;
        Decimal num5 = nullable2.Value;
        nullable2 = row.CuryAdjgDiscAmt;
        Decimal num6 = nullable2.Value;
        if (!(num5 + num6 - (Decimal) e.NewValue < 0M))
          return;
        object[] objArray1 = new object[1];
        nullable2 = row.CuryDocBal;
        Decimal num7 = nullable2.Value;
        nullable2 = row.CuryAdjgDiscAmt;
        Decimal num8 = nullable2.Value;
        objArray1[0] = (object) (num7 + num8).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray1);
      }
    }
    throw new PXSetPropertyException<PX.Objects.AP.APAdjust.adjdRefNbr>("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AP.APAdjust.adjdRefNbr>(sender)
    });
  }

  protected virtual void APAdjust_CuryAdjgDiscAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CalcBalances((PX.Objects.AP.APAdjust) e.Row, true);
  }

  protected virtual void APAdjust_CuryAdjgWhTaxAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    Decimal? nullable1;
    if (row.CuryDocBal.HasValue)
    {
      nullable1 = row.CuryDiscBal;
      if (nullable1.HasValue)
      {
        nullable1 = row.CuryWhTaxBal;
        if (nullable1.HasValue)
          goto label_4;
      }
    }
    this.CalcBalances(row, false);
label_4:
    nullable1 = row.CuryDocBal;
    if (nullable1.HasValue)
    {
      nullable1 = row.CuryWhTaxBal;
      if (nullable1.HasValue)
      {
        int? voidAdjNbr = row.VoidAdjNbr;
        if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        voidAdjNbr = row.VoidAdjNbr;
        if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        nullable1 = row.CuryWhTaxBal;
        Decimal num1 = nullable1.Value;
        nullable1 = row.CuryAdjgWhTaxAmt;
        Decimal num2 = nullable1.Value;
        if (num1 + num2 - (Decimal) e.NewValue < 0M)
        {
          object[] objArray = new object[1];
          nullable1 = row.CuryWhTaxBal;
          Decimal num3 = nullable1.Value;
          nullable1 = row.CuryAdjgWhTaxAmt;
          Decimal num4 = nullable1.Value;
          objArray[0] = (object) (num3 + num4).ToString();
          throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
        }
        nullable1 = row.CuryAdjgAmt;
        if (!nullable1.HasValue)
          return;
        if (sender.GetValuePending<PX.Objects.AP.APAdjust.curyAdjgAmt>(e.Row) != PXCache.NotSetValue)
        {
          nullable1 = (Decimal?) sender.GetValuePending<PX.Objects.AP.APAdjust.curyAdjgAmt>(e.Row);
          Decimal? curyAdjgAmt = row.CuryAdjgAmt;
          if (!(nullable1.GetValueOrDefault() == curyAdjgAmt.GetValueOrDefault() & nullable1.HasValue == curyAdjgAmt.HasValue))
            return;
        }
        Decimal? nullable2 = row.CuryDocBal;
        Decimal num5 = nullable2.Value;
        nullable2 = row.CuryAdjgWhTaxAmt;
        Decimal num6 = nullable2.Value;
        if (!(num5 + num6 - (Decimal) e.NewValue < 0M))
          return;
        object[] objArray1 = new object[1];
        nullable2 = row.CuryDocBal;
        Decimal num7 = nullable2.Value;
        nullable2 = row.CuryAdjgWhTaxAmt;
        Decimal num8 = nullable2.Value;
        objArray1[0] = (object) (num7 + num8).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray1);
      }
    }
    throw new PXSetPropertyException<PX.Objects.AP.APAdjust.adjdRefNbr>("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AP.APAdjust.adjdRefNbr>(sender)
    });
  }

  protected virtual void APAdjust_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    if (row == null || this.internalCall)
      return;
    bool? nullable = row.Released;
    bool flag1 = false;
    bool flag2 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue;
    PXCache pxCache1 = cache;
    PX.Objects.AP.APAdjust apAdjust1 = row;
    int num1;
    if (flag2)
    {
      nullable = row.Voided;
      if (!nullable.GetValueOrDefault())
      {
        num1 = row.AdjdRefNbr == null ? 1 : 0;
        goto label_5;
      }
    }
    num1 = 0;
label_5:
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.adjdDocType>(pxCache1, (object) apAdjust1, num1 != 0);
    PXCache pxCache2 = cache;
    PX.Objects.AP.APAdjust apAdjust2 = row;
    int num2;
    if (flag2)
    {
      nullable = row.Voided;
      if (!nullable.GetValueOrDefault())
      {
        num2 = row.AdjdRefNbr == null ? 1 : 0;
        goto label_9;
      }
    }
    num2 = 0;
label_9:
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.adjdRefNbr>(pxCache2, (object) apAdjust2, num2 != 0);
    PXCache pxCache3 = cache;
    PX.Objects.AP.APAdjust apAdjust3 = row;
    int num3;
    if (flag2)
    {
      nullable = row.Voided;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.curyAdjgAmt>(pxCache3, (object) apAdjust3, num3 != 0);
    PXCache pxCache4 = cache;
    PX.Objects.AP.APAdjust apAdjust4 = row;
    int num4;
    if (flag2)
    {
      nullable = row.Voided;
      num4 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.curyAdjgDiscAmt>(pxCache4, (object) apAdjust4, num4 != 0);
    PXCache pxCache5 = cache;
    PX.Objects.AP.APAdjust apAdjust5 = row;
    int num5;
    if (flag2)
    {
      nullable = row.Voided;
      num5 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.curyAdjgWhTaxAmt>(pxCache5, (object) apAdjust5, num5 != 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.adjBatchNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.APAdjust.adjBatchNbr>(cache, (object) row, !flag2);
  }

  protected virtual void APAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    string error = PXUIFieldAttribute.GetError<PX.Objects.AP.APAdjust.adjdRefNbr>(sender, e.Row);
    ((CancelEventArgs) e).Cancel = ((PX.Objects.AP.APAdjust) e.Row).AdjdRefNbr == null || !string.IsNullOrEmpty(error);
  }

  public virtual void APAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.AP.APAdjust row = (PX.Objects.AP.APAdjust) e.Row;
    JournalWithSubEntry.GLTranDocAP parentDoc = PXResultset<JournalWithSubEntry.GLTranDocAP>.op_Implicit(((PXSelectBase<JournalWithSubEntry.GLTranDocAP>) this.APPayments).Search<JournalWithSubEntry.GLTranDocAP.tranType, JournalWithSubEntry.GLTranDocAP.refNbr>((object) row.AdjgDocType, (object) row.AdjgRefNbr, Array.Empty<object>()));
    PXCache cache = ((PXSelectBase) this.APPayments).Cache;
    if ((e.Operation & 3) == 3)
      return;
    if (parentDoc == null)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (JournalWithSubEntry.AdjdDocDateIsAfterParentTranDate((IAdjustment) row, (GLTranDoc) parentDoc))
      {
        if (!(row.AdjgDocType != "CHK") || !(row.AdjgDocType != "VCK") || !(row.AdjgDocType != "PPM"))
        {
          bool? earlyChecks = ((PXSelectBase<APSetup>) this.apsetup).Current.EarlyChecks;
          bool flag = false;
          if (!(earlyChecks.GetValueOrDefault() == flag & earlyChecks.HasValue))
            goto label_9;
        }
        if (sender.RaiseExceptionHandling<PX.Objects.AP.APAdjust.adjdRefNbr>(e.Row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("{0} cannot be less than Document Date.", (PXErrorLevel) 5, new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<JournalWithSubEntry.GLTranDocAP.tranDate>(cache)
        })))
          throw new PXRowPersistingException(PXDataUtils.FieldName<PX.Objects.AP.APAdjust.adjdDocDate>(), (object) row.AdjdDocDate, "{0} cannot be less than Document Date.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<JournalWithSubEntry.GLTranDocAP.tranDate>(cache)
          });
      }
label_9:
      if (row.AdjdFinPeriodID.CompareTo(parentDoc.FinPeriodID) > 0)
      {
        if (!(row.AdjgDocType != "CHK") || !(row.AdjgDocType != "VCK") || !(row.AdjgDocType != "PPM"))
        {
          bool? earlyChecks = ((PXSelectBase<APSetup>) this.apsetup).Current.EarlyChecks;
          bool flag = false;
          if (!(earlyChecks.GetValueOrDefault() == flag & earlyChecks.HasValue))
            goto label_14;
        }
        if (sender.RaiseExceptionHandling<PX.Objects.AP.APAdjust.adjdRefNbr>(e.Row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("{0} cannot be less than Document Financial Period.", (PXErrorLevel) 5, new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<JournalWithSubEntry.GLTranDocAP.finPeriodID>(cache)
        })))
          throw new PXRowPersistingException(PXDataUtils.FieldName<PX.Objects.AP.APAdjust.adjdFinPeriodID>(), (object) row.AdjdFinPeriodID, "{0} cannot be less than Document Financial Period.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<JournalWithSubEntry.GLTranDocAP.finPeriodID>(cache)
          });
      }
label_14:
      if (row.AdjdDocType == "PPM")
      {
        row.AdjdCuryInfoID = parentDoc.CuryInfoID;
        row.AdjdOrigCuryInfoID = parentDoc.CuryInfoID;
      }
      Decimal? nullable = row.CuryDocBal;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
        sender.RaiseExceptionHandling<PX.Objects.AP.APAdjust.curyAdjgAmt>(e.Row, (object) row.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      if (row.AdjgDocType != "QCK")
      {
        nullable = row.CuryDiscBal;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
          sender.RaiseExceptionHandling<PX.Objects.AP.APAdjust.curyAdjgDiscAmt>(e.Row, (object) row.CuryAdjgDiscAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      }
      if (!(row.AdjgDocType != "QCK"))
        return;
      nullable = row.CuryWhTaxBal;
      Decimal num3 = 0M;
      if (!(nullable.GetValueOrDefault() < num3 & nullable.HasValue))
        return;
      sender.RaiseExceptionHandling<PX.Objects.AP.APAdjust.curyAdjgWhTaxAmt>(e.Row, (object) row.CuryAdjgWhTaxAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    }
  }

  protected virtual void APAdjust_CuryAdjgWhTaxAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CalcBalances((PX.Objects.AP.APAdjust) e.Row, true);
  }

  protected virtual GLTranDoc FindMatchingAdjd(PX.Objects.AP.APAdjust row)
  {
    return PXResultset<GLTranDoc>.op_Implicit(PXSelectBase<GLTranDoc, PXSelect<GLTranDoc, Where<GLTranDoc.batchNbr, Equal<Current<GLDocBatch.batchNbr>>, And<GLTranDoc.parentLineNbr, IsNull, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAP>, And<GLTranDoc.bAccountID, Equal<Required<GLTranDoc.bAccountID>>, And<GLTranDoc.tranType, Equal<Required<PX.Objects.AP.APAdjust.adjdDocType>>, And<GLTranDoc.refNbr, Equal<Required<GLTranDoc.refNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.VendorID,
      (object) row.AdjdDocType,
      (object) row.AdjdRefNbr
    }));
  }

  private void CalcBalances(PX.Objects.AP.APAdjust row, bool isCalcRGOL)
  {
    this.CalcBalances(row, isCalcRGOL, !this.TakeDiscAlways);
  }

  private void CalcBalances(PX.Objects.AP.APAdjust row, bool isCalcRGOL, bool DiscOnDiscDate)
  {
    PX.Objects.AP.APAdjust apAdjust = row;
    APPaymentBalanceCalculator balanceCalculator = new APPaymentBalanceCalculator((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo_CuryInfoID);
    using (IEnumerator<PXResult<PX.Objects.AP.APInvoice>> enumerator = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.APInvoice_VendorID_DocType_RefNbr).Select(new object[3]
    {
      (object) apAdjust.VendorID,
      (object) apAdjust.AdjdDocType,
      (object) apAdjust.AdjdRefNbr
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PX.Objects.AP.APInvoice originalInvoice = PXResult<PX.Objects.AP.APInvoice>.op_Implicit(enumerator.Current);
        balanceCalculator.CalcBalances<PX.Objects.AP.APInvoice>(apAdjust, originalInvoice, isCalcRGOL, DiscOnDiscDate, (APTran) null);
        return;
      }
    }
    GLTranDoc matchingAdjd = this.FindMatchingAdjd(apAdjust);
    if (matchingAdjd != null)
    {
      GLTranDoc copy = (GLTranDoc) ((PXGraph) this).Caches[typeof (GLTranDoc)].CreateCopy((object) matchingAdjd);
      GLTranDoc glTranDoc1 = copy;
      Decimal? curyApplAmt = glTranDoc1.CuryApplAmt;
      Decimal valueOrDefault1 = row.CuryAdjdAmt.GetValueOrDefault();
      glTranDoc1.CuryApplAmt = curyApplAmt.HasValue ? new Decimal?(curyApplAmt.GetValueOrDefault() - valueOrDefault1) : new Decimal?();
      GLTranDoc glTranDoc2 = copy;
      Decimal? nullable1 = glTranDoc2.CuryDiscTaken;
      Decimal? nullable2 = row.CuryAdjdDiscAmt;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault2);
      glTranDoc2.CuryDiscTaken = nullable3;
      GLTranDoc glTranDoc3 = copy;
      nullable1 = glTranDoc3.CuryTaxWheld;
      nullable2 = row.CuryAdjdWhTaxAmt;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault3);
      glTranDoc3.CuryTaxWheld = nullable4;
      balanceCalculator.CalcBalances<GLTranDoc>(apAdjust, copy, isCalcRGOL, DiscOnDiscDate, (APTran) null);
    }
    else
    {
      foreach (PXResult<PX.Objects.AP.APPayment> pxResult in ((PXSelectBase<PX.Objects.AP.APPayment>) this.APPayment_VendorID_DocType_RefNbr).Select(new object[3]
      {
        (object) apAdjust.VendorID,
        (object) apAdjust.AdjdDocType,
        (object) apAdjust.AdjdRefNbr
      }))
      {
        PX.Objects.AP.APPayment originalInvoice = PXResult<PX.Objects.AP.APPayment>.op_Implicit(pxResult);
        balanceCalculator.CalcBalances<PX.Objects.AP.APPayment>(apAdjust, originalInvoice, isCalcRGOL, DiscOnDiscDate, (APTran) null);
      }
    }
  }

  protected virtual void ARAdjust_AdjdRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    JournalWithSubEntry.GLTranDocAR current = ((PXSelectBase<JournalWithSubEntry.GLTranDocAR>) this.ARPayments).Current;
    ((CancelEventArgs) e).Cancel = current != null && current.TranType == "CSL";
  }

  protected virtual void ARAdjust_AdjdDocType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    sender.SetDefaultExt<PX.Objects.AR.ARAdjust.adjdRefNbr>(e.Row);
  }

  protected virtual void ARAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AR.ARAdjust row))
      return;
    try
    {
      using (IEnumerator<PXResult<PX.Objects.AR.ARInvoice>> enumerator = PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.ARInvoice.curyInfoID>>>, Where<PX.Objects.AR.ARInvoice.customerID, Equal<Current<JournalWithSubEntry.GLTranDocAR.bAccountID>>, And<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.AdjdDocType,
        (object) row.AdjdRefNbr
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.CurrencyInfo> current = (PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.CurrencyInfo>) enumerator.Current;
          this.ARAdjust_AdjdRefNbr_FieldUpdated<JournalWithSubEntry.ARRegisterAdapter<PX.Objects.AR.ARInvoice>>(new JournalWithSubEntry.ARRegisterAdapter<PX.Objects.AR.ARInvoice>(PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.CurrencyInfo>.op_Implicit(current)), PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.CurrencyInfo>.op_Implicit(current), row);
          return;
        }
      }
      using (IEnumerator<PXResult<GLTranDoc>> enumerator = PXSelectBase<GLTranDoc, PXSelectJoin<GLTranDoc, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<GLTranDoc.curyInfoID>>>, Where<GLTranDoc.batchNbr, Equal<Current<GLDocBatch.batchNbr>>, And<GLTranDoc.parentLineNbr, IsNull, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAR>, And<GLTranDoc.bAccountID, Equal<Required<GLTranDoc.bAccountID>>, And<GLTranDoc.tranType, Equal<Required<PX.Objects.AP.APAdjust.adjdDocType>>, And<GLTranDoc.refNbr, Equal<Required<GLTranDoc.refNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.CustomerID,
        (object) row.AdjdDocType,
        (object) row.AdjdRefNbr
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<GLTranDoc, PX.Objects.CM.CurrencyInfo> current = (PXResult<GLTranDoc, PX.Objects.CM.CurrencyInfo>) enumerator.Current;
          this.ARAdjust_AdjdRefNbr_FieldUpdated<JournalWithSubEntry.GlRegisterAdapter<GLTranDoc>>(new JournalWithSubEntry.GlRegisterAdapter<GLTranDoc>(PXResult<GLTranDoc, PX.Objects.CM.CurrencyInfo>.op_Implicit(current)), PXResult<GLTranDoc, PX.Objects.CM.CurrencyInfo>.op_Implicit(current), row);
          return;
        }
      }
      foreach (PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.AR.ARPayment, PXSelectJoin<PX.Objects.AR.ARPayment, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.ARPayment.curyInfoID>>>, Where<PX.Objects.AR.ARPayment.customerID, Equal<Current<JournalWithSubEntry.GLTranDocAR.bAccountID>>, And<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.AdjdDocType,
        (object) row.AdjdRefNbr
      }))
        this.ARAdjust_AdjdRefNbr_FieldUpdated<JournalWithSubEntry.ARRegisterAdapter<PX.Objects.AR.ARPayment>>(new JournalWithSubEntry.ARRegisterAdapter<PX.Objects.AR.ARPayment>(PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult)), PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult), row);
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
  }

  private void ARAdjust_AdjdRefNbr_FieldUpdated<T>(
    T invoice,
    PX.Objects.CM.CurrencyInfo aInvoiceInfo,
    PX.Objects.AR.ARAdjust adj)
    where T : class, IRegister, IInvoice
  {
    JournalWithSubEntry.GLTranDocAR current = ((PXSelectBase<JournalWithSubEntry.GLTranDocAR>) this.ARPayments).Current;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Select(new object[1]
    {
      (object) invoice.BAccountID
    }));
    DateTime? tranDate1 = current.TranDate;
    DateTime? tranDate2 = current.TranDate;
    PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(aInvoiceInfo);
    copy.CuryInfoID = new long?();
    PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.currencyinfo).Cache.Insert((object) copy);
    info.SetCuryEffDate(((PXSelectBase) this.currencyinfo).Cache, (object) tranDate2);
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfoEx).Insert(PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info));
    adj.CustomerID = invoice.BAccountID;
    adj.AdjgDocDate = tranDate1;
    adj.AdjgCuryInfoID = current.CuryInfoID;
    adj.AdjdCustomerID = invoice.BAccountID;
    adj.AdjdCuryInfoID = info.CuryInfoID;
    adj.AdjdOrigCuryInfoID = aInvoiceInfo.CuryInfoID;
    adj.AdjdBranchID = invoice.BranchID;
    adj.AdjdARAcct = invoice.AccountID;
    adj.AdjdARSub = invoice.SubID;
    adj.AdjdDocDate = invoice.DocDate;
    adj.AdjdFinPeriodID = invoice.FinPeriodID;
    adj.Released = new bool?(false);
    this.CalcBalances<T>(adj, customer, invoice, false, true);
    Decimal? curyDocBal = adj.CuryDocBal;
    Decimal? curyDiscBal1 = adj.CuryDiscBal;
    Decimal? nullable1 = curyDocBal.HasValue & curyDiscBal1.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - curyDiscBal1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable2 = adj.CuryDiscBal;
    Decimal? curyUnappliedBal = current.CuryUnappliedBal;
    Decimal? curyDiscBal2 = adj.CuryDiscBal;
    Decimal num1 = 0M;
    Decimal? nullable3;
    Decimal? nullable4;
    Decimal? nullable5;
    if (curyDiscBal2.GetValueOrDefault() >= num1 & curyDiscBal2.HasValue)
    {
      nullable3 = adj.CuryDocBal;
      nullable4 = adj.CuryDiscBal;
      nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      if (nullable5.GetValueOrDefault() <= num2 & nullable5.HasValue)
        return;
    }
    if (current != null)
      string.IsNullOrEmpty(current.TranDesc);
    if (current != null)
    {
      nullable5 = adj.AdjgBalSign;
      Decimal num3 = 0M;
      if (nullable5.GetValueOrDefault() < num3 & nullable5.HasValue)
      {
        nullable5 = curyUnappliedBal;
        Decimal num4 = 0M;
        if (nullable5.GetValueOrDefault() < num4 & nullable5.HasValue)
        {
          nullable1 = new Decimal?(Math.Min(nullable1.Value, Math.Abs(curyUnappliedBal.Value)));
          goto label_17;
        }
        goto label_17;
      }
    }
    if (current != null)
    {
      nullable5 = curyUnappliedBal;
      Decimal num5 = 0M;
      if (nullable5.GetValueOrDefault() > num5 & nullable5.HasValue)
      {
        nullable5 = adj.AdjgBalSign;
        Decimal num6 = 0M;
        if (nullable5.GetValueOrDefault() > num6 & nullable5.HasValue)
        {
          nullable1 = new Decimal?(Math.Min(nullable1.Value, curyUnappliedBal.Value));
          nullable3 = nullable1;
          Decimal? nullable6 = nullable2;
          nullable5 = nullable3.HasValue & nullable6.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
          nullable4 = adj.CuryDocBal;
          if (nullable5.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable5.HasValue & nullable4.HasValue)
          {
            nullable2 = new Decimal?(0M);
            goto label_17;
          }
          goto label_17;
        }
      }
    }
    if (current != null)
    {
      nullable4 = curyUnappliedBal;
      Decimal num7 = 0M;
      if (nullable4.GetValueOrDefault() <= num7 & nullable4.HasValue)
      {
        nullable4 = current.CuryTranAmt;
        Decimal num8 = 0M;
        if (nullable4.GetValueOrDefault() > num8 & nullable4.HasValue)
        {
          nullable1 = new Decimal?(0M);
          nullable2 = new Decimal?(0M);
        }
      }
    }
label_17:
    adj.CuryAdjgAmt = nullable1;
    adj.CuryAdjgDiscAmt = nullable2;
    adj.CuryAdjgWOAmt = new Decimal?(0M);
    this.CalcBalances<T>(adj, customer, invoice, true, true);
  }

  protected virtual void ARAdjust_CuryDocBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    PX.Objects.AR.ARAdjust row = (PX.Objects.AR.ARAdjust) e.Row;
    if (!this.internalCallAR)
    {
      if (e.Row != null && ((PX.Objects.AR.ARAdjust) e.Row).AdjdCuryInfoID.HasValue && !((PX.Objects.AR.ARAdjust) e.Row).CuryDocBal.HasValue && sender.GetStatus(e.Row) != 3)
        this.CalcBalances(row, false);
      if (e.Row != null)
        e.NewValue = (object) ((PX.Objects.AR.ARAdjust) e.Row).CuryDocBal;
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARAdjust_CuryDiscBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    PX.Objects.AR.ARAdjust row = (PX.Objects.AR.ARAdjust) e.Row;
    if (!this.internalCallAR)
    {
      if (e.Row != null && ((PX.Objects.AR.ARAdjust) e.Row).AdjdCuryInfoID.HasValue && !((PX.Objects.AR.ARAdjust) e.Row).CuryDiscBal.HasValue && sender.GetStatus(e.Row) != 3)
        this.CalcBalances(row, false);
      if (e.Row != null)
        e.NewValue = (object) ((PX.Objects.AR.ARAdjust) e.Row).CuryDiscBal;
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARAdjust_CuryAdjgAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.AR.ARAdjust row = (PX.Objects.AR.ARAdjust) e.Row;
    Decimal? nullable;
    if (row.CuryDocBal.HasValue)
    {
      nullable = row.CuryDiscBal;
      if (nullable.HasValue)
      {
        nullable = row.CuryWOBal;
        if (nullable.HasValue)
          goto label_4;
      }
    }
    this.CalcBalances(row, false, false);
label_4:
    nullable = row.CuryDocBal;
    if (!nullable.HasValue)
      throw new PXSetPropertyException<PX.Objects.AR.ARAdjust.adjdRefNbr>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AR.ARAdjust.adjdRefNbr>(sender)
      });
    int? voidAdjNbr = row.VoidAdjNbr;
    if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    voidAdjNbr = row.VoidAdjNbr;
    if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    nullable = row.CuryDocBal;
    Decimal num1 = nullable.Value;
    nullable = row.CuryAdjgAmt;
    Decimal num2 = nullable.Value;
    if (num1 + num2 - (Decimal) e.NewValue < 0M)
    {
      object[] objArray = new object[1];
      nullable = row.CuryDocBal;
      Decimal num3 = nullable.Value;
      nullable = row.CuryAdjgAmt;
      Decimal num4 = nullable.Value;
      objArray[0] = (object) (num3 + num4).ToString();
      throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
    }
  }

  protected virtual void ARAdjust_CuryAdjgAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.AR.ARAdjust row = (PX.Objects.AR.ARAdjust) e.Row;
    if (e.OldValue != null)
    {
      Decimal? nullable = row.CuryDocBal;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      {
        nullable = row.CuryAdjgAmt;
        Decimal oldValue = (Decimal) e.OldValue;
        if (nullable.GetValueOrDefault() < oldValue & nullable.HasValue)
          row.CuryAdjgDiscAmt = new Decimal?(0M);
      }
    }
    this.CalcBalances(row, true);
  }

  protected virtual void ARAdjust_CuryAdjgDiscAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.AR.ARAdjust row = (PX.Objects.AR.ARAdjust) e.Row;
    Decimal? nullable1;
    if (row.CuryDocBal.HasValue)
    {
      nullable1 = row.CuryDiscBal;
      if (nullable1.HasValue)
      {
        nullable1 = row.CuryWOBal;
        if (nullable1.HasValue)
          goto label_4;
      }
    }
    this.CalcBalances(row, false, false);
label_4:
    nullable1 = row.CuryDocBal;
    if (nullable1.HasValue)
    {
      nullable1 = row.CuryDiscBal;
      if (nullable1.HasValue)
      {
        int? voidAdjNbr = row.VoidAdjNbr;
        if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        voidAdjNbr = row.VoidAdjNbr;
        if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        nullable1 = row.CuryDiscBal;
        Decimal num1 = nullable1.Value;
        nullable1 = row.CuryAdjgDiscAmt;
        Decimal num2 = nullable1.Value;
        if (num1 + num2 - (Decimal) e.NewValue < 0M)
        {
          object[] objArray = new object[1];
          nullable1 = row.CuryDiscBal;
          Decimal num3 = nullable1.Value;
          nullable1 = row.CuryAdjgDiscAmt;
          Decimal num4 = nullable1.Value;
          objArray[0] = (object) (num3 + num4).ToString();
          throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
        }
        nullable1 = row.CuryAdjgAmt;
        if (!nullable1.HasValue)
          return;
        if (sender.GetValuePending<PX.Objects.AR.ARAdjust.curyAdjgAmt>(e.Row) != PXCache.NotSetValue)
        {
          nullable1 = (Decimal?) sender.GetValuePending<PX.Objects.AR.ARAdjust.curyAdjgAmt>(e.Row);
          Decimal? curyAdjgAmt = row.CuryAdjgAmt;
          if (!(nullable1.GetValueOrDefault() == curyAdjgAmt.GetValueOrDefault() & nullable1.HasValue == curyAdjgAmt.HasValue))
            return;
        }
        Decimal? nullable2 = row.CuryDocBal;
        Decimal num5 = nullable2.Value;
        nullable2 = row.CuryAdjgDiscAmt;
        Decimal num6 = nullable2.Value;
        if (!(num5 + num6 - (Decimal) e.NewValue < 0M))
          return;
        object[] objArray1 = new object[1];
        nullable2 = row.CuryDocBal;
        Decimal num7 = nullable2.Value;
        nullable2 = row.CuryAdjgDiscAmt;
        Decimal num8 = nullable2.Value;
        objArray1[0] = (object) (num7 + num8).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray1);
      }
    }
    throw new PXSetPropertyException<PX.Objects.AR.ARAdjust.adjdRefNbr>("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AR.ARAdjust.adjdRefNbr>(sender)
    });
  }

  protected virtual void ARAdjust_CuryAdjgDiscAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CalcBalances((PX.Objects.AR.ARAdjust) e.Row, true);
  }

  protected virtual void ARAdjust_CuryAdjgWOAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.AR.ARAdjust row = (PX.Objects.AR.ARAdjust) e.Row;
    Decimal? nullable1;
    if (row.CuryDocBal.HasValue)
    {
      nullable1 = row.CuryDiscBal;
      if (nullable1.HasValue)
      {
        nullable1 = row.CuryWOBal;
        if (nullable1.HasValue)
          goto label_4;
      }
    }
    this.CalcBalances(row, false, false);
label_4:
    nullable1 = row.CuryDocBal;
    if (nullable1.HasValue)
    {
      nullable1 = row.CuryWOBal;
      if (nullable1.HasValue)
      {
        int? voidAdjNbr = row.VoidAdjNbr;
        if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        voidAdjNbr = row.VoidAdjNbr;
        if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        nullable1 = row.CuryWOBal;
        Decimal num1 = nullable1.Value;
        nullable1 = row.CuryAdjgWOAmt;
        Decimal num2 = nullable1.Value;
        if (num1 + num2 - (Decimal) e.NewValue < 0M)
        {
          object[] objArray = new object[1];
          nullable1 = row.CuryWOBal;
          Decimal num3 = nullable1.Value;
          nullable1 = row.CuryAdjgWOAmt;
          Decimal num4 = nullable1.Value;
          objArray[0] = (object) (num3 + num4).ToString();
          throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
        }
        nullable1 = row.CuryAdjgAmt;
        if (!nullable1.HasValue)
          return;
        if (sender.GetValuePending<PX.Objects.AR.ARAdjust.curyAdjgAmt>(e.Row) != PXCache.NotSetValue)
        {
          nullable1 = (Decimal?) sender.GetValuePending<PX.Objects.AR.ARAdjust.curyAdjgAmt>(e.Row);
          Decimal? curyAdjgAmt = row.CuryAdjgAmt;
          if (!(nullable1.GetValueOrDefault() == curyAdjgAmt.GetValueOrDefault() & nullable1.HasValue == curyAdjgAmt.HasValue))
            return;
        }
        Decimal? nullable2 = row.CuryDocBal;
        Decimal num5 = nullable2.Value;
        nullable2 = row.CuryAdjgWOAmt;
        Decimal num6 = nullable2.Value;
        if (!(num5 + num6 - (Decimal) e.NewValue < 0M))
          return;
        object[] objArray1 = new object[1];
        nullable2 = row.CuryDocBal;
        Decimal num7 = nullable2.Value;
        nullable2 = row.CuryAdjgWOAmt;
        Decimal num8 = nullable2.Value;
        objArray1[0] = (object) (num7 + num8).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray1);
      }
    }
    throw new PXSetPropertyException<PX.Objects.AR.ARAdjust.adjdRefNbr>("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AR.ARAdjust.adjdRefNbr>(sender)
    });
  }

  protected virtual void ARAdjust_CuryAdjgWOAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CalcBalances((PX.Objects.AR.ARAdjust) e.Row, true, false);
  }

  protected virtual void ARAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    string error = PXUIFieldAttribute.GetError<PX.Objects.AR.ARAdjust.adjdRefNbr>(sender, e.Row);
    ((CancelEventArgs) e).Cancel = ((PX.Objects.AR.ARAdjust) e.Row).AdjdRefNbr == null || !string.IsNullOrEmpty(error);
  }

  protected virtual void ARAdjust_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.AR.ARAdjust row = (PX.Objects.AR.ARAdjust) e.Row;
    if (row == null)
      return;
    bool? nullable = row.Released;
    bool flag1 = !nullable.GetValueOrDefault();
    PXCache pxCache1 = cache;
    PX.Objects.AR.ARAdjust arAdjust1 = row;
    int num1;
    if (flag1)
    {
      nullable = row.Voided;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        num1 = row.AdjdRefNbr == null ? 1 : 0;
        goto label_5;
      }
    }
    num1 = 0;
label_5:
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARAdjust.adjdDocType>(pxCache1, (object) arAdjust1, num1 != 0);
    PXCache pxCache2 = cache;
    PX.Objects.AR.ARAdjust arAdjust2 = row;
    int num2;
    if (flag1)
    {
      nullable = row.Voided;
      bool flag3 = false;
      if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
      {
        num2 = row.AdjdRefNbr == null ? 1 : 0;
        goto label_9;
      }
    }
    num2 = 0;
label_9:
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARAdjust.adjdRefNbr>(pxCache2, (object) arAdjust2, num2 != 0);
    PXCache pxCache3 = cache;
    PX.Objects.AR.ARAdjust arAdjust3 = row;
    int num3;
    if (flag1)
    {
      nullable = row.Voided;
      bool flag4 = false;
      num3 = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARAdjust.curyAdjgAmt>(pxCache3, (object) arAdjust3, num3 != 0);
    PXCache pxCache4 = cache;
    PX.Objects.AR.ARAdjust arAdjust4 = row;
    int num4;
    if (flag1)
    {
      nullable = row.Voided;
      bool flag5 = false;
      num4 = nullable.GetValueOrDefault() == flag5 & nullable.HasValue ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARAdjust.curyAdjgDiscAmt>(pxCache4, (object) arAdjust4, num4 != 0);
    PXCache pxCache5 = cache;
    PX.Objects.AR.ARAdjust arAdjust5 = row;
    int num5;
    if (flag1)
    {
      nullable = row.Voided;
      bool flag6 = false;
      num5 = nullable.GetValueOrDefault() == flag6 & nullable.HasValue ? 1 : 0;
    }
    else
      num5 = 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARAdjust.curyAdjgWOAmt>(pxCache5, (object) arAdjust5, num5 != 0);
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARAdjust.adjBatchNbr>(cache, (object) row, !flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARAdjust.adjBatchNbr>(cache, (object) row, false);
  }

  protected virtual void ARAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.AR.ARAdjust row = (PX.Objects.AR.ARAdjust) e.Row;
    JournalWithSubEntry.GLTranDocAR glTranDocAr = PXResultset<JournalWithSubEntry.GLTranDocAR>.op_Implicit(((PXSelectBase<JournalWithSubEntry.GLTranDocAR>) this.ARPayments).Search<JournalWithSubEntry.GLTranDocAR.tranType, JournalWithSubEntry.GLTranDocAR.refNbr>((object) row.AdjgDocType, (object) row.AdjgRefNbr, Array.Empty<object>()));
    PXCache cache = ((PXSelectBase) this.ARPayments).Cache;
    if ((e.Operation & 3) == 3)
      return;
    if (glTranDocAr == null)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      Decimal? nullable = row.CuryDocBal;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
        sender.RaiseExceptionHandling<PX.Objects.AR.ARAdjust.curyAdjgAmt>(e.Row, (object) row.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      nullable = row.CuryDiscBal;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
        sender.RaiseExceptionHandling<PX.Objects.AR.ARAdjust.curyAdjgDiscAmt>(e.Row, (object) row.CuryAdjgDiscAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      nullable = row.CuryWOBal;
      Decimal num3 = 0M;
      if (nullable.GetValueOrDefault() < num3 & nullable.HasValue)
        sender.RaiseExceptionHandling<PX.Objects.AR.ARAdjust.curyAdjgWOAmt>(e.Row, (object) row.CuryAdjgWOAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      nullable = row.CuryAdjgWOAmt;
      Decimal num4 = 0M;
      if (!(nullable.GetValueOrDefault() > num4 & nullable.HasValue) || !string.IsNullOrEmpty(row.WriteOffReasonCode))
        return;
      if (sender.RaiseExceptionHandling<PX.Objects.AR.ARAdjust.writeOffReasonCode>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AR.ARAdjust.writeOffReasonCode>(sender)
      })))
        throw new PXRowPersistingException(PXDataUtils.FieldName<PX.Objects.AR.ARAdjust.writeOffReasonCode>(), (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AR.ARAdjust.writeOffReasonCode>(sender)
        });
    }
  }

  private static bool AdjdDocDateIsAfterParentTranDate(IAdjustment doc, GLTranDoc parentDoc)
  {
    if (!doc.AdjdDocDate.HasValue)
      throw new ArgumentException("doc.AdjdDocDate was null");
    if (!parentDoc.TranDate.HasValue)
      throw new ArgumentException("parentDoc.TranDate was null");
    return doc.AdjdDocDate.Value.CompareTo(parentDoc.TranDate.Value) > 0;
  }

  protected virtual void ARAdjust_CuryAdjgWhTaxAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CalcBalances((PX.Objects.AR.ARAdjust) e.Row, true);
  }

  protected virtual GLTranDoc FindMatchingAdjd(PX.Objects.AR.ARAdjust row)
  {
    return PXResultset<GLTranDoc>.op_Implicit(PXSelectBase<GLTranDoc, PXSelect<GLTranDoc, Where<GLTranDoc.batchNbr, Equal<Current<GLDocBatch.batchNbr>>, And<GLTranDoc.parentLineNbr, IsNull, And<GLTranDoc.tranModule, Equal<PX.Objects.GL.BatchModule.moduleAR>, And<GLTranDoc.bAccountID, Equal<Required<GLTranDoc.bAccountID>>, And<GLTranDoc.tranType, Equal<Required<PX.Objects.AP.APAdjust.adjdDocType>>, And<GLTranDoc.refNbr, Equal<Required<GLTranDoc.refNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.CustomerID,
      (object) row.AdjdDocType,
      (object) row.AdjdRefNbr
    }));
  }

  private void CalcBalances(PX.Objects.AR.ARAdjust row, bool isCalcRGOL)
  {
    this.CalcBalances(row, isCalcRGOL, true);
  }

  private void CalcBalances(PX.Objects.AR.ARAdjust row, bool isCalcRGOL, bool DiscOnDiscDate)
  {
    PX.Objects.AR.ARAdjust arAdjust = row;
    if (arAdjust == null || !arAdjust.CustomerID.HasValue)
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelectReadonly<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) arAdjust.CustomerID
    }));
    using (IEnumerator<PXResult<PX.Objects.AR.ARInvoice>> enumerator = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.ARInvoice_CustomerID_DocType_RefNbr).Select(new object[3]
    {
      (object) arAdjust.CustomerID,
      (object) arAdjust.AdjdDocType,
      (object) arAdjust.AdjdRefNbr
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PX.Objects.AR.ARInvoice invoice = PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(enumerator.Current);
        this.CalcBalances<PX.Objects.AR.ARInvoice>(arAdjust, customer, invoice, isCalcRGOL, DiscOnDiscDate);
        return;
      }
    }
    GLTranDoc matchingAdjd = this.FindMatchingAdjd(arAdjust);
    if (matchingAdjd != null)
    {
      GLTranDoc copy = (GLTranDoc) ((PXGraph) this).Caches[typeof (GLTranDoc)].CreateCopy((object) matchingAdjd);
      GLTranDoc glTranDoc1 = copy;
      Decimal? curyApplAmt = glTranDoc1.CuryApplAmt;
      Decimal valueOrDefault1 = row.CuryAdjdAmt.GetValueOrDefault();
      glTranDoc1.CuryApplAmt = curyApplAmt.HasValue ? new Decimal?(curyApplAmt.GetValueOrDefault() - valueOrDefault1) : new Decimal?();
      GLTranDoc glTranDoc2 = copy;
      Decimal? nullable = glTranDoc2.CuryDiscTaken;
      Decimal valueOrDefault2 = row.CuryAdjdDiscAmt.GetValueOrDefault();
      glTranDoc2.CuryDiscTaken = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - valueOrDefault2) : new Decimal?();
      GLTranDoc glTranDoc3 = copy;
      nullable = glTranDoc3.CuryTaxWheld;
      Decimal valueOrDefault3 = row.CuryAdjdWhTaxAmt.GetValueOrDefault();
      glTranDoc3.CuryTaxWheld = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - valueOrDefault3) : new Decimal?();
      this.CalcBalances<GLTranDoc>(arAdjust, customer, copy, isCalcRGOL, DiscOnDiscDate);
    }
    else
    {
      foreach (PXResult<PX.Objects.AR.ARPayment> pxResult in ((PXSelectBase<PX.Objects.AR.ARPayment>) this.ARPayment_CustomerID_DocType_RefNbr).Select(new object[3]
      {
        (object) arAdjust.CustomerID,
        (object) arAdjust.AdjdDocType,
        (object) arAdjust.AdjdRefNbr
      }))
      {
        PX.Objects.AR.ARPayment invoice = PXResult<PX.Objects.AR.ARPayment>.op_Implicit(pxResult);
        this.CalcBalances<PX.Objects.AR.ARPayment>(arAdjust, customer, invoice, isCalcRGOL, DiscOnDiscDate);
      }
    }
  }

  private void CalcBalances<T>(
    PX.Objects.AR.ARAdjust adj,
    PX.Objects.AR.Customer customer,
    T invoice,
    bool isCalcRGOL,
    bool DiscOnDiscDate)
    where T : class, IInvoice
  {
    Decimal? nullable1;
    if (this._AutoPaymentApp)
    {
      this.internalCallAR = true;
      PX.Objects.AR.ARAdjust arAdjust = PXResultset<PX.Objects.AR.ARAdjust>.op_Implicit(PXSelectBase<PX.Objects.AR.ARAdjust, PXSelectGroupBy<PX.Objects.AR.ARAdjust, Where<PX.Objects.AR.ARAdjust.adjdDocType, Equal<Required<PX.Objects.AR.ARAdjust.adjdDocType>>, And<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<Required<PX.Objects.AR.ARAdjust.adjdRefNbr>>, And<PX.Objects.AR.ARAdjust.released, Equal<False>, And<PX.Objects.AR.ARAdjust.voided, Equal<False>, And<Where<PX.Objects.AR.ARAdjust.adjgDocType, NotEqual<Required<PX.Objects.AR.ARAdjust.adjgDocType>>, Or<PX.Objects.AR.ARAdjust.adjgRefNbr, NotEqual<Required<PX.Objects.AR.ARAdjust.adjgRefNbr>>>>>>>>>, Aggregate<GroupBy<PX.Objects.AR.ARAdjust.adjdDocType, GroupBy<PX.Objects.AR.ARAdjust.adjdRefNbr, Sum<PX.Objects.AR.ARAdjust.curyAdjdAmt, Sum<PX.Objects.AR.ARAdjust.adjAmt, Sum<PX.Objects.AR.ARAdjust.curyAdjdDiscAmt, Sum<PX.Objects.AR.ARAdjust.adjDiscAmt>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
      {
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr,
        (object) adj.AdjgDocType,
        (object) adj.AdjgRefNbr
      }));
      this.internalCallAR = false;
      if (arAdjust != null && arAdjust.AdjdRefNbr != null)
      {
        ref T local1 = ref invoice;
        // ISSUE: variable of a boxed type
        __Boxed<T> local2 = (object) local1;
        Decimal? curyDocBal = local1.CuryDocBal;
        Decimal? curyAdjdAmt = arAdjust.CuryAdjdAmt;
        Decimal? nullable2 = arAdjust.CuryAdjdDiscAmt;
        Decimal? nullable3 = curyAdjdAmt.HasValue & nullable2.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable4;
        if (!(curyDocBal.HasValue & nullable3.HasValue))
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(curyDocBal.GetValueOrDefault() - nullable3.GetValueOrDefault());
        local2.CuryDocBal = nullable4;
        ref T local3 = ref invoice;
        // ISSUE: variable of a boxed type
        __Boxed<T> local4 = (object) local3;
        nullable3 = local3.DocBal;
        Decimal? adjAmt = arAdjust.AdjAmt;
        Decimal? nullable5 = arAdjust.AdjDiscAmt;
        nullable2 = adjAmt.HasValue & nullable5.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable6 = arAdjust.RGOLAmt;
        Decimal? nullable7;
        if (!(nullable2.HasValue & nullable6.HasValue))
        {
          nullable5 = new Decimal?();
          nullable7 = nullable5;
        }
        else
          nullable7 = new Decimal?(nullable2.GetValueOrDefault() + nullable6.GetValueOrDefault());
        nullable1 = nullable7;
        Decimal? nullable8;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable6 = new Decimal?();
          nullable8 = nullable6;
        }
        else
          nullable8 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
        local4.DocBal = nullable8;
        ref T local5 = ref invoice;
        // ISSUE: variable of a boxed type
        __Boxed<T> local6 = (object) local5;
        nullable1 = local5.CuryDiscBal;
        nullable3 = arAdjust.CuryAdjdDiscAmt;
        Decimal? nullable9;
        if (!(nullable1.HasValue & nullable3.HasValue))
        {
          nullable6 = new Decimal?();
          nullable9 = nullable6;
        }
        else
          nullable9 = new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault());
        local6.CuryDiscBal = nullable9;
        ref T local7 = ref invoice;
        // ISSUE: variable of a boxed type
        __Boxed<T> local8 = (object) local7;
        nullable3 = local7.DiscBal;
        nullable1 = arAdjust.AdjDiscAmt;
        Decimal? nullable10;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable6 = new Decimal?();
          nullable10 = nullable6;
        }
        else
          nullable10 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
        local8.DiscBal = nullable10;
      }
      this._AutoPaymentApp = false;
    }
    CuryHelper curyHelper = new CuryHelper((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo_CuryInfoID);
    new PaymentBalanceCalculator((IPXCurrencyHelper) curyHelper).CalcBalances(adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, (IInvoice) invoice, (IAdjustment) adj);
    if (DiscOnDiscDate)
      PaymentEntry.CalcDiscount(adj.AdjgDocDate, (IInvoice) invoice, (IAdjustment) adj);
    PaymentEntry.WarnDiscount<T, PX.Objects.AR.ARAdjust>((PXGraph) this, adj.AdjgDocDate, invoice, adj);
    if (customer != null && customer.SmallBalanceAllow.GetValueOrDefault() && adj.AdjgDocType != "REF" && adj.AdjdDocType != "CRM")
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo_CuryInfoID).Select(new object[1]
      {
        (object) adj.AdjgCuryInfoID
      }));
      PXCache cache = ((PXSelectBase) this.CurrencyInfo_CuryInfoID).Cache;
      PX.Objects.CM.CurrencyInfo info = currencyInfo;
      nullable1 = customer.SmallBalanceLimit;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      Decimal num;
      ref Decimal local = ref num;
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(cache, info, valueOrDefault, out local);
      adj.CuryWOBal = new Decimal?(num);
      adj.WOBal = customer.SmallBalanceLimit;
    }
    else
    {
      adj.CuryWOBal = new Decimal?(0M);
      adj.WOBal = new Decimal?(0M);
    }
    new PaymentBalanceAjuster((IPXCurrencyHelper) curyHelper).AdjustBalance((IAdjustment) adj);
    if (!isCalcRGOL || adj.Voided.GetValueOrDefault())
      return;
    new PaymentRGOLCalculator((IPXCurrencyHelper) curyHelper, (IAdjustment) adj, adj.ReverseGainLoss).Calculate((IInvoice) invoice);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AP.APRegister> e)
  {
    if (e.Row == null || e.Operation != 2 && e.Operation != 1)
      return;
    this.VerifyFinPeriodOnOriginalDocument<PX.Objects.AP.APRegister.finPeriodID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AP.APRegister>>) e).Cache, (object) e.Row, (object) e.Row.FinPeriodID, e.Row.DocType, e.Row.RefNbr);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AR.ARRegister> e)
  {
    if (e.Row == null || e.Operation != 2 && e.Operation != 1)
      return;
    this.VerifyFinPeriodOnOriginalDocument<PX.Objects.AR.ARRegister.finPeriodID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.AR.ARRegister>>) e).Cache, (object) e.Row, (object) e.Row.FinPeriodID, e.Row.DocType, e.Row.RefNbr);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CAAdj> e)
  {
    if (e.Row == null || e.Operation != 2 && e.Operation != 1)
      return;
    this.VerifyFinPeriodOnOriginalDocument<CAAdj.finPeriodID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CAAdj>>) e).Cache, (object) e.Row, (object) e.Row.FinPeriodID, e.Row.AdjTranType, e.Row.AdjRefNbr);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<Batch> e)
  {
    if (e.Row == null || e.Operation != 2 && e.Operation != 1)
      return;
    this.VerifyFinPeriodOnOriginalDocument<Batch.finPeriodID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<Batch>>) e).Cache, (object) e.Row, (object) e.Row.FinPeriodID, "GLE", e.Row.BatchNbr);
  }

  private void VerifyFinPeriodOnOriginalDocument<TFinPeriodID>(
    PXCache cache,
    object origDoc,
    object finPeriodID,
    string tranType,
    string refNbr)
    where TFinPeriodID : IBqlField
  {
    GLTranDoc glTranDoc = PXResultset<GLTranDoc>.op_Implicit(PXSelectBase<GLTranDoc, PXViewOf<GLTranDoc>.BasedOn<SelectFromBase<GLTranDoc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GLTranDoc.tranType, Equal<P.AsString>>>>>.And<BqlOperand<GLTranDoc.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) tranType,
      (object) refNbr
    }));
    if (glTranDoc == null)
      return;
    try
    {
      object obj = finPeriodID;
      cache.RaiseFieldVerifying<TFinPeriodID>(origDoc, ref obj);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.RaiseExceptionHandling<GLTranDoc.tranDate>((object) glTranDoc, (object) glTranDoc.TranDate, (Exception) new PXSetPropertyException<GLTranDoc.tranDate>(((Exception) ex).Message));
    }
    catch (PXOuterException ex)
    {
      ((PXSelectBase) this.GLTranModuleBatNbr).Cache.RaiseExceptionHandling<GLTranDoc.tranDate>((object) glTranDoc, (object) glTranDoc.TranDate, (Exception) new PXSetPropertyException<GLTranDoc.tranDate>(string.Join(". ", ex.InnerMessages), (PXErrorLevel) 4));
    }
  }

  public static bool IsAPInvoice(string aModule, string aTranType)
  {
    return aModule == "AP" && (aTranType == "ADR" || aTranType == "ACR" || aTranType == "INV" || aTranType == "QCK" || aTranType == "VQC");
  }

  public static bool IsAPPayment(string aModule, string aTranType)
  {
    if (aModule == "AP" && aTranType != null && aTranType.Length == 3)
    {
      switch (aTranType[1])
      {
        case 'C':
          if (aTranType == "QCK" || aTranType == "VCK")
            break;
          goto label_9;
        case 'E':
          if (aTranType == "REF")
            break;
          goto label_9;
        case 'H':
          if (aTranType == "CHK")
            break;
          goto label_9;
        case 'P':
          if (aTranType == "PPM")
            break;
          goto label_9;
        case 'Q':
          if (aTranType == "VQC")
            break;
          goto label_9;
        case 'R':
          if (!(aTranType == "VRF"))
            goto label_9;
          break;
        default:
          goto label_9;
      }
      return true;
    }
label_9:
    return false;
  }

  public static bool IsARInvoice(string aModule, string aTranType)
  {
    return aModule == "AR" && (aTranType == "DRM" || aTranType == "CRM" || aTranType == "INV" || aTranType == "CSL" || aTranType == "RCS");
  }

  public static bool IsARPayment(string aModule, string aTranType)
  {
    if (aModule == "AR" && aTranType != null && aTranType.Length == 3)
    {
      switch (aTranType[1])
      {
        case 'C':
          if (aTranType == "RCS")
            break;
          goto label_9;
        case 'E':
          if (aTranType == "REF")
            break;
          goto label_9;
        case 'M':
          if (aTranType == "PMT")
            break;
          goto label_9;
        case 'P':
          if (aTranType == "PPM" || aTranType == "RPM")
            break;
          goto label_9;
        case 'R':
          if (!(aTranType == "VRF"))
            goto label_9;
          break;
        case 'S':
          if (aTranType == "CSL")
            break;
          goto label_9;
        default:
          goto label_9;
      }
      return true;
    }
label_9:
    return false;
  }

  public static bool IsAPPrePayment(string aModule, string aTranType)
  {
    return aModule == "AP" && aTranType == "PPM";
  }

  public static bool IsARInvoice(GLTranDoc aRow)
  {
    return JournalWithSubEntry.IsARInvoice(aRow.TranModule, aRow.TranType);
  }

  public static bool IsARCreditMemo(GLTranDoc aRow)
  {
    return aRow.TranModule == "AR" && aRow.TranType == "CRM";
  }

  public static bool IsARPayment(GLTranDoc aRow)
  {
    return JournalWithSubEntry.IsARPayment(aRow.TranModule, aRow.TranType);
  }

  public static bool IsAPInvoice(GLTranDoc aRow)
  {
    return JournalWithSubEntry.IsAPInvoice(aRow.TranModule, aRow.TranType);
  }

  public static bool IsAPPayment(GLTranDoc aRow)
  {
    return JournalWithSubEntry.IsAPPayment(aRow.TranModule, aRow.TranType);
  }

  public static bool IsAPPrePayment(GLTranDoc aRow)
  {
    return JournalWithSubEntry.IsAPPayment(aRow.TranModule, aRow.TranType);
  }

  public static bool IsMixedType(GLTranDoc row)
  {
    bool flag = false;
    if (row.TranModule == "AP")
      flag = row.TranType == "QCK" || row.TranType == "VQC";
    if (row.TranModule == "AR")
      flag = row.TranType == "CSL" || row.TranType == "RCS";
    return flag;
  }

  protected static bool? IsDebitType(GLTranDoc tran)
  {
    return JournalWithSubEntry.IsDebitType(tran, false);
  }

  protected static bool IsDrCrAcctRequired(GLTranDoc row, bool aCredit)
  {
    bool flag1 = true;
    bool flag2 = true;
    bool? nullable1 = JournalWithSubEntry.IsDebitType(row, true);
    if (nullable1.HasValue)
    {
      if (row.IsChildTran)
      {
        if (nullable1.HasValue)
        {
          flag1 = nullable1.Value;
          flag2 = !nullable1.Value;
        }
      }
      else
      {
        bool? nullable2 = row.Split;
        bool flag3 = nullable2.Value;
        nullable2 = nullable1;
        bool flag4 = false;
        flag1 = nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue || nullable1.Value && !flag3;
        flag2 = nullable1.Value || !nullable1.Value && !flag3;
      }
    }
    else
      flag1 = flag2 = false;
    return !aCredit ? flag1 : flag2;
  }

  protected static bool? IsDebitType(GLTranDoc aRow, bool aAsInvoice)
  {
    bool flag = JournalWithSubEntry.IsMixedType(aRow);
    if (aRow.TranModule == "AP")
    {
      if (flag)
        return new bool?((aAsInvoice ? APInvoiceType.DrCr(aRow.TranType) : APPaymentType.DrCr(aRow.TranType)) == "D");
      if (JournalWithSubEntry.IsAPInvoice(aRow))
        return new bool?(APInvoiceType.DrCr(aRow.TranType) == "D");
      if (JournalWithSubEntry.IsAPPayment(aRow))
        return new bool?(APPaymentType.DrCr(aRow.TranType) == "D");
    }
    if (aRow.TranModule == "AR")
    {
      if (flag)
        return new bool?((aAsInvoice ? ARInvoiceType.DrCr(aRow.TranType) : ARPaymentType.DrCr(aRow.TranType)) == "D");
      if (JournalWithSubEntry.IsARInvoice(aRow))
        return new bool?(ARInvoiceType.DrCr(aRow.TranType) == "D");
      if (JournalWithSubEntry.IsARPayment(aRow))
        return new bool?(ARPaymentType.DrCr(aRow.TranType) == "D");
    }
    if (aRow.TranModule == "CA")
      return new bool?(aRow.CADrCr == "C");
    return aRow.TranModule == "GL" ? new bool?() : new bool?(false);
  }

  protected static bool? IsDebitTran(GLTranDoc aRow)
  {
    int? nullable = aRow.DebitAccountID;
    int num1 = nullable.HasValue ? 1 : 0;
    nullable = aRow.CreditAccountID;
    int num2 = nullable.HasValue ? 1 : 0;
    return num1 == num2 ? new bool?() : new bool?(aRow.DebitAccountID.HasValue);
  }

  protected static bool HasDocumentRow(GLTranDoc aRow) => aRow.TranModule != "GL";

  protected static Decimal GetSignedAmount(GLTranDoc aRow)
  {
    Decimal signedAmount = 0M;
    if (aRow.IsBalanced)
      return signedAmount;
    bool? nullable = JournalWithSubEntry.IsDebitTran(aRow);
    if (nullable.HasValue)
    {
      Decimal valueOrDefault;
      if (!nullable.Value)
      {
        Decimal? curyTranTotal = aRow.CuryTranTotal;
        valueOrDefault = (curyTranTotal.HasValue ? new Decimal?(-curyTranTotal.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      }
      else
        valueOrDefault = aRow.CuryTranTotal.GetValueOrDefault();
      signedAmount = valueOrDefault;
    }
    return signedAmount;
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (viewName == "GLTranModuleBatNbr")
    {
      if (this._ImportedDocs == null)
        this._ImportedDocs = new Dictionary<Pair<string, string>, int>();
      string defValue1 = JournalWithSubEntry.CorrectImportValue(values, "CreditAmt", "0");
      JournalWithSubEntry.CorrectImportValue(values, "CuryCreditAmt", defValue1);
      string defValue2 = JournalWithSubEntry.CorrectImportValue(values, "DebitAmt", "0");
      JournalWithSubEntry.CorrectImportValue(values, "CuryDebitAmt", defValue2);
      if (values.Contains((object) "RefNbr"))
      {
        if (values.Contains((object) "Split") && bool.Parse(values[(object) "Split"].ToString()) && values.Contains((object) "TranCode"))
        {
          Pair<string, string> key1 = new Pair<string, string>((string) values[(object) "TranCode"], (string) values[(object) "RefNbr"]);
          if (this._ImportedDocs.ContainsKey(key1))
            values[(object) "ParentLineNbr"] = (object) this._ImportedDocs[key1].ToString();
          else if (keys[(object) "LineNbr"] != null)
          {
            int num = int.Parse(keys[(object) "LineNbr"].ToString());
            this._ImportedDocs[key1] = num;
          }
          else
          {
            string str1 = ((string) values[(object) "TranCode"]).Trim();
            string str2 = ((string) values[(object) "RefNbr"]).Trim();
            foreach (GLTranDoc glTranDoc in ((PXGraph) this).Caches[typeof (GLTranDoc)].Inserted)
            {
              if (glTranDoc.TranCode == str1 && str2 == glTranDoc.ImportRefNbr)
              {
                int? lineNbr = glTranDoc.LineNbr;
                if (lineNbr.HasValue)
                {
                  Dictionary<Pair<string, string>, int> importedDocs = this._ImportedDocs;
                  Pair<string, string> key2 = key1;
                  lineNbr = glTranDoc.LineNbr;
                  int num = lineNbr.Value;
                  importedDocs[key2] = num;
                  values[(object) "ParentLineNbr"] = (object) this._ImportedDocs[key1].ToString();
                  break;
                }
                break;
              }
            }
          }
        }
        values[(object) "ImportRefNbr"] = values[(object) "RefNbr"];
        values[(object) "RefNbr"] = (object) null;
      }
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "EntryTypeID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "DebitAccountID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "CreditAccountID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "TaxCategoryID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "TaxZoneID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "TermsID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "DebitSubID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "CreditSubID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "PaymentMethodID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "BAccountID", (string) null);
      JournalWithSubEntry.CorrectImportEmptyStrings(values, "LocationID", (string) null);
    }
    return true;
  }

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  private static string CorrectImportEmptyStrings(
    IDictionary dic,
    string fieldName,
    string defValue)
  {
    string str1 = defValue;
    if (!dic.Contains((object) fieldName))
    {
      dic.Add((object) fieldName, (object) defValue);
    }
    else
    {
      string str2 = dic[(object) fieldName] == null ? (string) null : dic[(object) fieldName].ToString();
      if (string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str2.Trim()))
        dic[(object) fieldName] = (object) defValue;
      else
        str1 = str2;
    }
    return str1;
  }

  private static string CorrectImportValue(IDictionary dic, string fieldName, string defValue)
  {
    string str = defValue;
    if (!dic.Contains((object) fieldName))
    {
      dic.Add((object) fieldName, (object) defValue);
    }
    else
    {
      object obj = dic[(object) fieldName];
      string s;
      if (obj == null || string.IsNullOrEmpty(s = obj.ToString()) || !Decimal.TryParse(s, out Decimal _))
        dic[(object) fieldName] = (object) defValue;
      else
        str = s;
    }
    return str;
  }

  public bool RowImported(string viewName, object row, object oldRow) => true;

  public bool RowImporting(string viewName, object row) => true;

  public class JournalWithSubEntryDocumentExtension : 
    DocumentWithLinesGraphExtension<JournalWithSubEntry>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Documents = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>((PXSelectBase) this.Base.BatchModule);
      this.Lines = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine>((PXSelectBase) this.Base.GLTranModuleBatNbr);
    }

    protected override PX.Objects.Common.GraphExtensions.Abstract.Mapping.DocumentMapping GetDocumentMapping()
    {
      return new PX.Objects.Common.GraphExtensions.Abstract.Mapping.DocumentMapping(typeof (GLDocBatch))
      {
        HeaderTranPeriodID = typeof (GLDocBatch.tranPeriodID),
        HeaderDocDate = typeof (GLDocBatch.dateEntered)
      };
    }

    protected override DocumentLineMapping GetDocumentLineMapping()
    {
      return new DocumentLineMapping(typeof (GLTranDoc));
    }
  }

  [Serializable]
  public class RefDocKey : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _TranModule;
    protected string _TranType;
    protected string _RefNbr;

    [Branch(null, null, true, true, true, IsKey = true)]
    public virtual int? BranchID { get; set; }

    [PXDBString(2, IsFixed = true, IsKey = true)]
    public virtual string TranModule
    {
      get => this._TranModule;
      set => this._TranModule = value;
    }

    [PXDBString(3, IsFixed = true, IsKey = true)]
    public virtual string TranType
    {
      get => this._TranType;
      set => this._TranType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    public virtual string RefNbr
    {
      get => this._RefNbr;
      set => this._RefNbr = value;
    }

    public virtual void Copy(GLTranDoc src)
    {
      this.BranchID = src.BranchID;
      this.TranModule = src.TranModule;
      this.TranType = src.TranType;
      this.RefNbr = src.RefNbr;
    }

    public abstract class branchID : IBqlField, IBqlOperand
    {
    }

    public abstract class tranModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.RefDocKey.tranModule>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.RefDocKey.tranType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.RefDocKey.refNbr>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class GLTranDocAP : GLTranDoc
  {
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXUIField]
    public override string BatchNbr { get; set; }

    [PXDBInt]
    [PXVendorCustomerSelector(typeof (GLTranDoc.tranModule), typeof (GLTranDoc.curyID), CacheGlobal = true)]
    [PXUIField(DisplayName = "Vendor", Enabled = true, Visible = true, TabOrder = 13)]
    public override int? BAccountID { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField]
    public override string TaxCategoryID { get; set; }

    public new abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.module>
    {
    }

    public new abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.batchNbr>
    {
    }

    public new abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.lineNbr>
    {
    }

    public new abstract class tranModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.tranModule>
    {
    }

    public new abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.tranType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.refNbr>
    {
    }

    public new abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.bAccountID>
    {
    }

    public new abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.branchID>
    {
    }

    public new abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.curyInfoID>
    {
    }

    public new abstract class tranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.tranDate>
    {
    }

    public new abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.finPeriodID>
    {
    }

    public new abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.tranPeriodID>
    {
    }

    public new abstract class curyUnappliedBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.curyUnappliedBal>
    {
    }

    public new abstract class unappliedBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.unappliedBal>
    {
    }

    public new abstract class curyApplAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.curyApplAmt>
    {
    }

    public new abstract class applAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.applAmt>
    {
    }

    public new abstract class taxCategoryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAP.taxCategoryID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class GLTranDocAR : GLTranDoc
  {
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXUIField]
    public override string BatchNbr { get; set; }

    [PXDBInt]
    [PXVendorCustomerSelector(typeof (GLTranDoc.tranModule), typeof (GLTranDoc.curyID), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer", Enabled = true, Visible = true, TabOrder = 13)]
    public override int? BAccountID { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField]
    public override string TaxCategoryID { get; set; }

    public new abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.module>
    {
    }

    public new abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.batchNbr>
    {
    }

    public new abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.lineNbr>
    {
    }

    public new abstract class tranModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.tranModule>
    {
    }

    public new abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.tranType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.refNbr>
    {
    }

    public new abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.bAccountID>
    {
    }

    public new abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.branchID>
    {
    }

    public new abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.curyInfoID>
    {
    }

    public new abstract class tranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.tranDate>
    {
    }

    public new abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.finPeriodID>
    {
    }

    public new abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.tranPeriodID>
    {
    }

    public new abstract class curyUnappliedBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.curyUnappliedBal>
    {
    }

    public new abstract class unappliedBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.unappliedBal>
    {
    }

    public new abstract class curyApplAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.curyApplAmt>
    {
    }

    public new abstract class applAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.applAmt>
    {
    }

    public new abstract class taxCategoryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.GLTranDocAR.taxCategoryID>
    {
    }
  }

  [PXProjection(typeof (Select<PX.Objects.AP.APAdjust>))]
  [PXHidden]
  [Serializable]
  public class APAdjust3 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _AdjgDocType;
    protected string _AdjgRefNbr;
    protected string _AdjdDocType;
    protected string _AdjdRefNbr;
    protected bool? _Released;

    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (PX.Objects.AP.APAdjust.adjgDocType))]
    [PXUIField]
    public virtual string AdjgDocType
    {
      get => this._AdjgDocType;
      set => this._AdjgDocType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AP.APAdjust.adjgRefNbr))]
    [PXUIField]
    public virtual string AdjgRefNbr
    {
      get => this._AdjgRefNbr;
      set => this._AdjgRefNbr = value;
    }

    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (PX.Objects.AP.APAdjust.adjdDocType))]
    [PXDefault("INV")]
    [PXUIField]
    public virtual string AdjdDocType
    {
      get => this._AdjdDocType;
      set => this._AdjdDocType = value;
    }

    [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.AP.APAdjust.adjdRefNbr))]
    [PXDefault]
    [PXUIField]
    public virtual string AdjdRefNbr
    {
      get => this._AdjdRefNbr;
      set => this._AdjdRefNbr = value;
    }

    [PXDBBool(BqlField = typeof (PX.Objects.AP.APAdjust.released))]
    [PXDefault(false)]
    public virtual bool? Released
    {
      get => this._Released;
      set => this._Released = value;
    }

    public abstract class adjgDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.APAdjust3.adjgDocType>
    {
    }

    public abstract class adjgRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.APAdjust3.adjgRefNbr>
    {
    }

    public abstract class adjdDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.APAdjust3.adjdDocType>
    {
    }

    public abstract class adjdRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.APAdjust3.adjdRefNbr>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      JournalWithSubEntry.APAdjust3.released>
    {
    }
  }

  [PXProjection(typeof (Select<PX.Objects.AR.ARAdjust>))]
  [PXHidden]
  [Serializable]
  public class ARAdjust3 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _AdjgDocType;
    protected string _AdjgRefNbr;
    protected string _AdjdDocType;
    protected string _AdjdRefNbr;
    protected bool? _Released;

    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (PX.Objects.AR.ARAdjust.adjgDocType))]
    [PXUIField]
    public virtual string AdjgDocType
    {
      get => this._AdjgDocType;
      set => this._AdjgDocType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AR.ARAdjust.adjgRefNbr))]
    [PXUIField]
    public virtual string AdjgRefNbr
    {
      get => this._AdjgRefNbr;
      set => this._AdjgRefNbr = value;
    }

    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (PX.Objects.AR.ARAdjust.adjdDocType))]
    [PXDefault("INV")]
    [PXUIField]
    public virtual string AdjdDocType
    {
      get => this._AdjdDocType;
      set => this._AdjdDocType = value;
    }

    [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (PX.Objects.AR.ARAdjust.adjdRefNbr))]
    [PXDefault]
    [PXUIField]
    public virtual string AdjdRefNbr
    {
      get => this._AdjdRefNbr;
      set => this._AdjdRefNbr = value;
    }

    [PXDBBool(BqlField = typeof (PX.Objects.AR.ARAdjust.released))]
    [PXDefault(false)]
    public virtual bool? Released
    {
      get => this._Released;
      set => this._Released = value;
    }

    public abstract class adjgDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.ARAdjust3.adjgDocType>
    {
    }

    public abstract class adjgRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.ARAdjust3.adjgRefNbr>
    {
    }

    public abstract class adjdDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.ARAdjust3.adjdDocType>
    {
    }

    public abstract class adjdRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      JournalWithSubEntry.ARAdjust3.adjdRefNbr>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      JournalWithSubEntry.ARAdjust3.released>
    {
    }
  }

  public class APAdjdRefNbr2Attribute : PXCustomSelectorAttribute
  {
    private System.Type _searchType2;

    public APAdjdRefNbr2Attribute(System.Type SearchType1, System.Type SearchType2)
      : base(SearchType1, new System.Type[11]
      {
        typeof (PX.Objects.AP.APRegister.refNbr),
        typeof (PX.Objects.AP.APRegister.docDate),
        typeof (PX.Objects.AP.APRegister.finPeriodID),
        typeof (PX.Objects.AP.APRegister.vendorLocationID),
        typeof (PX.Objects.AP.APRegister.curyID),
        typeof (PX.Objects.AP.APRegister.curyOrigDocAmt),
        typeof (PX.Objects.AP.APRegister.curyDocBal),
        typeof (PX.Objects.AP.APRegister.status),
        typeof (PX.Objects.AP.APAdjust.APInvoice.dueDate),
        typeof (PX.Objects.AP.APAdjust.APInvoice.invoiceNbr),
        typeof (PX.Objects.AP.APRegister.docDesc)
      })
    {
      this._searchType2 = SearchType2;
    }

    protected virtual IEnumerable GetRecords(string aAdjdDocType)
    {
      JournalWithSubEntry.APAdjdRefNbr2Attribute refNbr2Attribute = this;
      PXView pxView1 = new PXView(refNbr2Attribute._Graph, !((PXSelectorAttribute) refNbr2Attribute)._DirtyRead, ((PXSelectorAttribute) refNbr2Attribute)._Select);
      PXCache adjustments = refNbr2Attribute._Graph.Caches[typeof (PX.Objects.AP.APAdjust)];
      object obj = (object) null;
      foreach (object current in PXView.Currents)
      {
        if (current != null && (current.GetType() == ((PXSelectorAttribute) refNbr2Attribute)._CacheType || current.GetType().IsSubclassOf(((PXSelectorAttribute) refNbr2Attribute)._CacheType)))
        {
          obj = current;
          break;
        }
      }
      if (obj == null)
        obj = adjustments.Current;
      PX.Objects.AP.APAdjust row = obj as PX.Objects.AP.APAdjust;
      string adjdDocType = aAdjdDocType;
      if (string.IsNullOrEmpty(adjdDocType))
      {
        if (row == null)
          yield break;
        adjdDocType = row.AdjdDocType;
      }
      PXView pxView2 = pxView1;
      object[] objArray1 = new object[1]
      {
        (object) adjdDocType
      };
      foreach (PXResult pxResult in pxView2.SelectMulti(objArray1))
      {
        PX.Objects.AP.APAdjust.APInvoice record = pxResult.GetItem<PX.Objects.AP.APAdjust.APInvoice>();
        if (row != null)
        {
          PX.Objects.AP.APAdjust apAdjust1 = (PX.Objects.AP.APAdjust) null;
          foreach (PX.Objects.AP.APAdjust apAdjust2 in adjustments.Inserted)
          {
            if (apAdjust2.AdjdDocType == record.DocType && apAdjust2.AdjdRefNbr == record.RefNbr && apAdjust2 != row && (!(apAdjust2.AdjgDocType == row.AdjgDocType) || !(apAdjust2.AdjgRefNbr == row.AdjgRefNbr)))
            {
              apAdjust1 = apAdjust2;
              break;
            }
          }
          if (apAdjust1 != null)
            continue;
        }
        yield return (object) record;
      }
      Search<GLTranDoc.refNbr> search = new Search<GLTranDoc.refNbr>();
      PXView pxView3 = new PXView(refNbr2Attribute._Graph, false, BqlCommand.CreateInstance(new System.Type[1]
      {
        refNbr2Attribute._searchType2
      }));
      object[] objArray2 = new object[1]
      {
        (object) adjdDocType
      };
      foreach (PXResult pxResult in pxView3.SelectMulti(objArray2))
      {
        GLTranDoc aSrc = pxResult.GetItem<GLTranDoc>();
        if (row != null)
        {
          PX.Objects.AP.APAdjust apAdjust3 = (PX.Objects.AP.APAdjust) null;
          foreach (PX.Objects.AP.APAdjust apAdjust4 in adjustments.Inserted)
          {
            if (apAdjust4.AdjdDocType == aSrc.TranType && apAdjust4.AdjdRefNbr == aSrc.RefNbr && apAdjust4 != row && (!(apAdjust4.AdjgDocType == row.AdjgDocType) || !(apAdjust4.AdjgRefNbr == row.AdjgRefNbr)))
            {
              apAdjust3 = apAdjust4;
              break;
            }
          }
          if (apAdjust3 != null)
            continue;
        }
        PX.Objects.AP.APAdjust.APInvoice aDest = new PX.Objects.AP.APAdjust.APInvoice();
        refNbr2Attribute.Copy(aDest, aSrc);
        yield return (object) aDest;
      }
    }

    protected virtual void Copy(PX.Objects.AP.APAdjust.APInvoice aDest, GLTranDoc aSrc)
    {
      aDest.RefNbr = aSrc.RefNbr;
      aDest.CuryDocBal = aSrc.CuryApplAmt;
      aDest.DocBal = aSrc.ApplAmt;
      aDest.DocDesc = aSrc.TranDesc;
      aDest.DocDate = aSrc.TranDate;
      aDest.CuryID = aSrc.CuryID;
      aDest.CuryOrigDocAmt = aSrc.CuryDocTotal;
      aDest.InvoiceNbr = aSrc.ExtRefNbr;
      aDest.FinPeriodID = aSrc.FinPeriodID;
      aDest.TranPeriodID = aSrc.TranPeriodID;
      aDest.VendorID = aSrc.BAccountID;
      aDest.VendorLocationID = aSrc.LocationID;
      aDest.Status = "D";
      aDest.DueDate = aSrc.DueDate;
      aDest.CuryOrigDiscAmt = aSrc.CuryDiscAmt;
      aDest.CuryDocBal = aSrc.CuryUnappliedBal;
      aDest.DocBal = aSrc.UnappliedBal;
      bool flag = APInvoiceType.DrCr(aSrc.TranType) == "D";
      aDest.APAccountID = flag ? aSrc.CreditAccountID : aSrc.DebitAccountID;
      aDest.APSubID = flag ? aSrc.CreditSubID : aSrc.DebitSubID;
      aDest.BranchID = aSrc.BranchID;
    }
  }

  public class ARAdjdRefNbr2Attribute : PXCustomSelectorAttribute
  {
    private System.Type _searchType2;

    public ARAdjdRefNbr2Attribute(System.Type SearchType1, System.Type SearchType2)
      : base(SearchType1, new System.Type[11]
      {
        typeof (PX.Objects.AR.ARRegister.refNbr),
        typeof (PX.Objects.AR.ARRegister.docDate),
        typeof (PX.Objects.AR.ARRegister.finPeriodID),
        typeof (PX.Objects.AR.ARRegister.customerLocationID),
        typeof (PX.Objects.AR.ARRegister.curyID),
        typeof (PX.Objects.AR.ARRegister.curyOrigDocAmt),
        typeof (PX.Objects.AR.ARRegister.curyDocBal),
        typeof (PX.Objects.AR.ARRegister.status),
        typeof (PX.Objects.AR.ARAdjust.ARInvoice.dueDate),
        typeof (PX.Objects.AR.ARAdjust.ARInvoice.invoiceNbr),
        typeof (PX.Objects.AR.ARRegister.docDesc)
      })
    {
      this._searchType2 = SearchType2;
    }

    protected virtual IEnumerable GetRecords(string aAdjdDocType)
    {
      JournalWithSubEntry.ARAdjdRefNbr2Attribute refNbr2Attribute = this;
      PXView pxView1 = new PXView(refNbr2Attribute._Graph, !((PXSelectorAttribute) refNbr2Attribute)._DirtyRead, ((PXSelectorAttribute) refNbr2Attribute)._Select);
      PXCache adjustments = refNbr2Attribute._Graph.Caches[((PXSelectorAttribute) refNbr2Attribute)._CacheType];
      object obj = (object) null;
      foreach (object current in PXView.Currents)
      {
        if (current != null && (current.GetType() == ((PXSelectorAttribute) refNbr2Attribute)._CacheType || current.GetType().IsSubclassOf(((PXSelectorAttribute) refNbr2Attribute)._CacheType)))
        {
          obj = current;
          break;
        }
      }
      if (obj == null)
        obj = adjustments.Current;
      PX.Objects.AR.ARAdjust row = obj as PX.Objects.AR.ARAdjust;
      string adjdDocType = aAdjdDocType;
      if (string.IsNullOrEmpty(adjdDocType))
      {
        if (row == null)
          yield break;
        adjdDocType = row.AdjdDocType;
      }
      PXView pxView2 = pxView1;
      object[] objArray1 = new object[1]
      {
        (object) adjdDocType
      };
      foreach (PXResult pxResult in pxView2.SelectMulti(objArray1))
      {
        PX.Objects.AR.ARAdjust.ARInvoice record = pxResult.GetItem<PX.Objects.AR.ARAdjust.ARInvoice>();
        PX.Objects.AR.ARAdjust arAdjust1 = (PX.Objects.AR.ARAdjust) null;
        if (row != null)
        {
          foreach (PX.Objects.AR.ARAdjust arAdjust2 in adjustments.Inserted)
          {
            if (arAdjust2.AdjdDocType == record.DocType && arAdjust2.AdjdRefNbr == record.RefNbr && arAdjust2 != row && (!(arAdjust2.AdjgDocType == row.AdjgDocType) || !(arAdjust2.AdjgRefNbr == row.AdjgRefNbr)))
            {
              arAdjust1 = arAdjust2;
              break;
            }
          }
        }
        if (arAdjust1 == null)
          yield return (object) record;
      }
      Search<GLTranDoc.refNbr> search = new Search<GLTranDoc.refNbr>();
      PXView pxView3 = new PXView(refNbr2Attribute._Graph, false, BqlCommand.CreateInstance(new System.Type[1]
      {
        refNbr2Attribute._searchType2
      }));
      object[] objArray2 = new object[1]
      {
        (object) adjdDocType
      };
      foreach (PXResult pxResult in pxView3.SelectMulti(objArray2))
      {
        GLTranDoc aSrc = pxResult.GetItem<GLTranDoc>();
        if (row != null)
        {
          PX.Objects.AR.ARAdjust arAdjust3 = (PX.Objects.AR.ARAdjust) null;
          foreach (PX.Objects.AR.ARAdjust arAdjust4 in adjustments.Inserted)
          {
            if (arAdjust4.AdjdDocType == aSrc.TranType && arAdjust4.AdjdRefNbr == aSrc.RefNbr && arAdjust4 != row && (!(arAdjust4.AdjgDocType == row.AdjgDocType) || !(arAdjust4.AdjgRefNbr == row.AdjgRefNbr)))
            {
              arAdjust3 = arAdjust4;
              break;
            }
          }
          if (arAdjust3 != null)
            continue;
        }
        PX.Objects.AR.ARAdjust.ARInvoice aDest = new PX.Objects.AR.ARAdjust.ARInvoice();
        refNbr2Attribute.Copy(aDest, aSrc);
        yield return (object) aDest;
      }
    }

    protected virtual void Copy(PX.Objects.AR.ARAdjust.ARInvoice aDest, GLTranDoc aSrc)
    {
      aDest.RefNbr = aSrc.RefNbr;
      aDest.CuryDocBal = aSrc.CuryApplAmt;
      aDest.DocBal = aSrc.ApplAmt;
      aDest.DocDesc = aSrc.TranDesc;
      aDest.DocDate = aSrc.TranDate;
      aDest.CuryID = aSrc.CuryID;
      aDest.CuryOrigDocAmt = aSrc.CuryDocTotal;
      aDest.InvoiceNbr = aSrc.ExtRefNbr;
      aDest.FinPeriodID = aSrc.FinPeriodID;
      aDest.TranPeriodID = aSrc.TranPeriodID;
      aDest.CustomerID = aSrc.BAccountID;
      aDest.CustomerLocationID = aSrc.LocationID;
      aDest.Status = "D";
      aDest.DueDate = aSrc.DueDate;
      aDest.CuryOrigDiscAmt = aSrc.CuryDiscAmt;
      aDest.CuryDocBal = aSrc.CuryUnappliedBal;
      aDest.DocBal = aSrc.UnappliedBal;
      bool flag = ARInvoiceType.DrCr(aSrc.TranType) == "C";
      aDest.ARAccountID = flag ? aSrc.DebitAccountID : aSrc.CreditAccountID;
      aDest.ARSubID = flag ? aSrc.DebitSubID : aSrc.CreditSubID;
      aDest.BranchID = aSrc.BranchID;
    }
  }

  private class PXManualNumberingException : PXException
  {
    internal PXManualNumberingException()
      : base("A reference number cannot be assigned to this document because the Manual Numbering check box is selected on the Numbering Sequences (CS201010) form for the numbering sequence specified for documents of this type. Only automatic numbering is supported on this form.")
    {
    }

    internal PXManualNumberingException(string message)
      : base(message)
    {
    }

    public PXManualNumberingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }

  public class APDocNumberingAttribute : AutoNumberAttribute
  {
    public APDocNumberingAttribute()
      : base(typeof (PX.Objects.AP.APRegister.docType), typeof (PX.Objects.AP.APRegister.docDate), new string[10]
      {
        "INV",
        "ACR",
        "ADR",
        "QCK",
        "VQC",
        "CHK",
        "PPM",
        "REF",
        "VCK",
        "PPI"
      }, new System.Type[10]
      {
        typeof (APSetup.invoiceNumberingID),
        typeof (APSetup.creditAdjNumberingID),
        typeof (APSetup.debitAdjNumberingID),
        typeof (APSetup.checkNumberingID),
        null,
        typeof (APSetup.checkNumberingID),
        typeof (APSetup.checkNumberingID),
        typeof (APSetup.checkNumberingID),
        null,
        typeof (APSetup.invoiceNumberingID)
      })
    {
    }
  }

  public class ARDocNumberingAttribute : AutoNumberAttribute
  {
    public ARDocNumberingAttribute()
      : base(typeof (PX.Objects.AR.ARRegister.docType), typeof (PX.Objects.AR.ARRegister.docDate), new string[12]
      {
        "INV",
        "DRM",
        "CRM",
        "FCH",
        "SMC",
        "CSL",
        "RCS",
        "PMT",
        "PPM",
        "REF",
        "RPM",
        "SMB"
      }, new System.Type[12]
      {
        typeof (ARSetup.invoiceNumberingID),
        typeof (ARSetup.debitAdjNumberingID),
        typeof (ARSetup.creditAdjNumberingID),
        typeof (ARSetup.finChargeNumberingID),
        null,
        typeof (ARSetup.paymentNumberingID),
        typeof (ARSetup.paymentNumberingID),
        typeof (ARSetup.paymentNumberingID),
        typeof (ARSetup.paymentNumberingID),
        typeof (ARSetup.paymentNumberingID),
        null,
        null
      })
    {
    }
  }

  public abstract class RegisterAdapter<T> : IRegister, IInvoice, PX.Objects.CM.IRegister, IDocumentKey
    where T : class, IBqlTable, IInvoice, new()
  {
    public T Entity { get; }

    public RegisterAdapter(T entity) => this.Entity = entity;

    public abstract int? BAccountID { get; set; }

    public abstract int? LocationID { get; set; }

    public abstract int? BranchID { get; set; }

    public virtual DateTime? DocDate
    {
      get => this.Entity.DocDate;
      set => this.Entity.DocDate = value;
    }

    public abstract int? AccountID { get; set; }

    public abstract int? SubID { get; set; }

    public string CuryID
    {
      get => this.Entity.CuryID;
      set => this.Entity.CuryID = value;
    }

    public abstract string FinPeriodID { get; set; }

    public abstract long? CuryInfoID { get; set; }

    public Decimal? CuryDocBal
    {
      get => this.Entity.CuryDocBal;
      set => this.Entity.CuryDocBal = value;
    }

    public Decimal? DocBal
    {
      get => this.Entity.DocBal;
      set => this.Entity.DocBal = value;
    }

    public Decimal? CuryDiscBal
    {
      get => this.Entity.CuryDiscBal;
      set => this.Entity.CuryDiscBal = value;
    }

    public Decimal? DiscBal
    {
      get => this.Entity.DiscBal;
      set => this.Entity.DiscBal = value;
    }

    public Decimal? CuryWhTaxBal
    {
      get => this.Entity.CuryWhTaxBal;
      set => this.Entity.CuryWhTaxBal = value;
    }

    public Decimal? WhTaxBal
    {
      get => this.Entity.WhTaxBal;
      set => this.Entity.WhTaxBal = value;
    }

    public DateTime? DiscDate
    {
      get => this.Entity.DiscDate;
      set => this.Entity.DiscDate = value;
    }

    public string DocType
    {
      get => this.Entity.DocType;
      set => this.Entity.DocType = value;
    }

    public string RefNbr
    {
      get => this.Entity.RefNbr;
      set => this.Entity.RefNbr = value;
    }

    public string OrigModule
    {
      get => this.Entity.OrigModule;
      set => this.Entity.OrigModule = value;
    }

    public Decimal? CuryOrigDocAmt
    {
      get => this.Entity.CuryOrigDocAmt;
      set => this.Entity.CuryOrigDocAmt = value;
    }

    public Decimal? OrigDocAmt
    {
      get => this.Entity.OrigDocAmt;
      set => this.Entity.OrigDocAmt = value;
    }

    public string DocDesc
    {
      get => this.Entity.DocDesc;
      set => this.Entity.DocDesc = value;
    }

    public bool? Released
    {
      get => this.Entity.Released;
      set => this.Entity.Released = value;
    }
  }

  public class APRegisterAdapter<T>(T entity) : JournalWithSubEntry.RegisterAdapter<T>(entity) where T : PX.Objects.AP.APRegister, IInvoice, new()
  {
    public override int? BranchID
    {
      get => this.Entity.BranchID;
      set => this.Entity.BranchID = value;
    }

    public override string FinPeriodID
    {
      get => this.Entity.FinPeriodID;
      set => this.Entity.FinPeriodID = value;
    }

    public override long? CuryInfoID
    {
      get => this.Entity.CuryInfoID;
      set => this.Entity.CuryInfoID = value;
    }

    public override int? BAccountID
    {
      get => this.Entity.VendorID;
      set => this.Entity.VendorID = value;
    }

    public override int? LocationID
    {
      get => this.Entity.VendorLocationID;
      set => this.Entity.VendorLocationID = value;
    }

    public override int? AccountID
    {
      get => this.Entity.APAccountID;
      set => this.Entity.APAccountID = value;
    }

    public override int? SubID
    {
      get => this.Entity.APSubID;
      set => this.Entity.APSubID = value;
    }
  }

  public class ARRegisterAdapter<T>(T entity) : JournalWithSubEntry.RegisterAdapter<T>(entity) where T : PX.Objects.AR.ARRegister, IInvoice, new()
  {
    public override int? BranchID
    {
      get => this.Entity.BranchID;
      set => this.Entity.BranchID = value;
    }

    public override string FinPeriodID
    {
      get => this.Entity.FinPeriodID;
      set => this.Entity.FinPeriodID = value;
    }

    public override long? CuryInfoID
    {
      get => this.Entity.CuryInfoID;
      set => this.Entity.CuryInfoID = value;
    }

    public override int? BAccountID
    {
      get => this.Entity.CustomerID;
      set => this.Entity.CustomerID = value;
    }

    public override int? LocationID
    {
      get => this.Entity.CustomerLocationID;
      set => this.Entity.CustomerLocationID = value;
    }

    public override int? AccountID
    {
      get => this.Entity.ARAccountID;
      set => this.Entity.ARAccountID = value;
    }

    public override int? SubID
    {
      get => this.Entity.ARSubID;
      set => this.Entity.ARSubID = value;
    }
  }

  public class GlRegisterAdapter<T>(T entity) : JournalWithSubEntry.RegisterAdapter<T>(entity) where T : GLTranDoc, IInvoice, new()
  {
    public override int? BranchID
    {
      get => this.Entity.BranchID;
      set => this.Entity.BranchID = value;
    }

    public override string FinPeriodID
    {
      get => this.Entity.FinPeriodID;
      set => this.Entity.FinPeriodID = value;
    }

    public override long? CuryInfoID
    {
      get => this.Entity.CuryInfoID;
      set => this.Entity.CuryInfoID = value;
    }

    public override DateTime? DocDate
    {
      get => this.Entity.TranDate;
      set => this.Entity.TranDate = value;
    }

    public override int? BAccountID
    {
      get => this.Entity.BAccountID;
      set => this.Entity.BAccountID = value;
    }

    public override int? LocationID
    {
      get => this.Entity.LocationID;
      set => this.Entity.LocationID = value;
    }

    private bool? IsDirect()
    {
      switch (this.Entity.TranModule)
      {
        case "AP":
          return new bool?(APInvoiceType.DrCr(this.Entity.TranType) == "D");
        case "AR":
          return new bool?(ARInvoiceType.DrCr(this.Entity.TranType) == "D");
        default:
          return new bool?();
      }
    }

    public override int? AccountID
    {
      get
      {
        bool? nullable = this.IsDirect();
        if (!nullable.HasValue)
          return new int?();
        return nullable.Value ? this.Entity.CreditAccountID : this.Entity.DebitAccountID;
      }
      set
      {
        bool? nullable = this.IsDirect();
        if (!nullable.HasValue)
          return;
        if (nullable.Value)
          this.Entity.CreditAccountID = value;
        else
          this.Entity.DebitAccountID = value;
      }
    }

    public override int? SubID
    {
      get
      {
        bool? nullable = this.IsDirect();
        if (!nullable.HasValue)
          return new int?();
        return nullable.Value ? this.Entity.CreditSubID : this.Entity.DebitSubID;
      }
      set
      {
        bool? nullable = this.IsDirect();
        if (!nullable.HasValue)
          return;
        if (nullable.Value)
          this.Entity.CreditSubID = value;
        else
          this.Entity.DebitSubID = value;
      }
    }
  }
}
