// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityReason
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class OpportunityReason
{
  public const string Created = "CR";
  public const string Technology = "TH";
  public const string Relationship = "RL";
  public const string Price = "PR";
  public const string Other = "OT";
  public const string InProcess = "IP";
  public const string CompanyMaturity = "CM";
  public const string ConvertedFromLead = "FL";
  public const string Qualified = "QL";
  public const string OrderPlaced = "OP";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new (string, string)[10]
      {
        ("CR", "Created"),
        ("TH", "Technology"),
        ("RL", "Relationship"),
        ("PR", "Price"),
        ("OT", "Other"),
        ("IP", "In Process"),
        ("CM", "Company Maturity"),
        ("FL", "Converted from Lead"),
        ("QL", "Qualified"),
        ("OP", "Order Placed")
      })
    {
    }
  }
}
