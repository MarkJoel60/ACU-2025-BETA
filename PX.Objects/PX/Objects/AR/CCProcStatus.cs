// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCProcStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public static class CCProcStatus
{
  public const 
  #nullable disable
  string Opened = "OPN";
  public const string Finalized = "FIN";
  public const string Error = "ERR";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "OPN", "FIN", "ERR" }, new string[3]
      {
        "Open",
        "Completed",
        "Error"
      })
    {
    }
  }

  public class opened : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCProcStatus.opened>
  {
    public opened()
      : base("OPN")
    {
    }
  }

  public class finalized : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCProcStatus.finalized>
  {
    public finalized()
      : base("FIN")
    {
    }
  }

  public class error : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCProcStatus.error>
  {
    public error()
      : base("ERR")
    {
    }
  }
}
