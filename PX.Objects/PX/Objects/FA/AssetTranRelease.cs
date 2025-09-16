// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetTranRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class AssetTranRelease : PXGraph<
#nullable disable
AssetTranRelease>
{
  public PXCancel<AssetTranRelease.ReleaseFilter> Cancel;
  public PXFilter<AssetTranRelease.ReleaseFilter> Filter;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (FARegister.refNbr))]
  public PXFilteredProcessing<FARegister, AssetTranRelease.ReleaseFilter, Where<True, Equal<True>>, OrderBy<Desc<FARegister.selected, Asc<FARegister.finPeriodID>>>> FADocumentList;
  public PXSelect<FATran> Trans;
  public PXSetup<FASetup> fasetup;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<FARegister.branchID> e)
  {
  }

  protected virtual void FARegister_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    using (new PXConnectionScope())
      PXFormulaAttribute.CalcAggregate<FATran.tranAmt>(((PXSelectBase) this.Trans).Cache, e.Row, true);
  }

  public IEnumerable fADocumentList(PXAdapter adapter)
  {
    AssetTranRelease.ReleaseFilter current = ((PXSelectBase<AssetTranRelease.ReleaseFilter>) this.Filter).Current;
    PXSelectBase<FARegister> pxSelectBase = (PXSelectBase<FARegister>) new PXSelect<FARegister, Where<FARegister.released, Equal<False>, And<FARegister.hold, Equal<False>>>>((PXGraph) this);
    if (current.Origin != null)
      pxSelectBase.WhereAnd<Where<FARegister.origin, Equal<Current<AssetTranRelease.ReleaseFilter.origin>>>>();
    return (IEnumerable) pxSelectBase.Select(Array.Empty<object>());
  }

  public AssetTranRelease()
  {
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault())
      throw new PXSetupNotEnteredException<FASetup>("This operation is not available in initialization mode. To exit the initialization mode, select the '{1}' checkbox on the '{0}' screen.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FASetup.updateGL>(((PXSelectBase) this.fasetup).Cache)
      });
    ((PXProcessingBase<FARegister>) this.FADocumentList).ParallelProcessingOptions = (Action<PXParallelProcessingOptions>) (settings =>
    {
      settings.IsEnabled = true;
      settings.AutoBatchSize = true;
      settings.BatchSize = 1;
    });
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<FARegister>) this.FADocumentList).SetProcessDelegate(AssetTranRelease.\u003C\u003Ec.\u003C\u003E9__8_1 ?? (AssetTranRelease.\u003C\u003Ec.\u003C\u003E9__8_1 = new PXProcessingBase<FARegister>.ProcessListDelegate((object) AssetTranRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__8_1))));
    ((PXProcessing<FARegister>) this.FADocumentList).SetProcessCaption("Release");
    ((PXProcessing<FARegister>) this.FADocumentList).SetProcessAllCaption("Release All");
  }

  public static DocumentList<Batch> ReleaseDoc(List<FARegister> list, bool isMassProcess)
  {
    return AssetTranRelease.ReleaseDoc(list, isMassProcess, true);
  }

  public static DocumentList<Batch> ReleaseDoc(
    List<FARegister> list,
    bool isMassProcess,
    bool AutoPost)
  {
    bool flag = false;
    AssetProcess instance1 = PXGraph.CreateInstance<AssetProcess>();
    JournalEntry instance2 = PXGraph.CreateInstance<JournalEntry>();
    PostGraph instance3 = PXGraph.CreateInstance<PostGraph>();
    Dictionary<(string, string), HashSet<int>> dictionary = new Dictionary<(string, string), HashSet<int>>();
    DocumentList<Batch> created = (DocumentList<Batch>) new BatchDocumentList<Batch, Batch.lineCntr>((PXGraph) instance1);
    for (int index = 0; index < list.Count; ++index)
    {
      FARegister doc = list[index];
      try
      {
        ((PXGraph) instance1).Clear();
        instance1.ProcessAssetTran(instance2, doc, created);
        if (((PXSelectBase<Batch>) instance2.BatchModule).Current != null)
        {
          (string, string) key = (((PXSelectBase<Batch>) instance2.BatchModule).Current.BatchNbr, ((PXSelectBase<Batch>) instance2.BatchModule).Current.Module);
          if (dictionary.ContainsKey(key))
            dictionary[key].Add(index);
          else
            dictionary.Add(key, new HashSet<int>() { index });
        }
        if (isMassProcess)
          PXProcessing<FARegister>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        created.Remove(((PXSelectBase<Batch>) instance2.BatchModule).Current);
        ((PXGraph) instance2).Clear();
        if (!isMassProcess)
          throw new PXMassProcessException(index, ex);
        PXProcessing<FARegister>.SetError(index, ex);
        flag = true;
      }
    }
    for (int index = 0; index < created.Count; ++index)
    {
      Batch b = created[index];
      try
      {
        if (instance1.AutoPost & AutoPost)
        {
          ((PXGraph) instance3).Clear();
          instance3.PostBatchProc(b);
        }
      }
      catch (Exception ex)
      {
        HashSet<int> source = dictionary[(b.BatchNbr, b.Module)];
        if (!isMassProcess)
          throw new PXMassProcessException(source.First<int>(), ex);
        flag = true;
        foreach (int num in source)
          PXProcessing<FARegister>.SetError(num, ex);
      }
    }
    if (flag)
      throw new PXException("One or more documents could not be released.");
    return !instance1.AutoPost ? new DocumentList<Batch>((PXGraph) instance1) : created;
  }

  [Serializable]
  public class ReleaseFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Origin;

    [PXDBString(1, IsFixed = true)]
    [FARegister.origin.List]
    [PXUIField(DisplayName = "Origin")]
    public virtual string Origin
    {
      get => this._Origin;
      set => this._Origin = value;
    }

    public abstract class origin : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AssetTranRelease.ReleaseFilter.origin>
    {
    }
  }
}
