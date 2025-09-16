// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ArrayOfDLExpansionType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class ArrayOfDLExpansionType
{
  private EmailAddressType[] mailboxField;
  private int indexedPagingOffsetField;
  private bool indexedPagingOffsetFieldSpecified;
  private int numeratorOffsetField;
  private bool numeratorOffsetFieldSpecified;
  private int absoluteDenominatorField;
  private bool absoluteDenominatorFieldSpecified;
  private bool includesLastItemInRangeField;
  private bool includesLastItemInRangeFieldSpecified;
  private int totalItemsInViewField;
  private bool totalItemsInViewFieldSpecified;

  /// <remarks />
  [XmlElement("Mailbox")]
  public EmailAddressType[] Mailbox
  {
    get => this.mailboxField;
    set => this.mailboxField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int IndexedPagingOffset
  {
    get => this.indexedPagingOffsetField;
    set => this.indexedPagingOffsetField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IndexedPagingOffsetSpecified
  {
    get => this.indexedPagingOffsetFieldSpecified;
    set => this.indexedPagingOffsetFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int NumeratorOffset
  {
    get => this.numeratorOffsetField;
    set => this.numeratorOffsetField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool NumeratorOffsetSpecified
  {
    get => this.numeratorOffsetFieldSpecified;
    set => this.numeratorOffsetFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int AbsoluteDenominator
  {
    get => this.absoluteDenominatorField;
    set => this.absoluteDenominatorField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AbsoluteDenominatorSpecified
  {
    get => this.absoluteDenominatorFieldSpecified;
    set => this.absoluteDenominatorFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool IncludesLastItemInRange
  {
    get => this.includesLastItemInRangeField;
    set => this.includesLastItemInRangeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IncludesLastItemInRangeSpecified
  {
    get => this.includesLastItemInRangeFieldSpecified;
    set => this.includesLastItemInRangeFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int TotalItemsInView
  {
    get => this.totalItemsInViewField;
    set => this.totalItemsInViewField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TotalItemsInViewSpecified
  {
    get => this.totalItemsInViewFieldSpecified;
    set => this.totalItemsInViewFieldSpecified = value;
  }
}
