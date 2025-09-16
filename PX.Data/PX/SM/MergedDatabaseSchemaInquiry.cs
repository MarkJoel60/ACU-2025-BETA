// Decompiled with JetBrains decompiler
// Type: PX.SM.MergedDatabaseSchemaInquiry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Data;
using PX.Data.ReferentialIntegrity.Merging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.SM;

[Obfuscation(Exclude = true)]
public class MergedDatabaseSchemaInquiry : PXGraph<MergedDatabaseSchemaInquiry>
{
  public PXPrevious<MergedInspectingTable> Previous;
  public PXNext<MergedInspectingTable> Next;
  public PXFirst<MergedInspectingTable> First;
  public PXLast<MergedInspectingTable> Last;
  public PXCancel<MergedInspectingTable> Cancel;
  public PXSelect<MergedInspectingTable> inspectingTables;
  public PXSelect<MergedTableReference> tableOutgoingReferences;
  public PXSelect<MergedTableReference> tableIncomingReferences;
  public PXAction<MergedInspectingTable> viewParent;
  public PXAction<MergedInspectingTable> viewChild;

  [InjectDependency]
  public ITableMergedReferencesInspector TableMergedReferencesInspector { get; set; }

  public MergedDatabaseSchemaInquiry() => this.Cancel.SetVisible(false);

  private static IEnumerable<MergedInspectingTable> GetMergedInspectingTables(
    ITableMergedReferencesInspector inspector)
  {
    return inspector != null ? inspector.GetMergedReferencesOfAllTables().Select<KeyValuePair<System.Type, MergedReferencesInspectionResult>, MergedInspectingTable>((Func<KeyValuePair<System.Type, MergedReferencesInspectionResult>, MergedInspectingTable>) (t => new MergedInspectingTable(t.Value))) : Enumerable.Empty<MergedInspectingTable>();
  }

  private static IEnumerable<MergedInspectingTable> GetMergedInspectingTables()
  {
    return MergedDatabaseSchemaInquiry.GetMergedInspectingTables(ServiceLocator.IsLocationProviderSet ? ServiceLocator.Current.GetInstance<ITableMergedReferencesInspector>() : (ITableMergedReferencesInspector) null);
  }

  protected IEnumerable InspectingTables()
  {
    return (IEnumerable) MergedDatabaseSchemaInquiry.GetMergedInspectingTables(this.TableMergedReferencesInspector);
  }

  protected IEnumerable TableOutgoingReferences()
  {
    MergedInspectingTable current = this.inspectingTables.Current;
    IEnumerable<MergedTableReference> mergedTableReferences;
    if (current == null)
    {
      mergedTableReferences = (IEnumerable<MergedTableReference>) null;
    }
    else
    {
      MergedReferencesInspectionResult inspectionResult = current.MergedInspectionResult;
      if (inspectionResult == null)
      {
        mergedTableReferences = (IEnumerable<MergedTableReference>) null;
      }
      else
      {
        IReadOnlyCollection<MergedReference> mergedReferences = inspectionResult.OutgoingMergedReferences;
        mergedTableReferences = mergedReferences != null ? mergedReferences.Select<MergedReference, MergedTableReference>((Func<MergedReference, MergedTableReference>) (r => new MergedTableReference(r))) : (IEnumerable<MergedTableReference>) null;
      }
    }
    return (IEnumerable) mergedTableReferences ?? (IEnumerable) Enumerable.Empty<MergedTableReference>();
  }

  protected IEnumerable TableIncomingReferences()
  {
    MergedInspectingTable current = this.inspectingTables.Current;
    IEnumerable<MergedTableReference> mergedTableReferences;
    if (current == null)
    {
      mergedTableReferences = (IEnumerable<MergedTableReference>) null;
    }
    else
    {
      MergedReferencesInspectionResult inspectionResult = current.MergedInspectionResult;
      if (inspectionResult == null)
      {
        mergedTableReferences = (IEnumerable<MergedTableReference>) null;
      }
      else
      {
        IReadOnlyCollection<MergedReference> mergedReferences = inspectionResult.IncomingMergedReferences;
        mergedTableReferences = mergedReferences != null ? mergedReferences.Select<MergedReference, MergedTableReference>((Func<MergedReference, MergedTableReference>) (r => new MergedTableReference(r))) : (IEnumerable<MergedTableReference>) null;
      }
    }
    return (IEnumerable) mergedTableReferences ?? (IEnumerable) Enumerable.Empty<MergedTableReference>();
  }

  [PXButton]
  [PXUIField(DisplayName = "")]
  protected virtual void ViewParent()
  {
    this.inspectingTables.Current = (MergedInspectingTable) this.inspectingTables.Search<InspectingTable.fullName>((object) this.tableOutgoingReferences.Current.ParentFullName);
    throw new PXRedirectRequiredException((PXGraph) this, false, (string) null);
  }

  [PXButton]
  [PXUIField(DisplayName = "")]
  protected virtual void ViewChild()
  {
    this.inspectingTables.Current = (MergedInspectingTable) this.inspectingTables.Search<InspectingTable.fullName>((object) this.tableIncomingReferences.Current.ChildFullName);
    throw new PXRedirectRequiredException((PXGraph) this, false, (string) null);
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXSelectorByMethod(typeof (MergedDatabaseSchemaInquiry), "GetMergedInspectingTables", typeof (Search<InspectingTable.fullName>), new System.Type[] {})]
  protected void MergedTableReference_ParentFullName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXSelectorByMethod(typeof (MergedDatabaseSchemaInquiry), "GetMergedInspectingTables", typeof (Search<InspectingTable.fullName>), new System.Type[] {})]
  protected void MergedTableReference_ChildFullName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXSelectorByMethod(typeof (MergedDatabaseSchemaInquiry), "GetMergedInspectingTables", typeof (Search<InspectingTable.fullName>), new System.Type[] {})]
  protected virtual void MergedInspectingTable_BaseClassName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXSelectorByMethod(typeof (MergedDatabaseSchemaInquiry), "GetMergedInspectingTables", typeof (Search<InspectingTable.fullName>), new System.Type[] {typeof (InspectingTable.className), typeof (InspectingTable.fullName), typeof (InspectingTable.description), typeof (InspectingTable.hasIncoming), typeof (InspectingTable.hasOutgoing)})]
  protected void MergedInspectingTable_FullName_CacheAttached(PXCache sender)
  {
  }
}
