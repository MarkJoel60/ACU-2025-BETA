// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Ship_To
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Ship_To : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Ship_To().ID_LIST, new ID.Ship_To().TX_LIST)
    {
    }
  }

  public class BillingCustomerBillTo : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Ship_To.BillingCustomerBillTo>
  {
    public BillingCustomerBillTo()
      : base("BT")
    {
    }
  }

  public class SOBillingCustomerLocation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Ship_To.SOBillingCustomerLocation>
  {
    public SOBillingCustomerLocation()
      : base("BL")
    {
    }
  }

  public class SOCustomerLocation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Ship_To.SOCustomerLocation>
  {
    public SOCustomerLocation()
      : base("LC")
    {
    }
  }

  public class ServiceOrderAddress : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Ship_To.ServiceOrderAddress>
  {
    public ServiceOrderAddress()
      : base("SO")
    {
    }
  }
}
