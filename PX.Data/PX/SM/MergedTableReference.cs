// Decompiled with JetBrains decompiler
// Type: PX.SM.MergedTableReference
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.ReferentialIntegrity;
using PX.Data.ReferentialIntegrity.Merging;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

[PXHidden]
[Serializable]
public class MergedTableReference : TableReference
{
  [PXString]
  [PXUIField(DisplayName = "Substitutable Parents", Enabled = false)]
  public string SubstitutableParents { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Substitutable Children", Enabled = false)]
  public string SubstitutableChildren { get; set; }

  public MergedTableReference()
  {
  }

  public MergedTableReference(MergedReference mergedReference)
    : base(mergedReference.Reference)
  {
    this.MergedReference = mergedReference;
    this.SubstitutableChildren = string.Join(Environment.NewLine, ((IEnumerable<System.Type>) mergedReference.ApplicableChildren).Select<System.Type, string>((Func<System.Type, string>) (t => $"{t.Name} ({t.FullName})")));
    this.SubstitutableParents = string.Join(Environment.NewLine, ((IEnumerable<System.Type>) mergedReference.ApplicableParents).Select<System.Type, string>((Func<System.Type, string>) (t => $"{t.Name} ({t.FullName})")));
  }

  public MergedReference MergedReference { get; }

  public override Reference Reference => this.MergedReference?.Reference;

  public abstract class substitutableParents
  {
  }

  public abstract class substitutableChildren
  {
  }
}
