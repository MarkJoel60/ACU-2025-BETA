// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_PostTo
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_PostTo : ListField_PostTo_CreateInvoice
{
  public new class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Batch_PostTo().ID_LIST, new ID.Batch_PostTo().TX_LIST)
    {
    }
  }

  public new class AR : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo.AR>
  {
    public AR()
      : base(nameof (AR))
    {
    }
  }

  public new class SO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo.SO>
  {
    public SO()
      : base(nameof (SO))
    {
    }
  }

  public new class SI : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo.SI>
  {
    public SI()
      : base(nameof (SI))
    {
    }
  }

  public class AP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo.AP>
  {
    public AP()
      : base(nameof (AP))
    {
    }
  }

  public class IN : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo.IN>
  {
    public IN()
      : base(nameof (IN))
    {
    }
  }

  public class AR_AP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_PostTo.AR_AP>
  {
    public AR_AP()
      : base("AA")
    {
    }
  }
}
