// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.CheckboxCombobox
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Maintenance.GI;

/// <exclude />
[Serializable]
public class CheckboxCombobox : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Checkbox { get; set; }

  [PXString]
  [PXUIField]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public 
  #nullable disable
  string Combobox { get; set; }

  /// <exclude />
  public abstract class checkbox : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CheckboxCombobox.checkbox>
  {
  }

  /// <exclude />
  public abstract class combobox : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CheckboxCombobox.combobox>
  {
  }
}
