// Decompiled with JetBrains decompiler
// Type: PX.SM.FilesLinkInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class FilesLinkInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Public Link", IsReadOnly = true)]
  [PXString]
  public virtual 
  #nullable disable
  string ExternalPath { get; set; }

  [PXUIField(DisplayName = "External Link", IsReadOnly = true)]
  [PXString]
  public virtual string InternalPath { get; set; }

  /// <exclude />
  public abstract class externalPath : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilesLinkInfo.externalPath>
  {
  }

  /// <exclude />
  public abstract class internalPath : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilesLinkInfo.internalPath>
  {
  }
}
