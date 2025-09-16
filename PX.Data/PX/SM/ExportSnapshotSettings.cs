// Decompiled with JetBrains decompiler
// Type: PX.SM.ExportSnapshotSettings
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
public class ExportSnapshotSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Name;
  protected string _dataType;

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Source Tenant", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Company { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description { get; set; }

  [PXUIField(DisplayName = "Export Mode")]
  [PXString(255 /*0xFF*/, IsUnicode = false)]
  [PXDefault("Full")]
  [ExportModeList]
  public virtual string ExportMode { get; set; }

  [PXUIField(DisplayName = "Prepare for Export")]
  [PXBool]
  [PXDefault(false)]
  public virtual bool? Prepare { get; set; }

  [PXUIField(DisplayName = "Export Format")]
  [PXString(3)]
  [PXDefault("ADB")]
  [PrepareModeList]
  public virtual string PrepareMode { get; set; }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExportSnapshotSettings.name>
  {
  }

  public abstract class company : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExportSnapshotSettings.company>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExportSnapshotSettings.description>
  {
  }

  public abstract class exportMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExportSnapshotSettings.exportMode>
  {
  }

  public abstract class prepare : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExportSnapshotSettings.prepare>
  {
  }

  public abstract class prepareMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExportSnapshotSettings.prepareMode>
  {
  }
}
