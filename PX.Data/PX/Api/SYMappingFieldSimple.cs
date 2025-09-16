// Decompiled with JetBrains decompiler
// Type: PX.Api.SYMappingFieldSimple
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYMappingFieldSimple : SYMappingField
{
  [PXDBString(4000, IsUnicode = true)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  [PXUIField(DisplayName = "Field Name")]
  public override 
  #nullable disable
  string FieldName { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Key", Enabled = false)]
  public virtual bool? IsKey { get; set; }

  public new abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMappingFieldSimple.fieldName>
  {
  }

  public abstract class isKey : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMappingFieldSimple.isKey>
  {
  }
}
