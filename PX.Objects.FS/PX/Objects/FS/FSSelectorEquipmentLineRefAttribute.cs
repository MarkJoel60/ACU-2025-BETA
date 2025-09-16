// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorEquipmentLineRefAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorEquipmentLineRefAttribute : PXSelectorAttribute
{
  public FSSelectorEquipmentLineRefAttribute(Type smEquipmentID, Type componentID)
    : base(BqlCommand.Compose(new Type[19]
    {
      typeof (Search<,>),
      typeof (FSEquipmentComponent.lineNbr),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (FSEquipmentComponent.SMequipmentID),
      typeof (Equal<>),
      typeof (Current<>),
      smEquipmentID,
      typeof (And<FSEquipmentComponent.status, Equal<ListField_Equipment_Status.Active>>),
      typeof (And<>),
      typeof (Where<,,>),
      typeof (Current<>),
      componentID,
      typeof (IsNull),
      typeof (Or<,>),
      typeof (FSEquipmentComponent.componentID),
      typeof (Equal<>),
      typeof (Current<>),
      componentID
    }), new Type[5]
    {
      typeof (FSEquipmentComponent.lineRef),
      typeof (FSEquipmentComponent.componentID),
      typeof (FSEquipmentComponent.longDescr),
      typeof (FSEquipmentComponent.serialNumber),
      typeof (FSEquipmentComponent.comment)
    })
  {
    this.SubstituteKey = typeof (FSEquipmentComponent.lineRef);
  }
}
