// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CommonSetupDecPl
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN;

public class CommonSetupDecPl : IPrefetchable, IPXCompanyDependent
{
  protected int _Qty = 2;
  protected int _PrcCst = 4;

  void IPrefetchable.Prefetch()
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<CommonSetup>(new PXDataField[2]
    {
      new PXDataField(typeof (CommonSetup.decPlQty).Name),
      new PXDataField(typeof (CommonSetup.decPlPrcCst).Name)
    }))
    {
      if (pxDataRecord == null)
        return;
      this._Qty = (int) pxDataRecord.GetInt16(0).Value;
      this._PrcCst = (int) pxDataRecord.GetInt16(1).Value;
    }
  }

  public static int Qty
  {
    get
    {
      CommonSetupDecPl withContextCache = PXDatabase.GetSlotWithContextCache<CommonSetupDecPl>(typeof (CommonSetupDecPl).Name, new Type[1]
      {
        typeof (CommonSetup)
      });
      return withContextCache == null ? 2 : withContextCache._Qty;
    }
  }

  public static int PrcCst
  {
    get
    {
      CommonSetupDecPl slot = PXDatabase.GetSlot<CommonSetupDecPl>(typeof (CommonSetupDecPl).Name, new Type[1]
      {
        typeof (CommonSetup)
      });
      return slot != null ? slot._PrcCst : 4;
    }
  }
}
