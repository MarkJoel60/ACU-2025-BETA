// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.CR;

#nullable enable
namespace PX.Objects.PM;

public class PMQuoteStatusAttribute : CRQuoteStatusAttribute
{
  public const 
  #nullable disable
  string Closed = "C";

  public PMQuoteStatusAttribute()
    : base(new string[10]
    {
      "D",
      "A",
      "S",
      "P",
      "R",
      "T",
      "O",
      "L",
      "C",
      "V"
    }, new string[10]
    {
      "Draft",
      "Approved",
      "Sent",
      "Pending Approval",
      "Rejected",
      "Accepted by Customer",
      "Converted",
      "Declined by Customer",
      nameof (Closed),
      "Approved"
    })
  {
  }

  public sealed class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMQuoteStatusAttribute.closed>
  {
    public closed()
      : base("C")
    {
    }
  }
}
