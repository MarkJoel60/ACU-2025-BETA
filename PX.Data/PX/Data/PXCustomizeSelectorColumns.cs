// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCustomizeSelectorColumns
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>This attribute defines a new set and order of the columns in the specified selector. You use this attribute in a DAC extension.</summary>
/// <example><para>For example, suppose that you have to add a new column to the ItemClassID selector, which is located on the General Settings tab of the Stock Items form. The original attributes of the field are the following.</para>
/// <code title="Example" lang="CS">
/// #region ItemClassID
///   public abstract class itemClassID : PX.Data.BQL.BqlString.Field&lt;itemClassID&gt;
///   {
///   }
///   protected String _ItemClassID;
/// 
///   [PXDBString(10, IsUnicode = true)]
///   [PXUIField(DisplayName = "Item Class", Visibility = PXUIVisibility.SelectorVisible)]
///   [PXSelector(typeof(Search&lt;INItemClass.itemClassID&gt;))]
///   public virtual String ItemClassID
///   {
///       get { return this._ItemClassID; }
///       set { this._ItemClassID = value; }
///   }
/// #endregion</code>
/// <code title="Example2" description="The ItemClassID selector contains two columns for the INItemClass.itemClassID and INItemClass.descr fields. The code below shows how to customize only the PXSelector attribute of the field if you need to insert a new column for the INItemClass.baseUnit field between the existing columns. The system will append attributes for the ItemClassID field because of the Append method in the PXMergeAttibutes specified in the DAC extension. In the result of the specified merge method, the ItemClassID field has all original attributes, and the PXCustomizeSelectorColumns attribute is added through the DAC extension. The selector control of the ItemClassID field has three columns, as specified in the PXCustomizeSelectorColumns attribute." groupname="Example" lang="CS">
/// public class IN_InventoryItem_Extension: PXCacheExtension&lt;PX.Objects.IN.InventoryItem&gt;
/// {
///   #region ItemClassID
///    [PXMergeAttributes(Method = MergeMethod.Append)]
///    [PXCustomizeSelectorColumns(
///       typeof(PX.Objects.IN.INItemClass.itemClassID),
///       typeof(PX.Objects.IN.INItemClass.baseUnit),
///       typeof(PX.Objects.IN.INItemClass.descr))]
///   #endregion
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class PXCustomizeSelectorColumns : PXEventSubscriberAttribute
{
  private readonly System.Type[] _columns;

  public PXCustomizeSelectorColumns(params System.Type[] columns) => this._columns = columns;

  public override void CacheAttached(PXCache cache)
  {
    string lower = this.FieldName.ToLower();
    cache.SelectingFields.Add(lower);
    cache.FieldSelectingEvents[lower] += new PXFieldSelecting(this.GraphFieldSelecting);
  }

  private void GraphFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    sender.SetAltered(this.FieldName, true);
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes((object) null, this.FieldName))
    {
      if (attribute is PXSelectorAttribute selectorAttribute)
      {
        selectorAttribute.SetFieldList(this._columns);
        selectorAttribute.Headers = (string[]) null;
        selectorAttribute.FieldSelecting(sender, e);
      }
    }
  }
}
