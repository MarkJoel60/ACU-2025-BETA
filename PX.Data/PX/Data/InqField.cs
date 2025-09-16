// Decompiled with JetBrains decompiler
// Type: PX.Data.InqField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#nullable enable
namespace PX.Data;

/// <exclude />
[PXHidden]
[Serializable]
public class InqField : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  public virtual 
  #nullable disable
  string Name { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string DisplayName { get; set; }

  public System.Type FieldType { get; set; }

  [PXBool]
  public bool? Visible { get; set; }

  [PXBool]
  public bool? Enabled { get; set; }

  [PXString]
  public string LinkCommand { get; set; }

  [JsonIgnore]
  [XmlIgnore]
  public string FieldName { get; set; }

  public Dictionary<string, string> DisplayNameTranslations { get; set; }

  private string DebuggerDisplay => $"{this.Name ?? "null"} | \"{this.DisplayName ?? "null"}\"";

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InqField.name>
  {
  }

  /// <exclude />
  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InqField.displayName>
  {
  }

  public abstract class visible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InqField.visible>
  {
  }

  /// <exclude />
  public abstract class enabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InqField.enabled>
  {
  }

  /// <exclude />
  public abstract class linkCommand : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InqField.linkCommand>
  {
  }
}
