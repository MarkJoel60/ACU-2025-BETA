// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Box1099NumberSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL.Fluent;

#nullable disable
namespace PX.Objects.AP;

public class Box1099NumberSelectorAttribute : PXSelectorAttribute
{
  public Box1099NumberSelectorAttribute()
    : base(typeof (SearchFor<AP1099Box.boxNbr>.In<SelectFrom<AP1099Box>>), typeof (AP1099Box.boxCD), typeof (AP1099Box.descr))
  {
    this.CacheGlobal = true;
    this.SubstituteKey = typeof (AP1099Box.boxCD);
    this.DescriptionField = typeof (AP1099Box.descr);
  }
}
