// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ACHPlugInSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

[PXHidden]
public class ACHPlugInSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CheckBox { get; set; }

  [PXDBString(2, IsUnicode = true, InputMask = "00")]
  [PXUIField(DisplayName = "Priority Code")]
  [PXDefault("01")]
  public 
  #nullable disable
  string PriorityCode { get; set; }

  [PXDBString(60, IsUnicode = true, InputMask = "")]
  [PXSelector(typeof (Search<PaymentMethodDetail.detailID, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PaymentMethod.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>), SubstituteKey = typeof (PaymentMethodDetail.descr), DirtyRead = true)]
  [PXDefault]
  public virtual string RemittanceSetting { get; set; }

  [PXDBString(60, IsUnicode = true, InputMask = "")]
  [PXSelector(typeof (Search<PaymentMethodDetail.detailID, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PaymentMethod.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>), SubstituteKey = typeof (PaymentMethodDetail.descr), DirtyRead = true)]
  [PXDefault]
  public virtual string VendorSetting { get; set; }

  [PXDBString(1, IsUnicode = true, InputMask = "CCCCCCCC")]
  [PX.ACHPlugInBase.FileIDModifier.List]
  [PXDefault("A")]
  public virtual string FileIDModifier { get; set; }

  [PXDBString(3)]
  [PX.ACHPlugInBase.ServiceClassCode.List]
  [PXDefault("100")]
  [PXUIField(DisplayName = "Service Class Code")]
  public string ServiceClassCode { get; set; }

  [PXDBString(20, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  public virtual string CompanyDiscretionaryData { get; set; }

  [PXDBString(3, IsUnicode = true, InputMask = "")]
  [PX.ACHPlugInBase.StandardEntryClassCode.List]
  [PXDefault("CCD")]
  public virtual string StandardEntryClassCode { get; set; }

  [PXDBString(3, IsUnicode = true, InputMask = "")]
  [PX.ACHPlugInBase.StandardIATEntryClassCode.List]
  [PXDefault("IAT")]
  public virtual string StandardIATEntryClassCode { get; set; }

  [PXDBString(3, IsUnicode = true, InputMask = "")]
  [PX.ACHPlugInBase.CreationDate.List]
  [PXDefault("CurrentDate")]
  public virtual string CreationDate { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "CCCCCCCCCC")]
  [PXUIField(DisplayName = "Company Entry Description")]
  [PXDefault("Payment")]
  public string CompanyEntryDescription { get; set; }

  [PXDBString(1, IsUnicode = true, InputMask = "0")]
  [PXUIField(DisplayName = "Originator Status Code")]
  [PXDefault("1")]
  public virtual string OriginatorStatusCode { get; set; }

  public abstract class checkBox : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ACHPlugInSettings.checkBox>
  {
  }

  public abstract class priorityCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.priorityCode>
  {
  }

  public abstract class remittanceSetting : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.remittanceSetting>
  {
  }

  public abstract class vendorSetting : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.vendorSetting>
  {
  }

  public abstract class fileIDModifier : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.fileIDModifier>
  {
  }

  public abstract class serviceClassCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.serviceClassCode>
  {
  }

  public abstract class companyDiscretionaryData : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.companyDiscretionaryData>
  {
  }

  public abstract class standardEntryClassCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.standardEntryClassCode>
  {
  }

  public abstract class standardIATEntryClassCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.standardEntryClassCode>
  {
  }

  public abstract class creationDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.creationDate>
  {
  }

  public abstract class companyEntryDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.companyEntryDescription>
  {
  }

  public abstract class originatorStatusCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ACHPlugInSettings.originatorStatusCode>
  {
  }
}
