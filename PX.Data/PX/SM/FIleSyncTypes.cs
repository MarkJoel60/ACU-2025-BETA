// Decompiled with JetBrains decompiler
// Type: PX.SM.FIleSyncTypes
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

/// <exclude />
public static class FIleSyncTypes
{
  /// <exclude />
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "F", "H", "S", "C" }, new string[4]
      {
        "FTP",
        "HTTP",
        "Shared Folder",
        "SFTP"
      })
    {
    }
  }

  /// <exclude />
  public class ftp : BqlType<IBqlString, string>.Constant<
  #nullable disable
  FIleSyncTypes.ftp>
  {
    public ftp()
      : base("F")
    {
    }
  }

  /// <exclude />
  public class sftp : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FIleSyncTypes.sftp>
  {
    public sftp()
      : base("C")
    {
    }
  }

  /// <exclude />
  public class http : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FIleSyncTypes.http>
  {
    public http()
      : base("H")
    {
    }
  }

  /// <exclude />
  public class share : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FIleSyncTypes.share>
  {
    public share()
      : base("S")
    {
    }
  }
}
