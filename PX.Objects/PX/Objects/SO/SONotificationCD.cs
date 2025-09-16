// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SONotificationCD
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

public class SONotificationCD
{
  public const 
  #nullable disable
  string SalesOrder = "SALES ORDER";
  public const string SalesOrderPayLink = "SALES ORDER PAY LINK";
  public const string SOInvoice = "SO INVOICE";

  public class salesOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SONotificationCD.salesOrder>
  {
    public salesOrder()
      : base("SALES ORDER")
    {
    }
  }

  public class salesOrderPayLink : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SONotificationCD.salesOrderPayLink>
  {
    public salesOrderPayLink()
      : base("SALES ORDER PAY LINK")
    {
    }
  }

  public class soInvoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SONotificationCD.soInvoice>
  {
    public soInvoice()
      : base("SO INVOICE")
    {
    }
  }
}
