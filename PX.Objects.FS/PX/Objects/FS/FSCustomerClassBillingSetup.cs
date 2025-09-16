// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCustomerClassBillingSetup
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSCustomerClassBillingSetup")]
[Serializable]
public class FSCustomerClassBillingSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsKey = true)]
  [PXParent(typeof (Select<PX.Objects.AR.CustomerClass, Where<PX.Objects.AR.CustomerClass.customerClassID, Equal<Current<FSCustomerClassBillingSetup.customerClassID>>>>))]
  [PXDBDefault(typeof (PX.Objects.AR.CustomerClass.customerClassID))]
  public virtual 
  #nullable disable
  string CustomerClassID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? CBID { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual string SrvOrdType { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXSelector(typeof (Search<FSBillingCycle.billingCycleID>), SubstituteKey = typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  [PXUIField(DisplayName = "Billing Cycle")]
  public virtual int? BillingCycleID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("NO")]
  [ListField_Frequency_Type.ListAtrribute]
  [PXUIField(DisplayName = "Frequency Type")]
  public virtual string FrequencyType { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("BT")]
  [ListField_Send_Invoices_To.ListAtrribute]
  [PXUIField(DisplayName = "Bill-To Address")]
  public virtual string SendInvoicesTo { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("SO")]
  [ListField_Ship_To.ListAtrribute]
  [PXUIField(DisplayName = "Ship-To Address")]
  public virtual string BillShipmentSource { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<FSCustomerClassBillingSetup>.By<FSCustomerClassBillingSetup.customerClassID, FSCustomerClassBillingSetup.cBID>
  {
    public static FSCustomerClassBillingSetup Find(
      PXGraph graph,
      string customerClassID,
      int? cBID,
      PKFindOptions options = 0)
    {
      return (FSCustomerClassBillingSetup) PrimaryKeyOf<FSCustomerClassBillingSetup>.By<FSCustomerClassBillingSetup.customerClassID, FSCustomerClassBillingSetup.cBID>.FindBy(graph, (object) customerClassID, (object) cBID, options);
    }
  }

  public static class FK
  {
    public class CustomerClass : 
      PrimaryKeyOf<PX.Objects.AR.CustomerClass>.By<PX.Objects.AR.CustomerClass.customerClassID>.ForeignKeyOf<FSCustomerClassBillingSetup>.By<FSCustomerClassBillingSetup.customerClassID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSCustomerClassBillingSetup>.By<FSCustomerClassBillingSetup.srvOrdType>
    {
    }

    public class BillingCycle : 
      PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleID>.ForeignKeyOf<FSCustomerClassBillingSetup>.By<FSCustomerClassBillingSetup.billingCycleID>
    {
    }
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.customerClassID>
  {
  }

  public abstract class cBID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCustomerClassBillingSetup.cBID>
  {
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.srvOrdType>
  {
  }

  public abstract class billingCycleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.billingCycleID>
  {
  }

  public abstract class frequencyType : ListField_Frequency_Type
  {
  }

  public abstract class sendInvoicesTo : ListField_Send_Invoices_To
  {
  }

  public abstract class billShipmentSource : ListField_Ship_To
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FSCustomerClassBillingSetup.Tstamp>
  {
  }
}
