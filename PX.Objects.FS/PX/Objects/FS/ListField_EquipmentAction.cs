// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_EquipmentAction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_EquipmentAction : ListField_EquipmentActionBase
{
  public new class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Equipment_Action().ID_LIST, new ID.Equipment_Action().TX_LIST)
    {
    }
  }

  public class ReplacingTargetEquipment : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentAction.ReplacingTargetEquipment>
  {
    public ReplacingTargetEquipment()
      : base("RT")
    {
    }
  }

  public class CreatingComponent : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentAction.CreatingComponent>
  {
    public CreatingComponent()
      : base("CC")
    {
    }
  }

  public class UpgradingComponent : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentAction.UpgradingComponent>
  {
    public UpgradingComponent()
      : base("UC")
    {
    }
  }
}
