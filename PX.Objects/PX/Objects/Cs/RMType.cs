// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public static class RMType
{
  public const 
  #nullable disable
  string GL = "GL";
  public const string PM = "PM";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "GL", "PM" }, new string[2]
      {
        "GL",
        "PM"
      })
    {
    }
  }

  public class RMTypeGL : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RMType.RMTypeGL>
  {
    public RMTypeGL()
      : base("GL")
    {
    }
  }

  public class RMTypePM : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RMType.RMTypePM>
  {
    public RMTypePM()
      : base("PM")
    {
    }
  }
}
