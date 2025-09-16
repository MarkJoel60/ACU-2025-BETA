// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceDocumentBill
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.DAC;

[PXCacheName("Compliance Document Bill Reference")]
public class ComplianceDocumentBill : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _NoteID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXParent(typeof (Select<ComplianceDocument, Where<ComplianceDocument.complianceDocumentID, Equal<Current<ComplianceDocumentBill.complianceDocumentID>>>>))]
  [PXUIField]
  [PXDBDefault(typeof (ComplianceDocument.complianceDocumentID))]
  [PXDBInt(IsKey = true)]
  public virtual int? ComplianceDocumentID { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "AP Doc. Type")]
  [APDocType.List]
  public virtual string DocType { get; set; }

  [PXDBString(30, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "AP Reference Nbr.")]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", FieldClass = "PaymentsByLines")]
  public virtual int? LineNbr { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Paid")]
  public virtual Decimal? AmountPaid { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>Primary Key</summary>
  /// <exclude />
  public class PK : 
    PrimaryKeyOf<ComplianceDocumentBill>.By<ComplianceDocumentBill.complianceDocumentID, ComplianceDocumentBill.docType, ComplianceDocumentBill.refNbr>
  {
    public static ComplianceDocumentBill Find(
      PXGraph graph,
      int? complianceDocumentID,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ComplianceDocumentBill) PrimaryKeyOf<ComplianceDocumentBill>.By<ComplianceDocumentBill.complianceDocumentID, ComplianceDocumentBill.docType, ComplianceDocumentBill.refNbr>.FindBy(graph, (object) complianceDocumentID, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class complianceDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceDocumentBill.complianceDocumentID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceDocumentBill.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceDocumentBill.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceDocumentBill.lineNbr>
  {
  }

  public abstract class amountPaid : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ComplianceDocumentBill.amountPaid>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceDocumentBill.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ComplianceDocumentBill.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentBill.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentBill.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocumentBill.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentBill.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentBill.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocumentBill.lastModifiedDateTime>
  {
  }
}
