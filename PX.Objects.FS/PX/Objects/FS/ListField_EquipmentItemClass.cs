// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_EquipmentItemClass
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_EquipmentItemClass : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Equipment_Item_Class().ID_LIST, new ID.Equipment_Item_Class().TX_LIST)
    {
    }
  }

  public class PartOtherInventory : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentItemClass.PartOtherInventory>
  {
    public PartOtherInventory()
      : base("OI")
    {
    }
  }

  public class ModelEquipment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentItemClass.ModelEquipment>
  {
    public ModelEquipment()
      : base("ME")
    {
    }
  }

  public class Component : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentItemClass.Component>
  {
    public Component()
      : base("CT")
    {
    }
  }

  public class Consumable : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_EquipmentItemClass.Consumable>
  {
    public Consumable()
      : base("CE")
    {
    }
  }
}
