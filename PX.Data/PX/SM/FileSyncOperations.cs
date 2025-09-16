// Decompiled with JetBrains decompiler
// Type: PX.SM.FileSyncOperations
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

/// <exclude />
public static class FileSyncOperations
{
  /// <exclude />
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "D", "U" }, new string[2]
      {
        "Import File",
        "Export File"
      })
    {
    }
  }

  /// <exclude />
  public class download : BqlType<IBqlString, string>.Constant<
  #nullable disable
  FileSyncOperations.download>
  {
    public download()
      : base("D")
    {
    }
  }

  /// <exclude />
  public class upload : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FileSyncOperations.upload>
  {
    public upload()
      : base("U")
    {
    }
  }
}
