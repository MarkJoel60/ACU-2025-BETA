// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.Extensions.SideBySideComparison.Link;

/// <summary>The filter that is used for linking entities.</summary>
[PXHidden]
public class LinkFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the link should be processed.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(true)]
  [PXUIField(DisplayName = "Process")]
  public bool? ProcessLink { get; set; }

  /// <summary>The caption under the grid with extra information.</summary>
  /// <remarks>Can be made hidden.</remarks>
  [PXString(IsUnicode = true)]
  [PXUnboundDefault("Select the field values that you want to use for the contact.")]
  [PXUIField(Enabled = false)]
  public 
  #nullable disable
  string Caption { get; set; }

  /// <summary>The ID of the entity that is used for linking.</summary>
  /// <value>
  /// Corresponds to the real ID of the entity, such as
  /// <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> or <see cref="P:PX.Objects.CR.Contact.ContactID" />.
  /// </value>
  [PXString]
  [PXUIField(DisplayName = "", Visible = false, Enabled = false)]
  public string LinkedEntityID { get; set; }

  public abstract class processLink : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LinkFilter.processLink>
  {
  }

  public abstract class caption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkFilter.caption>
  {
  }

  public abstract class linkedEntityID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkFilter.linkedEntityID>
  {
  }
}
