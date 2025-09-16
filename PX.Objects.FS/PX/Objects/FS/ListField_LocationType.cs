// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_LocationType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_LocationType : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.LocationType().ID_LIST, new ID.LocationType().TX_LIST)
    {
    }
  }

  public class Company : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_LocationType.Company>
  {
    public Company()
      : base("CO")
    {
    }
  }

  public class Customer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_LocationType.Customer>
  {
    public Customer()
      : base("CU")
    {
    }
  }
}
