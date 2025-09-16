// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNonInstantiatedExtensionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Prevents a DAC extension from being instantiated multiple times. This attribute can only be added to a DAC extension.
/// </summary>
/// <remarks>
/// <para>The <tt>PXNonInstantiatedExtension</tt> attribute is typically added to a DAC extension that is created specifically for the purpose of overriding some attributes.
///  Such an extension does not contain any field values, and this attribute prevents the extension from being instantiated multiple times, which helps save memory resources.</para>
/// <para>This attribute is automatically added by the system when a DAC extension is created in the Customization Project Editor and converted to a code file. However, it may also be added manually to
///  a DAC extension wherever applicable.</para>
/// </remarks>
/// <example><para>In the example below, the <tt>PXNonInstantiatedExtension</tt> attribute is added to a DAC extension that overrides the attributes of the <tt>ApproverID</tt> field of the base <tt>ARRegister</tt> DAC.</para>
/// <code title="Example" lang="CS">
/// namespace PX.Objects.AR
/// {
/// [PXNonInstantiatedExtension]
/// public class AR_ARRegister_ExistingColumn : PXCacheExtension&lt;PX.Objects.AR.ARRegister&gt;
///  {
///    #region ApproverID
///    [PX.TM.Owner]
///    public int? ApproverID { get; set; }
///    #endregion
///  }
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PXNonInstantiatedExtensionAttribute : Attribute
{
}
