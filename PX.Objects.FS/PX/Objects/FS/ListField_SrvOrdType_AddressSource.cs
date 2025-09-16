// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_SrvOrdType_AddressSource
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_SrvOrdType_AddressSource : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.SrvOrdType_AppAddressSource().ID_LIST, new ID.SrvOrdType_AppAddressSource().TX_LIST)
    {
    }
  }

  public class BUSINESS_ACCOUNT : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_AddressSource.BUSINESS_ACCOUNT>
  {
    public BUSINESS_ACCOUNT()
      : base("BA")
    {
    }
  }

  public class CUSTOMER_CONTACT : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_AddressSource.CUSTOMER_CONTACT>
  {
    public CUSTOMER_CONTACT()
      : base("CC")
    {
    }
  }

  public class BRANCH_LOCATION : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_AddressSource.BRANCH_LOCATION>
  {
    public BRANCH_LOCATION()
      : base("BL")
    {
    }
  }
}
