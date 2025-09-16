// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.IGenericInquiryDescriptionProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry;

internal interface IGenericInquiryDescriptionProvider
{
  /// <summary>
  /// Gets <see cref="T:PX.Data.GIDescription" /> by generic inquiry design identifier.
  /// </summary>
  /// <param name="designId">Generic inquiry design identifier</param>
  /// <returns>Found <see cref="T:PX.Data.GIDescription" /> or <see langword="null" /></returns>
  GIDescription Get(Guid designId);

  /// <summary>
  /// Gets <see cref="T:PX.Data.GIDescription" /> by generic inquiry name.
  /// </summary>
  /// <param name="name">Generic inquiry name</param>
  /// <returns>Found <see cref="T:PX.Data.GIDescription" /> or <see langword="null" /></returns>
  GIDescription GetByName(string name);

  /// <summary>
  /// Gets the list of all<see cref="T:PX.Data.GIDescription" />.
  /// </summary>
  /// <returns>List of <see cref="T:PX.Data.GIDescription" /></returns>
  IEnumerable<GIDescription> GetAll();
}
