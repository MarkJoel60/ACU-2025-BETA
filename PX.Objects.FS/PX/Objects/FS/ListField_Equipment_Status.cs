// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Equipment_Status
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Equipment_Status : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Equipment_Status().ID_LIST, new ID.Equipment_Status().TX_LIST)
    {
    }
  }

  public class Active : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Equipment_Status.Active>
  {
    public Active()
      : base("A")
    {
    }
  }

  public class Suspended : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Equipment_Status.Suspended>
  {
    public Suspended()
      : base("S")
    {
    }
  }

  public class Dispose : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Equipment_Status.Dispose>
  {
    public Dispose()
      : base("D")
    {
    }
  }
}
