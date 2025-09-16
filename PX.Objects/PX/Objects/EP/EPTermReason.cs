// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTermReason
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPTermReason
{
  public const 
  #nullable disable
  string Retirement = "RET";
  public const string Layoff = "LAY";
  public const string TerminatedForCause = "FIR";
  public const string Resignation = "RES";
  public const string Deceased = "DEC";
  public const string MedicalIssues = "MIS";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]
      {
        "RET",
        "LAY",
        "FIR",
        "RES",
        "DEC",
        "MIS"
      }, new string[6]
      {
        "Retirement",
        "Layoff",
        "Terminated for Cause",
        "Resignation",
        "Deceased",
        "Medical Issues"
      })
    {
    }
  }

  public class deseased : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPTermReason.deseased>
  {
    public deseased()
      : base("DEC")
    {
    }
  }
}
