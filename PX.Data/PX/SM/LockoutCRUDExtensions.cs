// Decompiled with JetBrains decompiler
// Type: PX.SM.LockoutCRUDExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public static class LockoutCRUDExtensions
{
  public static void Insert(this UPLock value)
  {
    PXDatabase.Insert<UPLock>((PXDataFieldAssign) new PXDataFieldAssign<UPLock.databaseID>((object) value.DatabaseID), (PXDataFieldAssign) new PXDataFieldAssign<UPLock.host>((object) value.Host), (PXDataFieldAssign) new PXDataFieldAssign<UPLock.date>((object) value.Date), (PXDataFieldAssign) new PXDataFieldAssign<UPLock.purpose>((object) value.Purpose));
  }
}
