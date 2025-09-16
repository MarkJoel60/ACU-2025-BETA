// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Merging.MergedReference
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Merging;

/// <summary>
/// Represents a <see cref="T:PX.Data.ReferentialIntegrity.Reference" /> that was merged from several other,
/// so its parent and child <see cref="T:PX.Data.IBqlTable" />s may be substituted with some
/// other applicable <see cref="T:PX.Data.IBqlTable" />s (e.g. their aliases, TPT-descendants or namesakes)
/// </summary>
[ImmutableObject(true)]
[PXInternalUseOnly]
public class MergedReference
{
  public static MergedReference FromReference(Reference reference)
  {
    Reference reference1 = reference;
    TableWithKeys tableWithKeys = reference.Parent;
    IEnumerable<System.Type> applicableParents = EnumerableExtensions.AsSingleEnumerable<System.Type>(tableWithKeys.Table);
    tableWithKeys = reference.Child;
    IEnumerable<System.Type> applicableChildren = EnumerableExtensions.AsSingleEnumerable<System.Type>(tableWithKeys.Table);
    return new MergedReference(reference1, applicableParents, applicableChildren);
  }

  public MergedReference Copy()
  {
    return new MergedReference(this.Reference, (IEnumerable<System.Type>) this.ApplicableParents, (IEnumerable<System.Type>) this.ApplicableChildren);
  }

  public MergedReference AppendApplicableChildren(IEnumerable<System.Type> children)
  {
    return new MergedReference(this.Reference, (IEnumerable<System.Type>) this.ApplicableParents, ((IEnumerable<System.Type>) this.ApplicableChildren).Concat<System.Type>(children));
  }

  public MergedReference AppendApplicableParents(IEnumerable<System.Type> parents)
  {
    return new MergedReference(this.Reference, ((IEnumerable<System.Type>) this.ApplicableParents).Concat<System.Type>(parents), (IEnumerable<System.Type>) this.ApplicableChildren);
  }

  public MergedReference AppendApplicableTables(MergedReference sourceReference)
  {
    return new MergedReference(this.Reference, ((IEnumerable<System.Type>) this.ApplicableParents).Concat<System.Type>((IEnumerable<System.Type>) sourceReference.ApplicableParents), ((IEnumerable<System.Type>) this.ApplicableChildren).Concat<System.Type>((IEnumerable<System.Type>) sourceReference.ApplicableChildren));
  }

  private MergedReference(
    Reference reference,
    IEnumerable<System.Type> applicableParents,
    IEnumerable<System.Type> applicableChildren)
  {
    this.Reference = reference;
    this.ApplicableParents = ImmutableHashSet.ToImmutableHashSet<System.Type>(applicableParents);
    this.ApplicableChildren = ImmutableHashSet.ToImmutableHashSet<System.Type>(applicableChildren);
  }

  public Reference Reference { get; private set; }

  public ImmutableHashSet<System.Type> ApplicableParents { get; private set; }

  public ImmutableHashSet<System.Type> ApplicableChildren { get; private set; }
}
