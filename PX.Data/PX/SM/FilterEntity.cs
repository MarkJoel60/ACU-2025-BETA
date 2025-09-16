// Decompiled with JetBrains decompiler
// Type: PX.SM.FilterEntity
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Xml.Serialization;

#nullable enable
namespace PX.SM;

[Serializable]
public class FilterEntity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _EntityTypeName;
  private string _Entity;
  private int? _ID;
  protected System.Type _EntityType;
  private object _Instance;

  [PXString(128 /*0x80*/, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Entity Type")]
  [MaskedTypeSelector]
  public virtual string EntityTypeName
  {
    get => this._EntityTypeName;
    set => this._EntityTypeName = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Entity", Visibility = PXUIVisibility.SelectorVisible)]
  [InstancesSelector]
  public string Entity
  {
    get => this._Entity;
    set => this._Entity = value;
  }

  [PXDBInt(IsKey = false)]
  [PXUIField(Visibility = PXUIVisibility.Invisible, Enabled = false)]
  public int? ID
  {
    get => this._ID;
    set => this._ID = value;
  }

  [SoapIgnore]
  [PXUIField(Visibility = PXUIVisibility.Invisible)]
  public virtual System.Type EntityType
  {
    get => this._EntityType;
    set => this._EntityType = value;
  }

  public object Instance
  {
    get => this._Instance;
    set => this._Instance = value;
  }

  public abstract class entityTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FilterEntity.entityTypeName>
  {
  }

  public abstract class entity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterEntity.entity>
  {
  }

  public abstract class iD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterEntity.iD>
  {
  }

  public abstract class entityType : IBqlField, IBqlOperand
  {
  }
}
