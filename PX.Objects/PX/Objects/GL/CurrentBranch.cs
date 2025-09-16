// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.CurrentBranch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Contains single record representing Current Branch of the company.
/// Can be used in Generic Inquiries for filtering.
/// </summary>
[PXProjection(typeof (Select<Branch, Where<Branch.branchID, Equal<CurrentValue<AccessInfo.branchID>>>>), Persistent = false)]
[PXCacheName("Current Branch")]
public class CurrentBranch : Branch
{
}
