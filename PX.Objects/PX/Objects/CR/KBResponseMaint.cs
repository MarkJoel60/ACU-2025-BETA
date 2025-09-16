// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.KBResponseMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CR;

public class KBResponseMaint : PXGraph<KBResponseMaint, KBResponse>
{
  public PXSelect<KBResponse> Responses;
  public PXAction<KBResponse> submit;

  [PXUIField(DisplayName = "Submit")]
  [PXButton(Tooltip = "Submit response")]
  public virtual IEnumerable Submit(PXAdapter adapter)
  {
    if (((PXSelectBase<KBResponse>) this.Responses).Current != null)
    {
      ((PXSelectBase<KBResponse>) this.Responses).Current.CreatedByID = new Guid?();
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }
}
