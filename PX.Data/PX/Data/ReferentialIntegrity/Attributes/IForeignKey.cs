// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.IForeignKey
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

public interface IForeignKey
{
  ReadOnlyDictionary<System.Type, System.Type> FieldsMapping { get; }

  System.Type ParentTable { get; }

  System.Type ChildTable { get; }

  IBqlTable FindParent(PXGraph graph, IBqlTable child, PKFindOptions options = PKFindOptions.None);

  IEnumerable<IBqlTable> SelectChildren(PXGraph graph, IBqlTable parent);
}
