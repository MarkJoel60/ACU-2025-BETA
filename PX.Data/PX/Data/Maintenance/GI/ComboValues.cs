// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.ComboValues
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
public class ComboValues : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXGuid(IsKey = true)]
  public Guid? DesignID { get; set; }

  [PXInt(IsKey = true)]
  public int? ParamNbr { get; set; }

  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Value")]
  public 
  #nullable disable
  string Value { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Label")]
  public string Label { get; set; }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ComboValues.designID>
  {
  }

  /// <exclude />
  public abstract class paramNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComboValues.paramNbr>
  {
  }

  /// <exclude />
  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComboValues.value>
  {
  }

  /// <exclude />
  public abstract class label : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComboValues.label>
  {
  }
}
