// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.NewFilter
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
public class NewFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Name", Enabled = false)]
  public 
  #nullable disable
  string Name { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Required")]
  public bool? Required { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Schema Field")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public string Field { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Display Name")]
  public string DisplayName { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Available Values")]
  public string AvailableValues { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Default Value")]
  public string DefaultValue { get; set; }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Columns Span")]
  public int? ColSpan { get; set; }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewFilter.name>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NewFilter.required>
  {
  }

  /// <exclude />
  public abstract class field : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewFilter.field>
  {
  }

  /// <exclude />
  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewFilter.displayName>
  {
  }

  /// <exclude />
  public abstract class availableValues : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NewFilter.availableValues>
  {
  }

  /// <exclude />
  public abstract class defaultValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewFilter.defaultValue>
  {
  }

  /// <exclude />
  public abstract class colSpan : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NewFilter.colSpan>
  {
  }
}
