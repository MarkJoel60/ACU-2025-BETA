// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.CCProcessingCenterBranch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.CC;

/// <summary>
/// Represents a mapping row for Processing Center, Branch, Payment Method and Cash Account
/// </summary>
[PXCacheName("Payment Creation Settings")]
public class CCProcessingCenterBranch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDBDefault(typeof (CCProcessingCenter.processingCenterID))]
  [PXParent(typeof (Select<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCProcessingCenterBranch.processingCenterID>>>>))]
  [PXUIField(DisplayName = "Proc. Center ID")]
  public virtual 
  #nullable disable
  string ProcessingCenterID { get; set; }

  /// <exclude />
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// Indicates that Processing Center will be set as defult for documents with the same Branch Id
  /// and Processing Center currency.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use by Default")]
  public virtual bool? DefaultForBranch { get; set; }

  /// <summary>
  /// Indicates that Payment Method ID will be set to Payment created by Payment Link with the Credit Card means of payments.
  /// </summary>
  [PXDefault]
  [PXSelector(typeof (Search2<PX.Objects.CA.PaymentMethod.paymentMethodID, InnerJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.creditCard>, And<CCProcessingCenterPmntMethod.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "Credit Card Payment Method")]
  [PXDBString(10, IsUnicode = true)]
  public virtual string CCPaymentMethodID { get; set; }

  /// <summary>
  /// Indicates that Cash Account ID will be set to Payment created by Payment Link with the Credit Card means of payments.
  /// </summary>
  [PXDefault]
  [CashAccount(typeof (CCProcessingCenterBranch.branchID), typeof (Search2<CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<CashAccount.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<CCProcessingCenterBranch.cCPaymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<Match<Current<AccessInfo.userName>>>>), ValidateValue = true, DisplayName = "Credit Card Cash Account")]
  public virtual int? CCCashAccountID { get; set; }

  /// <summary>
  /// Indicates that Payment Method ID will be set to Payment created by Payment Link with the EFT(ACH) means of payments.
  /// </summary>
  [PXDefault]
  [PXSelector(typeof (Search2<PX.Objects.CA.PaymentMethod.paymentMethodID, InnerJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.eft>, And<CCProcessingCenterPmntMethod.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "EFT Payment Method")]
  [PXDBString(10, IsUnicode = true)]
  public virtual string EFTPaymentMethodID { get; set; }

  /// <summary>
  /// Indicates that Cash Account ID will be set to Payment created by Payment Link with the EFT(ACH) means of payments.
  /// </summary>
  [PXDefault]
  [CashAccount(typeof (CCProcessingCenterBranch.branchID), typeof (Search2<CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<CashAccount.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<CCProcessingCenterBranch.eFTPaymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<Match<Current<AccessInfo.userName>>>>), DisplayName = "EFT Cash Account")]
  public virtual int? EFTCashAccountID { get; set; }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CCProcessingCenterBranch>.By<CCProcessingCenterBranch.processingCenterID, CCProcessingCenterBranch.branchID>
  {
    public static CCProcessingCenterBranch Find(PXGraph graph, string procCenterId, int? branchId)
    {
      return (CCProcessingCenterBranch) PrimaryKeyOf<CCProcessingCenterBranch>.By<CCProcessingCenterBranch.processingCenterID, CCProcessingCenterBranch.branchID>.FindBy(graph, (object) procCenterId, (object) branchId, (PKFindOptions) 0);
    }
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterBranch.processingCenterID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCProcessingCenterBranch.branchID>
  {
  }

  public abstract class defaultForBranch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenterBranch.defaultForBranch>
  {
  }

  public abstract class cCPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterBranch.cCPaymentMethodID>
  {
  }

  public abstract class cCCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenterBranch.cCCashAccountID>
  {
  }

  public abstract class eFTPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterBranch.eFTPaymentMethodID>
  {
  }

  public abstract class eFTCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenterBranch.eFTCashAccountID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CCProcessingCenterBranch.Tstamp>
  {
  }
}
