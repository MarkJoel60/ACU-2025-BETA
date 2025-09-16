// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.SOImportExternalTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.Common.Attributes;

#nullable enable
namespace PX.Objects.SO.DAC;

[PXCacheName("SO Import CC Payment")]
[PXVirtual]
public class SOImportExternalTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method", Required = true)]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  public virtual int? PMInstanceID { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "External Tran. ID")]
  public virtual string TranNumber { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center ID")]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current<SOImportExternalTran.pMInstanceID>>>>, Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<SOImportExternalTran.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isDefault, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>))]
  [PXSelector(typeof (Search2<CCProcessingCenter.processingCenterID, InnerJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenterPmntMethod.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<SOImportExternalTran.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>), DescriptionField = typeof (CCProcessingCenter.name), ValidateValue = false)]
  [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
  public virtual string ProcessingCenterID { get; set; }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOImportExternalTran.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOImportExternalTran.pMInstanceID>
  {
  }

  public abstract class tranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOImportExternalTran.tranNumber>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOImportExternalTran.processingCenterID>
  {
  }
}
