// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCustomerBillingSetup
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSCustomerBillingSetup")]
[Serializable]
public class FSCustomerBillingSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXParent(typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<FSCustomerBillingSetup.customerID>>>>))]
  [PXDBDefault(typeof (PX.Objects.AR.Customer.bAccountID))]
  public virtual int? CustomerID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? CBID { get; set; }

  /// <summary>Non-used field</summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBString(4, InputMask = ">AAAA")]
  [PXCheckUnique(new Type[] {typeof (FSCustomerBillingSetup.customerID)})]
  [PXDefault]
  [PXRestrictor(typeof (Where<FSSrvOrdType.active, Equal<True>>), null, new Type[] {})]
  [FSSelectorSrvOrdTypeNOTQuoteInternal]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

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

  [PXDBInt]
  [PXUIField]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXDefault]
  public virtual int? WeeklyFrequency { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault]
  public virtual int? MonthlyFrequency { get; set; }

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

  public class PK : PrimaryKeyOf<FSCustomerBillingSetup>.By<FSCustomerBillingSetup.cBID>
  {
    public static FSCustomerBillingSetup Find(PXGraph graph, int? cBID, PKFindOptions options = 0)
    {
      return (FSCustomerBillingSetup) PrimaryKeyOf<FSCustomerBillingSetup>.By<FSCustomerBillingSetup.cBID>.FindBy(graph, (object) cBID, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<FSCustomerBillingSetup>.By<FSCustomerBillingSetup.customerID, FSCustomerBillingSetup.cBID>
  {
    public static FSCustomerBillingSetup Find(
      PXGraph graph,
      int? customerID,
      int? cBID,
      PKFindOptions options = 0)
    {
      return (FSCustomerBillingSetup) PrimaryKeyOf<FSCustomerBillingSetup>.By<FSCustomerBillingSetup.customerID, FSCustomerBillingSetup.cBID>.FindBy(graph, (object) customerID, (object) cBID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSCustomerBillingSetup>.By<FSCustomerBillingSetup.customerID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSCustomerBillingSetup>.By<FSCustomerBillingSetup.srvOrdType>
    {
    }

    public class BillingCycle : 
      PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleID>.ForeignKeyOf<FSCustomerBillingSetup>.By<FSCustomerBillingSetup.billingCycleID>
    {
    }
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCustomerBillingSetup.customerID>
  {
  }

  public abstract class cBID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCustomerBillingSetup.cBID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSCustomerBillingSetup.active>
  {
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCustomerBillingSetup.srvOrdType>
  {
  }

  public abstract class billingCycleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCustomerBillingSetup.billingCycleID>
  {
  }

  public abstract class frequencyType : ListField_Frequency_Type
  {
  }

  public abstract class weeklyFrequency : ListField_WeekDaysNumber
  {
  }

  public abstract class monthlyFrequency : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSCustomerBillingSetup.monthlyFrequency>
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
    FSCustomerBillingSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCustomerBillingSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCustomerBillingSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSCustomerBillingSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCustomerBillingSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCustomerBillingSetup.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSCustomerBillingSetup.Tstamp>
  {
  }
}
