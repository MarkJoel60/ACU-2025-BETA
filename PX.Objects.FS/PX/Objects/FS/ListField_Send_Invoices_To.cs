// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Send_Invoices_To
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Send_Invoices_To : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Send_Invoices_To().ID_LIST, new ID.Send_Invoices_To().TX_LIST)
    {
    }
  }

  public class BillingCustomerBillTo : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Send_Invoices_To.BillingCustomerBillTo>
  {
    public BillingCustomerBillTo()
      : base("BT")
    {
    }
  }

  public class DefaultBillingCustomerLocation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Send_Invoices_To.DefaultBillingCustomerLocation>
  {
    public DefaultBillingCustomerLocation()
      : base("DF")
    {
    }
  }

  public class SOBillingCustomerLocation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Send_Invoices_To.SOBillingCustomerLocation>
  {
    public SOBillingCustomerLocation()
      : base("LC")
    {
    }
  }

  public class ServiceOrderAddress : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Send_Invoices_To.ServiceOrderAddress>
  {
    public ServiceOrderAddress()
      : base("SO")
    {
    }
  }
}
