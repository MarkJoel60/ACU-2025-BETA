// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_PriceType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_PriceType : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.PriceType().ID_LIST_PRICETYPE, new ID.PriceType().TX_LIST_PRICETYPE)
    {
    }
  }

  public class Contract : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_PriceType.Contract>
  {
    public Contract()
      : base("CONTR")
    {
    }
  }

  public class Customer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PriceType.Customer>
  {
    public Customer()
      : base("CUSTM")
    {
    }
  }

  public class PriceClass : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PriceType.PriceClass>
  {
    public PriceClass()
      : base("PRCLS")
    {
    }
  }

  public class Base : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PriceType.Base>
  {
    public Base()
      : base("BASEP")
    {
    }
  }

  public class Default : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PriceType.Default>
  {
    public Default()
      : base("DEFLT")
    {
    }
  }
}
