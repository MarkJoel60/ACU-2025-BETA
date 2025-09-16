// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CrossReferenceUniquenessForAPPriceWorksheetExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AP;
using System;

#nullable disable
namespace PX.Objects.IN;

[Serializable]
public class CrossReferenceUniquenessForAPPriceWorksheetExtension : 
  CrossReferenceUniquenessPriceWorksheet<APPriceWorksheetMaint>
{
  public static bool IsActive()
  {
    return CrossReferenceUniquenessPriceWorksheet<APPriceWorksheetMaint>.IsActiveImpl();
  }
}
