// Decompiled with JetBrains decompiler
// Type: PX.Data.ICaptionable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// An interface that you can implement in the graph to configure the form subtitle.
/// </summary>
/// <example>
/// <para>The following implementation hides the form subtitle.</para>
/// <code>
/// public string Caption() =&gt; string.Empty;
/// </code>
/// </example>
public interface ICaptionable
{
  /// <summary>Returns the string with the form subtitle.</summary>
  string Caption();
}
