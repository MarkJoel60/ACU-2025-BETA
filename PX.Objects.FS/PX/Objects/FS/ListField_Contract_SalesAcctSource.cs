// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Contract_SalesAcctSource
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Contract_SalesAcctSource : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Contract_SalesAcctSource().ID_LIST, new ID.Contract_SalesAcctSource().TX_LIST)
    {
    }
  }

  public class CUSTOMER_LOCATION : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_SalesAcctSource.CUSTOMER_LOCATION>
  {
    public CUSTOMER_LOCATION()
      : base("CL")
    {
    }
  }

  public class POSTING_CLASS : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_SalesAcctSource.POSTING_CLASS>
  {
    public POSTING_CLASS()
      : base("PC")
    {
    }
  }

  public class INVENTORY_ITEM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_SalesAcctSource.INVENTORY_ITEM>
  {
    public INVENTORY_ITEM()
      : base("II")
    {
    }
  }
}
