// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_PostTo_CreateInvoice
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_PostTo_CreateInvoice : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Batch_PostTo_Filter().ID_LIST, new ID.Batch_PostTo_Filter().TX_LIST)
    {
    }
  }

  public class AR : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo_CreateInvoice.AR>
  {
    public AR()
      : base(nameof (AR))
    {
    }
  }

  public class SO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo_CreateInvoice.SO>
  {
    public SO()
      : base(nameof (SO))
    {
    }
  }

  public class SI : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo_CreateInvoice.SI>
  {
    public SI()
      : base(nameof (SI))
    {
    }
  }

  public class PM : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo_CreateInvoice.PM>
  {
    public PM()
      : base(nameof (PM))
    {
    }
  }
}
