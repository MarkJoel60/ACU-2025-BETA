// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.DacRelations.DacRelationServiceExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.ReferentialIntegrity.DacRelations;

[PXInternalUseOnly]
public static class DacRelationServiceExtensions
{
  /// <summary>
  /// Returns relation information for a particular DAC type specified by <typeparamref name="Table" />.
  /// </summary>
  /// <typeparam name="Table">DAC type</typeparam>
  /// <param name="dacRelationService"><see cref="T:PX.Data.ReferentialIntegrity.DacRelations.IDacRelationService" /> instance</param>
  /// <returns>The information about all incoming and outgoing relations for a particular DAC.</returns>
  public static TableRelations GetTableRelations<Table>(this IDacRelationService dacRelationService) where Table : IBqlTable
  {
    return dacRelationService.GetTableRelations(typeof (Table));
  }
}
