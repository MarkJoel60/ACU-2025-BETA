// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceDocumentPaymentReference
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.DAC;

[PXHidden]
[PXBreakInheritance]
public class ComplianceDocumentPaymentReference : ComplianceDocumentReference
{
  [PXDefault(typeof (APPayment.docType))]
  [PXDBString]
  public override 
  #nullable disable
  string Type { get; set; }

  [PXDBDefault(typeof (APPayment.refNbr))]
  [PXDBString]
  public override string ReferenceNumber { get; set; }

  [PXDefault(typeof (APPayment.noteID))]
  [PXDBGuid(false)]
  public override Guid? RefNoteId { get; set; }

  public new abstract class complianceDocumentReferenceId : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.complianceDocumentReferenceId>
  {
  }

  public new abstract class type : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.type>
  {
  }

  public new abstract class referenceNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.referenceNumber>
  {
  }

  public new abstract class refNoteId : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.refNoteId>
  {
  }

  public new abstract class noteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.noteID>
  {
  }

  public new abstract class tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.tstamp>
  {
  }

  public new abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocumentPaymentReference.lastModifiedDateTime>
  {
  }
}
