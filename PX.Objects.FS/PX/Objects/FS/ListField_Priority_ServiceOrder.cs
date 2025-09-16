// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Priority_ServiceOrder
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Priority_ServiceOrder : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Priority_ServiceOrder().ID_LIST, new ID.Priority_ServiceOrder().TX_LIST)
    {
    }
  }

  public class Low : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Priority_ServiceOrder.Low>
  {
    public Low()
      : base("L")
    {
    }
  }

  public class Medium : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Priority_ServiceOrder.Medium>
  {
    public Medium()
      : base("M")
    {
    }
  }

  public class High : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Priority_ServiceOrder.High>
  {
    public High()
      : base("H")
    {
    }
  }
}
