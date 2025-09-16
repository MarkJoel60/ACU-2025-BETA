// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.CCProcessingCenterMaintTerminal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.CA;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class CCProcessingCenterMaintTerminal : PXGraphExtension<CCProcessingCenterMaint>
{
  public PXSelect<CCProcessingCenterTerminal, Where<CCProcessingCenterTerminal.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>> POSTerminals;
  public PXSelect<DefaultTerminal, Where<DefaultTerminal.processingCenterID, Equal<Current<CCProcessingCenter.processingCenterID>>>> DefaultTerminals;
  public PXAction<CCProcessingCenter> importTerminals;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>();
  }

  /// <summary>Import terminals from the processing center</summary>
  /// <param name="adapter"></param>
  /// <returns></returns>
  [PXUIField]
  [PXButton]
  public virtual IEnumerable ImportTerminals(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCProcessingCenterMaintTerminal.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new CCProcessingCenterMaintTerminal.\u003C\u003Ec__DisplayClass4_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.procCenter = ((PXSelectBase<CCProcessingCenter>) this.Base.ProcessingCenter).Current;
    ((PXAction) this.Base.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation<CCProcessingCenterMaint>((PXGraphExtension<CCProcessingCenterMaint>) this, new PXToggleAsyncDelegate((object) cDisplayClass40, __methodptr(\u003CImportTerminals\u003Eb__0)));
    return adapter.Get();
  }

  private static string HandleImportTermianlError(Exception exception)
  {
    string str = exception.Message;
    if (exception is PXOuterException pxOuterException && pxOuterException.InnerMessages.Length != 0)
      str = str + Environment.NewLine + string.Join(Environment.NewLine, pxOuterException.InnerMessages);
    return str;
  }

  protected virtual void _(Events.RowSelected<CCProcessingCenter> e)
  {
    CCProcessingCenter row = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CCProcessingCenter>>) e).Cache;
    if (row == null)
      return;
    bool flag = row.ProcessingTypeName != null;
    PXUIFieldAttribute.SetVisible<CCProcessingCenter.acceptPOSPayments>(cache, (object) row, flag && CCProcessingFeatureHelper.IsFeatureSupported(row, CCProcessingFeature.TerminalGetter));
  }

  protected virtual void _(Events.RowSelected<CCProcessingCenterTerminal> e)
  {
    CCProcessingCenterTerminal row = e.Row;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CCProcessingCenterTerminal>>) e).Cache;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterTerminal.terminalID>(cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterTerminal.terminalName>(cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<CCProcessingCenterTerminal.isActive>(cache, (object) row, row.CanBeEnabled.GetValueOrDefault());
  }

  protected virtual void _(Events.RowUpdated<CCProcessingCenterTerminal> e)
  {
    CCProcessingCenterTerminal row = e.Row;
    CCProcessingCenterTerminal oldRow = e.OldRow;
    bool? isActive;
    int num;
    if (oldRow == null)
    {
      num = 0;
    }
    else
    {
      isActive = oldRow.IsActive;
      num = isActive.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0 || row == null)
      return;
    isActive = row.IsActive;
    bool flag = false;
    if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
      return;
    foreach (PXResult<DefaultTerminal> pxResult in PXSelectBase<DefaultTerminal, PXSelect<DefaultTerminal, Where<DefaultTerminal.processingCenterID, Equal<Required<DefaultTerminal.processingCenterID>>, And<DefaultTerminal.terminalID, Equal<Required<DefaultTerminal.terminalID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.ProcessingCenterID,
      (object) row.TerminalID
    }))
      ((PXSelectBase<DefaultTerminal>) this.DefaultTerminals).Delete(PXResult<DefaultTerminal>.op_Implicit(pxResult));
  }
}
