// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraph`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The same as <tt>PXGraph&lt;TGraph&gt;</tt> but appends the following standard actions for the provided DAC:
/// 	<list type="bullet">
/// 		<item>
/// 			<description>
/// 				<tt>Save</tt>
/// 			</description>
/// 		</item>
/// 		<item><description>
/// 			<tt>Insert</tt>
/// 		</description></item>
/// 		<item><description>
/// 			<tt>Edit</tt>
/// 		</description></item>
/// 		<item><description>
/// 			<tt>Delete</tt>
/// 		</description></item>
/// 		<item><description>
/// 			<tt>Cancel</tt>
/// 		</description></item>
/// 		<item><description>
/// 			<tt>Prev</tt>
/// 		</description></item>
/// 		<item><description>
/// 			<tt>Next</tt>
/// 		</description></item>
/// 		<item><description>
/// 			<tt>First</tt>
/// 		</description></item>
/// 		<item><description>
/// 			<tt>Last</tt>
/// 		</description></item>
/// 	</list>
/// The DAC is specified in the second type parameter.
/// </summary>
/// <example><para>The code below declares a graph that includes a pre-defined set of actions for the Contact DAC. If a webpage is bound to this graph, the webpage toolbar will include the action buttons, which may be used to save, insert, delete, and navigate to Contact data records selected by the primary data view (the data view defined first).</para>
/// <code title="Example" lang="CS">
/// public class ContactMaint : PXGraph&lt;ContactMaint, Contact&gt;
/// {
/// ...
/// }</code>
/// </example>
public class PXGraph<TGraph, TPrimary> : PXGraph
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <summary>The action that saves changes stored in the caches to the database. The code of an application graph typically saves changes through this action as well. To invoke it from code, use the PressSave() method of the Actions property.</summary>
  public PXSave<TPrimary> Save;
  /// <summary>The action that discard changes to the data from the caches.</summary>
  public PXCancel<TPrimary> Cancel;
  /// <summary>The action that inserts a new data record into the primary cache.</summary>
  public PXInsert<TPrimary> Insert;
  /// <summary>The action that deletes the Current data record of the primary cache.</summary>
  public PXDelete<TPrimary> Delete;
  /// <summary>The action that archives the Current data record of the primary cache</summary>
  public PXArchive<TPrimary> Archive;
  /// <summary>The action that extracts from archive the Current data record of the primary cache</summary>
  public PXExtract<TPrimary> Extract;
  /// <summary>The action that is represented on the user interface by an expandable menu that includes Copy and Paste items.</summary>
  public PXCopyPasteAction<TPrimary> CopyPaste;
  /// <summary>The action that navigates to the first data record in the primary data view. The data record is set to the Current property of the primary cache.</summary>
  public PXFirst<TPrimary> First;
  /// <summary>The action that navigates to the previous data record in the primary data view. The data record is set to the Current property of the primary cache.</summary>
  public PXPrevious<TPrimary> Previous;
  /// <summary>The action that navigates to the next data record in the primary data view. The data record is set to the Current property of the primary cache.</summary>
  public PXNext<TPrimary> Next;
  /// <summary>The action that navigates to the last data record in the primary data view. The data record is set to the Current property of the primary cache.</summary>
  public PXLast<TPrimary> Last;

  /// <exclude />
  public override bool CanClipboardCopyPaste() => true;
}
