// Decompiled with JetBrains decompiler
// Type: PX.Api.SYInsertFrom
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
public class SYInsertFrom : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Mapping")]
  [PXSelector(typeof (SYMapping.mappingID), SubstituteKey = typeof (SYMapping.name))]
  public Guid? MappingID { get; set; }

  public abstract class mappingID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  SYInsertFrom.mappingID>
  {
  }
}
