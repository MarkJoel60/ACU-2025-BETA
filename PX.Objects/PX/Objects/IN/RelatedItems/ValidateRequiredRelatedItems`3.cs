// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.ValidateRequiredRelatedItems`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN.RelatedItems;

public abstract class ValidateRequiredRelatedItems<TGraph, TSubstitutableDocument, TSubstitutableLine> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TSubstitutableDocument : class, IBqlTable, ISubstitutableDocument, new()
  where TSubstitutableLine : class, IBqlTable, ISubstitutableLine, new()
{
  protected virtual bool IsMassProcessing
  {
    get => PXLongOperation.GetCustomInfoForCurrentThread("PXProcessingState") != null;
  }

  public virtual bool Validate(TSubstitutableLine substitutableLine)
  {
    if (!this.SubstitutionRequired(substitutableLine))
      return true;
    this.ThrowError();
    return false;
  }

  protected virtual bool SubstitutionRequired(TSubstitutableLine substitutableLine)
  {
    return substitutableLine.SubstitutionRequired.GetValueOrDefault();
  }

  public abstract void ThrowError();
}
