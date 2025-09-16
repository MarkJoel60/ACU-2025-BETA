// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FromToFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FromToFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DateBegin { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? DateEnd { get; set; }

  public abstract class dateBegin : BqlType<IBqlDateTime, DateTime>.Field<
  #nullable disable
  FromToFilter.dateBegin>
  {
  }

  public abstract class dateEnd : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FromToFilter.dateEnd>
  {
  }
}
