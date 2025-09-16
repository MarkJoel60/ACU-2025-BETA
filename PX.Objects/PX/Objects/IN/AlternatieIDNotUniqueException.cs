// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AlternatieIDNotUniqueException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.IN;

[Serializable]
public class AlternatieIDNotUniqueException : PXSetPropertyException
{
  public AlternatieIDNotUniqueException(string newAltID)
    : base("Value '{0}' for Alternate ID is already used for another inventory item.", (PXErrorLevel) 4, new object[1]
    {
      (object) newAltID
    })
  {
  }

  public AlternatieIDNotUniqueException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public AlternatieIDNotUniqueException(string alternateID, string inventoryCD, string uom)
    : base("The {0} alternate ID already exists for the {1} item, but with the {2} UOM. Specify another alternate ID in the document, or remove the UOM specified for the current alternate ID on the Cross-Reference tab of the Stock Items (IN202500) or Non-Stock Items (IN202000) forms.", (PXErrorLevel) 4, new object[3]
    {
      (object) alternateID,
      (object) inventoryCD,
      (object) uom
    })
  {
  }
}
