// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CABankTranStatus
{
  public const 
  #nullable disable
  string Matched = "M";
  public const string InvoiceMatched = "I";
  public const string Created = "C";
  public const string Hidden = "H";
  public const string ExpenseReceiptMatched = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "M", "I", "R", "C", "H" }, new string[5]
      {
        "Matched to Payment",
        "Matched to Invoice",
        "Matched to Expense Receipt",
        "Created",
        "Hidden"
      })
    {
    }
  }

  public class ImagesListAttribute : PXImagesListAttribute
  {
    private static string getCustomSprite(string sprite) => "main@" + sprite;

    public ImagesListAttribute()
      : base(new string[4]{ "M", "I", "C", "H" }, new string[4]
      {
        "Matched to Payment",
        "Matched to Invoice",
        "Created",
        "Hidden"
      }, new string[4]
      {
        CABankTranStatus.ImagesListAttribute.getCustomSprite("Link"),
        CABankTranStatus.ImagesListAttribute.getCustomSprite("LinkWB"),
        CABankTranStatus.ImagesListAttribute.getCustomSprite("RecordAdd"),
        CABankTranStatus.ImagesListAttribute.getCustomSprite("Preview")
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankTranStatus.hold>
  {
    public hold()
      : base("M")
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankTranStatus.balanced>
  {
    public balanced()
      : base("I")
    {
    }
  }

  public class unposted : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankTranStatus.unposted>
  {
    public unposted()
      : base("C")
    {
    }
  }

  public class posted : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CABankTranStatus.posted>
  {
    public posted()
      : base("H")
    {
    }
  }
}
