// Decompiled with JetBrains decompiler
// Type: PX.SM.RowTaskInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Async.Internal;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class RowTaskInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  internal 
  #nullable disable
  OperationKey OperationKey { get; set; }

  public object NativeKey => this.OperationKey.Original;

  [PXString(IsKey = true, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Key", Visible = false, IsReadOnly = true)]
  public string Key { get; set; }

  [PXString(IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "User", IsReadOnly = true)]
  public string User { get; set; }

  [PXString(IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Screen", IsReadOnly = true)]
  public string Screen { get; set; }

  [PXString(IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Title", IsReadOnly = true)]
  public string Title { get; set; }

  [PXDBInt(IsKey = false)]
  [PXUIField(DisplayName = "Processed")]
  public int? Processed { get; set; }

  [PXDBInt(IsKey = false)]
  [PXUIField(DisplayName = "Total")]
  public int? Total { get; set; }

  [PXDBInt(IsKey = false)]
  [PXUIField(DisplayName = "Errors")]
  public int? Errors { get; set; }

  [PXString(IsKey = false)]
  [PXUIField(DisplayName = "Time", IsReadOnly = true)]
  public string WorkTime { get; set; }

  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowTaskInfo.key>
  {
  }

  public abstract class user : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowTaskInfo.user>
  {
  }

  public abstract class screen : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowTaskInfo.screen>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowTaskInfo.title>
  {
  }

  public abstract class processed : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RowTaskInfo.processed>
  {
  }

  public abstract class total : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RowTaskInfo.total>
  {
  }

  public abstract class errors : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RowTaskInfo.errors>
  {
  }

  public abstract class time : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RowTaskInfo.time>
  {
  }
}
