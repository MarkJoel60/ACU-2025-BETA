// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AP.GraphExtensions.VendorClassMaintLienWaiverExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CN.Common.Services.DataProviders;
using PX.Objects.CN.Compliance.PM.DAC;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CN.Compliance.AP.GraphExtensions;

public class VendorClassMaintLienWaiverExtension : PXGraphExtension<
#nullable disable
VendorClassMaint>
{
  public PXSetup<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> LienWaiverSetup;
  public FbqlSelect<SelectFromBase<LienWaiverRecipient, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Right<PMProject>.On<BqlOperand<
  #nullable enable
  LienWaiverRecipient.projectId, IBqlInt>.IsEqual<
  #nullable disable
  PMProject.contractID>>>>, LienWaiverRecipient>.View LienWaiverRecipientProjects;
  public PXAction<PX.Objects.AP.VendorClass> AddToProjects;

  [InjectDependency]
  public IProjectDataProvider ProjectDataProvider { get; set; }

  private string VendorClassId
  {
    get => ((PXSelectBase<PX.Objects.AP.VendorClass>) this.Base.CurVendorClassRecord).Current.VendorClassID;
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public IEnumerable lienWaiverRecipientProjects()
  {
    return (IEnumerable) this.ProjectDataProvider.GetProjects((PXGraph) this.Base).Select<PMProject, PXResult<LienWaiverRecipient, PMProject>>(new Func<PMProject, PXResult<LienWaiverRecipient, PMProject>>(this.MaintainLienWaiverRecipientsLinks));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Add to Project")]
  public virtual void addToProjects()
  {
    // ISSUE: method pointer
    if (!WebDialogResultExtension.IsPositive(((PXSelectBase<LienWaiverRecipient>) this.LienWaiverRecipientProjects).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CaddToProjects\u003Eb__11_0)), true)))
      return;
    EnumerableExtensions.ForEach<LienWaiverRecipient>((IEnumerable<LienWaiverRecipient>) ((PXSelectBase<LienWaiverRecipient>) this.LienWaiverRecipientProjects).SelectMain(Array.Empty<object>()), new Action<LienWaiverRecipient>(this.MaintainLienWaiverRecipientsLinks));
    ((PXGraph) this.Base).Actions.PressSave();
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.VendorClass> args)
  {
    if (args.Row == null)
      return;
    ((PXAction) this.AddToProjects).SetVisible(((PXSelectBase<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>) this.LienWaiverSetup).Current.ShouldGenerateConditional.GetValueOrDefault() || ((PXSelectBase<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup>) this.LienWaiverSetup).Current.ShouldGenerateUnconditional.GetValueOrDefault());
  }

  public virtual void VendorClass_RowPersisted(PXCache cache, PXRowPersistedEventArgs args)
  {
    if (cache.GetStatus(args.Row) != 2 || !this.IsAddVendorClassConfirmed() || !WebDialogResultExtension.IsPositive(((PXSelectBase<LienWaiverRecipient>) this.LienWaiverRecipientProjects).AskExt(true)))
      return;
    EnumerableExtensions.ForEach<LienWaiverRecipient>((IEnumerable<LienWaiverRecipient>) ((PXSelectBase<LienWaiverRecipient>) this.LienWaiverRecipientProjects).SelectMain(Array.Empty<object>()), new Action<LienWaiverRecipient>(this.MaintainLienWaiverRecipientsLinks));
  }

  private void PrepareDialogWindow()
  {
    PXGraph.RowPersistedEvents rowPersisted = ((PXGraph) this.Base).RowPersisted;
    VendorClassMaintLienWaiverExtension lienWaiverExtension = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) lienWaiverExtension, __vmethodptr(lienWaiverExtension, VendorClass_RowPersisted));
    rowPersisted.RemoveHandler<PX.Objects.AP.VendorClass>(pxRowPersisted);
    ((PXSelectBase) this.LienWaiverRecipientProjects).Cache.Clear();
    ((PXGraph) this.Base).Actions.PressSave();
  }

  private void MaintainLienWaiverRecipientsLinks(LienWaiverRecipient lienWaiverRecipient)
  {
    if (lienWaiverRecipient.Selected.GetValueOrDefault())
      return;
    ((PXSelectBase<LienWaiverRecipient>) this.LienWaiverRecipientProjects).Delete(lienWaiverRecipient);
  }

  private PXResult<LienWaiverRecipient, PMProject> MaintainLienWaiverRecipientsLinks(
    PMProject project)
  {
    LienWaiverRecipient lienWaiverRecipient = this.GetLinkedLienWaiverRecipient((Contract) project);
    if (lienWaiverRecipient == null)
      return new PXResult<LienWaiverRecipient, PMProject>(this.CreateLienWaiverRecipient((Contract) project), project);
    if (!lienWaiverRecipient.Selected.HasValue)
      lienWaiverRecipient.Selected = new bool?(true);
    return new PXResult<LienWaiverRecipient, PMProject>(lienWaiverRecipient, project);
  }

  private LienWaiverRecipient CreateLienWaiverRecipient(Contract project)
  {
    return new LienWaiverRecipient()
    {
      ProjectId = project.ContractID,
      VendorClassId = this.VendorClassId
    };
  }

  private LienWaiverRecipient GetLinkedLienWaiverRecipient(Contract project)
  {
    return ((PXGraph) this.Base).Select<LienWaiverRecipient>().SingleOrDefault<LienWaiverRecipient>((Expression<Func<LienWaiverRecipient, bool>>) (recipient => recipient.ProjectId == project.ContractID && recipient.VendorClassId == this.VendorClassId));
  }

  private bool IsAddVendorClassConfirmed()
  {
    return WebDialogResultExtension.IsPositive(((PXSelectBase<PX.Objects.AP.VendorClass>) this.Base.VendorClassRecord).Ask("Would you like to add Vendor Class to existing projects for automatic Lien Waiver generation?", (MessageButtons) 4));
  }
}
