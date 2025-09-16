// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSubstituteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Indicates that the derived DAC should replace its base DACs
/// in a specific graph or all graphs.</summary>
/// <remarks>The attribute is placed on the definition of a DAC that is
/// derived from another DAC. The attribute is used primarily to make the
/// declarative references of the base DAC in definitions of calculations
/// and links from child objects to parent objects be interpreted as the
/// references of the derived DAC.</remarks>
/// <example>
/// The code below shows the use of the <tt>PXSubstitute</tt> attributes
/// on the <tt>APInvoice</tt> DAC.
/// <code>
/// [System.SerializableAttribute()]
/// [PXSubstitute(GraphType = typeof(APInvoiceEntry))]
/// [PXSubstitute(GraphType = typeof(TX.TXInvoiceEntry))]
/// [PXPrimaryGraph(typeof(APInvoiceEntry))]
/// public partial class APInvoice : APRegister, IInvoice
/// { ... }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class PXSubstituteAttribute : Attribute
{
  protected System.Type graphType_;
  protected System.Type parentType_;

  /// <summary>Gets or sets the specific graph in which the derived DAC
  /// replaces base DACs.</summary>
  public System.Type GraphType
  {
    get => this.graphType_;
    set => this.graphType_ = value;
  }

  /// <summary>Gets or sets the base DAC type up to which all types in the
  /// inheritance hierarchy are substituted with the derived DAC. By
  /// default, the property has the null value, which means that all base
  /// DACs are substituted with the derived DAC.</summary>
  public System.Type ParentType
  {
    get => this.parentType_;
    set => this.parentType_ = value;
  }
}
