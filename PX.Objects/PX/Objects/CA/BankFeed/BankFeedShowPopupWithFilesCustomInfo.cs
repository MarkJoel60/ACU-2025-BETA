// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedShowPopupWithFilesCustomInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CA.GraphExtensions;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.BankFeed;

public class BankFeedShowPopupWithFilesCustomInfo : IPXCustomInfo
{
  private readonly IEnumerable<BankFeedFile> _fileList;
  private readonly BankFeedShowPopupWithFilesCustomInfo.PopupType _popupType;

  public BankFeedShowPopupWithFilesCustomInfo(
    IEnumerable<BankFeedFile> fileList,
    BankFeedShowPopupWithFilesCustomInfo.PopupType popupType)
  {
    this._fileList = fileList;
    this._popupType = popupType;
  }

  public void Complete(PXLongRunStatus status, PXGraph graph)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    BankFeedShowPopupWithFilesCustomInfo.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new BankFeedShowPopupWithFilesCustomInfo.\u003C\u003Ec__DisplayClass4_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.bankFeedGraph = graph as CABankFeedMaint;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass40.bankFeedGraph == null || status != 2 || ((PXGraph) cDisplayClass40.bankFeedGraph).Caches[typeof (BankFeedFile)].Cached.Count() > 0L)
      return;
    foreach (BankFeedFile file in this._fileList)
    {
      // ISSUE: reference to a compiler-generated field
      GraphHelper.Hold(((PXGraph) cDisplayClass40.bankFeedGraph).Caches[typeof (BankFeedFile)], (object) file);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.graphExt = ((PXGraph) cDisplayClass40.bankFeedGraph).GetExtension<CABankFeedMaintFile>();
    if (this._popupType == BankFeedShowPopupWithFilesCustomInfo.PopupType.LoadTransactions)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXSelectBase<CABankFeedMaintFile.LoadFileTransactionsFilter>) cDisplayClass40.graphExt.LoadFileTransactions).AskExt(new PXView.InitializePanel((object) cDisplayClass40, __methodptr(\u003CComplete\u003Eb__0)), true);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXSelectBase<CABankFeedMaintFile.ShowFileTransactionsFilter>) cDisplayClass40.graphExt.ShowFileTransactions).AskExt(new PXView.InitializePanel((object) cDisplayClass40, __methodptr(\u003CComplete\u003Eb__1)), true);
    }
  }

  public enum PopupType
  {
    ShowTransactions,
    LoadTransactions,
  }
}
