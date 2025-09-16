// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CRAssigmentScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.EP;
using System;

#nullable disable
namespace PX.Objects.EP;

public class CRAssigmentScope : IDisposable
{
  private readonly IAssign source;
  private readonly int? workgroupID;
  private readonly int? ownerID;

  public CRAssigmentScope(IAssign source)
  {
    this.source = source;
    this.workgroupID = source.WorkgroupID;
    this.ownerID = source.OwnerID;
  }

  public virtual void Dispose()
  {
    this.source.WorkgroupID = this.workgroupID;
    this.source.OwnerID = this.ownerID;
  }
}
