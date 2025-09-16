// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Default_Billing_Customer_Source
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Default_Billing_Customer_Source : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Default_Billing_Customer_Source().ID_LIST, new ID.Default_Billing_Customer_Source().TX_LIST)
    {
    }
  }

  public class Service_Order_Customer : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Default_Billing_Customer_Source.Service_Order_Customer>
  {
    public Service_Order_Customer()
      : base("SO")
    {
    }
  }

  public class Default_Customer : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Default_Billing_Customer_Source.Default_Customer>
  {
    public Default_Customer()
      : base("DC")
    {
    }
  }

  public class Specific_Customer : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Default_Billing_Customer_Source.Specific_Customer>
  {
    public Specific_Customer()
      : base("LC")
    {
    }
  }
}
