// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PXSetupBase`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions;

public class PXSetupBase<TSelf, TGraph, THeader, TSetup, Where> : PXGraphExtension<TGraph>
  where TSelf : PXSetupBase<TSelf, TGraph, THeader, TSetup, Where>
  where TGraph : PXGraph
  where THeader : class, IBqlTable, new()
  where TSetup : class, IBqlTable, new()
  where Where : IBqlWhere, new()
{
  public PXSelect<TSetup, Where> UserSetupView;
  public PXAction<THeader> UserSetupDialog;

  public TSetup UserSetup => this.EnsureUserSetup();

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "User Settings")]
  protected virtual void userSetupDialog()
  {
    this.EnsureUserSetup();
    PXCache cach = this.Base.Caches[typeof (TSetup)];
    if (this.UserSetupView.AskExt() == WebDialogResult.OK)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        cach.IsDirty = true;
        cach.Persist(PXDBOperation.Insert);
        cach.Persist(PXDBOperation.Update);
        transactionScope.Complete();
      }
      this.Base.Clear();
    }
    else
    {
      cach.Clear();
      cach.ClearQueryCacheObsolete();
    }
  }

  protected virtual TSetup EnsureUserSetup()
  {
    if ((object) this.UserSetupView.Current == null)
      this.UserSetupView.Current = (TSetup) this.UserSetupView.Select();
    if ((object) this.UserSetupView.Current == null)
      this.UserSetupView.Current = this.UserSetupView.Cache.Inserted.Cast<TSetup>().FirstOrDefault<TSetup>();
    if ((object) this.UserSetupView.Current == null)
      this.UserSetupView.Current = this.UserSetupView.Insert();
    this.UserSetupView.Cache.IsDirty = false;
    return this.UserSetupView.Current;
  }

  /// <summary>
  /// Gets a <typeparamref name="TSetup" /> instance for a <typeparamref name="TGraph" /> instance.
  /// </summary>
  public static TSetup For(TGraph graph) => graph.FindImplementation<TSelf>().UserSetup;
}
