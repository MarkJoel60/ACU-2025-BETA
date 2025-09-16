// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Schedule_EquipmentAction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public abstract class ListField_Schedule_EquipmentAction : ListField_EquipmentActionBase
{
  public new class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Schedule_Equipment_Action().ID_LIST, new ID.Schedule_Equipment_Action().TX_LIST)
    {
    }
  }
}
