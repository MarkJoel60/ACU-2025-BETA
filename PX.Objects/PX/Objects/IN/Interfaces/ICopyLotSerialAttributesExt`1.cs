// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Interfaces.ICopyLotSerialAttributesExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN.Interfaces;

public interface ICopyLotSerialAttributesExt<TAttributesHeader> where TAttributesHeader : class, ILotSerialAttributesHeader, IBqlTable, new()
{
  PXSelectBase<TAttributesHeader> GetAttributesHeaderView();

  TAttributesHeaderDestination CopyAttributes<TAttributesHeaderSource, TAttributesHeaderDestination>(
    TAttributesHeaderSource source,
    TAttributesHeaderDestination destination,
    PXCache<TAttributesHeaderSource> sourceCache = null,
    PXCache<TAttributesHeaderDestination> destinationCache = null)
    where TAttributesHeaderSource : class, IBqlTable, ILotSerialAttributesHeader, new()
    where TAttributesHeaderDestination : class, IBqlTable, ILotSerialAttributesHeader, new();
}
