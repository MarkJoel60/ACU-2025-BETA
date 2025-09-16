// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphOrderBy`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The same as <tt>PXGraph&lt;TGraph&gt;</tt> but appends the following standard actions for the provided DAC: <tt>Save</tt>, <tt>Insert</tt>, <tt>Edit</tt>,
/// <tt>Delete</tt>, <tt>Cancel</tt>, <tt>Prev</tt>, <tt>Next</tt>, <tt>First</tt>, <tt>Last</tt>. The DAC is specified in the second type parameter. Sort
/// expression for First/Last/Next/Prev buttons is specified in the third type parameter.</summary>
/// <example><para>The code below declares a graph that includes a pre-defined set of actions for the Contact DAC.</para>
/// <code title="Example" lang="CS">
/// public class ContactMaint : PXGraph&lt;ContactMaint, Contact, OrderBy&lt;AscContact.createdDateTime&gt;&gt;&gt;
/// {
/// ...
/// }</code>
/// </example>
public class PXGraphOrderBy<TGraph, TPrimary, TOrderBy> : PXGraph
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
  where TOrderBy : class, IBqlOrderBy, new()
{
  public PXSave<TPrimary> Save;
  public PXCancel<TPrimary> Cancel;
  public PXInsert<TPrimary> Insert;
  public PXDelete<TPrimary> Delete;
  public PXCopyPasteAction<TPrimary> CopyPaste;
  public PXFirstOrderBy<TPrimary, TOrderBy> First;
  public PXPreviousOrderBy<TPrimary, TOrderBy> Previous;
  public PXNextOrderBy<TPrimary, TOrderBy> Next;
  public PXLastOrderBy<TPrimary, TOrderBy> Last;

  /// <exclude />
  public override bool CanClipboardCopyPaste() => true;
}
