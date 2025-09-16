// Decompiled with JetBrains decompiler
// Type: PX.Data.NotLike`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>
/// Checks if the preceding operand does not match the pattern specified in <tt>Operand</tt>.
/// Equivalent to SQL operator NOT LIKE.
/// </summary>
/// <typeparam name="Operand">The operand to compare to.</typeparam>
/// <example><para>The code below shows the definition of a DAC field. The PXSelector attribute configures the lookup control that will represent the PageID field in the UI.</para>
/// 	<code title="Example" lang="CS">
/// [PXDBGuid(IsKey = true)]
/// [PXSelector(typeof(Search2&lt;SimpleWikiPage.pageID,
///     InnerJoin&lt;WikiDescriptor, On&lt;WikiDescriptor.pageID, Equal&lt;SimpleWikiPage.wikiID&gt;&gt;&gt;,
///     Where&lt;WikiDescriptor.articleType, Equal&lt;WikiArticleTypeAttribute.kb&gt;,
///         And&lt;SimpleWikiPage.name, NotLike&lt;GenTemplateLeftLike&gt;,
///         And&lt;SimpleWikiPage.name, NotLike&lt;TemplateLeftLike&gt;,
///         And&lt;SimpleWikiPage.name, NotLike&lt;ContainerTemplateLeftLike&gt;&gt;&gt;&gt;&gt;&gt;))]
/// public virtual Guid? PageID { get; set; }</code>
/// </example>
public class NotLike<Operand> : ComparisonBase<Operand> where Operand : IBqlOperand
{
  protected override bool? verifyCore(object val, object value)
  {
    bool? nullable = Like<Operand>.CheckLike(val as string, value as string);
    return !nullable.HasValue ? new bool?() : new bool?(!nullable.GetValueOrDefault());
  }

  protected override bool isBypass(object val) => false;

  /// <exclude />
  public NotLike()
    : base("NOT LIKE", true)
  {
  }

  protected override void parseNonFieldOperand(
    PXGraph graph,
    StringBuilder text,
    System.Action parseOperand)
  {
    if (graph != null && text != null)
      graph.SqlDialect.scriptLikeOperand(text, parseOperand);
    else
      parseOperand();
  }
}
