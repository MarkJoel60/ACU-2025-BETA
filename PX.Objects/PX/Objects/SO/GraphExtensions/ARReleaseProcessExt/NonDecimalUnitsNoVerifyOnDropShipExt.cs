// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARReleaseProcessExt.NonDecimalUnitsNoVerifyOnDropShipExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.AR;
using PX.Objects.Extensions;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARReleaseProcessExt;

/// <summary>
/// Disabling of validation for decimal values for drop ship lines in IN Issue
/// </summary>
public class NonDecimalUnitsNoVerifyOnDropShipExt : 
  NonDecimalUnitsNoVerifyOnDropShipExt<ARReleaseProcess, INTran>
{
  protected override bool IsDropShipLine(INTran line) => line.SOShipmentType == "H";
}
