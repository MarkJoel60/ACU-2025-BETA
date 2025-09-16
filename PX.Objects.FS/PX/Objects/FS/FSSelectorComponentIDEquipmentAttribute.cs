// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorComponentIDEquipmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorComponentIDEquipmentAttribute : PXSelectorAttribute
{
  public FSSelectorComponentIDEquipmentAttribute()
    : base(typeof (Search2<FSModelTemplateComponent.componentID, InnerJoin<FSModelComponent, On<FSModelComponent.componentID, Equal<FSModelTemplateComponent.componentID>>>, Where<FSModelComponent.modelID, Equal<Current<FSEquipment.inventoryID>>>>), new Type[5]
    {
      typeof (FSModelTemplateComponent.componentCD),
      typeof (FSModelTemplateComponent.optional),
      typeof (FSModelComponent.active),
      typeof (FSModelComponent.descr),
      typeof (FSModelComponent.classID)
    })
  {
    this.SubstituteKey = typeof (FSModelTemplateComponent.componentCD);
  }
}
