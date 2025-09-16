// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ISubaccountProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common;

public interface ISubaccountProvider
{
  /// <summary>
  /// Creates a <see cref="P:PX.Objects.GL.Sub.SubCD" /> using a given mask and source fields / values.
  /// </summary>
  /// <param name="mask">Subaccount mask.</param>
  /// <param name="sourceFieldValues">Source field values to create a subaccount.</param>
  /// <param name="sourceFields">Source fields that will be used to create a subaccount value.</param>
  string MakeSubaccount<Field>(string mask, object[] sourceFieldValues, Type[] sourceFields) where Field : IBqlField;

  /// <summary>
  /// Retrieves the internal ID of a subaccount by a substitute natural key.
  /// </summary>
  int? GetSubaccountID(string subaccountCD);
}
