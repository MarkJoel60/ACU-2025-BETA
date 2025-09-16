// Decompiled with JetBrains decompiler
// Type: PX.SM.DatabaseSchemaInquiry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Data;
using PX.Data.ReferentialIntegrity;
using PX.Data.ReferentialIntegrity.Inspecting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.SM;

[Obfuscation(Exclude = true)]
public class DatabaseSchemaInquiry : PXGraph<DatabaseSchemaInquiry>
{
  public PXPrevious<InspectingTable> Previous;
  public PXNext<InspectingTable> Next;
  public PXFirst<InspectingTable> First;
  public PXLast<InspectingTable> Last;
  public PXCancel<InspectingTable> Cancel;
  public PXSelect<InspectingTable> inspectingTables;
  public PXSelect<TableReference> tableOutgoingReferences;
  public PXSelect<TableReference> tableIncomingReferences;
  public PXAction<InspectingTable> viewParent;
  public PXAction<InspectingTable> viewChild;

  [InjectDependency]
  public ITableReferenceInspector TableReferencesInspector { get; set; }

  public DatabaseSchemaInquiry() => this.Cancel.SetVisible(false);

  private static IEnumerable<InspectingTable> GetInspectingTables(ITableReferenceInspector inspector)
  {
    return inspector != null ? inspector.GetReferencesOfAllDacs().Select<KeyValuePair<System.Type, ReferencesInspectionResult>, InspectingTable>((Func<KeyValuePair<System.Type, ReferencesInspectionResult>, InspectingTable>) (t => new InspectingTable(t.Value))) : Enumerable.Empty<InspectingTable>();
  }

  private static IEnumerable<InspectingTable> GetInspectingTables()
  {
    return DatabaseSchemaInquiry.GetInspectingTables(ServiceLocator.IsLocationProviderSet ? ServiceLocator.Current.GetInstance<ITableReferenceInspector>() : (ITableReferenceInspector) null);
  }

  protected IEnumerable InspectingTables()
  {
    return (IEnumerable) DatabaseSchemaInquiry.GetInspectingTables();
  }

  protected IEnumerable TableOutgoingReferences()
  {
    InspectingTable current = this.inspectingTables.Current;
    IEnumerable<TableReference> tableReferences;
    if (current == null)
    {
      tableReferences = (IEnumerable<TableReference>) null;
    }
    else
    {
      ReferencesInspectionResult inspectionResult = current.InspectionResult;
      if (inspectionResult == null)
      {
        tableReferences = (IEnumerable<TableReference>) null;
      }
      else
      {
        IReadOnlyCollection<Reference> outgoingReferences = inspectionResult.OutgoingReferences;
        tableReferences = outgoingReferences != null ? outgoingReferences.Select<Reference, TableReference>((Func<Reference, TableReference>) (r => new TableReference(r))) : (IEnumerable<TableReference>) null;
      }
    }
    return (IEnumerable) tableReferences ?? (IEnumerable) Enumerable.Empty<TableReference>();
  }

  protected IEnumerable TableIncomingReferences()
  {
    InspectingTable current = this.inspectingTables.Current;
    IEnumerable<TableReference> tableReferences;
    if (current == null)
    {
      tableReferences = (IEnumerable<TableReference>) null;
    }
    else
    {
      ReferencesInspectionResult inspectionResult = current.InspectionResult;
      if (inspectionResult == null)
      {
        tableReferences = (IEnumerable<TableReference>) null;
      }
      else
      {
        IReadOnlyCollection<Reference> incomingReferences = inspectionResult.IncomingReferences;
        tableReferences = incomingReferences != null ? incomingReferences.Select<Reference, TableReference>((Func<Reference, TableReference>) (r => new TableReference(r))) : (IEnumerable<TableReference>) null;
      }
    }
    return (IEnumerable) tableReferences ?? (IEnumerable) Enumerable.Empty<TableReference>();
  }

  [PXButton]
  [PXUIField(DisplayName = "")]
  protected virtual void ViewParent()
  {
    this.inspectingTables.Current = (InspectingTable) this.inspectingTables.Search<InspectingTable.fullName>((object) this.tableOutgoingReferences.Current.ParentFullName);
    throw new PXRedirectRequiredException((PXGraph) this, false, (string) null);
  }

  [PXButton]
  [PXUIField(DisplayName = "")]
  protected virtual void ViewChild()
  {
    this.inspectingTables.Current = (InspectingTable) this.inspectingTables.Search<InspectingTable.fullName>((object) this.tableIncomingReferences.Current.ChildFullName);
    throw new PXRedirectRequiredException((PXGraph) this, false, (string) null);
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXSelectorByMethod(typeof (DatabaseSchemaInquiry), "GetInspectingTables", typeof (Search<InspectingTable.fullName>), new System.Type[] {})]
  protected virtual void TableReference_ParentFullName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXSelectorByMethod(typeof (DatabaseSchemaInquiry), "GetInspectingTables", typeof (Search<InspectingTable.fullName>), new System.Type[] {})]
  protected virtual void TableReference_ChildFullName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXSelectorByMethod(typeof (DatabaseSchemaInquiry), "GetInspectingTables", typeof (Search<InspectingTable.fullName>), new System.Type[] {})]
  protected virtual void InspectingTable_BaseClassName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXSelectorByMethod(typeof (DatabaseSchemaInquiry), "GetInspectingTables", typeof (Search<InspectingTable.fullName>), new System.Type[] {typeof (InspectingTable.className), typeof (InspectingTable.fullName), typeof (InspectingTable.description), typeof (InspectingTable.hasIncoming), typeof (InspectingTable.hasOutgoing)})]
  protected virtual void InspectingTable_FullName_CacheAttached(PXCache sender)
  {
  }
}
