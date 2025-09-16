// Decompiled with JetBrains decompiler
// Type: PX.Data.ICrossCompanyPrefetchable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <remarks>Requires an explicit <see cref="M:PX.Data.PXDatabase.SelectCrossCompanyTimeStamp" /> call before retrieving the value of the DB slot;
/// otherwise, the value won't be invalidated if the table has been modified from another tenant.</remarks>
public interface ICrossCompanyPrefetchable
{
}
