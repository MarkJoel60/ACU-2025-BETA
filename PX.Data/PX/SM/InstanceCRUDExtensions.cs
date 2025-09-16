// Decompiled with JetBrains decompiler
// Type: PX.SM.InstanceCRUDExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public static class InstanceCRUDExtensions
{
  public static void Insert(this Instance value)
  {
    PXDatabase.Insert<Instance>((PXDataFieldAssign) new PXDataFieldAssign<Instance.installationID>((object) value.InstallationID), (PXDataFieldAssign) new PXDataFieldAssign<Instance.databaseInfo>((object) value.DatabaseInfo), (PXDataFieldAssign) new PXDataFieldAssign<Instance.date>((object) value.Date));
  }

  public static void Update(this Instance value)
  {
    PXDatabase.Update<Instance>((PXDataFieldParam) new PXDataFieldAssign<Instance.databaseInfo>((object) value.DatabaseInfo), (PXDataFieldParam) new PXDataFieldAssign<Instance.date>((object) value.Date), (PXDataFieldParam) new PXDataFieldRestrict<Instance.installationID>((object) value.InstallationID));
  }
}
