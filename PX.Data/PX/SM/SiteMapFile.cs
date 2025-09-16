// Decompiled with JetBrains decompiler
// Type: PX.SM.SiteMapFile
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
public class SiteMapFile : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _NodeID;
  protected 
  #nullable disable
  string _Title;
  protected int? _Position;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? NodeID
  {
    get => this._NodeID;
    set => this._NodeID = value;
  }

  [PXDBLocalString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBInt]
  public virtual int? Position
  {
    get => this._Position;
    set => this._Position = value;
  }

  /// <exclude />
  public abstract class nodeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SiteMapFile.nodeID>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMapFile.title>
  {
  }

  /// <exclude />
  public abstract class position : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteMapFile.position>
  {
  }
}
