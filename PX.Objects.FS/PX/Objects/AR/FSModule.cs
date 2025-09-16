// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.FSModule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data.BQL;
using PX.Objects.CA;

#nullable enable
namespace PX.Objects.AR;

public class FSModule : PXModule
{
  public const 
  #nullable disable
  string FS = "FS";

  public class fs : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSModule.fs>
  {
    public fs()
      : base("FS")
    {
    }
  }

  public class fs_ : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSModule.fs_>
  {
    public fs_()
      : base("FS%")
    {
    }
  }
}
