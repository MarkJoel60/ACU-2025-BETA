// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQOrderCategory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.RQ;

public class RQOrderCategory
{
  public const 
  #nullable disable
  string PO = "PO";
  public const string SO = "SO";

  public class po : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQOrderCategory.po>
  {
    public po()
      : base("PO")
    {
    }
  }

  public class so : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQOrderCategory.so>
  {
    public so()
      : base("SO")
    {
    }
  }
}
