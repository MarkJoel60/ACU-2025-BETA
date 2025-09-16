// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectOriginalsRestrict
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class PXSelectOriginalsRestrict : PXDummyDataFieldRestrict
{
  internal static PXDataFieldRestrict SelectOriginalValues = (PXDataFieldRestrict) new PXSelectOriginalsRestrict(false);
  internal static PXDataFieldRestrict SelectAllOriginalValues = (PXDataFieldRestrict) new PXSelectOriginalsRestrict(true);
  internal bool SelectOnlyAbsent;

  private PXSelectOriginalsRestrict(bool selectOnlyAbsent)
    : base("037AD739-5022-40FE-A781-EF611A931E66", PXDbType.Unspecified, new int?(0), (object) null)
  {
    this.SelectOnlyAbsent = selectOnlyAbsent;
  }
}
