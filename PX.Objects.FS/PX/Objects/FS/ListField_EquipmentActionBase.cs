// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_EquipmentActionBase
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_EquipmentActionBase : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Equipment_Action_Base().ID_LIST, new ID.Equipment_Action_Base().TX_LIST)
    {
    }
  }

  public class None : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_EquipmentActionBase.None>
  {
    public None()
      : base("NO")
    {
    }
  }

  public class SellingTargetEquipment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentActionBase.SellingTargetEquipment>
  {
    public SellingTargetEquipment()
      : base("ST")
    {
    }
  }

  public class ReplacingComponent : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentActionBase.ReplacingComponent>
  {
    public ReplacingComponent()
      : base("RC")
    {
    }
  }
}
