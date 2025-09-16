// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceAttributeType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.DAC;

[PXCacheName("Compliance Attribute Type")]
[Serializable]
public class ComplianceAttributeType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? ComplianceAttributeTypeID { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Type")]
  [PXDefault]
  public virtual 
  #nullable disable
  string Type { get; set; }

  public abstract class complianceAttributeTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ComplianceAttributeType.complianceAttributeTypeID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComplianceAttributeType.type>
  {
  }
}
