// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraph`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The same as <see cref="T:PX.Data.PXGraph`1">PXGraph&lt;TGraph&gt;</see>
/// but appends the following standard actions for the provided DAC:
/// <tt>Save</tt>, <tt>Insert</tt>, <tt>Edit</tt>, <tt>Delete</tt>,
/// <tt>Cancel</tt>, <tt>Prev</tt>, <tt>Next</tt>, <tt>First</tt>,
/// <tt>Last</tt>. The DAC is specified in the second type
/// parameter. Name field for First/Last/Next/Prev buttons is specified in the third type parameter.</summary>
/// <example><para>The code below declares a graph that includes a pre-defined set of actions for the Contact DAC.</para>
/// <code title="Example" lang="CS">
/// public class ContactMaint : PXGraph&lt;ContactMaint, Contact, Contact.fullName&gt;
/// {
/// ...
/// }</code>
/// </example>
public class PXGraph<TGraph, TPrimary, TName> : PXGraph
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
  where TName : class, IBqlField
{
  /// <summary>The action that saves changes stored in the caches to the database. The code of an application graph typically saves changes through this action as well. To invoke it from code, use the PressSave() method of the Actions property.</summary>
  public PXSave<TPrimary> Save;
  /// <summary>The action that discard changes to the data from the caches.</summary>
  public PXCancel<TPrimary, TName> Cancel;
  /// <summary>The action that inserts a new data record into the primary cache.</summary>
  public PXInsert<TPrimary> Insert;
  /// <summary>The action that deletes the Current data record of the primary cache.</summary>
  public PXDelete<TPrimary> Delete;
  /// <summary>The action that is represented on the user interface by an expandable menu that includes Copy and Paste items.</summary>
  public PXCopyPasteAction<TPrimary> CopyPaste;
  /// <summary>The action that navigates to the first data record in the primary data view. The data record is set to the Current property of the primary cache.</summary>
  public PXFirst<TPrimary, TName> First;
  /// <summary>The action that navigates to the previous data record in the primary data view. The data record is set to the Current property of the primary cache.</summary>
  public PXPrevious<TPrimary, TName> Previous;
  /// <summary>The action that navigates to the next data record in the primary data view. The data record is set to the Current property of the primary cache.</summary>
  public PXNext<TPrimary, TName> Next;
  /// <summary>The action that navigates to the last data record in the primary data view. The data record is set to the Current property of the primary cache.</summary>
  public PXLast<TPrimary, TName> Last;

  /// <exclude />
  public override bool CanClipboardCopyPaste() => true;
}
