// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ConsolidationImport.SubOffImportSubaccountMapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL.ConsolidationImport;

public class SubOffImportSubaccountMapper : IImportSubaccountMapper
{
  private readonly Func<int?> _tryGetDefaultSubID;

  public SubOffImportSubaccountMapper(Func<int?> tryGetDefaultSubID)
  {
    this._tryGetDefaultSubID = tryGetDefaultSubID;
  }

  public Sub.Keys GetMappedSubaccountKeys(string subaccountCD)
  {
    return new Sub.Keys()
    {
      SubCD = string.Empty,
      SubID = this._tryGetDefaultSubID()
    };
  }
}
