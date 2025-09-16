// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXRelation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
public class PXRelation : ICloneable
{
  public PXTable First;
  public RelationType Relation;
  public PXTable Second;
  private List<PXOnCond> on = new List<PXOnCond>();

  public List<PXOnCond> On => this.on;

  public object Clone()
  {
    return (object) new PXRelation()
    {
      First = (this.First == null ? (PXTable) null : (PXTable) this.First.Clone()),
      Second = (this.Second == null ? (PXTable) null : (PXTable) this.Second.Clone()),
      Relation = this.Relation,
      on = this.On.Select<PXOnCond, PXOnCond>((Func<PXOnCond, PXOnCond>) (c => (PXOnCond) c.Clone())).ToList<PXOnCond>()
    };
  }
}
