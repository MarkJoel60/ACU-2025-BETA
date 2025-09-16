// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.Discount;

public static class DiscountType
{
  public const 
  #nullable disable
  string Line = "L";
  public const string Group = "G";
  public const string Document = "D";
  public const string ExternalDocument = "B";
  public const string Flat = "F";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("L", "Line"),
        PXStringListAttribute.Pair("G", "Group"),
        PXStringListAttribute.Pair("D", "Document"),
        PXStringListAttribute.Pair("B", "External Document")
      })
    {
    }
  }

  public class LineDiscount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountType.LineDiscount>
  {
    public LineDiscount()
      : base("L")
    {
    }
  }

  public class GroupDiscount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountType.GroupDiscount>
  {
    public GroupDiscount()
      : base("G")
    {
    }
  }

  public class DocumentDiscount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountType.DocumentDiscount>
  {
    public DocumentDiscount()
      : base("D")
    {
    }
  }

  public class ExternalDocumentDiscount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountType.ExternalDocumentDiscount>
  {
    public ExternalDocumentDiscount()
      : base("B")
    {
    }
  }
}
