// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXImportSubstituteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
[PXInternalUseOnly]
public class PXImportSubstituteAttribute : PXImportAttribute
{
  private readonly System.Type _itemsCacheType;

  /// <exclude />
  public PXImportSubstituteAttribute(System.Type substituteCacheType)
    : this((System.Type) null, substituteCacheType)
  {
  }

  /// <exclude />
  public PXImportSubstituteAttribute(System.Type primaryTable, System.Type substituteCacheType)
    : base(primaryTable)
  {
    this._table = primaryTable;
    this._itemsCacheType = substituteCacheType;
  }

  public PXImportSubstituteAttribute(
    System.Type primaryTable,
    System.Type substituteCacheType,
    PXImportAttribute.IPXImportWizard importer)
    : this(primaryTable, substituteCacheType)
  {
    this._importer = importer;
  }

  /// <exclude />
  public virtual void ViewCreated(PXGraph graph, string viewName)
  {
    this._itemsCache = graph.Caches[this._itemsCacheType];
    base.ViewCreated(graph, viewName);
    if (this._importer == null)
      return;
    BqlCommand.Compose(new System.Type[2]
    {
      typeof (PXImportAttribute.PXImporter<>),
      this._itemsCache.GetItemType()
    }).GetField("_suppressLongRun", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue((object) this._importer, (object) true);
  }
}
