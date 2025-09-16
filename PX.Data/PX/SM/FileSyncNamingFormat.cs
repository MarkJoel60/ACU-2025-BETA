// Decompiled with JetBrains decompiler
// Type: PX.SM.FileSyncNamingFormat
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

/// <exclude />
public static class FileSyncNamingFormat
{
  /// <exclude />
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "R", "D" }, new string[2]
      {
        "Revision",
        "Date"
      })
    {
    }
  }

  /// <exclude />
  public class revision : BqlType<IBqlString, string>.Constant<
  #nullable disable
  FileSyncNamingFormat.revision>
  {
    public revision()
      : base("R")
    {
    }
  }

  /// <exclude />
  public class date : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FileSyncNamingFormat.date>
  {
    public date()
      : base("D")
    {
    }
  }
}
