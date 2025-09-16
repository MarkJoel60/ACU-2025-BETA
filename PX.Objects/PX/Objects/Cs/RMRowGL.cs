// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMRowGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class RMRowGL : PXCacheExtension<
#nullable disable
RMRow>
{
  protected int? _DataSourceID;

  [RMDataSourceGL]
  public virtual int? DataSourceID
  {
    get => this._DataSourceID;
    set => this._DataSourceID = value;
  }

  public abstract class dataSourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMRowGL.dataSourceID>
  {
  }
}
