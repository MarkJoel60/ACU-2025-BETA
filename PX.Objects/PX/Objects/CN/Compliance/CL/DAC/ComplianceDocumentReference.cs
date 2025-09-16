// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceDocumentReference
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CN.Common.DAC;
using System;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.DAC;

[PXCacheName("Compliance Document Reference")]
public class ComplianceDocumentReference : BaseCache, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? ComplianceDocumentReferenceId { get; set; }

  [PXDBString]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXDBString]
  public virtual string ReferenceNumber { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? RefNoteId { get; set; }

  [PXDBCreatedByID]
  public override Guid? CreatedById { get; set; }

  public abstract class complianceDocumentReferenceId : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentReference.complianceDocumentReferenceId>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceDocumentReference.type>
  {
  }

  public abstract class referenceNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentReference.referenceNumber>
  {
  }

  public abstract class refNoteId : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentReference.refNoteId>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceDocumentReference.noteID>
  {
  }

  public abstract class tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    ComplianceDocumentReference.tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentReference.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentReference.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocumentReference.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceDocumentReference.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceDocumentReference.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceDocumentReference.lastModifiedDateTime>
  {
  }
}
