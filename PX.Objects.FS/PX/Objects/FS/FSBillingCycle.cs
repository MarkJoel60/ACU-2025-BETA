// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSBillingCycle
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

[PXCacheName("Billing Cycle")]
[PXPrimaryGraph(typeof (BillingCycleMaint))]
[Serializable]
public class FSBillingCycle : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? BillingCycleID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">AAAAAAAAAAAAAAA")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  [NormalizeWhiteSpace]
  public virtual 
  #nullable disable
  string BillingCycleCD { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Group Billing Documents By")]
  [ListField_Billing_Cycle_Type.ListAtrribute]
  [PXDefault("TC")]
  public virtual string BillingCycleType { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bill Only Completed or Closed Service Orders")]
  public virtual bool? InvoiceOnlyCompletedServiceOrder { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("MT")]
  [ListField_Time_Cycle_Type.ListAtrribute]
  [PXUIField(DisplayName = "Time Cycle Type")]
  public virtual string TimeCycleType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Day of Week", Visible = false)]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXDefault]
  public virtual int? TimeCycleWeekDay { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Day of Month")]
  [PXIntList(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 /*0x10*/, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 /*0x1F*/}, new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})]
  [PXDefault]
  public virtual int? TimeCycleDayOfMonth { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Run Billing For")]
  [ListField_Billing_By.ListAtrribute]
  [PXDefault("AP")]
  public virtual string BillingBy { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Separate Billing Documents by Customer Location")]
  public virtual bool? GroupBillByLocations { get; set; }

  public class PK : PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleID>
  {
    public static FSBillingCycle Find(PXGraph graph, int? billingCycleID, PKFindOptions options = 0)
    {
      return (FSBillingCycle) PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleID>.FindBy(graph, (object) billingCycleID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleCD>
  {
    public static FSBillingCycle Find(PXGraph graph, string billingCycleCD, PKFindOptions options = 0)
    {
      return (FSBillingCycle) PrimaryKeyOf<FSBillingCycle>.By<FSBillingCycle.billingCycleCD>.FindBy(graph, (object) billingCycleCD, options);
    }
  }

  public abstract class billingCycleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSBillingCycle.billingCycleID>
  {
  }

  public abstract class billingCycleCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillingCycle.billingCycleCD>
  {
  }

  public abstract class billingCycleType : ListField_Billing_Cycle_Type
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBillingCycle.descr>
  {
  }

  public abstract class invoiceOnlyCompletedServiceOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSBillingCycle.invoiceOnlyCompletedServiceOrder>
  {
  }

  public abstract class timeCycleType : ListField_Time_Cycle_Type
  {
  }

  public abstract class timeCycleWeekDay : ListField_WeekDaysNumber
  {
  }

  public abstract class timeCycleDayOfMonth : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSBillingCycle.timeCycleDayOfMonth>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSBillingCycle.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillingCycle.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSBillingCycle.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSBillingCycle.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSBillingCycle.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSBillingCycle.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSBillingCycle.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSBillingCycle.noteID>
  {
  }

  public abstract class billingBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBillingCycle.billingBy>
  {
    public abstract class Values : ListField_Billing_By
    {
    }
  }

  public abstract class groupBillByLocations : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSBillingCycle.groupBillByLocations>
  {
  }
}
