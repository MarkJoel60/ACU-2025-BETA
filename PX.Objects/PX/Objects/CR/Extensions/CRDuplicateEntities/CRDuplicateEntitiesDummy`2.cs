// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntitiesDummy`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System.Collections;

#nullable enable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <summary>
/// Extension that is used when ordinary <see cref="N:PX.Objects.CR.Extensions.CRDuplicateEntities" /> extension is switched off. This extension is made as a stub to avoid "View/Action doesn't exist" error.
/// </summary>
public abstract class CRDuplicateEntitiesDummy<TGraph, TMain> : PXGraphExtension<
#nullable disable
TGraph>
  where TGraph : PXGraph
  where TMain : class, IBqlTable, new()
{
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRDuplicateRecord, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Contact>.On<BqlOperand<
  #nullable enable
  Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  CRDuplicateRecord.contactID>>>, FbqlJoins.Left<DuplicateContact>.On<BqlOperand<
  #nullable enable
  DuplicateContact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  CRDuplicateRecord.duplicateContactID>>>, FbqlJoins.Left<BAccountR>.On<BqlOperand<
  #nullable enable
  BAccountR.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  DuplicateContact.bAccountID>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.CRLead>.On<BqlOperand<
  #nullable enable
  PX.Objects.CR.Standalone.CRLead.contactID, IBqlInt>.IsEqual<
  #nullable disable
  CRDuplicateRecord.contactID>>>>.Order<By<Asc<DuplicateContact.contactPriority>, Asc<DuplicateContact.contactID>>>, CRDuplicateRecord>.View Duplicates;
  public PXAction<TMain> CheckForDuplicates;
  public PXAction<TMain> DuplicateMerge;
  public PXAction<TMain> DuplicateAttach;
  public PXAction<TMain> ViewDuplicate;
  public PXAction<TMain> ViewDuplicateRefContact;
  public PXAction<TMain> MarkAsValidated;
  public PXAction<TMain> CloseAsDuplicate;

  protected virtual IEnumerable duplicates()
  {
    yield break;
  }

  [PXUIField]
  [PXButton(ImageKey = "Process", DisplayOnMainToolbar = false)]
  public virtual IEnumerable checkForDuplicates(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable duplicateMerge(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable duplicateAttach(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable viewDuplicate(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable viewDuplicateRefContact(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable markAsValidated(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable closeAsDuplicate(PXAdapter adapter) => adapter.Get();
}
