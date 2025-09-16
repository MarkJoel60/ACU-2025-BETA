// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>A BQL parameter.</summary>
public interface IBqlParameter : IBqlOperand, IBqlCreator, IBqlVerifier
{
  /// <summary>Returns the type of the underlying field or argument.</summary>
  /// <returns>Referenced type.</returns>
  System.Type GetReferencedType();

  /// <summary>Gets a value indicating whether the parameter raises <tt>FieldDefaulting</tt> for an empty value.</summary>
  bool TryDefault { get; }

  /// <summary>Gets a value indicating whether the parameter is <tt>Current</tt> or <tt>Optional</tt>.</summary>
  bool HasDefault { get; }

  /// <summary>Gets a value indicating whether the parameter is a required, an optional, or a method argument.</summary>
  bool IsVisible { get; }

  /// <summary>Gets a value indicating whether the parameter is a method argument.</summary>
  bool IsArgument { get; }

  /// <summary>
  /// 
  /// </summary>
  System.Type MaskedType { get; set; }

  /// <summary>
  /// 
  /// </summary>
  bool NullAllowed { get; set; }
}
