// Decompiled with JetBrains decompiler
// Type: PX.SM.RowClipboard
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class RowClipboard : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  internal 
  #nullable disable
  string ExternalName;

  [PXInt(IsKey = true)]
  public int? OrderId { get; set; }

  [PXBool(IsKey = false)]
  [PXUIField(DisplayName = "Active")]
  public bool? Active { get; set; }

  [PXInt(IsKey = false)]
  [PXUIField(DisplayName = "Line")]
  public int? Line { get; set; }

  [PXString(IsKey = false, InputMask = "")]
  [PXUIField(DisplayName = "Container")]
  public string CName { get; set; }

  [PXString(IsKey = false, InputMask = "")]
  [PXUIField(DisplayName = "Field")]
  public string FName { get; set; }

  [PXString(IsKey = false, InputMask = "")]
  [PXUIField(DisplayName = "Value")]
  public string Value { get; set; }

  /// <exclude />
  public abstract class orderId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RowClipboard.orderId>
  {
  }

  /// <exclude />
  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RowClipboard.active>
  {
  }

  /// <exclude />
  public abstract class line : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RowClipboard.line>
  {
  }

  /// <exclude />
  public abstract class cName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowClipboard.cName>
  {
  }

  /// <exclude />
  public abstract class fName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowClipboard.fName>
  {
  }

  /// <exclude />
  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowClipboard.value>
  {
  }
}
