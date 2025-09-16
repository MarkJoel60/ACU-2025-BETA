// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPostInfo
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSPostInfo")]
[Serializable]
public class FSPostInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  [PXUIField(Enabled = false)]
  public virtual int? PostID { get; set; }

  [PXDBInt]
  public virtual int? AppointmentID { get; set; }

  [PXDBInt]
  public virtual int? SOID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Invoiced through Sales Order")]
  public virtual bool? SOPosted { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Sales Order Type")]
  public virtual 
  #nullable disable
  string SOOrderType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Sales Order Nbr.")]
  public virtual string SOOrderNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Sales Order Line Nbr.")]
  public virtual int? SOLineNbr { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Invoiced through AR")]
  public virtual bool? ARPosted { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "AR Document Type")]
  public virtual string ARDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AR Ref. Nbr.")]
  public virtual string ARRefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "AR Line Nbr.")]
  public virtual int? ARLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Invoiced through AP")]
  public virtual bool? APPosted { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "AP Document Type")]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AP Reference Nbr.")]
  public virtual string APRefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "AP Line Nbr.")]
  public virtual int? APLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Invoiced through IN")]
  public virtual bool? INPosted { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "IN Document Type")]
  public virtual string INDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "IN Reference Nbr.")]
  public virtual string INRefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "IN Line Nbr.")]
  public virtual int? INLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Invoiced through SO Invoice")]
  public virtual bool? SOInvPosted { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "SO Invoice Document Type")]
  public virtual string SOInvDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "SO Invoice Ref. Nbr.")]
  public virtual string SOInvRefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "SO Invoice Line Nbr.")]
  public virtual int? SOInvLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Invoiced through PM")]
  public virtual bool? PMPosted { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "PM Document Type")]
  public virtual string PMDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PM Reference Nbr.")]
  public virtual string PMRefNbr { get; set; }

  [PXDBLong]
  [PXUIField(DisplayName = "PM Tran ID")]
  public virtual long? PMTranID { get; set; }

  public virtual bool isPosted()
  {
    return this.APPosted.GetValueOrDefault() || this.SOPosted.GetValueOrDefault() || this.ARPosted.GetValueOrDefault() || this.INPosted.GetValueOrDefault() || this.PMPosted.GetValueOrDefault() || this.SOInvPosted.GetValueOrDefault() || this.PMPosted.GetValueOrDefault();
  }

  public class PK : PrimaryKeyOf<FSPostInfo>.By<FSPostInfo.postID>
  {
    public static FSPostInfo Find(PXGraph graph, int? postID, PKFindOptions options = 0)
    {
      return (FSPostInfo) PrimaryKeyOf<FSPostInfo>.By<FSPostInfo.postID>.FindBy(graph, (object) postID, options);
    }
  }

  public static class FK
  {
    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.appointmentID>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.appointmentID>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.sOID>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.sOID>
    {
    }

    public class SOOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.sOOrderType>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.sOOrderType, FSPostInfo.sOOrderNbr>
    {
    }

    public class SOOrderLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.sOOrderType, FSPostInfo.sOOrderNbr, FSPostInfo.sOLineNbr>
    {
    }

    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.arDocType, FSPostInfo.arRefNbr>
    {
    }

    public class ARInvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.arDocType, FSPostInfo.arRefNbr, FSPostInfo.aRLineNbr>
    {
    }

    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.apDocType, FSPostInfo.apRefNbr>
    {
    }

    public class APInvoiceLine : 
      PrimaryKeyOf<APTran>.By<APTran.tranType, APTran.refNbr, APTran.lineNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.apDocType, FSPostInfo.apRefNbr, FSPostInfo.aPLineNbr>
    {
    }

    public class INIssue : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.iNDocType, FSPostInfo.iNRefNbr>
    {
    }

    public class INIssueLine : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.iNDocType, FSPostInfo.iNRefNbr, FSPostInfo.iNLineNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.sOInvDocType, FSPostInfo.sOInvRefNbr>
    {
    }

    public class SOInvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.sOInvDocType, FSPostInfo.sOInvRefNbr, FSPostInfo.sOInvLineNbr>
    {
    }

    public class PMRegisterLine : 
      PrimaryKeyOf<PMTran>.By<PMTran.tranID>.ForeignKeyOf<FSPostInfo>.By<FSPostInfo.pMTranID>
    {
    }
  }

  public abstract class postID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostInfo.postID>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostInfo.appointmentID>
  {
  }

  public abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostInfo.sOID>
  {
  }

  public abstract class sOPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostInfo.sOPosted>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostInfo.sOLineNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSPostInfo.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostInfo.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSPostInfo.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSPostInfo.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostInfo.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSPostInfo.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSPostInfo.Tstamp>
  {
  }

  public abstract class aRPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostInfo.aRPosted>
  {
  }

  public abstract class arDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.arDocType>
  {
  }

  public abstract class arRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.arRefNbr>
  {
  }

  public abstract class aRLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostInfo.aRLineNbr>
  {
  }

  public abstract class aPPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostInfo.aPPosted>
  {
  }

  public abstract class apDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.apDocType>
  {
  }

  public abstract class apRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.apRefNbr>
  {
  }

  public abstract class aPLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostInfo.aPLineNbr>
  {
  }

  public abstract class iNPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostInfo.iNPosted>
  {
  }

  public abstract class iNDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.iNDocType>
  {
  }

  public abstract class iNRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.iNRefNbr>
  {
  }

  public abstract class iNLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostInfo.iNLineNbr>
  {
  }

  public abstract class sOInvPosted : IBqlField, IBqlOperand
  {
  }

  public abstract class sOInvDocType : IBqlField, IBqlOperand
  {
  }

  public abstract class sOInvRefNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class sOInvLineNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class pMPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostInfo.pMPosted>
  {
  }

  public abstract class pMDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.pMDocType>
  {
  }

  public abstract class pMRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostInfo.pMRefNbr>
  {
  }

  public abstract class pMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSPostInfo.pMTranID>
  {
  }
}
