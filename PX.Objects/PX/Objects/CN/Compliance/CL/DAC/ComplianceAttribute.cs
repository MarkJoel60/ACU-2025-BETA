// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceAttribute
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

[PXCacheName("Compliance Attribute")]
[Serializable]
public class ComplianceAttribute : BaseCache, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? AttributeId { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<ComplianceAttributeType.complianceAttributeTypeID, Where<ComplianceAttributeType.type, NotEqual<ComplianceDocumentType.status>>>), SubstituteKey = typeof (ComplianceAttributeType.type))]
  [PXDefault]
  public virtual int? Type { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  [PXDefault]
  public virtual 
  #nullable disable
  string Value { get; set; }

  public abstract class attributeId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceAttribute.attributeId>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComplianceAttribute.type>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceAttribute.value>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceAttribute.noteID>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ComplianceAttribute.tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComplianceAttribute.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceAttribute.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceAttribute.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ComplianceAttribute.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComplianceAttribute.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ComplianceAttribute.lastModifiedDateTime>
  {
  }
}
