// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_SrvOrdType_SalesAcctSource
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_SrvOrdType_SalesAcctSource : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public static 
    #nullable disable
    string[] GetIDList()
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.warehouse>() || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
        return new ID.SrvOrdType_SalesAcctSource().ID_LIST;
      return new string[3]{ "II", "PC", "CL" };
    }

    public static string[] GetTXList()
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.warehouse>() || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
        return new ID.SrvOrdType_SalesAcctSource().TX_LIST;
      return new string[3]
      {
        "Inventory Item",
        "Posting Class",
        "Customer/Vendor Location"
      };
    }

    public ListAtrribute()
      : base(ListField_SrvOrdType_SalesAcctSource.ListAtrribute.GetIDList(), ListField_SrvOrdType_SalesAcctSource.ListAtrribute.GetTXList())
    {
    }

    public virtual void CacheAttached(PXCache sender)
    {
      this._AllowedValues = ListField_SrvOrdType_SalesAcctSource.ListAtrribute.GetIDList();
      this._AllowedLabels = ListField_SrvOrdType_SalesAcctSource.ListAtrribute.GetTXList();
      this._NeutralAllowedLabels = this._AllowedLabels;
      base.CacheAttached(sender);
    }
  }

  public class INVENTORY_ITEM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_SalesAcctSource.INVENTORY_ITEM>
  {
    public INVENTORY_ITEM()
      : base("II")
    {
    }
  }

  public class WAREHOUSE : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_SalesAcctSource.WAREHOUSE>
  {
    public WAREHOUSE()
      : base("WH")
    {
    }
  }

  public class POSTING_CLASS : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_SalesAcctSource.POSTING_CLASS>
  {
    public POSTING_CLASS()
      : base("PC")
    {
    }
  }

  public class CUSTOMER_LOCATION : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_SalesAcctSource.CUSTOMER_LOCATION>
  {
    public CUSTOMER_LOCATION()
      : base("CL")
    {
    }
  }
}
