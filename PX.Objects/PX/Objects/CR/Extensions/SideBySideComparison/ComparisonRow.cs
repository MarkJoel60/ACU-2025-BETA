// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace PX.Objects.CR.Extensions.SideBySideComparison;

/// <summary>
/// The base class that represents a comparison between the values of the same field of two <see cref="T:PX.Data.IBqlTable" />.
/// The class is used to select the value from one or another field.
/// </summary>
/// <remarks>
/// The child classes, which are <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkComparisonRow" /> and <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeComparisonRow" />,
/// are used in <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkEntitiesExt`3" /> and <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesExt`2" />
/// to show the comparison between the left and right entities.
/// </remarks>
[PXHidden]
public class ComparisonRow : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The type of <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to <see cref="P:System.Type.FullName" /> of the related <see cref="T:PX.Data.IBqlTable" /> type.
  /// </value>
  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Item Type", Visible = false)]
  public virtual 
  #nullable disable
  string ItemType { get; set; }

  /// <summary>
  /// The name of the field of <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to <see langword="nameof" /> of the field property.
  /// </value>
  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Field", Enabled = false)]
  public virtual string FieldName { get; set; }

  /// <summary>
  /// The hash code (<see cref="M:PX.Data.PXCache.GetObjectHashCode(System.Object)" />) of the left <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "", Visible = false)]
  public virtual int? LeftHashCode { get; set; }

  /// <summary>
  /// The hash code (<see cref="M:PX.Data.PXCache.GetObjectHashCode(System.Object)" />) of the right <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "", Visible = false)]
  public virtual int? RightHashCode { get; set; }

  /// <summary>
  /// The display name of the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.FieldName" /> field of <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to <see cref="P:PX.Data.PXFieldState.DisplayName" />
  /// for <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.FieldName" />.
  /// </value>
  [PXString]
  [PXUIField(DisplayName = "Field", Enabled = false)]
  public virtual string FieldDisplayName { get; set; }

  /// <summary>The order of the current row in the view.</summary>
  [PXInt]
  [PXUIField(DisplayName = "", Visible = false, Enabled = false)]
  public virtual int? Order { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that this row is used in processing
  /// but should not be displayed in the UI.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "", Visible = false, Enabled = false)]
  public virtual bool? Hidden { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that this row allows
  /// <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.Selection" /> to have the <see cref="F:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonSelection.None" /> value.
  /// </summary>
  /// <value>
  /// The value of this field is set during the <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" /> creation
  /// in <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3" /> or in it's child classes.
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "", Enabled = false, Visible = false)]
  public virtual bool? EnableNoneSelection { get; set; }

  /// <summary>
  /// The raw (<see cref="T:System.Int32" />) version of <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.Selection" />.
  /// </summary>
  /// <remarks>
  /// Use the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.Selection" /> field in code unless you need it in attributes.
  /// </remarks>
  [PXInt]
  [PXUIField(DisplayName = "", Enabled = false, Visible = false)]
  public virtual int? SelectionRaw { get; set; }

  /// <summary>
  /// Specifies the selection for the current row.
  /// It shows which <see cref="T:PX.Data.IBqlTable" /> field value (left or right) should be used
  /// or is <see langword="null" /> if <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.EnableNoneSelection" /> is set to <see langword="true" />.
  /// </summary>
  /// <remarks>
  /// For attributes, use <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.SelectionRaw" />.
  /// </remarks>
  public virtual ComparisonSelection Selection
  {
    get => (ComparisonSelection) this.SelectionRaw.GetValueOrDefault();
    set => this.SelectionRaw = new int?((int) value);
  }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the left <see cref="T:PX.Data.IBqlTable" />'s field value
  /// should be used.
  /// </summary>
  /// <value>
  /// Returns <see langword="true" /> if <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.Selection" /> is set to <see cref="F:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonSelection.Left" />
  /// and <see langword="false" /> otherwise.
  /// The behavior of the set accessor depends on <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.EnableNoneSelection" />.
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "")]
  [PXDependsOnFields(new System.Type[] {typeof (ComparisonRow.selectionRaw)})]
  public virtual bool? LeftValueSelected
  {
    get => new bool?(this.Selection == ComparisonSelection.Left);
    set
    {
      if (this.Selection == ComparisonSelection.Left && value.HasValue && !value.GetValueOrDefault())
      {
        bool? enableNoneSelection = this.EnableNoneSelection;
        if (enableNoneSelection.HasValue && enableNoneSelection.GetValueOrDefault())
        {
          this.Selection = ComparisonSelection.None;
          return;
        }
      }
      this.Selection = !value.HasValue || !value.GetValueOrDefault() ? ComparisonSelection.Right : ComparisonSelection.Left;
    }
  }

  /// <summary>
  /// The value of the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.FieldName" /> field of the left <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <value>The internal value of the field.</value>
  [PXString]
  [PXUIField(DisplayName = "", Enabled = false)]
  public virtual string LeftValue { get; set; }

  /// <summary>
  /// The display name for the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.FieldName" /> field if it is a selector
  /// (see <see cref="T:PX.Data.PXSelectorAttribute" />) for the left <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <remarks>
  /// Do not use directly. The property is for internal use only.
  /// </remarks>
  [PXString]
  public virtual string LeftValue_description { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the right <see cref="T:PX.Data.IBqlTable" />'s field value
  /// should be used.
  /// </summary>
  /// <value>
  /// Returns <see langword="true" /> if <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.Selection" /> is set to <see cref="F:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonSelection.Right" />
  /// and <see langword="false" /> otherwise.
  /// The behavior of the set accessor depends on <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.EnableNoneSelection" />.
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "")]
  [PXDependsOnFields(new System.Type[] {typeof (ComparisonRow.selectionRaw)})]
  public virtual bool? RightValueSelected
  {
    get => new bool?(this.Selection == ComparisonSelection.Right);
    set
    {
      if (this.Selection == ComparisonSelection.Right && value.HasValue && !value.GetValueOrDefault())
      {
        bool? enableNoneSelection = this.EnableNoneSelection;
        if (enableNoneSelection.HasValue && enableNoneSelection.GetValueOrDefault())
        {
          this.Selection = ComparisonSelection.None;
          return;
        }
      }
      this.Selection = !value.HasValue || !value.GetValueOrDefault() ? ComparisonSelection.Left : ComparisonSelection.Right;
    }
  }

  /// <summary>
  /// The value of the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.FieldName" /> field of the right <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <value>The internal value of the field.</value>
  [PXString]
  [PXUIField(DisplayName = "", Enabled = false)]
  public virtual string RightValue { get; set; }

  /// <summary>
  /// The display name for the <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.FieldName" /> field if it is a selector
  /// (see <see cref="T:PX.Data.PXSelectorAttribute" />) for the right <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <remarks>
  /// Do not use directly. The property is for internal use only.
  /// </remarks>
  [PXString]
  public virtual string RightValue_description { get; set; }

  /// <summary>
  /// An intermediate property for the cache of the left <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <remarks>
  /// Do not use directly. The property is for internal use only.
  /// </remarks>
  public PXCache LeftCache { get; set; }

  /// <summary>
  /// An intermediate property for the cache of the right <see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <remarks>
  /// Do not use directly. The property is for internal use only.
  /// </remarks>
  public PXCache RightCache { get; set; }

  /// <summary>
  /// An intermediate property for the field state of the left <see cref="T:PX.Data.IBqlTable" />'s field.
  /// </summary>
  /// <remarks>
  /// Do not use directly. The property is for internal use only.
  /// </remarks>
  public PXFieldState LeftFieldState { get; set; }

  /// <summary>
  /// An intermediate property for the field state of the right <see cref="T:PX.Data.IBqlTable" />'s field.
  /// </summary>
  /// <remarks>
  /// Do not use directly. The property is for internal use only.
  /// </remarks>
  public PXFieldState RightFieldState { get; set; }

  public virtual string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(((object) this).GetType().Name);
    if (string.IsNullOrEmpty(this.ItemType))
    {
      stringBuilder.Append(" <!>");
    }
    else
    {
      stringBuilder.Append(" | ");
      bool? hidden = this.Hidden;
      if (hidden.HasValue && hidden.GetValueOrDefault())
        stringBuilder.Append(" Hidden | ");
      stringBuilder.Append(((IEnumerable<string>) this.ItemType.Split('.')).Last<string>());
      stringBuilder.Append(".");
      stringBuilder.Append(this.FieldName);
      stringBuilder.Append(": ");
      stringBuilder.Append(getBox(this.LeftValueSelected));
      stringBuilder.Append(getValue(this.LeftValue));
      stringBuilder.Append("<->");
      stringBuilder.Append(getBox(this.RightValueSelected));
      stringBuilder.Append(getValue(this.RightValue));
    }
    return stringBuilder.ToString();

    static string getBox(bool? value)
    {
      return !value.HasValue || !value.GetValueOrDefault() ? " [ ] " : " [X] ";
    }

    static string getValue(string value)
    {
      return string.IsNullOrWhiteSpace(value) ? " {} " : $" {{{value}}} ";
    }
  }

  public abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComparisonRow.itemType>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComparisonRow.fieldName>
  {
  }

  public abstract class leftHashCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComparisonRow.leftHashCode>
  {
  }

  public abstract class rightHashCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComparisonRow.rightHashCode>
  {
  }

  public abstract class fieldDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ComparisonRow.fieldDisplayName>
  {
  }

  public abstract class order : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComparisonRow.order>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ComparisonRow.hidden>
  {
  }

  public abstract class enableNoneSelection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ComparisonRow.enableNoneSelection>
  {
  }

  public abstract class selectionRaw : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ComparisonRow.selectionRaw>
  {
  }

  public abstract class leftValueSelected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ComparisonRow.leftValueSelected>
  {
  }

  public abstract class leftValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComparisonRow.leftValue>
  {
  }

  public abstract class rightValueSelected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ComparisonRow.rightValueSelected>
  {
  }

  public abstract class rightValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ComparisonRow.rightValue>
  {
  }
}
