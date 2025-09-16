// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorComponentIDByFSEquipmentComponentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorComponentIDByFSEquipmentComponentAttribute : PXSelectorAttribute
{
  public FSSelectorComponentIDByFSEquipmentComponentAttribute(Type smEquipmentID)
    : base(BqlCommand.Compose(new Type[14]
    {
      typeof (Search2<,,>),
      typeof (FSModelTemplateComponent.componentID),
      typeof (LeftJoin<,>),
      typeof (FSEquipmentComponent),
      typeof (On<,>),
      typeof (FSEquipmentComponent.componentID),
      typeof (Equal<>),
      typeof (FSModelTemplateComponent.componentID),
      typeof (Where<,,>),
      typeof (FSEquipmentComponent.SMequipmentID),
      typeof (Equal<>),
      typeof (Current<>),
      smEquipmentID,
      typeof (And<FSEquipmentComponent.status, Equal<ListField_Equipment_Status.Active>>)
    }), new Type[4]
    {
      typeof (FSModelTemplateComponent.componentCD),
      typeof (FSModelTemplateComponent.descr),
      typeof (FSModelTemplateComponent.optional),
      typeof (FSModelTemplateComponent.classID)
    })
  {
    this.SubstituteKey = typeof (FSModelTemplateComponent.componentCD);
  }
}
