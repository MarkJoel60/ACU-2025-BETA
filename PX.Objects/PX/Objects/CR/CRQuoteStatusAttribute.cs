// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRQuoteStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class CRQuoteStatusAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Draft = "D";
  public const string Approved = "A";
  public const string Sent = "S";
  public const string PendingApproval = "P";
  public const string Rejected = "R";
  public const string Accepted = "T";
  public const string Converted = "O";
  public const string Declined = "L";
  public const string QuoteApproved = "V";

  public CRQuoteStatusAttribute()
    : base(new string[9]
    {
      "D",
      "A",
      "S",
      "P",
      "R",
      "T",
      "O",
      "L",
      "V"
    }, new string[9]
    {
      nameof (Draft),
      "Prepared",
      nameof (Sent),
      "Pending Approval",
      nameof (Rejected),
      nameof (Accepted),
      nameof (Converted),
      nameof (Declined),
      nameof (Approved)
    })
  {
  }

  protected CRQuoteStatusAttribute(string[] allowedValues, string[] allowedLabels)
    : base(allowedValues, allowedLabels)
  {
  }

  public sealed class draft : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRQuoteStatusAttribute.draft>
  {
    public draft()
      : base("D")
    {
    }
  }

  public sealed class approved : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRQuoteStatusAttribute.approved>
  {
    public approved()
      : base("A")
    {
    }
  }

  public sealed class sent : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRQuoteStatusAttribute.sent>
  {
    public sent()
      : base("S")
    {
    }
  }

  public sealed class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRQuoteStatusAttribute.pendingApproval>
  {
    public pendingApproval()
      : base("P")
    {
    }
  }

  public sealed class rejected : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRQuoteStatusAttribute.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }

  public sealed class accepted : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRQuoteStatusAttribute.accepted>
  {
    public accepted()
      : base("T")
    {
    }
  }

  public sealed class converted : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRQuoteStatusAttribute.converted>
  {
    public converted()
      : base("O")
    {
    }
  }

  public sealed class declined : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRQuoteStatusAttribute.declined>
  {
    public declined()
      : base("L")
    {
    }
  }

  public sealed class quoteApproved : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRQuoteStatusAttribute.quoteApproved>
  {
    public quoteApproved()
      : base("V")
    {
    }
  }
}
