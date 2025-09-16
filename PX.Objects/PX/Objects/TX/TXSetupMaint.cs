// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.TX;

public class TXSetupMaint : PXGraph<TXSetupMaint>
{
  public PXSelect<TXSetup> TXSetupRecord;
  public PXSave<TXSetup> Save;
  public PXCancel<TXSetup> Cancel;
}
