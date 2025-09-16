// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.SetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Metadata;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class SetupMaint : PXGraph<SetupMaint>
{
  public PXSelect<FASetup> FASetupRecord;
  public PXSave<FASetup> Save;
  public PXCancel<FASetup> Cancel;
  /// <summary>
  /// Collection of graphs, access to which depends on UpdateGL preference.
  /// </summary>
  private static readonly List<Type> UpdateGLDependentGraphs = new List<Type>()
  {
    typeof (DisposalProcess),
    typeof (SplitProcess),
    typeof (TransferProcess),
    typeof (AssetGLTransactions),
    typeof (AssetTranRelease)
  };

  public virtual void Persist()
  {
    if (((PXSelectBase<FASetup>) this.FASetupRecord).Current != null && ((bool?) ((PXSelectBase) this.FASetupRecord).Cache.GetValueOriginal<FASetup.updateGL>((object) ((PXSelectBase<FASetup>) this.FASetupRecord).Current)).GetValueOrDefault() != ((PXSelectBase<FASetup>) this.FASetupRecord).Current.UpdateGL.GetValueOrDefault())
    {
      foreach (Type glDependentGraph in SetupMaint.UpdateGLDependentGraphs)
      {
        string screenId = PXSiteMapProviderExtensions.FindSiteMapNodeUnsecure(PXSiteMap.Provider, glDependentGraph)?.ScreenID;
        if (!string.IsNullOrEmpty(screenId))
          this.ScreenInfoCacheControl.InvalidateCache(screenId);
      }
    }
    ((PXGraph) this).Persist();
  }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }
}
