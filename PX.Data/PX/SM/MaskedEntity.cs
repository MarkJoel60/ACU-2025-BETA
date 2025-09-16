// Decompiled with JetBrains decompiler
// Type: PX.SM.MaskedEntity
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class MaskedEntity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private bool? _Selected;
  private int? _ID;
  private 
  #nullable disable
  string _Entity;
  private object _Instance;

  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.Invisible, Enabled = false)]
  public int? ID
  {
    get => this._ID;
    set => this._ID = value;
  }

  [PXString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Entity", Enabled = false)]
  public string Entity
  {
    get => this._Entity;
    set => this._Entity = value;
  }

  public object Instance
  {
    get => this._Instance;
    set => this._Instance = value;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MaskedEntity.selected>
  {
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MaskedEntity.iD>
  {
  }

  public abstract class entity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MaskedEntity.entity>
  {
  }
}
