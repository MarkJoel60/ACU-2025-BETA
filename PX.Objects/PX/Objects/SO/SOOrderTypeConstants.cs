// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderTypeConstants
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

public class SOOrderTypeConstants
{
  public const 
  #nullable disable
  string SalesOrder = "SO";
  public const string TransferOrder = "TR";
  public const string RMAOrder = "RM";
  public const string QuoteOrder = "QT";
  public const string Invoice = "IN";
  public const string CreditMemo = "CM";
  public const string DebitMemo = "DM";
  public const string StandardOrder = "ST";

  public class salesOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderTypeConstants.salesOrder>
  {
    public salesOrder()
      : base("SO")
    {
    }
  }

  public class transferOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOOrderTypeConstants.transferOrder>
  {
    public transferOrder()
      : base("TR")
    {
    }
  }

  public class rmaOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderTypeConstants.rmaOrder>
  {
    public rmaOrder()
      : base("RM")
    {
    }
  }

  public class quoteOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderTypeConstants.quoteOrder>
  {
    public quoteOrder()
      : base("QT")
    {
    }
  }

  public class invoiceOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderTypeConstants.invoiceOrder>
  {
    public invoiceOrder()
      : base("IN")
    {
    }
  }

  public class creditMemo : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderTypeConstants.creditMemo>
  {
    public creditMemo()
      : base("CM")
    {
    }
  }
}
