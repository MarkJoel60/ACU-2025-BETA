// Decompiled with JetBrains decompiler
// Type: PX.Api.SYReplace
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYReplace : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault(0)]
  [PXInt]
  [PXIntList]
  [PXUIField(DisplayName = "Column")]
  public int? ColumnIndex { get; set; }

  [PXString(125, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Find Column Value")]
  public 
  #nullable disable
  string SearchValue { get; set; }

  [PXString(125, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Replace With")]
  public string ReplaceValue { get; set; }

  [PXDefault(false)]
  [PXBool]
  [PXUIField(DisplayName = "Match Case")]
  public bool? MatchCase { get; set; }

  [PXString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result", Enabled = false)]
  public string ReplaceResult { get; set; }

  public abstract class columnIndex : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYReplace.columnIndex>
  {
  }

  public abstract class searchValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYReplace.searchValue>
  {
  }

  public abstract class replaceValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYReplace.replaceValue>
  {
  }

  public abstract class matchCase : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYReplace.matchCase>
  {
  }

  public abstract class replaceResult : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYReplace.replaceResult>
  {
  }
}
