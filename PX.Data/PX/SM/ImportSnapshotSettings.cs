// Decompiled with JetBrains decompiler
// Type: PX.SM.ImportSnapshotSettings
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
public class ImportSnapshotSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Name;
  protected string _dataType;
  protected bool? _Transaction;
  protected bool? _Customzation;
  protected bool? _CreateMissingCustomObjects;

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Destination Tenant", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Company { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string Description { get; set; }

  [PXUIField(DisplayName = "Data Type")]
  [PXString(2, IsUnicode = false, IsFixed = true, IsKey = true)]
  [AvailableDataTypesList]
  public virtual string DataType { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? Transaction
  {
    get => this._Transaction;
    set => this._Transaction = value;
  }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? CreateNew { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? AppendData { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Import Customization Data")]
  public virtual bool? Customization
  {
    get => this._Customzation;
    set => this._Customzation = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Create Custom Tables and Columns in Database")]
  public virtual bool? CreateMissingCustomObjects
  {
    get => this._CreateMissingCustomObjects;
    set => this._CreateMissingCustomObjects = value;
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportSnapshotSettings.name>
  {
  }

  public abstract class company : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportSnapshotSettings.company>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ImportSnapshotSettings.description>
  {
  }

  public abstract class dataType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportSnapshotSettings.dataType>
  {
  }

  public abstract class transaction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ImportSnapshotSettings.transaction>
  {
  }

  public abstract class createNew : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ImportSnapshotSettings.createNew>
  {
  }

  public abstract class appendData : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ImportSnapshotSettings.appendData>
  {
  }

  public abstract class customization : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ImportSnapshotSettings.customization>
  {
  }

  public abstract class createMissingCustomObjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ImportSnapshotSettings.createMissingCustomObjects>
  {
  }
}
