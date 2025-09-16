// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FASetupDefinition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

public class FASetupDefinition : IPrefetchable, IPXCompanyDependent
{
  public bool? UpdateGL { get; private set; }

  void IPrefetchable.Prefetch()
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<FASetup>(new PXDataField[1]
    {
      new PXDataField("updateGL")
    }))
      this.UpdateGL = pxDataRecord != null ? pxDataRecord.GetBoolean(0) : new bool?(false);
  }

  public static FASetupDefinition GetSlot()
  {
    return PXDatabase.GetSlot<FASetupDefinition>(typeof (FASetup).FullName, new Type[1]
    {
      typeof (FASetup)
    });
  }
}
