// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Status_Route
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Status_Route : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Status_Route().ID_LIST, new ID.Status_Route().TX_LIST)
    {
    }
  }

  public class Open : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Route.Open>
  {
    public Open()
      : base("O")
    {
    }
  }

  public class InProcess : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Route.InProcess>
  {
    public InProcess()
      : base("P")
    {
    }
  }

  public class Canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Route.Canceled>
  {
    public Canceled()
      : base("X")
    {
    }
  }

  public class Completed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Route.Completed>
  {
    public Completed()
      : base("C")
    {
    }
  }

  public class Closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Route.Closed>
  {
    public Closed()
      : base("Z")
    {
    }
  }
}
