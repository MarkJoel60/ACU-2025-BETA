// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPostDet
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

[Serializable]
public class FSPostDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<FSPostBatch, Where<FSPostBatch.batchID, Equal<Current<FSPostDet.batchID>>>>))]
  [PXDBDefault(typeof (FSPostBatch.batchID))]
  [PXUIField(DisplayName = "Batch ID")]
  public virtual int? BatchID { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(Enabled = false)]
  public virtual int? PostDetID { get; set; }

  [PXDBInt]
  [PXUIField(Enabled = false)]
  public virtual int? PostID { get; set; }

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

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Invoiced through AR")]
  public virtual bool? ARPosted { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "AR Document Type")]
  public virtual string ARDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AR Reference Nbr.")]
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

  [PXString(15)]
  [PXUIField(DisplayName = "Document Nbr.", Enabled = false)]
  public virtual string Mem_DocNbr
  {
    get
    {
      if (this.APPosted.GetValueOrDefault())
        return this.APRefNbr;
      if (this.ARPosted.GetValueOrDefault())
        return this.ARRefNbr;
      if (this.INPosted.GetValueOrDefault())
        return this.INRefNbr;
      if (this.SOPosted.GetValueOrDefault())
        return this.SOOrderNbr;
      if (this.SOInvPosted.GetValueOrDefault())
        return this.SOInvRefNbr;
      return this.PMPosted.GetValueOrDefault() ? this.PMRefNbr : string.Empty;
    }
  }

  [PXString(3)]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  public virtual string Mem_DocType
  {
    get
    {
      if (this.APPosted.GetValueOrDefault())
        return this.APDocType;
      if (this.ARPosted.GetValueOrDefault())
        return this.ARDocType;
      if (this.INPosted.GetValueOrDefault())
        return this.INDocType;
      if (this.SOPosted.GetValueOrDefault())
        return this.SOOrderType;
      if (this.SOInvPosted.GetValueOrDefault())
        return this.SOInvDocType;
      return this.PMPosted.GetValueOrDefault() ? this.PMDocType : string.Empty;
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  public virtual string PostDocType
  {
    get
    {
      if (this.APPosted.GetValueOrDefault())
        return "AP Invoice";
      if (this.ARPosted.GetValueOrDefault())
        return "AR Invoice";
      if (this.SOPosted.GetValueOrDefault())
        return "Sales Order";
      if (this.SOInvPosted.GetValueOrDefault())
        return "SO Invoice";
      if (this.PMPosted.GetValueOrDefault())
        return "Project";
      return this.INPosted.GetValueOrDefault() ? "Issue" : string.Empty;
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string PostDocReferenceNbr
  {
    get
    {
      if (this.APPosted.GetValueOrDefault())
        return $"{this.APDocType}, {this.APRefNbr}";
      if (this.ARPosted.GetValueOrDefault())
        return $"{this.ARDocType}, {this.ARRefNbr}";
      if (this.SOPosted.GetValueOrDefault())
        return $"{this.SOOrderType}, {this.SOOrderNbr}";
      if (this.SOInvPosted.GetValueOrDefault())
        return $"{this.SOInvDocType}, {this.SOInvRefNbr}";
      if (this.PMPosted.GetValueOrDefault())
        return $"{this.PMDocType}, {this.PMRefNbr}";
      return this.INPosted.GetValueOrDefault() ? $"{this.INDocType}, {this.INRefNbr}" : string.Empty;
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Issue Reference Nbr.", Enabled = false)]
  public virtual string INPostDocReferenceNbr
  {
    get => this.INPosted.GetValueOrDefault() ? $"{this.INDocType}, {this.INRefNbr}" : string.Empty;
  }

  [PXString(2)]
  [PXUIField(DisplayName = "Post To", Enabled = false)]
  public virtual string Mem_PostedIn
  {
    get
    {
      if (this.APPosted.GetValueOrDefault())
        return "AP";
      if (this.ARPosted.GetValueOrDefault())
        return "AR";
      if (this.INPosted.GetValueOrDefault())
        return "IN";
      if (this.SOPosted.GetValueOrDefault())
        return "SO";
      if (this.SOInvPosted.GetValueOrDefault())
        return "SI";
      return this.PMPosted.GetValueOrDefault() ? "PM" : string.Empty;
    }
  }

  [PXString(15, IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string InvoiceRefNbr { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  public virtual string InvoiceDocType { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Invoice Nbr.", Enabled = false)]
  public virtual string InvoiceReferenceNbr
  {
    get
    {
      return (this.InvoiceDocType != null ? this.InvoiceDocType.Trim() + ", " : "") + (this.InvoiceRefNbr != null ? this.InvoiceRefNbr.Trim() : "");
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Batch Number", Enabled = false)]
  public virtual string BatchNbr { get; set; }

  public class PK : PrimaryKeyOf<FSPostDet>.By<FSPostDet.batchID, FSPostDet.postDetID>
  {
    public static FSPostDet Find(
      PXGraph graph,
      int? batchID,
      int? postDetID,
      PKFindOptions options = 0)
    {
      return (FSPostDet) PrimaryKeyOf<FSPostDet>.By<FSPostDet.batchID, FSPostDet.postDetID>.FindBy(graph, (object) batchID, (object) postDetID, options);
    }
  }

  public static class FK
  {
    public class PostBatch : 
      PrimaryKeyOf<FSPostBatch>.By<FSPostBatch.batchNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.batchID>
    {
    }

    public class SOOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<FSPostDet>.By<FSPostDet.sOOrderType>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.sOOrderType, FSPostDet.sOOrderNbr>
    {
    }

    public class SOOrderLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.sOOrderType, FSPostDet.sOOrderNbr, FSPostDet.sOLineNbr>
    {
    }

    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.arDocType, FSPostDet.arRefNbr>
    {
    }

    public class ARInvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.arDocType, FSPostDet.arRefNbr, FSPostDet.aRLineNbr>
    {
    }

    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.apDocType, FSPostDet.apRefNbr>
    {
    }

    public class APInvoiceLine : 
      PrimaryKeyOf<APTran>.By<APTran.tranType, APTran.refNbr, APTran.lineNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.apDocType, FSPostDet.apRefNbr, FSPostDet.aPLineNbr>
    {
    }

    public class INIssue : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.iNDocType, FSPostDet.iNRefNbr>
    {
    }

    public class INIssueLine : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.iNDocType, FSPostDet.iNRefNbr, FSPostDet.iNLineNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.sOInvDocType, FSPostDet.sOInvRefNbr>
    {
    }

    public class SOInvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<FSPostDet>.By<FSPostDet.sOInvDocType, FSPostDet.sOInvRefNbr, FSPostDet.sOInvLineNbr>
    {
    }

    public class PMRegisterLine : 
      PrimaryKeyOf<PMTran>.By<PMTran.tranID>.ForeignKeyOf<FSPostDet>.By<FSPostDet.pMTranID>
    {
    }
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDet.batchID>
  {
  }

  public abstract class postDetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDet.postDetID>
  {
  }

  public abstract class postID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDet.postID>
  {
  }

  public abstract class sOPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostDet.sOPosted>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDet.sOLineNbr>
  {
  }

  public abstract class aRPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostDet.aRPosted>
  {
  }

  public abstract class arDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.arDocType>
  {
  }

  public abstract class arRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.arRefNbr>
  {
  }

  public abstract class aRLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDet.aRLineNbr>
  {
  }

  public abstract class aPPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostDet.aPPosted>
  {
  }

  public abstract class apDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.apDocType>
  {
  }

  public abstract class apRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.apRefNbr>
  {
  }

  public abstract class aPLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDet.aPLineNbr>
  {
  }

  public abstract class iNPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSPostDet.iNPosted>
  {
  }

  public abstract class iNDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.iNDocType>
  {
  }

  public abstract class iNRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.iNRefNbr>
  {
  }

  public abstract class iNLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSPostDet.iNLineNbr>
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
  FSPostDet.pMPosted>
  {
  }

  public abstract class pMDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.pMDocType>
  {
  }

  public abstract class pMRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.pMRefNbr>
  {
  }

  public abstract class pMTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSPostDet.pMTranID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSPostDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSPostDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSPostDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSPostDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSPostDet.Tstamp>
  {
  }

  public abstract class mem_DocNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.mem_DocNbr>
  {
  }

  public abstract class mem_DocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.mem_DocType>
  {
  }

  public abstract class postDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.postDocType>
  {
  }

  public abstract class postDocReferenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostDet.postDocReferenceNbr>
  {
  }

  public abstract class iNPostDocReferenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostDet.iNPostDocReferenceNbr>
  {
  }

  public abstract class mem_PostedIn : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.mem_PostedIn>
  {
  }

  public abstract class invoiceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.invoiceRefNbr>
  {
  }

  public abstract class invoiceDocType : IBqlField, IBqlOperand
  {
  }

  public abstract class invoiceReferenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSPostDet.invoiceReferenceNbr>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSPostDet.batchNbr>
  {
  }
}
