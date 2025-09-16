// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RateTypeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public class RateTypeMaint : PXGraph<RateTypeMaint>, ICaptionable
{
  public PXSelect<PMRateType> RateTypes;
  public PXSavePerRow<PMRateType> Save;
  public PXCancel<PMRateType> Cancel;

  public string Caption() => string.Empty;
}
