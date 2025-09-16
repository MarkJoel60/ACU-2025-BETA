// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.ReferentialIntegrityViolationException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

[PXInternalUseOnly]
[Serializable]
public class ReferentialIntegrityViolationException : PXException
{
  public ReadOnlyCollection<Reference> ViolatedReferences { get; }

  public ReferentialIntegrityViolationException(IEnumerable<Reference> violatedReferences)
    : this(violatedReferences, "Referential integrity violations occurred for this operation.")
  {
  }

  public ReferentialIntegrityViolationException(
    IEnumerable<Reference> violatedReferences,
    string message)
    : base(message)
  {
    this.ViolatedReferences = violatedReferences.ToList<Reference>().AsReadOnly();
  }

  protected ReferentialIntegrityViolationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [PXLocalizable]
  public class Messages
  {
    public const string ReferenceViolationErrorMessage = "Referential integrity violations occurred for this operation.";
  }
}
