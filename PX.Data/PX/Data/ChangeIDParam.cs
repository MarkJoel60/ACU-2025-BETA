// Decompiled with JetBrains decompiler
// Type: PX.Data.ChangeIDParam
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data;

/// <exclude />
[PXCacheName("Change ID Param")]
[PXHidden]
[Serializable]
public class ChangeIDParam : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "ID", Required = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string CD { get; set; }

  /// <exclude />
  public abstract class cD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ChangeIDParam.cD>
  {
  }
}
