// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_LineType_Pickup_Delivery
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_LineType_Pickup_Delivery : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.LineType_Pickup_Delivery().ID_LIST_SERVICE, new ID.LineType_Pickup_Delivery().TX_LIST_SERVICE)
    {
    }
  }

  public class Pickup_Delivery : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_LineType_Pickup_Delivery.Pickup_Delivery>
  {
    public Pickup_Delivery()
      : base("PU_DL")
    {
    }
  }
}
