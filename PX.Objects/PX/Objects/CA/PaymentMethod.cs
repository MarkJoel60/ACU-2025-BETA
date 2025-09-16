// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP.PaymentProcessor.DAC;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>The payment method settings.</summary>
[PXCacheName("Payment Method")]
[PXPrimaryGraph(typeof (PaymentMethodMaint))]
[Serializable]
public class PaymentMethod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PaymentMethod.paymentMethodID>))]
  [PXReferentialIntegrityCheck]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  [PXDBForeignIdentity(typeof (PMInstance))]
  public virtual int? PMInstanceID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("CHC")]
  [PaymentMethodType.List]
  [PXUIField]
  public virtual string PaymentType { get; set; }

  [PXDBString(10)]
  [PXUIField(DisplayName = "Direct Deposit File Format")]
  [DirectDepositTypeList]
  public virtual string DirectDepositFileFormat { get; set; }

  [PXInt]
  [PXDefault]
  public virtual int? DefaultCashAccountID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? UseForAR { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? UseForAP { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Remittance Information for Cash Account")]
  public virtual bool? UseForCA { get; set; }

  [PXString]
  [PaymentMethod.aPAdditionalProcessing.List]
  [PXDefault("N")]
  [PXDBCalced(typeof (Switch<Case<Where<PaymentMethod.aPPrintChecks, Equal<True>>, PaymentMethod.aPAdditionalProcessing.printChecks, Case<Where<PaymentMethod.aPCreateBatchPayment, Equal<True>>, PaymentMethod.aPAdditionalProcessing.createBatchPayment>>, PaymentMethod.aPAdditionalProcessing.notRequired>), typeof (string))]
  [PXUIField(DisplayName = "Additional Processing")]
  public virtual string APAdditionalProcessing { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Release Batch Payment Before Export")]
  [PXUIEnabled(typeof (Where<Current<PaymentMethod.aPAdditionalProcessing>, Equal<PaymentMethod.aPAdditionalProcessing.createBatchPayment>>))]
  [PXUIVisible(typeof (Where<Current<PaymentMethod.aPAdditionalProcessing>, Equal<PaymentMethod.aPAdditionalProcessing.createBatchPayment>>))]
  public virtual bool? SkipExport { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Batch Payment")]
  public virtual bool? APCreateBatchPayment { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("E")]
  [ACHExportMethod.List]
  [PXUIField]
  [PXUIVisible(typeof (Where<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.aPCreateBatchPayment, Equal<True>>>))]
  public virtual string APBatchExportMethod { get; set; }

  [PXDBGuid(false)]
  [PXUIField]
  [PXSelector(typeof (Search<SYMapping.mappingID, Where<SYMapping.mappingType, Equal<SYMapping.mappingType.typeExport>>>), SubstituteKey = typeof (SYMapping.name))]
  [PXDefault]
  [PXUIVisible(typeof (Where<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.aPCreateBatchPayment, Equal<True>, And<PaymentMethod.aPBatchExportMethod, Equal<ACHExportMethod.exportScenario>>>>))]
  [PXUIRequired(typeof (Where<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.aPCreateBatchPayment, Equal<True>, And<PaymentMethod.aPBatchExportMethod, Equal<ACHExportMethod.exportScenario>>>>))]
  public virtual Guid? APBatchExportSYMappingID { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField]
  [ACHPlugInType]
  [PXDefault]
  public virtual string APBatchExportPlugInTypeName { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that payments with 0 amount should not be exported to EFT file.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Payments with Zero Amount")]
  public virtual bool? SkipPaymentsWithZeroAmt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Batch Seq. Number")]
  public virtual bool? RequireBatchSeqNum { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Checks")]
  public virtual bool? APPrintChecks { get; set; }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.ap_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXDefault]
  [PXUIRequired(typeof (Where<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.aPPrintChecks, Equal<True>>>))]
  public virtual string APCheckReportID { get; set; }

  [PXDBShort(MinValue = 1)]
  [PXDefault(10)]
  [PXUIField(DisplayName = "Lines per Stub")]
  public virtual short? APStubLines { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? APPrintRemittance { get; set; }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Remittance Report")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.ap_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXDefault]
  [PXUIRequired(typeof (Where<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.aPPrintChecks, Equal<True>, And<PaymentMethod.aPPrintRemittance, Equal<True>>>>))]
  public virtual string APRemittanceReportID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Require Unique Payment Ref.")]
  [PXDefault(true)]
  public virtual bool? APRequirePaymentRef { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Integrated Processing")]
  public virtual bool? ARIsProcessingRequired { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "One Instance Per Customer")]
  public virtual bool? ARIsOnePerCustomer { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Batch Deposit")]
  public virtual bool? ARDepositAsBatch { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Void Using Clearing Account")]
  public virtual bool? ARVoidOnDepositAccount { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Document Date as Void Date")]
  public virtual bool? ARDefaultVoidDateToDocumentDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Has Billing Information")]
  public virtual bool? ARHasBillingInfo { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Contains Personal Data", FieldClass = "GDPR")]
  public virtual bool? ContainsPersonalData { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Set Payment Date to Bank Transaction Date")]
  public virtual bool? PaymentDateToBankDate { get; set; }

  [PXNote(DescriptionField = typeof (PaymentMethod.paymentMethodID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Require Card/Account Number")]
  public virtual bool? IsAccountNumberRequired
  {
    [PXDependsOnFields(new Type[] {typeof (PaymentMethod.aRIsOnePerCustomer)})] get
    {
      return !this.ARIsOnePerCustomer.HasValue ? this.ARIsOnePerCustomer : new bool?(!this.ARIsOnePerCustomer.Value);
    }
    set => this.ARIsOnePerCustomer = value.HasValue ? new bool?(!value.Value) : value;
  }

  [PXBool]
  [PXUIField]
  [PXFormula(typeof (IIf<Where<PaymentMethod.aPPrintChecks, Equal<True>, Or<PaymentMethod.aPCreateBatchPayment, Equal<True>>>, True, False>))]
  public virtual bool? PrintOrExport { get; set; }

  [PXBool]
  public virtual bool? HasProcessingCenters
  {
    [PXDependsOnFields(new Type[] {typeof (PaymentMethod.aRIsProcessingRequired)})] get
    {
      return new bool?(PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && this.ARIsProcessingRequired.GetValueOrDefault());
    }
    set
    {
    }
  }

  [PXBool]
  public virtual bool? IsUsingPlugin
  {
    [PXDependsOnFields(new Type[] {typeof (PaymentMethod.aPAdditionalProcessing), typeof (PaymentMethod.aPBatchExportMethod), typeof (PaymentMethod.aPBatchExportPlugInTypeName)})] get
    {
      return new bool?(this.APAdditionalProcessing == "B" && this.APBatchExportMethod == "P" && !string.IsNullOrEmpty(this.APBatchExportPlugInTypeName));
    }
    set
    {
    }
  }

  /// <summary>
  /// Shows that it is necessary to send Payment Receipt emails from the mass processing screen.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Send Payment Receipts Automatically")]
  public virtual bool? SendPaymentReceiptsAutomatically { get; set; }

  /// <summary>AP external payment processor id</summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "External Payment Processor")]
  [PXSelector(typeof (Search<APExternalPaymentProcessor.externalPaymentProcessorID>), new Type[] {typeof (APExternalPaymentProcessor.externalPaymentProcessorID), typeof (APExternalPaymentProcessor.name)}, DescriptionField = typeof (APExternalPaymentProcessor.name))]
  [PXRestrictor(typeof (Where<BqlOperand<APExternalPaymentProcessor.isActive, IBqlBool>.IsEqual<True>>), "External Payment Processor '{0}' is inactive", new Type[] {typeof (APExternalPaymentProcessor.name)})]
  public virtual string ExternalPaymentProcessorID { get; set; }

  /// <summary>
  /// Need Account filter only for PaymentMethodType.ExternalPaymentProcessor
  /// </summary>
  [PXBool]
  public virtual bool? NeedAccountFilter
  {
    [PXDependsOnFields(new Type[] {typeof (PaymentMethod.paymentType)})] get
    {
      return new bool?(this.PaymentType == "EPP");
    }
    set
    {
    }
  }

  public class PK : PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>
  {
    public static PaymentMethod Find(PXGraph graph, string paymentMethodID, PKFindOptions options = 0)
    {
      return (PaymentMethod) PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.FindBy(graph, (object) paymentMethodID, options);
    }
  }

  public static class FK
  {
    public class CustomerPaymentMethod : 
      PrimaryKeyOf<PX.Objects.AR.CustomerPaymentMethod>.By<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<PaymentMethod>.By<PaymentMethod.pMInstanceID>
    {
    }

    public class ExportScenario : 
      PrimaryKeyOf<SYMapping>.By<SYMapping.mappingID>.ForeignKeyOf<PaymentMethod>.By<PaymentMethod.aPBatchExportSYMappingID>
    {
    }

    public class APCheckReport : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<PaymentMethod>.By<PaymentMethod.aPCheckReportID>
    {
    }

    public class RemittanceReport : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<PaymentMethod>.By<PaymentMethod.aPRemittanceReportID>
    {
    }
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentMethod.pMInstanceID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentMethod.descr>
  {
  }

  public abstract class paymentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentMethod.paymentType>
  {
  }

  public abstract class directDepositFileFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.directDepositFileFormat>
  {
  }

  public abstract class defaultCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PaymentMethod.defaultCashAccountID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethod.isActive>
  {
  }

  public abstract class useForAR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethod.useForAR>
  {
  }

  public abstract class useForAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethod.useForAP>
  {
  }

  public abstract class useForCA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethod.useForCA>
  {
  }

  public abstract class aPAdditionalProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.aPAdditionalProcessing>
  {
    public const string PrintChecks = "P";
    public const string CreateBatchPayment = "B";
    public const string NotRequired = "N";

    public class printChecks : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PaymentMethod.aPAdditionalProcessing.printChecks>
    {
      public printChecks()
        : base("P")
      {
      }
    }

    public class createBatchPayment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PaymentMethod.aPAdditionalProcessing.createBatchPayment>
    {
      public createBatchPayment()
        : base("B")
      {
      }
    }

    public class notRequired : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PaymentMethod.aPAdditionalProcessing.notRequired>
    {
      public notRequired()
        : base("N")
      {
      }
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "P", "B", "N" }, new string[3]
        {
          "Print Checks",
          "Create Batch Payments",
          "Not Required"
        })
      {
      }
    }
  }

  public abstract class skipExport : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethod.skipExport>
  {
  }

  public abstract class aPCreateBatchPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aPCreateBatchPayment>
  {
  }

  public abstract class aPBatchExportMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.aPBatchExportMethod>
  {
  }

  public abstract class aPBatchExportSYMappingID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PaymentMethod.aPBatchExportSYMappingID>
  {
  }

  public abstract class aPBatchExportPlugInTypeName : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PaymentMethod.aPBatchExportPlugInTypeName>
  {
  }

  public abstract class skipPaymentsWithZeroAmt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.skipPaymentsWithZeroAmt>
  {
  }

  public abstract class requireBatchSeqNum : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.requireBatchSeqNum>
  {
  }

  public abstract class aPPrintChecks : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethod.aPPrintChecks>
  {
  }

  public abstract class aPCheckReportID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.aPCheckReportID>
  {
  }

  public abstract class aPStubLines : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  PaymentMethod.aPStubLines>
  {
  }

  public abstract class aPPrintRemittance : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aPPrintRemittance>
  {
  }

  public abstract class aPRemittanceReportID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.aPRemittanceReportID>
  {
  }

  public abstract class aPRequirePaymentRef : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aPRequirePaymentRef>
  {
  }

  public abstract class aRIsProcessingRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aRIsProcessingRequired>
  {
  }

  public abstract class aRIsOnePerCustomer : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aRIsOnePerCustomer>
  {
  }

  public abstract class aRDepositAsBatch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aRDepositAsBatch>
  {
  }

  public abstract class aRVoidOnDepositAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aRVoidOnDepositAccount>
  {
  }

  public abstract class aRDefaultVoidDateToDocumentDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aRDefaultVoidDateToDocumentDate>
  {
  }

  public abstract class aRHasBillingInfo : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.aRHasBillingInfo>
  {
  }

  public abstract class containsPersonalData : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.containsPersonalData>
  {
  }

  public abstract class paymentDateToBankDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.paymentDateToBankDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PaymentMethod.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PaymentMethod.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PaymentMethod.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PaymentMethod.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PaymentMethod.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PaymentMethod.Tstamp>
  {
  }

  public abstract class isAccountNumberRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.isAccountNumberRequired>
  {
  }

  public abstract class printOrExport : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethod.printOrExport>
  {
  }

  public abstract class hasProcessingCenters : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.hasProcessingCenters>
  {
  }

  public abstract class isUsingPlugin : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethod.isUsingPlugin>
  {
  }

  public abstract class sendPaymentReceiptsAutomatically : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.sendPaymentReceiptsAutomatically>
  {
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethod.externalPaymentProcessorID>
  {
  }

  public abstract class needAccountFilter : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethod.needAccountFilter>
  {
  }
}
