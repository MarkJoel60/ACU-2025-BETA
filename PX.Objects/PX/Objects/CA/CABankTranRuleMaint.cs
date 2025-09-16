// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranRuleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class CABankTranRuleMaint : CABankTranRuleMaintBase
{
  public PXSave<CABankTranRule> Save;
  public PXCancel<CABankTranRule> Cancel;
  public PXInsert<CABankTranRule> Insert;
  public PXDelete<CABankTranRule> Delete;
  public PXArchive<CABankTranRule> Archive;
  public PXExtract<CABankTranRule> Extract;
  public PXCopyPasteAction<CABankTranRule> CopyPaste;
  public PXFirst<CABankTranRule> First;
  public PXPrevious<CABankTranRule> Previous;
  public PXNext<CABankTranRule> Next;
  public PXLast<CABankTranRule> Last;

  public virtual bool CanClipboardCopyPaste() => true;
}
