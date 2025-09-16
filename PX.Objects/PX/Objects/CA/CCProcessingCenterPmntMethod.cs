// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCProcessingCenterPmntMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Payment Method for Credit Card Processing Center")]
[Serializable]
public class CCProcessingCenterPmntMethod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDBDefault(typeof (CCProcessingCenter.processingCenterID))]
  [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID, Where<CCProcessingCenter.isActive, Equal<True>>>))]
  [PXParent(typeof (Select<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CCProcessingCenterPmntMethod.processingCenterID>>>>))]
  [PXUIField(DisplayName = "Proc. Center ID")]
  public virtual 
  #nullable disable
  string ProcessingCenterID { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (PaymentMethod.paymentMethodID))]
  [PXSelector(typeof (Search<PaymentMethod.paymentMethodID, Where<PaymentMethod.aRIsProcessingRequired, Equal<True>>>))]
  [PXUIField(DisplayName = "Payment Method")]
  [PXParent(typeof (Select<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<CCProcessingCenterPmntMethod.paymentMethodID>>>>))]
  public virtual string PaymentMethodID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Default")]
  [UniqueBool(typeof (CCProcessingCenterPmntMethod.paymentMethodID))]
  public virtual bool? IsDefault { get; set; }

  [PXDBInt]
  [PXDefault(10)]
  [PXUIField(DisplayName = "Funds Hold Period (Days)")]
  public virtual int? FundHoldPeriod { get; set; }

  [PXDBInt]
  [PXDefault(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<Parent<CCProcessingCenter.isExternalAuthorizationOnly>, IBqlBool>.IsEqual<True>>.Else<int0>))]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<Parent<CCProcessingCenter.isExternalAuthorizationOnly>, IBqlBool>.IsEqual<True>>.Else<CCProcessingCenterPmntMethod.reauthDelay>))]
  [PXUIRequired(typeof (Where<Parent<CCProcessingCenter.isExternalAuthorizationOnly>, Equal<False>>))]
  [PXUIEnabled(typeof (Where<Parent<CCProcessingCenter.isExternalAuthorizationOnly>, Equal<False>>))]
  [PXUIField(DisplayName = "Reauthorization Delay (Hours)")]
  public virtual int? ReauthDelay { get; set; }

  public class PK : 
    PrimaryKeyOf<CCProcessingCenterPmntMethod>.By<CCProcessingCenterPmntMethod.processingCenterID, CCProcessingCenterPmntMethod.paymentMethodID>
  {
    public static CCProcessingCenterPmntMethod Find(
      PXGraph graph,
      string processingCenterID,
      string paymentMethodID,
      PKFindOptions options = 0)
    {
      return (CCProcessingCenterPmntMethod) PrimaryKeyOf<CCProcessingCenterPmntMethod>.By<CCProcessingCenterPmntMethod.processingCenterID, CCProcessingCenterPmntMethod.paymentMethodID>.FindBy(graph, (object) processingCenterID, (object) paymentMethodID, options);
    }
  }

  public static class FK
  {
    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<CCProcessingCenterPmntMethod>.By<CCProcessingCenterPmntMethod.processingCenterID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CCProcessingCenterPmntMethod>.By<CCProcessingCenterPmntMethod.paymentMethodID>
    {
    }
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterPmntMethod.processingCenterID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterPmntMethod.paymentMethodID>
  {
  }

  public abstract class isActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenterPmntMethod.isActive>
  {
  }

  public abstract class isDefault : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenterPmntMethod.isDefault>
  {
  }

  public abstract class fundHoldPeriod : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenterPmntMethod.fundHoldPeriod>
  {
  }

  public abstract class reauthDelay : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenterPmntMethod.reauthDelay>
  {
  }
}
