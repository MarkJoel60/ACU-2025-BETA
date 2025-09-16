// Decompiled with JetBrains decompiler
// Type: PX.Data.MassProcess.FieldValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Data.MassProcess;

/// <exclude />
[DebuggerDisplay("Display='{DisplayName}' Name='{Name}' Cache='{CacheName}'")]
[PXVirtual]
[Serializable]
public class FieldValue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private bool? _Selected;
  private bool? _Hidden;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXString]
  public virtual 
  #nullable disable
  string CacheName { get; set; }

  [PXString(IsKey = true)]
  public virtual string Name { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Name", Enabled = false)]
  public virtual string DisplayName { get; set; }

  [PXUIField(DisplayName = "Value")]
  public virtual string Value { get; set; }

  [PXString]
  public virtual string AttributeID { get; set; }

  [PXInt]
  public virtual int? Order { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? Hidden
  {
    get => this._Hidden;
    set
    {
      this._Hidden = value;
      bool? hidden = this._Hidden;
      bool flag = true;
      this._Selected = hidden.GetValueOrDefault() == flag & hidden.HasValue ? new bool?(false) : this._Selected;
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Required", Enabled = false)]
  public virtual bool? Required { get; set; }

  /// <exclude />
  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FieldValue.selected>
  {
  }

  /// <exclude />
  public abstract class cacheName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FieldValue.cacheName>
  {
  }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FieldValue.name>
  {
  }

  /// <exclude />
  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FieldValue.displayName>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FieldValue.value>
  {
  }

  /// <exclude />
  public abstract class attributeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FieldValue.attributeID>
  {
  }

  /// <exclude />
  public abstract class order : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FieldValue.order>
  {
  }

  /// <exclude />
  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FieldValue.hidden>
  {
  }

  /// <exclude />
  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FieldValue.required>
  {
  }
}
