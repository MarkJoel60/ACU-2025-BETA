// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPostDoc
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSPostDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false)]
  public virtual Guid? ProcessID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBInt]
  public virtual int? BillingCycleID { get; set; }

  [PXDBString]
  public virtual 
  #nullable disable
  string GroupKey { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  [PXSelector(typeof (Search<FSAppointment.appointmentID>), SubstituteKey = typeof (FSAppointment.refNbr))]
  public virtual int? AppointmentID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [PXSelector(typeof (Search<FSServiceOrder.sOID>), SubstituteKey = typeof (FSServiceOrder.refNbr))]
  public virtual int? SOID { get; set; }

  [PXDBInt]
  public virtual int? RowIndex { get; set; }

  [PXDBBool]
  public virtual bool? PostNegBalanceToAP { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  public virtual string PostOrderType { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  public virtual string PostOrderTypeNegativeBalance { get; set; }

  [PXShort]
  public virtual short? InvtMult { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBInt]
  public virtual int? BatchID { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  public virtual string EntityType { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Posted to")]
  public virtual string PostedTO { get; set; }

  [PXDBString(3, InputMask = ">aaa")]
  [PXUIField(DisplayName = "Document Type")]
  public virtual string PostDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Nbr.")]
  public virtual string PostRefNbr { get; set; }

  public virtual object DocLineRef { get; set; }

  public virtual object INDocLineRef { get; set; }

  public abstract class processID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSPostDoc.processID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDoc.recordID>
  {
  }

  public abstract class billingCycleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDoc.billingCycleID>
  {
  }

  public abstract class groupKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDoc.groupKey>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDoc.appointmentID>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDoc.sOID>
  {
  }

  public abstract class rowIndex : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDoc.rowIndex>
  {
  }

  public abstract class postNegBalanceToAP : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSPostDoc.postNegBalanceToAP>
  {
  }

  public abstract class postOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDoc.postOrderType>
  {
  }

  public abstract class postOrderTypeNegativeBalance : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostDoc.postOrderTypeNegativeBalance>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSPostDoc.invtMult>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSPostDoc.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostDoc.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSPostDoc.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSPostDoc.Tstamp>
  {
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDoc.batchID>
  {
  }

  public abstract class entityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDoc.entityType>
  {
  }

  public abstract class postedTO : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDoc.postedTO>
  {
  }

  public abstract class postDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDoc.postDocType>
  {
  }

  public abstract class postRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDoc.postRefNbr>
  {
  }
}
