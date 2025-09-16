// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.PXSyncFolderSpecification
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.Update.ExchangeService;
using PX.SM;

#nullable disable
namespace PX.Objects.CS.Email;

public class PXSyncFolderSpecification
{
  public readonly string Name;
  public readonly bool Categorized;
  public readonly DistinguishedFolderIdNameType Type;
  public readonly PXEmailSyncDirection.Directions Direction;
  public readonly PXSyncMovingCondition[] MoveTo;

  public PXSyncFolderSpecification(string name, DistinguishedFolderIdNameType type)
    : this(name, type, (PXEmailSyncDirection.Directions) 3, true)
  {
  }

  public PXSyncFolderSpecification(
    string name,
    DistinguishedFolderIdNameType type,
    PXEmailSyncDirection.Directions direction,
    bool categorized,
    params PXSyncMovingCondition[] moveTo)
  {
    this.Name = name;
    this.Type = type;
    this.Categorized = categorized;
    this.Direction = direction;
    this.MoveTo = moveTo;
  }
}
